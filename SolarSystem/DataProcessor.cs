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
        private Sun sun;                        // экземпляр класса Солнце
        private List<Planet> planets;           // список планет
        private List<int> satNumbers;           //  число спутников для каждой планеты
        private PlanetsListInfo planetListInfo;
        protected string[] linesSystem;           // массив для считывания данных
        protected string[] linesSatellites;           // массив для считывания данных

        public string pathSystem { get; private set; }
        public string pathSatellites { get; private set; }

        // конструктор класса - нужен для создания экземпляра
        // с нужными параметрами
        public DataProcessor(string pathSystem, string pathSatellites)
        {
            this.pathSystem = pathSystem;
            this.pathSatellites = pathSatellites;
            Init();
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        private void Init()
        {
            planets = new List<Planet>();
            satNumbers = new List<int>();
            planetListInfo = new PlanetsListInfo(planets);
            ReadData();                 // чтение данных
            AddDataToList();            // добавление данных в список
            AddSatellitesDataToList();
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
        /// Отображение информации о списке планет (planetListInfo)
        /// </summary>
        public void DisplayInformation()
        {
            DisplaySolarSystem();

            Console.WriteLine("Отобразить информацию о планетах? (yes/no)");
            if (Console.ReadLine() == "yes")
                planetListInfo.DisplayInformation();

            Console.WriteLine("Отобразить спутники планеты? (yes/no)");
            if (Console.ReadLine() == "yes")
                planetListInfo.DisplaySatellites();

            Console.WriteLine("Добавить спутник планеты? (yes/no)");
            if (Console.ReadLine() == "yes")
                planetListInfo.AddSatellites();

            Console.WriteLine("Удалить спутник планеты? (yes/no)");
            if (Console.ReadLine() == "yes")
                planetListInfo.RemoveSatellites();

            Console.WriteLine("Отсортировать спутники планеты? (yes/no)");
            if (Console.ReadLine() == "yes")
                planetListInfo.SortSatellites();
        }

        /// <summary>
        /// Отображение исходных данных
        /// </summary>
        private void DisplaySolarSystem()
        {
            sun.Display();
            foreach (Planet p in planets)
                p.Display();
        }

    }
}