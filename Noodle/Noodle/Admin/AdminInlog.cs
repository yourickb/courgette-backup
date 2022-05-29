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
            string username = "";
            string password = "";

            do
            {
                Console.Clear();

                if (taalSetting == "nl")
                {
                    Display("Admin Login!");
                    Display("Vul uw admin gebruikersnaam en wachtwoord in!\n\n");
                    Display("Gebruikersnaam :");
                    username = Console.ReadLine();
                    Display("Wachtwoord :");
                    password = Console.ReadLine();
                    if (username != goodUsername || password != goodPassword)
                    {
                        Display("Je gebruikersnaam en/of wachtwoord is vekeerd, druk [ENTER] om het opnieuw te proberen, of druk [5] om terug te gaan");
                    }
                    if (username == goodUsername && password == goodPassword)
                    {
                        Display("Druk [ENTER] om door te gaan of druk [5] om terug te gaan");
                    }

                }


                else
                {
                    Display("Admin Login!\n");
                    Display("Fill your username and password in!\n\n");
                    Display("Username :\n");
                    Display("Password");
                    Display("");
                }

                input = Console.ReadKey();
                if (username == "Jan" && password == "123" && input.Key == ConsoleKey.Enter)
                {
                    _option1_1.Show();
                }
                
                else if (input.Key == ConsoleKey.D5)
                {
                    var MainMenu = new MainMenu();
                    MainMenu.Show();
                }
                //input = Console.ReadKey();
                
                if (username != "Jan" && password != "123")
                {
                    var Admin1 = new AdminInlog();
                    Admin1.Show();
                }
            }
            //als je 1, 2, 3, 4, etc drukt dan gaat ie naar de bijbehorende optie en dan naar het volgend cs bestand
            while (input.Key != ConsoleKey.D1 && input.Key != ConsoleKey.D2 && input.Key != ConsoleKey.D3 && input.Key != ConsoleKey.D4 && input.Key != ConsoleKey.D5);

            if (username == "Jan" && password == "123")
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