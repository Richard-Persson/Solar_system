using SpaceSim;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;

namespace Solar_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int gr1 = 500; // Merrcury, Venus, Earth, 
        public static int gr2 = 600; // Mars
        public static int gr3 = 1200;// Jupiter
        public static int gr4 = 2750;// Saturn
        public static int gr5 = 3750;// Uranus
        public static int gr6 = 5000;// Pluto & Nepturne

        public static int tid = 0;
        static Boolean toggle = false;

        List<Ellipse> Planeter = new List<Ellipse>();
        List<Ellipse> orbit = new List<Ellipse>();


        //Initialiserer Planeter
        Star Sun = new Star("Sun");
        Planet mercury = new Planet("Mercury", 2440, 57_910, 88, 176);
        Planet venus = new Planet("Venus", 6051, 108_200, 225, 243);
        Planet earth = new Planet("Earth", 6371, 149_600, 365, 1, new Moon("The moon", 384, 27, 30));
        Planet mars = new Planet("Mars", 3389, 227_940, 687, 1);
        Planet jupiter = new Planet("Jupiter", 71_492, 778_330, 4333, 0.5);
        Planet saturn = new Planet("Saturn", 58_232, 1_429_400, 10_759, 0.5, new Moon("Pan", 134, (int)0.58, 23));
        Planet uranus = new Planet("Uranus", 25_362, 2_870_990, 30_685, 0.7);
        Planet neptune = new Planet("Neptune", 24_622, 4_504_300, 60_190, 0.65);
        Planet pluto = new Planet("Pluto", 1_151, 5_913_520, 90_550, 0.65);


        public MainWindow()
        {
            InitializeComponent();


         



            //Lager planetene sin Orbit , TODO: gjør om dette til metoder og fikse NEPTUNE && PLUTO sin orbit

            orbit = new List<Ellipse> {
            OrbitMaker(mercury),
            OrbitMaker(venus),
            OrbitMaker(earth),
            OrbitMaker(mars),
            OrbitMaker(jupiter),
            OrbitMaker(saturn),
            OrbitMaker(uranus),
        };
        



            //Legger til tegninger i Grid TODO: Iterer gjennom en liste istedenfor
          orbit.ForEach(orbit => { SolarSystem.Children.Add(orbit); });


            DispatcherTimer t = new()
            {
                
                Interval = TimeSpan.FromMilliseconds(100)
            };

            t.Tick += Timer_Tick;

            t.Start();

        }


        public void Timer_Tick(object? sender, EventArgs e)
        {
            RemovePlanets(Planeter);
            tid++;
            CreatePlanets(Sun,mercury,venus,earth,mars,jupiter,saturn,uranus,neptune,pluto);
        }




        //TODO Fikse skalering? 
        private Ellipse EllipseMaker( SpaceObject p)
        {
            Ellipse e = new Ellipse();

            double scale = SizeConverter(p.object_radius);

            e.Height = scale;
            e.Width  = scale;
            e.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            if (p.GetType() == typeof(Planet))
            {

                //Pluto & Neptune
                if (p.orbital_radius > 3_000_000)
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 5000, p.CalculatePosition(tid).Y /5000, 0, 0);

                }
                //Uranus
                else if (p.orbital_radius > 2_000_000 && p.orbital_radius < 3_000_000)

                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 3750, p.CalculatePosition(tid).Y / 3750, 0, 0);

                }

                //Saturn
                else if (p.orbital_radius > 1_000_000 && p.orbital_radius<2_000_000)

                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 2750, p.CalculatePosition(tid).Y / 2750, 0, 0);

                }
                //Jupiter
                else if (p.orbital_radius>500_000 && p.orbital_radius<1_000_000)
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 1200, p.CalculatePosition(tid).Y / 1200, 0, 0);
                }
                //Mars
                else if (p.orbital_radius > 200_000 && p.orbital_radius < 500_000)
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 600, p.CalculatePosition(tid).Y / 600, 0, 0);
                }

                //Alle under Mars
                else
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 500, p.CalculatePosition(tid).Y / 500, 0, 0);
                }

               
            }

            switch (p.name.ToLower())
            {
                case "sun":
                    e.Fill = new SolidColorBrush(Color.FromRgb(255,255,0));
                    break;


                case "venus":

                    e.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;


                case "earth":
                    e.Fill = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                  
                    break;

                case "mars":

                    e.Fill = new SolidColorBrush(Color.FromRgb(255, 165, 0));
                    break;

                case "saturn":

                    e.Fill = new SolidColorBrush(Color.FromRgb(206, 184, 184));
                    break;

                case "mercury":

                    e.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 200));
                    break;

                case "jupiter":

                    e.Fill = new SolidColorBrush(Color.FromRgb(235, 243, 246));
                    break;

                case "uranus":

                    e.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;

                case "neptune":

                    e.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                    break;

                case "pluto":

                    e.Fill = new SolidColorBrush(Color.FromRgb(255, 241, 213));
                    break;


            }
           

            return e;
        }

        private double SizeConverter(double radius){ return (  (radius / 8000) +10); }


        private void CreatePlanets(Star Sun,Planet mercury, Planet venus, Planet earth, Planet mars, Planet jupiter,
                                   Planet saturn, Planet uranus, Planet neptune, Planet pluto) {
           

            Planeter = new List<Ellipse>
            {
           EllipseMaker(Sun),
           EllipseMaker(mercury),
           EllipseMaker(venus),
           EllipseMaker(earth),
           EllipseMaker(mars),
           EllipseMaker(jupiter),
           EllipseMaker(saturn),
           EllipseMaker(uranus),
           EllipseMaker(neptune),
           EllipseMaker(pluto),
        };

            Planeter.ForEach(planet => { SolarSystem.Children.Add(planet); });

        }

        private void RemovePlanets(List<Ellipse> planeter)
        {
            foreach (var planet in planeter)
            {
                SolarSystem.Children.Remove(planet);
            }
        }



      static private Ellipse OrbitMaker(Planet planet)
        {
            int divide;

            divide = planet.orbital_radius < 200_000 ? gr1 :
                       planet.orbital_radius < 500_000 ? gr2 :
                       planet.orbital_radius < 1_000_000 ? gr3 :
                       planet.orbital_radius < 2_000_000 ? gr4 :
                       planet.orbital_radius < 3_000_000 ? gr5 : gr6;

            Ellipse e = new Ellipse();
            e.Stroke = new SolidColorBrush(Color.FromRgb(1, 1, 1));
            e.Height = planet.orbital_radius / divide;
            e.Width = planet.orbital_radius / divide;

     
            return e;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!toggle)
            {
                orbit.ForEach(orbit => { SolarSystem.Children.Remove(orbit); });
                toggle = true;
            }
            else
            {
                orbit.ForEach(orbit => { SolarSystem.Children.Add(orbit); }); 
                toggle = false;
            }

        }
    }
}