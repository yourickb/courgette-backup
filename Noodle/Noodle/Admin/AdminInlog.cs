using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{
    class AdminInlog : Step // zie resoverzicht.cs voor uitleg
    {
        private readonly Step _option1_1;

        public AdminInlog()
        { //als je de optie kiest gaat ie over naar ResOverzicht.cs
            _option1_1 = new Admin();

            _option1_1.SetPrevious(this);
        }
        public override void Show()
        {
            Console.Clear();
            Log("[Step 2]");
            var greetingsJson = new WelcomePage();
            string taalSetting = greetingsJson.Getlanguage();
            string goodUsername = greetingsJson.Getusername();
            string goodPassword = greetingsJson.Getpassword();
            ConsoleKeyInfo input;
            string inputUsername = "";
            string inputPassword = "";

            do
            {
                Console.Clear();

                if (taalSetting == "nl")
                {
                    Display("Admin Login!");
                    Display("Vul uw admin gebruikersnaam en wachtwoord in!");
                    bool correcteInlog = false;
                    while (correcteInlog == false)
                    {
                        Display("\nGebruikersnaam :");
                        inputUsername =  Console.ReadLine();
                        if (inputUsername == "terug")
                        {
                            var MainMenu = new MainMenu();
                            MainMenu.Show();
                        }
                        Display("Wachtwoord :");
                        inputPassword = Console.ReadLine();


                        if (inputUsername != goodUsername || inputPassword != goodPassword)
                        {
                            
                            Display("Je gebruikersnaam en/of wachtwoord is vekeerd, proberen het opnieuw, of typ 'terug' als gebruiksnaam om terug te gaan");
                            correcteInlog = false;
                        }
                        else
                        {
                            Display("Druk [ENTER] om door te gaan of druk [5] om terug te gaan");
                            correcteInlog = true;
                        }
                    }
                }

                input = Console.ReadKey();

            }
            //als je 1, 2, 3, 4, etc drukt dan gaat ie naar de bijbehorende optie en dan naar het volgend cs bestand
            while (input.Key != ConsoleKey.Enter && input.Key != ConsoleKey.D5);

            if (input.Key == ConsoleKey.Enter)
            {
                _option1_1.Show();
            }
            
            if (input.Key == ConsoleKey.D5)
            { // als je key escape is gaat ie terug naar het mainmenu
                var MainMenu = new MainMenu();
                MainMenu.Show();
            }



        }
    }
}