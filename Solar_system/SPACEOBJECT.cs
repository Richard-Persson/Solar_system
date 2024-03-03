using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Markup;

namespace SpaceSim{
    public class SpaceObject
    {
        public string name { get; }
        public SpaceObject(String name) {

            this.name = name;
        }

        public virtual void Draw()
        {
            Console.WriteLine(name);
        }

        public virtual void MoreInformation(int tid)
        {
            
        }


    }


    public class Star : SpaceObject
    {
        private Point p = new Point(0, 0);
        private int time = 0;
        public Star(string name) : base(name) { }

        public Point P{ get { return p; } }
        public int Time { get { return time; } }
        public override void Draw() {
            Console.Write($"Star        : ");
            base.Draw();
        }


       
    }


    public class Planet : SpaceObject
    {

        //Distance to sun
        public int orbital_radius { get; set; }
        //How long around the sun
        public int orbital_period { get; }

        public int object_radius { get; set; }
        //Length of day
        public int rotational_period{ get; set; }

        public Moon moon { get; set; }
        private Color color { get; set; }

    public Planet(string name,int orbital_radius, int orbital_period,int rotational_period, Moon moon) : base(name) { 
            this.orbital_radius = orbital_radius;
            this.orbital_period = orbital_period;
            this.rotational_period = rotational_period;
            this.moon = moon;
        }


       public Planet( String name,int orbital_radius, int orbital_period,int rotational_period) : base(name)
        {
            this.orbital_radius = orbital_radius;
            this.orbital_period = orbital_period;
            this.rotational_period = rotational_period;
        }


    public Planet(string name)  : base(name) { }
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

        //TODO Tror beregningen fungerer nå
        private Point CalculatePosition(int tid)
        {

            double radians = (2 * Math.PI * tid) / orbital_period;

            double x = orbital_radius * Math.Cos(radians);

            if (moon != null)
                moon.orbital_radius = moon.orbital_radius + this.orbital_radius;

            double y = orbital_radius * Math.Sin(radians);
           

            return new Point((int)x, (int)y);
        }

    }
    
    public class Moon : Planet
    {
        public Moon(string name, int orbital_radius, int orbital_period, int rotational_period) :
               base(name, orbital_radius, orbital_period, rotational_period) {  }


        public  Point CalculatePosition(int tid)
        {

            double radians = (2 * Math.PI * tid) / orbital_period;

            double x = orbital_radius * Math.Cos(radians);
            double y = orbital_radius * Math.Sin(radians);


            return new Point((int)x,(int) y);
        }
      
    }

    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(string name, int orbital_radius, int orbital_period, int rotational_period):
                      base(name, orbital_radius, orbital_period, rotational_period) { }
        

        public override void Draw()
        {
            Console.Write($"DwarfPlanet : {name} \n");
           
        }

    }

    public class Comet : SpaceObject
    {
        public Comet(string name) : base (name) { }

        public override void Draw()
        {
            Console.Write("Comet        : ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(string name) : base(name) { }

        public override void Draw()
        {
            Console.Write("AsteroidBelt: ");
            base.Draw();
        }
    }


    public class Asteroid : AsteroidBelt
    {
        public Asteroid(string name) : base(name) { }

        public override void Draw()
        {
            Console.Write("Asteroid    : ");
            base.Draw();
        }
    }

}