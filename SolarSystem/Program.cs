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
            // создаем экземпляр класса "обработчик данных", с параметром "путь к файлу"
            DataProcessor dp1 = new DataProcessorSystem("Data/SolarSystemInfo2.txt");
            dp1.GetInformation();
            DataProcessor dp2 = new DataProcessorSystem("Data/Satellites.txt");
            dp2.GetInformation();
        }
    }
}