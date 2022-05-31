﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace Noodle
{

    class Reserveer : Step
    {
        public class ReservatieInformaties
        {
            // string voor stukje "overzicht" in de json, voeg hier meer bij toe als je ook meer stukjes in de json zet
            public string Reserveringen { get; set; }
            public int IDCounter { get; set; }
        }

        public ReservatieInformaties Deserialize()
        {
            string json = File.ReadAllText("ReservatieInformatie.json");
            var ReservatieInformatieJson = System.Text.Json.JsonSerializer.Deserialize<ReservatieInformaties>(json);
            return ReservatieInformatieJson;
        }

        public override void Show()
        {
            //Variable benoemen
            ConsoleKeyInfo input;
            string reservatieNaam = null;
            int reservatieDatumMaand = 0;
            int reservatieDatumDag = 0;
            string reservatieTijd = null;
            string reservatieAantal = null;
            var ReservatieInformatieJson = Deserialize();
            string Reserverings = ReservatieInformatieJson.Reserveringen;
            int IDCounters = ReservatieInformatieJson.IDCounter;

            static void JsonDing(string kaas, int jen)
            {
                string Kees = kaas;
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                int Jan = jen;
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("Reserveringen");
                    writer.WriteValue(Kees);
                    writer.WritePropertyName("IDCounter");
                    writer.WriteValue(Jan);
                    writer.WriteEnd();
                }
                string json = JsonConvert.SerializeObject(sb);
                string sk = (sb.ToString());
                //Console.Clear();
                //Console.WriteLine(sb.ToString());
                File.WriteAllText("ReservatieInformatie.json", sk);
            }
            static string ReturnLeeg()
            {
                return "Er zijn geen reserveringen";
            }
            static string Zero(string number)
            {
                if (number == "1" || number == "2" || number == "3" || number == "4" || number == "5" || number == "6" || number == "7" || number == "8" || number == "9")
                    return "0" + number;
                else
                    return number;
            }
            static void Terug()
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();

            }

            do
            {
                //Leeg maken van de console
                Console.Clear();
                string taalSetting = "nl";


                if (taalSetting == "nl")
                {
                    Display("Bedankt voor het kiezen voor de Noodle! \nOm te kunnen reserveren hebben wij wat informatie nodig");
                    Display("");

                    //Enter om te beginnen met reserveren
                    do
                    {
                        //Display dat je op enter moet klikken om terug te gaan
                        Display("Druk op [ENTER] om de reservering te beginnen en [5] om af te breken\n\nNote: Als u wilt teruggaan tijdens het maken van de reservering typ 'terug' in");


                        //Met 5 terug aan het einde
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.D5)
                        {
                            var MainMenu = new MainMenu();
                            MainMenu.Show();

                        }
                    }
                    while (input.Key != ConsoleKey.Enter && input.Key != ConsoleKey.D5);

                    Display("\nVul alstublieft uw voor- en achternaam in:");

                    //Invullen van de naam
                    bool geldigeNaam = false;
                    while (geldigeNaam == false)
                    {
                        reservatieNaam = Console.ReadLine();
                        if (reservatieNaam == "terug")
                        {
                            Terug();
                        }
                        else if(reservatieNaam.Length < 1)
                        {
                            geldigeNaam = false;
                            Display("Vul astublieft een geldige naam in");
                        }
                        else
                        {
                            geldigeNaam = true;
                        }
                    }

                    Display("\nWelke Maand wilt u komen?");
                    bool geldigeMaand = false;
                    int jaar = DateTime.Now.Year;
                    int reservatieDatumJaar = 0;

                    while (geldigeMaand == false)
                    {
                        string maandString = Console.ReadLine();
                        if (maandString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(maandString, out reservatieDatumMaand) && reservatieDatumMaand <= 12 && reservatieDatumMaand > 0)
                        {
                            geldigeMaand = true;
                            if(reservatieDatumMaand < DateTime.Now.Month)
                            {
                                reservatieDatumJaar =  DateTime.Now.AddYears(1).Year;

                            }
                            else
                            {
                                reservatieDatumJaar =  jaar;

                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDeze maand is niet geldig. Vul alstublieft een nummer in tussen 1 (januari) en 12 (december)");
                            geldigeMaand = false;
                        }
                    }
                    Display("\nWelke Dag wilt u komen?");

                    //Controleert of de dag klopt. Met een max van het aantal dagen in die maand
                    bool geldigeDag = false;
                    while (geldigeDag == false)
                    {
                        string dagString = Console.ReadLine();
                        if (dagString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(dagString, out reservatieDatumDag))
                        {
                            //  Maanden met 31 dagen
                            if (reservatieDatumMaand == 1 || reservatieDatumMaand == 3 || reservatieDatumMaand == 5 || reservatieDatumMaand == 7 || reservatieDatumMaand == 8 || reservatieDatumMaand == 10 || reservatieDatumMaand == 12)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumDag > 31)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 31");
                                    geldigeDag = false;

                                }
                                else
                                {
                                    geldigeDag = true;
                                    if (reservatieDatumDag < DateTime.Now.Day && reservatieDatumMaand == DateTime.Now.Month)
                                    {
                                        reservatieDatumJaar = DateTime.Now.AddYears(1).Year;

                                    }
                                }
                            }
                            // Maanden met 30 dagen
                            else if (reservatieDatumMaand == 4 || reservatieDatumMaand == 6 || reservatieDatumMaand == 9 || reservatieDatumMaand == 11)
                            {
                                if (reservatieDatumDag < 0 || reservatieDatumDag > 30)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en de 30");
                                    geldigeDag = false;
                                }
                                else
                                {
                                    geldigeDag = true;
                                    if (reservatieDatumDag < DateTime.Now.Day && reservatieDatumMaand == DateTime.Now.Month)
                                    {
                                        reservatieDatumJaar = DateTime.Now.AddYears(1).Year;
                                    }
                                }
                            }

                            //Februari
                            else if (reservatieDatumMaand == 2)
                            {
                                if (DateTime.IsLeapYear(reservatieDatumJaar))
                                {
                                    if (reservatieDatumDag < 0 || reservatieDatumDag > 29)
                                    {
                                        Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 29");
                                        geldigeDag = false;
                                    }
                                    else
                                    {
                                        geldigeDag = true;
                                    }
                                }

                                else
                                {
                                    if (reservatieDatumDag < 0 || reservatieDatumDag > 28)
                                    {
                                        Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 28");
                                        geldigeDag = false;
                                    }
                                    else
                                    {
                                        geldigeDag = true;
                                    }
                                }
                            }
                        }
                    }



                    Display("\nWelke tijd wilt u komen? (uur:minuut). U kunt per half uur reserveren. De eerst mogelijke tijd is 17:00. Reserveren kan tot 22:00.");
                    //Controleert of de tijd klopt.

                    bool geldigeTijd = false;
                    while(geldigeTijd == false)
                    {
                        reservatieTijd = Console.ReadLine();
                        if (reservatieTijd == "terug")
                        {
                            Terug();
                        }
                        else if (reservatieTijd == "17:00" || reservatieTijd == "17:30" || reservatieTijd == "18:00" || reservatieTijd == "18:30" || reservatieTijd == "19:00"
                            || reservatieTijd == "19:30" || reservatieTijd == "20:00" || reservatieTijd == "20:30" || reservatieTijd == "21:00" || reservatieTijd == "21:30" || reservatieTijd == "22:00")
                        {
                            geldigeTijd = true;
                        }
                        else
                        {
                            Display("\nDit is geen geldig tijd. Let op dat het binnen de reserveertijden valt. Er kan alleen per half uur gereserveerd worden. Noteer het op de volgende manier (uur:minuut)");
                            geldigeTijd = false;
                        }
                    }


                    Display("\nMet hoeveel mensen wilt u komen?");
                    //Controleert met hoeveel mensen ze komen. De tafels hebben maximaal 6 plekken. Alles daarboven ervoor bellen
                    bool geldigAantalPersonen = false;
                    while (geldigAantalPersonen == false)
                    {
                        reservatieAantal = Console.ReadLine();
                        if (reservatieAantal == "terug")
                        {
                            Terug();
                        }
                        else if (reservatieAantal == "1" || reservatieAantal == "2" || reservatieAantal == "3" || reservatieAantal == "4" || reservatieAantal == "5" || reservatieAantal == "6")
                        {
                            geldigAantalPersonen = true;  
                        }
                        else if(Int32.Parse(reservatieAantal) < 1)
                        {
                            Display("Dit is geen geldig aantal mensen, vul gelieve een nummer boven de 0 in");
                            geldigAantalPersonen = false;
                           
                        }
                        else
                        {
                            Display("\nVoor reserveringen van meer dan 6 personen moet u even bellen naar het restaurant.\n\nDruk [5] om terug om naar het hoofdmenu te gaan.");
                            input = Console.ReadKey();
                            if (input.Key == ConsoleKey.D5)
                            {
                                var MainMenu = new MainMenu();
                                MainMenu.Show();
                            }
                        }
                    }
                    Display("\nBedankt voor je reservering! Druk op [ENTER] om de reservering te bevestigen!\nAls u de reservering niet wilt bevestigen druk dan op [5]");


                    //Met escape terug aan het einde
                    input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Display("\nBedankt voor het reserveren.");
                        Display($"\nNaam: {reservatieNaam} \nDatum: {reservatieDatumDag}-{reservatieDatumMaand}-{reservatieDatumJaar} \nTijd: {reservatieTijd}\nAantal personen: {reservatieAantal}\nKlik op [5] om terug te gaan naar het hoofdmenu");
                        if (Reserverings == ReturnLeeg())
                        {
                            Reserverings = "";
                        }
                        string dagZero = Zero(Convert.ToString(reservatieDatumDag));
                        string maandZero = Zero(Convert.ToString(reservatieDatumMaand));
                        string Jantien = "ID" + Convert.ToString(IDCounters) + "-" + dagZero + "/" + maandZero + "/" + Convert.ToString(reservatieDatumJaar) + "-" + reservatieAantal + " Personen-" + reservatieTijd + "-Naam: " + reservatieNaam;
                        //Display(Jantien);
                        if (Reserverings != ReturnLeeg())
                        {
                            Reserverings += Jantien + "\n";
                        }
                        else if (Reserverings == ReturnLeeg())
                        {
                            Reserverings = Jantien + "\n";
                        }
                        else if (Reserverings == "")
                        {
                            Reserverings = ReturnLeeg();
                        }

                        int Johan = IDCounters;
                        Johan += 1;
                        JsonDing(Reserverings, Johan);
                    }

                    if (input.Key == ConsoleKey.D5)
                    {
                        var MainMenu = new MainMenu();
                        MainMenu.Show();
                    }
                }
            }
            while (reservatieNaam == null || reservatieDatumMaand == 0 || reservatieTijd == null || reservatieAantal == null);


            //Met escape terug aan het einde
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.D5)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();
    
            }
        
        }
        public void Serialize(ResOverzicht resoverzicht)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            // serialized het weer want vs wil dat
            var ReservatieInformatieJson = System.Text.Json.JsonSerializer.Serialize(resoverzicht, serializeOptions);
            File.WriteAllText("ReservatieInformatie.json", ReservatieInformatieJson);
        }
    }
}