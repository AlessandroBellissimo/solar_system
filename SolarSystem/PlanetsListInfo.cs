using System;
using System.Collections.Generic;

namespace SolarSystem
{
    /// <summary>
    /// Класс, в котором вычисляются задачи
    /// для списка планет
    /// Задачи:
    /// - найти максимально удаленную от солнца планету
    /// - найти самую тяжелую
    /// - самую легкую планету
    /// - планету с максимальным количеством спутников
    /// </summary>
    public class PlanetsListInfo
    {
        private List<Planet> planets;

        public PlanetsListInfo(List<Planet> planets)
        {
            this.planets = planets;
        }

        private Planet PlanetWithMaxMass()
        {
            List<Planet> ps = planets;
            int N = ps.Count;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Mass.CompareTo(y.Mass);
            });
            return ps[N - 1];
        }

        private Planet PlanetWithMinMass()
        {
            List<Planet> ps = planets;
            int N = ps.Count;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Mass.CompareTo(y.Mass);
            });
            return ps[0];
        }

        private Planet PlanetWithMaxDistance()
        {
            List<Planet> ps = planets;
            int N = ps.Count;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Distance.CompareTo(y.Distance);
            });
            return ps[N - 1];
        }

        private Planet PlanetWithMaxSatellites()
        {
            List<Planet> ps = planets;
            int N = ps.Count;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Satellites.Count.CompareTo(y.Satellites.Count);
            });
            return ps[N - 1];
        }

        /// <summary>
        /// Отображение информации о списке планет
        /// </summary>
        public void DisplayInformation()
        {
            Console.WriteLine("Maximal distance: {0} ({1})",
                PlanetWithMaxDistance().Distance, PlanetWithMaxDistance().Name);
            Console.WriteLine("Maximal mass: {0} ({1})",
                PlanetWithMaxMass().Mass, PlanetWithMaxMass().Name);
            Console.WriteLine("Minimal mass: {0} ({1})",
                PlanetWithMinMass().Mass, PlanetWithMinMass().Name);
            Console.WriteLine("Maximal satellites number: {0} ({1})",
                PlanetWithMaxSatellites().Satellites.Count, PlanetWithMaxSatellites().Name);
        }


        /// <summary>
        /// Отображение информации о списке спутников
        /// </summary>
        public void DisplaySatellites()
        {
            Planet planet = ChoosePlanet();
            Console.WriteLine("Спутники планеты {0}: ", planet.Name);
            for (int i = 0; i < planet.Satellites.Count; i++)
            {
                Console.WriteLine(planet.Satellites[i].Name);
            }
        }

        /// <summary>
        /// Добавление спутников к планете из списка
        /// </summary>
        public void AddSatellites()
        {
            Planet planet = ChoosePlanet();
            Console.WriteLine("Введите название нового спутника:");
            string name = Console.ReadLine();
            Satellite s = new Satellite(name, planet);
            planet.Satellites.Add(s);
            Console.WriteLine("Обновленный список спутников планеты {0}: ", planet.Name);
            for (int i = 0; i < planet.Satellites.Count; i++)
            {
                Console.WriteLine(planet.Satellites[i].Name);
            }
        }

        /// <summary>
        /// Удаление спутника планеты из списка
        /// </summary>
        public void RemoveSatellites()
        {
            Planet planet = ChoosePlanet();
            Console.WriteLine("Номер спутника, который нужно удалить:");
            int satNumber = int.Parse(Console.ReadLine());
            if (satNumber < 0 || satNumber >= planet.Satellites.Count)
            {
                satNumber = 0;
                Console.WriteLine("Спутника с таким номером нет в списке");
            }
            else
            {
                planet.Satellites.RemoveAt(satNumber);
                Console.WriteLine("Обновленный список спутников планеты {0}: ", planet.Name);
                for (int i = 0; i < planet.Satellites.Count; i++)
                {
                    Console.WriteLine(planet.Satellites[i].Name);
                }
            }
        }

        /// <summary>
        /// Сортировка спутников планеты из списка
        /// </summary>
        public void SortSatellites()
        {
            Planet planet = ChoosePlanet();
            planet.Satellites.Sort(delegate (Satellite x, Satellite y)
            {
                return x.Name.CompareTo(y.Name);
            });
            Console.WriteLine("Обновленный список спутников планеты {0}: ", planet.Name);
            for (int i = 0; i < planet.Satellites.Count; i++)
            {
                Console.WriteLine(planet.Satellites[i].Name);
            }
        }

        /// <summary>
        /// Алгоритм выбора планеты из списка
        /// </summary>
        /// <returns></returns>
        private Planet ChoosePlanet()
        {
            Console.WriteLine("Выберите планету из списка:");
            Planet planet = null;
            string pname;
            bool found = false;

            while (found != true)
            {
                pname = Console.ReadLine();
                int count = 0;
                foreach (Planet p in planets)
                {
                    if (pname != p.Name)
                    {
                        count++;
                        continue;
                    }
                    else
                    {
                        planet = p;
                        break;
                    }
                }
                if (count == planets.Count)
                {
                    Console.WriteLine("Такой планеты нет в списке. Выберите планету из списка");
                }
                else
                {
                    found = true;
                }
            }
            return planet;
        }
    }
}