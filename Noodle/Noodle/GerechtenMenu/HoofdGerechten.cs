using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class hoofdgerechten : Step
    {
        public class Menuhoofdgerechten
        {
            public string Menu { get; set; }
        }

        public Menuhoofdgerechten Deserialize()
        {
            string json = File.ReadAllText("Menuhoofdgerechten.json");
            var MenuhoofdgerechtenJson = JsonSerializer.Deserialize<Menuhoofdgerechten>(json);
            return MenuhoofdgerechtenJson;
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
                    var MenuhoofdgerechtenJson = Deserialize();
                    if (taalSetting == "nl")
                    {
                        Console.WriteLine("Je kan uit de volgende hoofdgerechten kiezen: ");
                        Console.WriteLine(MenuhoofdgerechtenJson.Menu);
                    }

                    else
                    {
                        Console.WriteLine("You can choose from the following main courses: ");
                        Console.WriteLine(MenuhoofdgerechtenJson.Menu);
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
        public void Serialize(Menuhoofdgerechten hoofdgerechten)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var MenuhoofdgerechtenJson = JsonSerializer.Serialize(hoofdgerechten, serializeOptions);
            File.WriteAllText("Menuvoorgerechten.json", MenuhoofdgerechtenJson);
        }
    }
}