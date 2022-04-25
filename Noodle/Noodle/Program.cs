using System;


namespace Noodle
{
    class Program
    {
        static void Main(string[] args)
        {
            Restaurant Noodle = new Restaurant();
            Console.OutputEncoding = System.Text.Encoding.Unicode; //zorgt voor het werken van euroteken
            Noodle.Run();
        }
    }
}
