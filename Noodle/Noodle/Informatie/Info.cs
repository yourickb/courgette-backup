using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Informatie : Step
    {
        public class ResInformatie
        {
            public string Info { get; set; }
        }

        public ResInformatie Deserialize()
        {
            string json = File.ReadAllText("ResInformatie.json");
            var ResInformatieJson = JsonSerializer.Deserialize<ResInformatie>(json);
            return ResInformatieJson;
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
                    var ResInformatieJson = Deserialize();
                    if (taalSetting == "nl")
                    {
                        Console.WriteLine(ResInformatieJson.Info);
                    }

                    else
                    {
                        Console.WriteLine("Contact details of the restaurant: ");
                        Console.WriteLine(" ");
                        Console.WriteLine(ResInformatieJson.Info);
                    }

                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.D5);

            if (input.Key == ConsoleKey.D5)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();

            }
        }
        public void Serialize(ResInformatie informatie)
        {
            var serializeOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var ResInformatieJson = JsonSerializer.Serialize(informatie, serializeOptions);
            File.WriteAllText("ResInformatie.json", ResInformatieJson);
        }
    }
}