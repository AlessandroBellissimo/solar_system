using System;

namespace SolarSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Program program = new Program();
            program.Run();
            Console.ReadKey();
        }

        private void Run()
        {
            DataProcessor dp1 = new DataProcessor("Data/SolarSystemInfo2.txt","Data/Satellites.txt");
            Console.WriteLine("Отобразить информацию о солнечной системе? (yes/no)");
            if (Console.ReadLine() == "yes")
            {
                dp1.DisplayInformation();
            }
        }
    }
}