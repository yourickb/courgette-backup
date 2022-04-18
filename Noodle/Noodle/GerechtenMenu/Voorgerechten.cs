using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Voorgerechten : Step
    {
        public class Menuvoorgerechten
        {
            public string Menu { get; set; }
        }

        public Menuvoorgerechten Deserialize()
        {
        string json = File.ReadAllText("Menuvoorgerechten.json");
        var MenuvoorgerechtenJson = JsonSerializer.Deserialize<Menuvoorgerechten>(json);
        return MenuvoorgerechtenJson;
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
                    var MenuvoorgerechtenJson = Deserialize();
                    if (taalSetting == "nl")
                    {
                        Console.WriteLine("Je kan uit de volgende voorgerechten kiezen: ");
                        Console.WriteLine(MenuvoorgerechtenJson.Menu);
                    }

                    else
                    {
                        Console.WriteLine("You can choose from the following starters:");
                        Console.WriteLine(MenuvoorgerechtenJson.Menu);
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
        public void Serialize(Menuvoorgerechten voorgerechten)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var MenuvoorgerechtenJson = JsonSerializer.Serialize(voorgerechten, serializeOptions);
            File.WriteAllText("Menuvoorgerechten.json", MenuvoorgerechtenJson);
        }
    }
}