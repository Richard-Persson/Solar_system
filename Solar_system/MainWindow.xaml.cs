using SpaceSim;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Solar_system
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int nr1 = 2000;
        public static int nr2 = 400;
        public MainWindow()
        {
            InitializeComponent();


            //Initialiserer Planeter
            Planet mercury = new Planet("Mercury", 2440, 57910, 88, 176);
            Planet venus = new Planet("Venus", 6051, 108200, 225, 243);
            Planet earth = new Planet("Earth", 6371 ,149600, 365, 1, new Moon("The moon", 384, 27, 30));
            Planet mars = new Planet("Mars", 3389, 227940, 687, 1);
            Planet jupiter = new Planet("Jupiter", 71_492, 778_330, 4333, 0.5);
            Planet saturn = new Planet("Saturn", 58_232, 1_429_400, 10_759, 0.5, new Moon("Pan", 134, (int)0.58, 23));
           
          
          



            Star  Sun = new Star("Sun");

            //Lager Planeter
            Ellipse sun = EllipseMaker(Sun);
            Ellipse merkur = EllipseMaker(mercury);
            Ellipse venus1 = EllipseMaker(venus);
            Ellipse jorden = EllipseMaker(earth);
            Ellipse mars1 = EllipseMaker(mars);
            Ellipse jupiter1 = EllipseMaker(jupiter);
            Ellipse saturn1  = EllipseMaker(saturn);
          
          
         


            //Lager planetene sin Orbit , TODO: gjør om dette til metoder
            Ellipse orbitEarth = OrbitMaker(earth);
            Ellipse orbitSaturn = OrbitMaker(saturn);
            Ellipse orbitMercury = OrbitMaker(mercury);




            //Legger til tegninger i Grid TODO: Iterer gjennom en liste istedenfor
            SolarSystem.Children.Add(sun);
            SolarSystem.Children.Add(merkur);
            SolarSystem.Children.Add(venus1);
            SolarSystem.Children.Add(jorden);
            SolarSystem.Children.Add(mars1);
            SolarSystem.Children.Add(jupiter1);
            SolarSystem.Children.Add(saturn1);
            SolarSystem.Children.Add(orbitEarth);
            SolarSystem.Children.Add(orbitSaturn);
            SolarSystem.Children.Add(orbitMercury);
          
           
         

           
        }

     

        //TODO Fikse skalering? 
        private Ellipse EllipseMaker( SpaceObject p)
        {
            Ellipse e = new Ellipse();

            double scale = SizeConverter(p.object_radius);

            int tid = 0;

            e.Height = scale;
            e.Width  = scale;
            e.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            if (p.GetType() == typeof(Planet))
            {

                if (p.orbital_radius > 1000_000)
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 2000, p.CalculatePosition(tid).Y / 2000, 0, 0);

                }

                else if (p.orbital_radius>500_000 && p.orbital_radius<1_000_000)
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 1200, p.CalculatePosition(tid).Y / 1200, 0, 0);
                }


                else
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 400, p.CalculatePosition(tid).Y / 400, 0, 0);
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

                    e.Fill = new SolidColorBrush(Color.FromRgb(128, 128, 6));
                    break;

                case "mercury":

                    e.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 200));
                    break;

                case "jupiter":

                    e.Fill = new SolidColorBrush(Color.FromRgb(100, 200, 200));
                    break;

            }
           

            return e;
        }

        private double SizeConverter(double radius){ return (  (radius / 8000) +20); }


        private Ellipse OrbitMaker(Planet planet)
        {
            int divide;

            divide = planet.orbital_radius > 1_000_000 ? nr1 : nr2;

            Ellipse e = new Ellipse();
            e.Stroke = new SolidColorBrush(Color.FromRgb(1, 1, 1));
            e.Height = planet.orbital_radius / divide;
            e.Width = planet.orbital_radius / divide;

            return e;
        }
    }
}