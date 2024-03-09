using System;
using System.Security.Cryptography;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows;
namespace SpaceSim{
    public class SpaceObject
    {

        //Distance to sun
        public int orbital_radius { get; set; }
        //How long around the sun
        public int orbital_period { get; set; }

        public double object_radius { get; set; }
        //Length of day
        public double rotational_period { get; set; }

        public Moon moon { get; set; }
        public Color color { get; set; }


        public string name { get; }
        public SpaceObject(String name, Color color) {

            this.name = name;
           this.color = color;
        }

        public virtual void Draw()
        {
            Console.WriteLine(name);
        }

        public virtual void MoreInformation(int tid)
        {
            
        }

        public Point CalculatePosition(int tid)
        {

            double radians = (2 * Math.PI * tid) / orbital_period;

            double x = orbital_radius * Math.Cos(radians);

            if (moon != null)
                moon.orbital_radius = moon.orbital_radius + this.orbital_radius;

            double y = orbital_radius * Math.Sin(radians);


            return new Point((int)x, (int)y);
        }

    }

    public class Star : SpaceObject
    {
        private Point p = new Point(0, 0);
        private int time = 0;
        public int object_radius {  get; set; }
        public Star(string name, Color color) : base(name,color) { base.object_radius = 696340; }

        public Point P{ get { return p; } }
        public int Time { get { return time; } }
        public override void Draw() {
            Console.Write($"Star        : ");
            base.Draw();
        }


       
    }


    public class Planet : SpaceObject
    {
        public int object_radius { get; set; }

        public Planet(string name,Color color, int object_radius,int orbital_radius, int orbital_period,double rotational_period, Moon moon) : base(name,color) {
          
            base.object_radius=object_radius;
            base.orbital_radius = orbital_radius;
            base.orbital_period = orbital_period;
            base.rotational_period = rotational_period;
            base.moon = moon;

        }


       public Planet( string name, Color color, int object_radius,int orbital_radius, int orbital_period,double rotational_period) : base(name,color)
        {
            base.object_radius =object_radius;
            base.orbital_radius = orbital_radius;
            base.orbital_period = orbital_period;
            base.rotational_period = rotational_period;
            base.color = color;
        }


    public Planet(string name, Color color)  : base(name,color) { }
        public override void Draw() {

            Console.Write($"Planet      : " );
            base.Draw();
          
        }

        public override void MoreInformation(int tid)
        {
            if (moon != null)
            {
                Console.WriteLine($"Name: {name}  Position {CalculatePosition(tid)}  tid = {tid}  \n" +
                    $"Moons: {moon.name} | {moon.CalculatePosition(tid)}");
            }
            else
            {

                Console.WriteLine($"Name: {name} poistion {CalculatePosition(tid)}");
            }
        }


    }
    
    public class Moon : Planet
    {
        public Moon(string name, Color color,int orbital_radius, int orbital_period, int rotational_period) :
               base(name, color) {  }

      
    }

    public class DwarfPlanet : Planet
    {
        public int object_radius { get; set; }
        public DwarfPlanet(string name, Color color,int object_radius ,int orbital_radius, int orbital_period, double  rotational_period):  base(name, color) {
            base.object_radius = object_radius;
            base.orbital_radius = orbital_radius;
            base.orbital_period = orbital_period;
            base.rotational_period = rotational_period;
            base.color = color;
        }
        

        public override void Draw()
        {
            Console.Write($"DwarfPlanet : {name} \n");
           
        }

    }

    public class Comet : SpaceObject
    {
        public Comet(string name, Color color) : base (name,color) { }

        public override void Draw()
        {
            Console.Write("Comet        : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(string name, Color color) : base(name,color) { }

        public override void Draw()
        {
            Console.Write("AsteroidBelt: ");
            base.Draw();
        }
    }


    public class Asteroid : AsteroidBelt
    {
        public Asteroid(string name, Color color) : base(name, color) { }

        public override void Draw()
        {
            Console.Write("Asteroid    : ");
            base.Draw();
        }
    }

}