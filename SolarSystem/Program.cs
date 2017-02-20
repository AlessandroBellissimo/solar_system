﻿using System;

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
            DataProcessor dp1 = new DataProcessor("Data/SolarSystemInfo2.txt","Data/Satellites.txt");
            dp1.GetInformation();
        }
    }
}