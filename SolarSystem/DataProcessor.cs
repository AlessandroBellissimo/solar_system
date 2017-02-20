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
    public abstract class DataProcessor
    {
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
        protected void ReadData()
        {
            lines = File.ReadAllLines(path);
        }

        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        protected abstract void AddDataToList();

        /// <summary>
        /// Отображение данных
        /// </summary>
        protected abstract void DisplayData();

        /// <summary>
        /// Отображение информации о списке планет
        /// </summary>
        protected abstract void DisplayInformation();
    }

    /// <summary>
    /// Обработка файла со списком планет
    /// </summary>
    public class DataProcessorSystem:DataProcessor
    {
        private Sun sun;        // экземпляр класса Солнце
        private List<Planet> planets;    // список планет

        // конструктор класса - нужен для создания экземпляра
        // с нужными параметрами
        public DataProcessorSystem(string path):
            base(path)
        {}
        
        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        protected override void AddDataToList()
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
        /// Отображение данных
        /// </summary>
        protected override void DisplayData()
        {
            sun.Display();
            foreach (Planet p in planets)
                p.Display();
        }

        /// <summary>
        /// Отображение информации о списке планет
        /// </summary>
        protected override void DisplayInformation()
        {
            PlanetsListInfo list = new PlanetsListInfo(planets);
            Console.WriteLine("Maximal distance: {0} ({1})",
                list.PlanetWithMaxDistance().Distance, list.PlanetWithMaxDistance().Name);
            Console.WriteLine("Maximal mass: {0} ({1})",
                list.PlanetWithMaxMass().Mass, list.PlanetWithMaxMass().Name);
            Console.WriteLine("Minimal mass: {0} ({1})",
                list.PlanetWithMinMass().Mass, list.PlanetWithMinMass().Name);
            //Console.WriteLine("Maximal satellites number: {0} ({1})",
            //    list.PlanetWithMaxSatellites().Satellites, list.PlanetWithMaxSatellites().Name);
        }
    }

    /// <summary>
    /// Обработка файла со списком планет и их спутников
    /// </summary>
    public class DataProcessorSatellites : DataProcessor
    {
        private Planet planet;                  // экземпляр класса планета
        private List<Satellite> satellites;    // список спутников

        // конструктор класса - нужен для создания экземпляра
        // с нужными параметрами
        public DataProcessorSatellites(string path) :
            base(path)
        { }

        /// <summary>
        /// Добавление данных в список небесных тел
        /// </summary>
        protected override void AddDataToList()
        {
            int N = lines.Length;
            string[][] lineArray = new string[N][];

        }

        /// <summary>
        /// Отображение данных
        /// </summary>
        protected override void DisplayData()
        {

        }

        /// <summary>
        /// Отображение информации о списке планет
        /// </summary>
        protected override void DisplayInformation()
        {
            
        }
    }
}