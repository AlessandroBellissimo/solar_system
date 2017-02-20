using System;
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
        protected string[] linesSystem;           // массив для считывания данных
        protected string[] linesSatellites;           // массив для считывания данных

        public string pathSystem { get; private set; }
        public string pathSatellites { get; private set; }

        // конструктор класса - нужен для создания экземпляра
        // с нужными параметрами
        public DataProcessor(string pathSystem,string pathSatellites)
        {
            this.pathSystem = pathSystem;
            this.pathSatellites = pathSatellites;
        }

        // метод получения данных
        public void GetInformation()
        {
            ReadData();                 // чтение данных
            AddDataToList();            // добавление данных в список
            AddSatellitesDataToList();
            DisplayData();              // отображение данных
            DisplayInformation();       // отображение информации о списке планет
        }

        /// <summary>
        /// Чтение данных из файла
        /// </summary>
        private void ReadData()
        {
            linesSystem = File.ReadAllLines(pathSystem);
            linesSatellites = File.ReadAllLines(pathSatellites);
        }

        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        private void AddDataToList()
        {
            int N = linesSystem.Length;
            string[][] lineArray = new string[N][];
            planets = new List<Planet>();

            for (int i = 0; i < N - 1; i++)
            {
                lineArray[i] = linesSystem[i + 1].Split();

                string name = lineArray[i][0];
                double mass = Double.Parse(lineArray[i][1]);
                double radius = Double.Parse(lineArray[i][2]);
                double distance = Double.Parse(lineArray[i][3]);
                int satNum = Int32.Parse(lineArray[i][4]);

                if (distance == 0)
                    sun = new Sun(name, mass, radius, satNum);
                else
                {
                    Planet p = new Planet(name, mass, radius, satNum, distance);
                    planets.Add(p);
                    sun.Satellites.Add(p);
                }
            }
        }

        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        private void AddSatellitesDataToList()
        {
            int N = linesSatellites.Length;
            string[][] lineArray = new string[N][];
            satNumbers = new List<int>();

            for (int i = 0; i < N; i++)
            {
                lineArray[i] = linesSatellites[i].Split();
            }
            
            for (int i = 0; i < planets.Count; i++)
            {
                List<Satellite> satellites = new List<Satellite>();
                for (int j = 1; j < N; j++)
                {
                    if (lineArray[j][i] != "")
                    {
                        Satellite s = new Satellite(lineArray[j][i], planets[i]);
                        planets[i].Satellites.Add(s);
                    }
                }
                satNumbers.Add(planets[i].Satellites.Count);
            }
        }
        /// <summary>
        /// Отображение исходных данных
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