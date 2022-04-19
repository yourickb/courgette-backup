using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class ResOverzicht : Step //class van de cs file, waarmee je in andere files naar deze kan verwijzen (bijv van admin selectie scherm naar overzicht scherm)
    {
        public class AdminOverzichtReservering
        {
            // string voor stukje "overzicht" in de json, voeg hier meer bij toe als je ook meer stukjes in de json zet
            public string Overzicht { get; set; }
        }
        // hier zet ie de json om tot leesbaar formaat
        public AdminOverzichtReservering Deserialize()
        {
        string json = File.ReadAllText("AdminOverzichtReservering.json");
        var AdminOverzichtReserveringJson = JsonSerializer.Deserialize<AdminOverzichtReservering>(json);
        return AdminOverzichtReserveringJson;
        }

        public override void Show()
        {
            Log("");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage(); //pakt de taal van de greetingsfile
            ConsoleKeyInfo input;

            do
            {
                {
                    Console.Clear();
                    var AdminOverzichtReserveringJson = Deserialize();
                    if (taalSetting == "nl")
                    { //de tweede write line pakt ie wat onder "overzicht" staat in het json bestand
                        Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                        Console.WriteLine(AdminOverzichtReserveringJson.Overzicht);
                    }

                    else
                    {
                        Console.WriteLine("Here is the overview of the reservations: ");
                        Console.WriteLine(AdminOverzichtReserveringJson.Overzicht);
                    }
                    // input wordt wat de gebruiker erin typt
                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.Escape);
            
            if (input.Key == ConsoleKey.Escape)
            { // bij escape gaat ie weer terug naar Admin.cs
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
            var AdminOverzichtReserveringJson = JsonSerializer.Serialize(resoverzicht, serializeOptions);
            File.WriteAllText("AdminOverzichtReservering.json", AdminOverzichtReserveringJson);
        }
    }
}