﻿using System;
using System.Collections.Generic;
using System.IO;

namespace SolarSystem
{
    /// <summary>
    /// Класс, который обрабатывает
    /// исходные данные:
    /// - считывает
    /// - преобразует в нужный формат
    /// - отображает на экран
    /// </summary>
    public class DataProcessor
    {
        private Sun sun;                    // экземпляр класса Солнце
        private List<Planet> planets;       // список планет
        private List<int> satNumbers;      //  число спутников для каждой планеты
        public string path { get; private set; }
        protected string[] lines; // массив для считывания данных

        // конструктор класса - нужен для создания экземпляра
        // с нужными параметрами
        public DataProcessor(string path)
        {
            this.path = path;
        }

        // метод получения данных
        public void GetInformation()
        {
            ReadData();         // чтение данных
            AddDataToList();   // добавление данных в список
            DisplayData();      // отображение данных
            DisplayInformation();   // отображение информации о списке планет
        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        private void ReadData()
        {
            lines = File.ReadAllLines(path);
        }

        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        private void AddDataToList()
        {
            int N = lines.Length;
            string[][] lineArray = new string[N][];
            planets = new List<Planet>();

            for (int i = 0; i < N - 1; i++)
            {
                lineArray[i] = lines[i + 1].Split();

                string name = lineArray[i][0];
                double mass = Double.Parse(lineArray[i][1]);
                double radius = Double.Parse(lineArray[i][2]);
                double distance = Double.Parse(lineArray[i][3]);
                int satNum = Int32.Parse(lineArray[i][4]);

                if (distance == 0)
                    sun = new Sun(name, mass, radius, satNum);
                else
                    planets.Add(new Planet(name, mass, radius, satNum, distance));
            }
        }

        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        private void AddSatellitesDataToList()
        {
            int N = lines.Length;
            string[][] lineArray = new string[N][];
            planets = new List<Planet>();
            satNumbers = new List<int>();

            for (int i = 0; i < N; i++)
            {
                lineArray[i] = lines[i].Split();
            }

            for (int i = 0; i < lineArray[0].Length; i++)
            {
                planets.Add(new Planet(lineArray[0][i]));
            }


            // создаем 8 списков - для каждой планеты
            // заполняем каждый список
            // читаем 

            for (int i = 0; i < planets.Count; i++)
            {
                List<Satellite> satellites = new List<Satellite>();
                Console.WriteLine("Спутники планеты {0}:", planets[i].Name);
                for (int j = 1; j < N; j++)
                {
                    if (lineArray[j][i] != "")
                    {
                        Satellite s = new Satellite(lineArray[j][i], planets[i]);
                        planets[i].Satellites.Add(s);
                        Console.WriteLine("{0} ", s.Name);
                    }
                }
                satNumbers.Add(planets[i].Satellites.Count);
                Console.WriteLine("Число спутников: {0} ", planets[i].Satellites.Count);
            }
        }
        /// <summary>
        /// Отображение данных
        /// </summary>
        private void DisplayData()
        {
            sun.Display();
            foreach (Planet p in planets)
                p.Display();
        }

        /// <summary>
        /// Отображение информации о списке планет
        /// </summary>
        private void DisplayInformation()
        {
            PlanetsListInfo list = new PlanetsListInfo(planets);
            Console.WriteLine("Maximal distance: {0} ({1})",
                list.PlanetWithMaxDistance().Distance, list.PlanetWithMaxDistance().Name);
            Console.WriteLine("Maximal mass: {0} ({1})",
                list.PlanetWithMaxMass().Mass, list.PlanetWithMaxMass().Name);
            Console.WriteLine("Minimal mass: {0} ({1})",
                list.PlanetWithMinMass().Mass, list.PlanetWithMinMass().Name);
            Console.WriteLine("Maximal satellites number: {0} ({1})",
                list.PlanetWithMaxSatellites().Satellites.Count, list.PlanetWithMaxSatellites().Name);
        }
    }
}