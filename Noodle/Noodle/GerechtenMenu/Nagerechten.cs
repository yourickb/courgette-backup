using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Nagerechten : Step
    {
        public class Menunagerechten
        {
            public string Menu { get; set; }
        }

        public Menunagerechten Deserialize()
        {
            string json = File.ReadAllText("Menunagerechten.json");
            var MenunagerechtenJson = JsonSerializer.Deserialize<Menunagerechten>(json);
            return MenunagerechtenJson;
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
                    var MenunagerechtenJson = Deserialize();
                    if (taalSetting == "nl")
                    {
                        Console.WriteLine("Je kan uit de volgende nagerechten kiezen: ");
                        Console.WriteLine(MenunagerechtenJson.Menu);
                    }

                    else
                    {
                        Console.WriteLine("You can choose from the following main courses: ");
                        Console.WriteLine(MenunagerechtenJson.Menu);
                    }

                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.D5);

            if (input.Key == ConsoleKey.D5)
            {
                var GerechtenMenu = new GerechtenMenu();
                GerechtenMenu.Show();

            }
        }
        public void Serialize(Menunagerechten nagerechten)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var MenunagerechtenJson = JsonSerializer.Serialize(nagerechten, serializeOptions);
            File.WriteAllText("Menuvoorgerechten.json", MenunagerechtenJson);
        }
    }
}