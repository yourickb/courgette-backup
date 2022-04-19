using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{
    class Admin : Step
    {
        private readonly Step _option1_1;
        private readonly Step _option1_2;
        private readonly Step _option1_3;
        private readonly Step _option1_4;

        public Admin()
        {
            _option1_1 = new ResOverzicht();
            _option1_2 = new ResOverzicht();
            _option1_3 = new ResOverzicht();
            _option1_4 = new ResOverzicht();

            _option1_1.SetPrevious(this);
            _option1_2.SetPrevious(this);
            _option1_3.SetPrevious(this);
            _option1_4.SetPrevious(this);
        }
        public override void Show()
        {
            Console.Clear();
            Log("[Step 2]");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage();
            ConsoleKeyInfo input;

            do
            {
                Console.Clear();

                if (taalSetting == "nl")
                {
                    Display("Kies alstublieft uit één van de volgende opties: (dit gaat allemaal nog alleen naar de AdminOverzichtReservering json file)");
                    Display("");
                    Display("[1] Overzicht van de reserveringen");
                    Display("[2] Overzicht van de tafels");
                    Display("[3] Bestellingen per tafel");
                    Display("[4] Aanpassen van het menu");
                    Display("");
                }

                else
                {
                    Display("Please choose one of the following options:");
                    Display("");
                    Display("[1] Overview of the reservations");
                    Display("[2] Overview of the tables");
                    Display("[3] Orders per table");
                    Display("[4] Change the menu");
                    Display("");
                }

                input = Console.ReadKey();
            }
            while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4 && input.Key != ConsoleKey.Escape);

            if (input.Key == ConsoleKey.D1)
            {
                _option1_1.Show();
            }

            if (input.Key == ConsoleKey.D2)
            {
                _option1_2.Show();
            }

            if (input.Key == ConsoleKey.D3)
            {
                _option1_3.Show();
            }
            
            if (input.Key == ConsoleKey.D4)
            {
                _option1_4.Show();
            }
            
            if (input.Key == ConsoleKey.Escape)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();
            }
        }
    }
}