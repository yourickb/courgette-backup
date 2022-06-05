using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Noodle
{

    class ResOverzicht : Step //class van de cs file, waarmee je in andere files naar deze kan verwijzen (bijv van admin selectie scherm naar overzicht scherm)
    {
        public class ReservatieInformaties
        {
            // string voor stukje "overzicht" in de json, voeg hier meer bij toe als je ook meer stukjes in de json zet
            public string Reserveringen { get; set; }
            public int IDCounter { get; set; }
        }


        // hier zet ie de json om tot leesbaar formaat
        public ReservatieInformaties Deserialize()
        {
            string json = File.ReadAllText("ReservatieInformatie.json");
            var ReservatieInformatieJson = System.Text.Json.JsonSerializer.Deserialize<ReservatieInformaties>(json);
            return ReservatieInformatieJson;
        }

        public override void Show()
        {
            static void jsonWriter(string reservering, int idCount)
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
                var ResOverzicht = new ResOverzicht();
                ResOverzicht.Show();

            }
            static string ReturnLeeg()  
            {
                return "Er zijn geen reserveringen";
            }

            Log("");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage(); //pakt de taal van de greetingsfile
            ConsoleKeyInfo input;
            var ReservatieInformatieJson = Deserialize();
            string Reserveringen = ReservatieInformatieJson.Reserveringen;
            int IDCounter = ReservatieInformatieJson.IDCounter;
            if (Reserveringen == "")
            {
                Reserveringen = ReturnLeeg();
            }
            string ReserveringenOpDatum = string.Join("\n", Reserveringen
                .Split('\n')
                .OrderBy(item => DateTime.TryParseExact(
                    Regex.Match(item, "[0-9]{1,2}/[0-9]{1,2}/[0-9][0-9][0-9]{1,2}").Value,
                    "d/M/yyyy",
                    null,
                    DateTimeStyles.AssumeLocal,
                    out var date)
                ? date
                : DateTime.MaxValue));
            do
            {
                {
                    Console.Clear();
                    if (taalSetting == "nl")
                    { 
                        Console.Clear();
                        Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                        Console.WriteLine(ReserveringenOpDatum);
                        Console.WriteLine("\n[1] Voeg een reservering toe");
                        Console.WriteLine("[2] Wijzig een reservering");
                        Console.WriteLine("[3] Verwijder alle reserveringen");
                        Console.WriteLine("[4] Verwijder een specifieke reservering");
                        Console.WriteLine("[5] Ga terug naar het Admin overzicht");

                    }
                    // input wordt wat de gebruiker erin typt
                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.D5 && input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4);
            if (input.Key == ConsoleKey.D1)
            {
                Console.Clear();
                Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                Console.WriteLine(ReserveringenOpDatum);
                Display("\nVoeg hier je reserving toe als: \ndd/mm/jjjj-x Personen-uu:mm-Naam: (naam hier)\nNote: Je hoeft er geen ID bij te zetten");
                if (Reserveringen != "Er zijn geen reserveringen")
                {
                    string nieuweReservering = "ID" + Convert.ToString(IDCounter) + "-" + Console.ReadLine();
                    Reserveringen += nieuweReservering + "\n";
                }
                if (Reserveringen == "Er zijn geen reserveringen")
                {
                    string nieuweReservering = "ID" + Convert.ToString(IDCounter) + "-" + Console.ReadLine();
                    Reserveringen = nieuweReservering + "\n";

                }
                if (Reserveringen == "")
                {
                    Reserveringen = ReturnLeeg();
                }
                int IDCountOmhoog = IDCounter;
                IDCountOmhoog += 1;
                jsonWriter(Reserveringen, IDCountOmhoog);

            }
            if (input.Key == ConsoleKey.D2)
            {
                Console.Clear();
                Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                Console.WriteLine(ReserveringenOpDatum);
                Console.WriteLine("\nTyp de reservering in die je wil wijzigen *exact* over");
                string InputReservering = Console.ReadLine();
                Console.WriteLine("\nTyp nu in waar je het in wil wijzigen als: \nIDxx-dd/mm/jjjj-x Personen-uu:mm-Naam: (naam hier)\nNote: ID hetzelfde laten");
                string WijzigdeReserving = Console.ReadLine();
                Reserveringen = Reserveringen.Replace(InputReservering + "\n", WijzigdeReserving + "\n");
                if (Reserveringen == "")
                {
                    Reserveringen = ReturnLeeg();
                }
                jsonWriter(Reserveringen, IDCounter);
                

            }
            if (input.Key == ConsoleKey.D3)
            {
                Reserveringen = ReturnLeeg();
                int IDCountOmhoog = IDCounter;
                if (IDCountOmhoog > 999)
                {
                    IDCountOmhoog = 1;
                }
                jsonWriter(Reserveringen, IDCountOmhoog);

            }
            if (input.Key == ConsoleKey.D4)
            {
                Console.Clear();
                Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                Console.WriteLine(ReserveringenOpDatum);
                Console.WriteLine("\nTyp de reservering in die je wil verwijderen *exact* over");
                string temp = Console.ReadLine();
                Reserveringen = Reserveringen.Replace(temp + "\n", "");
                jsonWriter(Reserveringen, IDCounter);
            }

            if (input.Key == ConsoleKey.D5)
            { // bij 5 gaat ie weer terug naar Admin.cs
                var Admin = new Admin();
                Admin.Show();

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