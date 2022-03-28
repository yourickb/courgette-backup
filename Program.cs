//Groep 4 Project B code
string keuze = "";
string stopString = "";
bool stop = true;
// zodat de terugknop werkt op hele simpele wijze
while (stop)
{
    // Titel van de applicatie
    Console.Clear();
    Console.WriteLine("Welkom bij ons restaurant! |\r");
    Console.WriteLine("---------------------------┘\n");
    
    // vragen wat de gebruiker wil doen
    Console.WriteLine("Wat wil je doen?\n");

    Console.WriteLine("Kies een optie van de lijst: \n");
    // vraagt om iets te kiezen

    Console.WriteLine("\tmenu - Het menu");
    Console.WriteLine("\treserveren - Reserveren");
    Console.WriteLine("\tinfo - Informatie over het restaurant");
    Console.WriteLine("\tlogin - Log in scherm voor medewerkers");
    Console.WriteLine("\toptie - Hier komt nog iets");
    Console.WriteLine("\tstoppen - Hiermee stop je het programma\n");

    Console.Write("Je optie? ");
    keuze = Console.ReadLine();

    while (keuze != "menu" && keuze != "reserveren" && keuze != "info" && keuze != "optie" && keuze != "login" && keuze != "stoppen" && keuze != "terug")
    {
        Console.Write("Misschien heb je het verkeerd getypt, typ hier de goede of typ terug als je terug wilt gaan: ");
        keuze = (Console.ReadLine());
    }
    // zorgt ervoor dat je niet iets anders intypt

    switch (keuze)
    {
        case "menu": // als je menu kiest
            Console.Clear();
            Console.WriteLine("Ons menu!");
            Console.WriteLine("-Voorgerechten-\n");
            Console.WriteLine("Gerecht: Salade");
            Console.WriteLine("Gerecht: Soep\n");
            Console.WriteLine("-Hoofdgerechten-\n ");
            Console.WriteLine("Gerecht: Kapsalon");
            Console.WriteLine("Gerecht: Zak Patat\n");
            Console.WriteLine("-Nagerechten-\n");
            Console.WriteLine("Gerecht: Kaas");
            Console.WriteLine("Gerecht: Boterham");
            Console.WriteLine("Gerecht: Yogurt\n");
            Console.Write("Druk return/enter om terug te gaan naar het begin ");
            Console.ReadLine();
            break;
       
        case "reserveren": // als je reserveren kiest
            Console.Clear();
            Console.WriteLine("Je kan hier reserveren... (de rest komt nog)\n");
            Console.Write("Uw dag (dag/maand/jaar): ");
            Console.ReadLine();
            Console.Write("Uw tijd (uur/minuut): ");
            Console.ReadLine();
            Console.WriteLine("Helaas zitten wij dan vol of werkt dit nog niet!");
            Console.Write("Druk return/enter om terug te gaan naar het begin ");
            Console.ReadLine();
            break;
        
        case "info": // als je info kiest
            Console.Clear();
            Console.WriteLine("Info over restaurant... (de rest komt nog)\n");
            Console.Write("Druk return/enter om terug te gaan naar het begin ");
            Console.ReadLine();
            break;
        
        case "login": // als je login kiest
            Console.Clear();
            Console.WriteLine("Login scherm voor medewerkers\n");
            Console.Write("Gebruikersnaam: \n");
            Console.ReadLine();
            Console.Write("Wachtwoord: \n");
            Console.ReadLine();
            Console.Write("Dit werkt nog niet, druk return/enter om terug te gaan naar het begin: ");
            Console.ReadLine();
            break;

        case "optie": // als je optie kiest
            Console.Clear();
            Console.WriteLine("Dit is nog niks Frans en Anouk! ;)\n");
            Console.Write("Druk return/enter om terug te gaan naar het begin ");
            Console.ReadLine();
            break;
        
        case "terug": // als je terug kiest als je het fout hebt ingetypt
            Console.Clear();
            break;

        case "stoppen": // als je stoppen kiest als je wilt stoppen
            Console.Clear();
            Console.Write("Druk return/enter om terug te gaan naar het begin of typ stop als je wilt stoppen: ");
            stopString = Console.ReadLine();
            // checkt als je stopt of terug naar het beginscherm gaat.
            if (stopString == "stop")
            {
                stop = false;
                break;
            }
            else if (stopString != "stop")
            {
                Console.Clear();
                break;
            }
            break;

    {
		"Straat": "Wijnhaven, 107, 3011 WN",
		"Telefoonnummer": "010-5012345"
	},

	{
		"Menu": "Voorgerechten",
		"Gerecht 1": "Komt nog",
		"Gerecht 2": "Komt nog",
		"Gerecht 3": "Komt nog",
		"Gerecht 4": "Komt nog",
		"Gerecht 5": "Komt nog",
		"Gerecht 6": "Komt nog",
		"Gerecht 7": "Komt nog",
		"Gerecht 8": "Komt nog",
		"Gerecht 9": "Komt nog",
		"Gerecht 10": "Komt nog"

	},

	{
		"Menu": "Hoofdgerechten",
		"Gerecht 1": "Komt nog",
		"Gerecht 2": "Komt nog",
		"Gerecht 3": "Komt nog",
		"Gerecht 4": "Komt nog",
		"Gerecht 5": "Komt nog",
		"Gerecht 6": "Komt nog",
		"Gerecht 7": "Komt nog",
		"Gerecht 8": "Komt nog",
		"Gerecht 9": "Komt nog",
		"Gerecht 10": "Komt nog"

	},

	{
		"Menu": "Nagerechten",
		"Gerecht 1": "Komt nog",
		"Gerecht 2": "Komt nog",
		"Gerecht 3": "Komt nog",
		"Gerecht 4": "Komt nog",
		"Gerecht 5": "Komt nog",
		"Gerecht 6": "Komt nog",
		"Gerecht 7": "Komt nog",
		"Gerecht 8": "Komt nog",
		"Gerecht 9": "Komt nog",
		"Gerecht 10": "Komt nog"

	},

	{

		"Reservering 1": "Naam, telefoonnummer, datum, tijd",
		"Reservering 2": "Naam, telefoonnummer, datum, tijd",
		"Reservering 3": "Naam, telefoonnummer, datum, tijd",
		"Reservering 4": "Naam, telefoonnummer, datum, tijd",
		"Reservering 5": "Naam, telefoonnummer, datum, tijd",
		"Reservering 6": "Naam, telefoonnummer, datum, tijd",
		"Reservering 7": "Naam, telefoonnummer, datum, tijd",
		"Reservering 8": "Naam, telefoonnummer, datum, tijd",
		"Reservering 9": "Naam, telefoonnummer, datum, tijd",
		"Reservering 10": "Naam, telefoonnummer, datum, tijd"

	},

	{

		"Tafel 1": "Aantal personen, naam, tijd",
		"Tafel 2": "Aantal personen, naam, tijd",
		"Tafel 3": "Aantal personen, naam, tijd",
		"Tafel 4": "Aantal personen, naam, tijd",
		"Tafel 5": "Aantal personen, naam, tijd",
		"Tafel 6": "Aantal personen, naam, tijd",
		"Tafel 7": "Aantal personen, naam, tijd",
		"Tafel 8": "Aantal personen, naam, tijd",
		"Tafel 9": "Aantal personen, naam, tijd",
		"Tafel 10": "Aantal personen, naam, tijd"
	
	}
}
