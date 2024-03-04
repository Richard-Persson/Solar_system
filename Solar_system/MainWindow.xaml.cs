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
        public MainWindow()
        {
            InitializeComponent();


            //Initialiserer Planeter
            Planet earth = new Planet("Earth", 6371 ,149600, 365, 1, new Moon("The moon", 384, 27, 30));
            Planet saturn = new Planet("Saturn", 58232, 1429400, 10759, (int)0.5, new Moon("Pan", 134, (int)0.58, 23));
            Planet mercury = new Planet("Mercury", 2440,57910, 88, 176);


            Star  Sun = new Star("Sun");

            //Lager Planeter
            Ellipse jorden = EllipseMaker(earth);
            Ellipse sun = EllipseMaker(Sun);
            Ellipse saturn1  = EllipseMaker(saturn);
            Ellipse merkur = EllipseMaker(mercury);


            //Lager planetene sin Orbit , TODO: gjør om dette til metoder
            Ellipse orbitEarth = new Ellipse();
            Ellipse orbitSaturn = new Ellipse();
            Ellipse orbitMercury = new Ellipse();

            orbitEarth.Stroke = new SolidColorBrush(Color.FromRgb(1, 1, 1));
            orbitEarth.Height = earth.orbital_radius/450;
            orbitEarth.Width = earth.orbital_radius/450;

            orbitSaturn.Stroke = new SolidColorBrush(Color.FromRgb(1, 1, 1));
            orbitSaturn.Height = saturn.orbital_radius / 2000;
            orbitSaturn.Width = saturn.orbital_radius / 2000;

            orbitMercury.Stroke = new SolidColorBrush(Color.FromRgb(1, 1, 1));
            orbitMercury.Height = mercury.orbital_radius / 450;
            orbitMercury.Width = mercury.orbital_radius / 450;



            //Legger til tegninger i Grid TODO: Iterer gjennom en liste istedenfor
            SolarSystem.Children.Add(sun);
            SolarSystem.Children.Add(jorden);
            SolarSystem.Children.Add(saturn1);
            SolarSystem.Children.Add(merkur);
            SolarSystem.Children.Add(orbitEarth);
            SolarSystem.Children.Add(orbitSaturn);
            SolarSystem.Children.Add(orbitMercury);
         

           
        }

     

        //TODO Fikse skalering? 
        private Ellipse EllipseMaker( SpaceObject p)
        {
            Ellipse e = new Ellipse();

            int scale = SizeConverter(p.object_radius);

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

                else
                {
                    e.Margin = new Thickness(p.CalculatePosition(tid).X / 450, p.CalculatePosition(tid).Y / 450, 0, 0);
                }

               
            }

            switch (p.name.ToLower())
            {
                case "sun":
                    e.Fill = new SolidColorBrush(Color.FromRgb(255,255,0));
                    break;

                case "earth":
                    e.Fill = new SolidColorBrush(Color.FromRgb(0, 128, 0));
                  
                    break;

                case "saturn":

                    e.Fill = new SolidColorBrush(Color.FromRgb(128, 128, 6));
                    break;

                case "mercury":

                    e.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 200));
                    break;

            }
           

            return e;
        }

        private int SizeConverter(int radius){ return (  (radius / 10000) +20); }
    }
}