using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Drankenkaart : Step
    {
        public class MenuDrankenkaart
        {
            public string Menu { get; set; }
        }

        public MenuDrankenkaart Deserialize()
        {
        string json = File.ReadAllText("MenuDrankenkaart.json");
        var MenuDrankenkaartJson = JsonSerializer.Deserialize<MenuDrankenkaart>(json);
        return MenuDrankenkaartJson;
        }

        public override void Show()
        {
            Log("");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage();
            ConsoleKeyInfo input;

            do
            {
                {
                    Console.Clear();
                    var MenuDrankenkaartJson = Deserialize();
                    if (taalSetting == "nl")
                    {
                        Console.WriteLine("Je kan uit de volgende dranken kiezen: ");
                        Console.WriteLine(MenuDrankenkaartJson.Menu);
                    }



                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.Escape);
            
            if (input.Key == ConsoleKey.Escape)
            {
                var GerechtenMenu = new GerechtenMenu();
                GerechtenMenu.Show();

            }
        }
        public void Serialize(MenuDrankenkaart dranken)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var MenuDrankenkaartJson = JsonSerializer.Serialize(dranken, serializeOptions);
            File.WriteAllText("MenuDrankenkaart.json", MenuDrankenkaartJson);
        }
    }
}