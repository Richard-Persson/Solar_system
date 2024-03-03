using System;
using System.Collections.Generic;
using System.Numerics;
using SpaceSim;


class Astronomy
{
    public static void Main(string[] args)
    {
        List<SpaceObject> solarSystem = new List<SpaceObject>
        {
            new Star ("Sun"),

            new Planet("Mercury",57910,88,176), //(name, Distance to sun, orbital period, length of a day (earth days))
            new Planet("Venus",108200,224, 243 ),
            new Planet("Earth",149600,365,1,new Moon("The moon",384,27,30)),
            new Planet("Saturn",1429400,10759,(int)0.5,new Moon("Pan",134,(int)0.58,23)),

            new DwarfPlanet("Pluto",5913520,90550,6),
            new Moon("The moon",384,27,30),
            new AsteroidBelt("Ceres"),
            new Asteroid("Asteroid 1")
    
        };

        foreach (SpaceObject obj in solarSystem)
        {
               if(obj.GetType()==typeof(Planet))
                    obj.Draw();

        }


        string input = "";
        while (input != "q")
        {
            Console.WriteLine("\n Velg Planet:   ");
            string navn = Console.ReadLine();
            Console.WriteLine("Velg Tid: ");
            string tid = Console.ReadLine();

           


            foreach (SpaceObject obj in solarSystem)
            {

                if (obj.name.ToUpper() == navn.ToUpper())
                    obj.MoreInformation(Int32.Parse(tid));

            }

             Console.WriteLine("press q to quit else continue");
            input = Console.ReadLine();
        }
    }
}

