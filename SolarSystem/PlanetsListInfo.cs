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

        public Planet PlanetWithMaxMass()
        {
            List<Planet> ps = planets;
            int N = ps.Capacity;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Mass.CompareTo(y.Mass);
            });
            return ps[N - 1];
        }

        public Planet PlanetWithMinMass()
        {
            List<Planet> ps = planets;
            int N = ps.Capacity;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Mass.CompareTo(y.Mass);
            });
            return ps[0];
        }

        public Planet PlanetWithMaxDistance()
        {
            List<Planet> ps = planets;
            int N = ps.Capacity;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Distance.CompareTo(y.Distance);
            });
            return ps[N - 1];
        }

        public Planet PlanetWithMaxSatellites()
        {
            List<Planet> ps = planets;
            int N = ps.Capacity;
            ps.Sort(delegate (Planet x, Planet y)
            {
                return x.Satellites.Count.CompareTo(y.Satellites.Count);
            });
            return ps[N - 1];
        }
    }
}