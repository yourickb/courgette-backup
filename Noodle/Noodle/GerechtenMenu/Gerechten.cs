using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class GerechtenMenu : Step
    {
        private readonly Step _option1_1;
        private readonly Step _option1_2;
        private readonly Step _option1_3;
        private readonly Step _option1_4;

        public GerechtenMenu()
        {
            _option1_1 = new Voorgerechten();
            _option1_2 = new hoofdgerechten();
            _option1_3 = new Nagerechten();
            _option1_4 = new Drankenkaart();

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
                    Display("Kies alstublieft uit één van de volgende opties:");
                    Display("");
                    Display("[1] Voorgerechten");
                    Display("[2] Hoofdgerechten");
                    Display("[3] Nagerechten");
                    Display("[4] Drankenkaart");
                    Display("[5] Terug");
                }

                else
                {
                    Display("Please choose one of the following options:");
                    Display("");
                    Display("[1] Starter");
                    Display("[2] Main course ");
                    Display("[3] dessert");
                    Display("");
                }

                input = Console.ReadKey();
            }
            while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4 && input.Key  != ConsoleKey.D5);

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

            if(input.Key == ConsoleKey.D4)
            {
                _option1_4.Show();
            }
            
            if (input.Key == ConsoleKey.D5)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();
            }
        }
    }
}
