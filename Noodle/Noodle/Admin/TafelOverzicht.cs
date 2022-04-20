using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class TafelOverzicht : Step //class van de cs file, waarmee je in andere files naar deze kan verwijzen (bijv van admin selectie scherm naar overzicht scherm)
    {
        public class TafelOverzichtReservering
        {
            // string voor stukje "overzicht" in de json, voeg hier meer bij toe als je ook meer stukjes in de json zet
            public string Tafel { get; set; }
        }
        // hier zet ie de json om tot leesbaar formaat
        public TafelOverzichtReservering Deserialize()
        {
        string json = File.ReadAllText("TafelOverzichtReservering.json");
        var TafelOverzichtReserveringJson = JsonSerializer.Deserialize<TafelOverzichtReservering>(json);
        return TafelOverzichtReserveringJson;
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
                    var TafelOverzichtReserveringJson = Deserialize();
                    if (taalSetting == "nl")
                    { //de tweede write line pakt ie wat onder "overzicht" staat in het json bestand
                        Console.WriteLine("Hier is het overzicht van de tafels: ");
                        Console.WriteLine(TafelOverzichtReserveringJson.Tafel);
                    }

                    else
                    {
                        Console.WriteLine("Here is the overview of the tables: ");
                        Console.WriteLine(TafelOverzichtReserveringJson.Tafel);
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
        public void Serialize(TafelOverzicht tafeloverzicht)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            // serialized het weer want vs wil dat
            var TafelOverzichtReserveringJson = JsonSerializer.Serialize(tafeloverzicht, serializeOptions);
            File.WriteAllText("TafelOverzichtReservering.json", TafelOverzichtReserveringJson);
        }
    }
}