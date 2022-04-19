using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Noodle
{

    class Reserveer : Step
    {
        public Reserveer()
        {
        }

        public override void Show()
        {
            ConsoleKeyInfo input;
            string reservatieNaam = null;
            int reservatieDatumMaand = 0;
            int reservatieDatumDag = 0;
            string reservatieTijd = null;   
            int reservatieAantal = 0;
            DateTime todayDate = DateTime.Today;

            do
            {
                Console.Clear();
                string taalSetting = "nl";

                if (taalSetting == "nl")
                {
                    Display("Bedankt voor het kiezen voor de Noodle \nOm te kunnen reserveren hebben wij wat informatie nodig");
                    Display("");
                    Display("\nVul alstublieft u hele naam in:");
                    reservatieNaam = Console.ReadLine();
                    Display("\nWelke Maand wilt u komen?");
                    bool validMonth = false;
                    while (validMonth == false)
                    {
                        reservatieDatumMaand = Convert.ToInt32(Console.ReadLine());
                        if (reservatieDatumMaand <= 12 && reservatieDatumMaand > 0)
                        {
                            validMonth = true;
                        }
                        else
                        {
                            Display("Die maand lijkt niet te bestaan. Vul gelieve een maand in tussen 1 (januari) en 12 (december).");
                            validMonth = false;
                        }
                    }
                    Display("\nWelke Dag wilt u komen?");
                    bool validDay = false;
                    while (validDay == false)
                    {
                        reservatieDatumDag = Convert.ToInt32(Console.ReadLine());
                        if (reservatieDatumMaand == 1 || reservatieDatumMaand == 3 || reservatieDatumMaand == 5 || reservatieDatumMaand == 7 || reservatieDatumMaand ==8 || reservatieDatumMaand == 10 || reservatieDatumMaand == 12)
                        {
                            if (reservatieDatumDag < 0 || reservatieDatumDag > 31)
                            {
                                Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 31");
                                validDay = false;

                            }
                            else
                            {
                                validDay = true;
                            }
                        }
                        else if (reservatieDatumMaand == 4 || reservatieDatumMaand == 6 || reservatieDatumMaand == 9 || reservatieDatumMaand == 11)
                        {
                            if (reservatieDatumDag < 0 || reservatieDatumMaand > 30)
                            {
                                Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en de 30");
                                validDay = false;
                            }
                            else
                            {
                                validDay = true;
                            }
                        }
                        else if(reservatieDatumMaand == 2) 
                        {
                            if (reservatieDatumDag < 0 || reservatieDatumDag > 28)
                            {
                                Display("\nDit is geen geldige dag voor deze maand. Vul alstublieft een nummer in tussen de 1 en 28");
                                validDay = false;

                            }
                            else
                            {
                                validDay = true;
                            }
                        }
                      

                    }



                    Display("\nWelke tijd wilt u komen? (uur:minuut)");
                    reservatieTijd= Console.ReadLine();
                    Display("\nMet hoeveel mensen wilt u komen?");
                    reservatieAantal = Convert.ToInt32(Console.ReadLine());
                    


                }
            }
            while(reservatieNaam == null || reservatieDatumMaand == 0 || reservatieTijd == null || reservatieAantal == 0);


            input = Console.ReadKey();
            if (input.Key == ConsoleKey.Escape)
            {
                var MainMenu = new MainMenu();
                MainMenu.Show();
            }



        }
    }

}