using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Reserveer : Step
    {
        public Reserveer()
        {
        }



        public override void Show()
        {
            //Variable benoemen
            ConsoleKeyInfo input;
            string reservatieNaam = null;
            int reservatieDatumMaand = 0;
            int reservatieDatumDag = 0;
            string reservatieTijd = null;
            int reservatieAantal = 0;


            do
            {
                //Leeg maken van de console
                Console.Clear();
                string taalSetting = "nl";

                //Als het in het Nederlands staat
                if (taalSetting == "nl")
                {
                    Display("Bedankt voor het kiezen voor de Noodle \nOm te kunnen reserveren hebben wij wat informatie nodig");
                    Display("");
                    Display("\nVul alstublieft u hele naam in:");

                    //Invullen van de naam
                    reservatieNaam = Console.ReadLine();
                    Display("\nWelke Maand wilt u komen?");
                    bool validMonth = false;

                    //Controleert of de maand klopt, loopt van 1 tot en met 12
                    while (validMonth == false)
                    {
                        reservatieDatumMaand = Convert.ToInt32(Console.ReadLine());
                        if (reservatieDatumMaand <= 12 && reservatieDatumMaand > 0)
                        {
                            validMonth = true;
                        }
                        else
                        {
                            Display("Die maand lijkt niet te bestaan. Vul gelieve een maand in tussen 1 (januari) en 12 (december).");
                            validMonth = false;
                        }
                    }
                    Display("\nWelke Dag wilt u komen?");

                    //Controleert of de dag klopt. Met een max van het aantal dagen in die maand
                    bool validDay = false;
                    while (validDay == false)
                    {
                        reservatieDatumDag = Convert.ToInt32(Console.ReadLine());
                        //  Maanden met 31 dagen
                        if (reservatieDatumMaand == 1 || reservatieDatumMaand == 3 || reservatieDatumMaand == 5 || reservatieDatumMaand == 7 || reservatieDatumMaand == 8 || reservatieDatumMaand == 10 || reservatieDatumMaand == 12)
                        {
                            if (reservatieDatumDag < 0 || reservatieDatumDag > 31)
                            {
                                Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 31");
                                validDay = false;

                            }
                            else
                            {
                                validDay = true;
                            }
                        }
                        // Maanden met 30 dagen
                        else if (reservatieDatumMaand == 4 || reservatieDatumMaand == 6 || reservatieDatumMaand == 9 || reservatieDatumMaand == 11)
                        {
                            if (reservatieDatumDag < 0 || reservatieDatumMaand > 30)
                            {
                                Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en de 30");
                                validDay = false;
                            }
                            else
                            {
                                validDay = true;
                            }
                        }
                        //Februari
                        else if (reservatieDatumMaand == 2)
                        {
                            if (reservatieDatumDag < 0 || reservatieDatumDag > 28)
                            {
                                Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 28");
                                validDay = false;
                            }
                            else
                            {
                                validDay = true;
                            }
                        }
                    }



                    Display("\nWelke tijd wilt u komen? (uur:minuut). U kunt per half uur reserveren.");
                    //Controleert of de tijd klopt.
                    reservatieTijd = Console.ReadLine();


                    Display("\nMet hoeveel mensen wilt u komen?");
                    //Controleert met hoeveel mensen ze komen. De tafels hebben maximaal 4 plekken. Alles daarboven ervoor bellen
                    reservatieAantal = Convert.ToInt32(Console.ReadLine());
                    if (reservatieAantal > 4)
                    {
                        Display("Voor reserveringen van meer dan 4 personen moet u even bellen naar het restaurant.");
                    }
                }
            }
            while (reservatieNaam == null || reservatieDatumMaand == 0 || reservatieTijd == null || reservatieAantal == 0);


            //Met escape terug aan het einde
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.Escape)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();

            }

        }

        
        public class ReservatieInformatie
        {
            public string Naam { get; set; } 
            public int Maand { get; set; } 
            public int Dag { get; set; } 
            public string Tijd { get; set; }
            public int AantalPersonen { get; set; } 
        }


        public ReservatieInformatie Deserialize()
        {
            string json = File.ReadAllText("ReservatieInformatie.json");
            var ReservatieInformatieJson = JsonSerializer.Deserialize<ReservatieInformatie>(json);
            return ReservatieInformatieJson;
        }


        public void Serialize(ReservatieInformatie Informatie)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true

                
            };

            var ReservatieInformatieJson = JsonSerializer.Serialize(Informatie, serializeOptions);
            File.WriteAllText("ReservatieInformatie.json", ReservatieInformatieJson);
        }


    }
}