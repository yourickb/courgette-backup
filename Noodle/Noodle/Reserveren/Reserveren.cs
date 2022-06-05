using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;

namespace Noodle
{

    class Reserveer : Step
    {
        public class ReservatieInformaties
        {
            // informatie van de JSON file pakken
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
            string maandString = null;
            string dagString = null;
            string reservatieTijd = null;
            string reservatieAantal = null;
            var ReservatieInformatieJson = Deserialize();
            string Reserveringen = ReservatieInformatieJson.Reserveringen;
            int IDCounter = ReservatieInformatieJson.IDCounter;
            string dagZero = null; 
            string maandZero = null;
            (int, int, int, int, int) reservatieTuple = (0, 31, 30, 29, 28);
            int[] resMaand31Dagen = { 1, 3, 5, 7, 8, 10, 12 };
            int[] resMaand30Dagen = { 4, 6, 9, 11 };
            string[] maxPersonen = { "1", "2", "3", "4", "5", "6"};
            string[] ongeldigPersonen = { "0", "-1", "-2", "-3", "-4", "-5" };
            string[] overMaxPersonen = { "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };

            static void jsonWriter(string reservering, int idCount) // JSON writer wat er voor zorgt dat elke nieuwe reservering opgeslagen wordt
            {
                StringBuilder sb = new StringBuilder();
                StringWriter sw = new StringWriter(sb);
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartObject();
                    writer.WritePropertyName("Reserveringen");
                    writer.WriteValue(reservering);
                    writer.WritePropertyName("IDCounter");
                    writer.WriteValue(idCount);
                    writer.WriteEnd();
                }
                string json = JsonConvert.SerializeObject(sb);
                string sk = (sb.ToString());
                File.WriteAllText("ReservatieInformatie.json", sk);
            }
            static string ReturnLeeg()
            {
                return "Er zijn geen reserveringen";
            }

            string nulErbij(Func<string, string, string> Add0, string num1, string num2) => Add0(num1, num2);
            // HOF 0 toevoegen

            string Add0(string number1, string number2) => number2 + number1;
            // onderdeel HOF 0 voor de datum te plakken
            
            int jaarLater(int jaar, int nieuweJaar) => jaar + nieuweJaar;
            // losse function voor leapyear

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
                        Display("Druk op [ENTER] om de reservering te beginnen en [5] om af te breken\n\nNote: Als u wilt teruggaan tijdens het maken van de reservering typ 'terug' in\n");


                        //Met 5 terug aan het einde
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.D5)
                        {
                            var MainMenu = new MainMenu();
                            MainMenu.Show();

                        }
                    }
                    while (input.Key != ConsoleKey.Enter && input.Key != ConsoleKey.D5);

                    Display("\nVul alstublieft uw volledige naam in:");

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
                            Display("Vul alstublieft een geldige naam in");
                        }
                        else
                        {
                            geldigeNaam = true;
                        }
                    }

                    Display("\nWelke maand wilt u komen? (als getal)");
                    bool geldigeMaand = false;
                    int jaar = DateTime.Now.Year;
                    int reservatieDatumJaar = 0;

                    while (geldigeMaand == false)
                    {
                        maandString = Console.ReadLine();
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
                    Display("\nWelke dag wilt u komen? (als getal)");

                    //Controleert of de dag klopt. Met een max van het aantal dagen in die maand
                    bool geldigeDag = false;
                    while (geldigeDag == false)
                    {
                        dagString = Console.ReadLine();
                        if (dagString == "terug")
                        {
                            Terug();
                        }
                        else if (int.TryParse(dagString, out reservatieDatumDag))
                        {
                            //  Maanden met 31 dagen
                            if (resMaand31Dagen.Contains(reservatieDatumMaand))
                            {
                                if (reservatieDatumDag <= reservatieTuple.Item1 || reservatieDatumDag > reservatieTuple.Item2)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 31");
                                    geldigeDag = false;

                                }
                                else
                                {
                                    geldigeDag = true;
                                    if (reservatieDatumDag < DateTime.Now.Day && reservatieDatumMaand == DateTime.Now.Month)
                                    {
                                        reservatieDatumJaar = jaarLater(DateTime.Now.Year, 1);

                                    }
                                }
                            }
                            // Maanden met 30 dagen
                            else if (resMaand30Dagen.Contains(reservatieDatumMaand))
                            {
                                if (reservatieDatumDag <= reservatieTuple.Item1 || reservatieDatumDag > reservatieTuple.Item3)
                                {
                                    Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en de 30");
                                    geldigeDag = false;
                                }
                                else
                                {
                                    geldigeDag = true;
                                    if (reservatieDatumDag < DateTime.Now.Day && reservatieDatumMaand == DateTime.Now.Month)
                                    {
                                        reservatieDatumJaar = jaarLater(DateTime.Now.Year, 1);
                                    }
                                }
                            }

                            //Februari
                            else if (reservatieDatumMaand == 2)
                            {
                                if (DateTime.IsLeapYear(reservatieDatumJaar))
                                {
                                    if (reservatieDatumDag <= reservatieTuple.Item1 || reservatieDatumDag > reservatieTuple.Item4)
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
                                    if (reservatieDatumDag <= reservatieTuple.Item1 || reservatieDatumDag > reservatieTuple.Item5)
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


                    Display("\nMet hoeveel mensen wilt u komen? (als getal)");
                    //Controleert met hoeveel mensen ze komen. De tafels hebben maximaal 4 plekken. Alles daarboven ervoor bellen
                    bool geldigAantalPersonen = false;
                    while (geldigAantalPersonen == false)
                    {
                        reservatieAantal = Console.ReadLine();
                        if (reservatieAantal == "terug")
                        {
                            Terug();
                        }
                        else if (maxPersonen.Contains(reservatieAantal))
                        {
                            geldigAantalPersonen = true;
                        }
                        else if (ongeldigPersonen.Contains(reservatieAantal))
                        {
                            Display("\nDit is geen geldig aantal personen.\nTyp een ander getal in of typ 'terug' om af te breken.");
                            geldigAantalPersonen = false;
                        }
                        else if (overMaxPersonen.Contains(reservatieAantal))
                        {
                            Display("\nVoor reserveringen van meer dan 6 personen moet u even bellen naar het restaurant.\nTyp een ander getal in of typ 'terug' om af te breken.");
                            geldigAantalPersonen = false;
                        }
                        else
                        {
                            Display("\nVoor reserveringen van meer dan 6 personen moet u even bellen naar het restaurant of dit is geen geldig aantal personen.\nTyp een ander getal in of typ 'terug' om af te breken.");
                            geldigAantalPersonen = false;
                        }
                    }

                    Display("\nBedankt voor je reservering! Druk op [ENTER] om de reservering te bevestigen!\nAls u de reservering niet wilt bevestigen druk dan op [5]");


                    //Met escape terug aan het einde
                    input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                     
                        // voor het toevoegen van 0 dmv HOF
                        if (reservatieDatumMaand <= 9)
                        {
                            maandZero = nulErbij(Add0, maandString, "0");
                        }
                        
                        if (reservatieDatumDag <= 9)
                        {
                            dagZero = nulErbij(Add0, dagString, "0");
                        }
                        
                        if (reservatieDatumMaand > 9)
                        {
                            maandZero = Convert.ToString(reservatieDatumMaand);
                        }

                        if (reservatieDatumDag > 9)
                        {
                            dagZero = Convert.ToString(reservatieDatumDag);
                        }
                        string reserveringString = "ID" + Convert.ToString(IDCounter) + "-" + dagZero + "/" + maandZero + "/" + Convert.ToString(reservatieDatumJaar) + "-" + reservatieAantal + " Personen-" + reservatieTijd + "-Naam: " + reservatieNaam;
                        if (Reserveringen != ReturnLeeg())
                        {
                            Reserveringen += reserveringString + "\n";
                        }
                        else if (Reserveringen == ReturnLeeg())
                        {
                            Reserveringen = reserveringString + "\n";
                        }
                        else if (Reserveringen == "")
                        {
                            Reserveringen = ReturnLeeg();
                        }
                        Display("\nBedankt voor het reserveren.");
                        Display($"\nNaam: {reservatieNaam} \nDatum: {dagZero}-{maandZero}-{reservatieDatumJaar} \nTijd: {reservatieTijd}\nAantal personen: {reservatieAantal}\nDruk op [5] om terug te gaan naar het hoofdmenu");
                        int IDCounterOmhoog = IDCounter;
                        IDCounterOmhoog += 1;
                        jsonWriter(Reserveringen, IDCounterOmhoog);
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