using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class MenuAanpassing : Step //class van de cs file, waarmee je in andere files naar deze kan verwijzen (bijv van admin selectie scherm naar overzicht scherm)
    {
        public class MenuAanpassen
        {
            // string voor stukje "overzicht" in de json, voeg hier meer bij toe als je ook meer stukjes in de json zet
            public string MenuAanpas { get; set; }
        }
        // hier zet ie de json om tot leesbaar formaat
        public MenuAanpassen Deserialize()
        {
        string json = File.ReadAllText("MenuAanpassen.json");
        var MenuAanpassenJson = JsonSerializer.Deserialize<MenuAanpassen>(json);
        return MenuAanpassenJson;
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
                    var MenuAanpassenJson = Deserialize();
                    if (taalSetting == "nl")
                    { //de tweede write line pakt ie wat onder "overzicht" staat in het json bestand
                        Console.WriteLine("Pas hier het menu aan: (komt nog)");
                        Console.WriteLine(MenuAanpassenJson.MenuAanpas);
                    }

                    else
                    {
                        Console.WriteLine("Change the menu here: ");
                        Console.WriteLine(MenuAanpassenJson.MenuAanpas);
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
        public void Serialize(MenuAanpassing menuaanpassing)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            // serialized het weer want vs wil dat
            var MenuAanpassenJson = JsonSerializer.Serialize(menuaanpassing, serializeOptions);
            File.WriteAllText("MenuAanpassen.json", MenuAanpassenJson);
        }
    }
}