using System;
using System.Collections.Generic;

namespace SolarSystem
{
    /// <summary>
    /// Абстрактное небесное тело
    /// у которого есть параметры
    /// название, масса и радиус
    /// </summary>
    public abstract class CelestialBody
    {
        public string Name { get; set; }
        public double Mass { get; set; }
        public double Radius { get; set; }

        public CelestialBody(string name, double mass, double radius)
        {
            Name = name;
            Mass = mass;
            Radius = radius;
        }

        /// <summary>
        /// Метод отображения информации об объекте
        /// </summary>
        public abstract void Display();
    }

    /// <summary>
    /// Солнце:
    /// имеет параметры
    /// название, масса и радиус
    /// + число спутников
    /// </summary>
    public class Sun : CelestialBody // двоеточие значит наследование от класса CelestialBody
    {
        public List<Planet> Satellites { get; set; }

        // конструктор для нужного построения экземпляра
        // двоеточие значит, что name, mass, radius наследуем от класса CelestialBody
        public Sun(string name, double mass, double radius, int satellites)
            : base(name, mass, radius)
        {
            Satellites = new List<Planet>();
        }

        /// <summary>
        /// Переопределенный метод
        /// отображения информации об объекте
        /// Переопределенный - в который добавили доп.информацию
        /// </summary>
        public override void Display()
        {
            Console.WriteLine("Star: {0}, mass: {1}, radius: {2}, satellites: {3}",
                this.Name, this.Mass, this.Radius, this.Satellites.Count);
        }
    }

    /// <summary>
    /// Планета
    /// имеет параметры
    /// название, масса и радиус
    /// + число спутников и расстояние от Солнца
    /// </summary>
    public class Planet : CelestialBody
    {
        public List<Satellite> Satellites { get; set; }
        public double Distance { get; set; }

        public Planet(string name, double mass, double radius,
            int satellites, double distance)
            : base(name, mass, radius)
        {
            Satellites = new List<Satellite>();
            Distance = distance;
        }

        /// <summary>
        /// Переопределенные метод
        /// отображения информации об объекте
        /// </summary>
        public override void Display()
        {
            Console.WriteLine("Planet: {0}, mass: {1}, radius: {2}, satellites: {3}, distance: {4}",
                this.Name, this.Mass, this.Radius, this.Satellites.Count, this.Distance);
        }
    }
    /// <summary>
    /// Спутник
    /// имеет параметры
    /// название, масса и радиус
    /// + планета, чьим спутником является
    /// </summary>
    public class Satellite : CelestialBody
    {
        public Planet SatelliteOf { get; set; }

        public Satellite(string name, double mass, double radius, Planet satelliteOf)
            : base(name, 1, 1)
        {
            satelliteOf = SatelliteOf;
        }

        /// <summary>
        /// Переопределенные метод
        /// отображения информации об объекте
        /// </summary>
        public override void Display()
        {
            Console.WriteLine("Satellite name: {0}, satellite of {1}",
                this.Name, this.SatelliteOf.Name);
        }
    }
}