using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class ResOverzicht : Step
    {
        public class AdminOverzichtReservering
        {
            public string Overzicht { get; set; }
        }

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
            string taalSetting = greetingsJson.Getlanguage();
            ConsoleKeyInfo input;

            do
            {
                {
                    Console.Clear();
                    var AdminOverzichtReserveringJson = Deserialize();
                    if (taalSetting == "nl")
                    {
                        Console.WriteLine("Hier is het overzicht van de reserveringen: ");
                        Console.WriteLine(AdminOverzichtReserveringJson.Overzicht);
                    }

                    else
                    {
                        Console.WriteLine("Here is the overview of the reservations: ");
                        Console.WriteLine(AdminOverzichtReserveringJson.Overzicht);
                    }

                    input = Console.ReadKey();

                }
            }
            while (input.Key != ConsoleKey.Escape);
            
            if (input.Key == ConsoleKey.Escape)
            {
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

            var AdminOverzichtReserveringJson = JsonSerializer.Serialize(resoverzicht, serializeOptions);
            File.WriteAllText("AdminOverzichtReservering.json", AdminOverzichtReserveringJson);
        }
    }
}