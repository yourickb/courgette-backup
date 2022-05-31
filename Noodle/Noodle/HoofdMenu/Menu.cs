using System;
using System.Collections.Generic;
using System.Text;

namespace Noodle
{
    public class MainMenu : Step
    {
        private readonly Step _option1;
        private readonly Step _option2;
        private readonly Step _option3;
        private readonly Step _option4;

        public MainMenu()
        {
            _option1 = new Reserveer();
            _option2 = new GerechtenMenu();
            _option3 = new Informatie();
            _option4 = new AdminInlog();

            _option1.SetPrevious(this);
            _option2.SetPrevious(this);
            _option3.SetPrevious(this);
            _option4.SetPrevious(this);
        }

        public override void Show()
        {
            Log("[Step 2]");
            var greetingsJson = new WelcomePage();
            string taalSetting = "nl";

            ConsoleKeyInfo input;

            do
            {
                Console.Clear();

                if (taalSetting == "nl")
                {

                    Display("Welkom bij Restaurant De Noodle");
                    Display("Kies alstublieft uit één van de volgende opties:");
                    Display("");
                    Display("[1] Reserveer");
                    Display("[2] GerechtenMenu");
                    Display("[3] Informatie");
                    Display("[4] Admin");
                }
                input = Console.ReadKey();
            }
            while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4);

            if (input.Key == ConsoleKey.D1)
            {
                _option1.Show();
            }

            if (input.Key == ConsoleKey.D2)
            {
                _option2.Show();
            }

            if (input.Key == ConsoleKey.D3)
            {
                _option3.Show();
            }

            if (input.Key == ConsoleKey.D4)
            {
                _option4.Show();
            }
        }
    }
}
