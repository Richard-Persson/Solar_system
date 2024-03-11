using SpaceSim;
using System;
using System.ComponentModel;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Shell;
using System.Windows.Threading;
namespace Solar_system
{
    public partial class MainWindow : Window
    {
        public static int gr1 = 500; // Merrcury, Venus, Earth, 
        public static int gr2 = 600; // Mars
        public static int gr3 = 1200;// Jupiter
        public static int gr4 = 2750;// Saturn
        public static int gr5 = 3750;// Uranus
        public static int gr6 = 5000;// Pluto & Nepturne

        //Diverse globale variabler
        DispatcherTimer t = new();
        public static int tid = 0;
        static Boolean toggle = false;
        static int hastighet = 100;
     

        List<Ellipse> Planeter = new List<Ellipse>();
        List<Ellipse> orbit = new List<Ellipse>();


      
        //Initialiserer Planeter
        Star Sun = new Star("Sun",Color.FromRgb(255,255,0));
        Planet mercury = new Planet("Mercury", Color.FromRgb(244, 184, 119), 2440, 57_910, 88, 176);
        Planet venus = new Planet("Venus", Color.FromRgb(211, 138, 61), 6051, 108_200, 225, 243);
        Planet earth = new Planet("Earth", Color.FromRgb(0, 150, 0), 6371, 149_600, 365, 1, new Moon("The moon", Color.FromRgb(128, 128, 128), 1737, 384, 27, 30));
        Planet mars = new Planet("Mars", Color.FromRgb(255, 164, 27), 3389, 227_940, 687, 1);
        Planet jupiter = new Planet("Jupiter", Color.FromRgb(249, 225, 125), 71_492, 778_330, 4333, 0.5);
        Planet saturn = new Planet("Saturn", Color.FromRgb(251, 241, 172), 58_232, 1_429_400, 10_759, 0.5, new Moon("Pan", Color.FromRgb(255, 255, 0),1737, 134, (int)0.58, 23));
        Planet uranus = new Planet("Uranus", Color.FromRgb(172, 251, 241), 25_362, 2_870_990, 30_685, 0.7);
        Planet neptune = new Planet("Neptune", Color.FromRgb(0, 114, 254), 24_622, 4_504_300, 60_190, 0.65);
        Planet pluto = new Planet("Pluto", Color.FromRgb(250, 248, 232),  1_151, 5_913_520, 90_550, 0.65);
        Moon moon = new Moon("The moon", Color.FromRgb(128, 128, 128), 1737, 384, 27, 30);



        Ellipse m = new Ellipse();

        public MainWindow()
        {
            InitializeComponent();

            //Lager planetene sin Orbit
            orbit = new List<Ellipse> {
            OrbitMaker(mercury),
            OrbitMaker(venus),
            OrbitMaker(earth),
            OrbitMaker(mars),
            OrbitMaker(jupiter),
            OrbitMaker(saturn),
            OrbitMaker(uranus),
            OrbitMaker(neptune),
            OrbitMaker(pluto)
            };

            

            //Legger til tegninger i Grid 
            orbit.ForEach(orbit => { SolarSystem.Children.Add(orbit); });

            //Lager en enkelt planet for zoom
            planet.Children.Add(Single_Planet(earth));


            //Starter timer
            t.Tick += Timer_Tick;
            t.Interval = TimeSpan.FromMilliseconds(hastighet);
            t.Start();


        }

        private void Endre_Hastighet(int nyHastighet)
        {
            if (hastighet+nyHastighet > 0)
            {
                hastighet += nyHastighet;
            }
          t.Stop();
          t.Interval = TimeSpan.FromMilliseconds(hastighet);
          t.Start();

           
          hastighetTekst.Content = "Hastighet: " + hastighet.ToString();
          
        }


        //Oppdaterer posisjon på planeter
        public void Timer_Tick(object? sender, EventArgs e)
        {
         
            RemovePlanets(Planeter);
            RemoveMoon();
            tid++;
            CreatePlanets(Sun, mercury, venus, earth, mars, jupiter, saturn, uranus, neptune, pluto);
            CreateMoon(moon);
            dager.Content = "DAG: " + tid.ToString();
            
        }


        
        //Lager planetene og skalerer dem
        private Ellipse EllipseMaker( SpaceObject p)
        {
            Ellipse e = new Ellipse();

            double scale = SizeConverter(p.object_radius);

            e.Height = scale;
            e.Width  = scale;
            e.Stroke = new SolidColorBrush(Color.FromRgb(128,128,128));
            e.Fill = new SolidColorBrush(p.color);

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

            return e;
        }


        //Når man velger en planet for og zoome inn på TODO: Fikse denne sånn at månen drar rundt planeten
        private Ellipse Single_Planet(SpaceObject p)
        {
            Ellipse e = new Ellipse();

            e.Height = 400;
            e.Width = 400;
            e.Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            e.Fill = new SolidColorBrush(p.color);

            return e;
        }

        private double SizeConverter(double radius){ return (  (radius / 8000) +10); }


        //Gjør om Fra SpaceObject til Ellipse
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

            Planeter.ForEach(planet => { SolarSystem.Children.Add(planet);  });

        }
       
        //Lager måne til jorden
        private void CreateMoon(Moon moon)
        {
          
            m.Height = 100;
            m.Width = 100;
            m.Fill = new SolidColorBrush(moon.color);
            m.Margin = new Thickness(moon.CalculatePosition(tid).X + 200, moon.CalculatePosition(tid).Y + 200, 0, 0);
            planet.Children.Add(m);
        }

        //Fjerner måne
        private void RemoveMoon()
        {
            planet.Children.Remove(m);
        }

        //Fjerner alle planeter
        private void RemovePlanets(List<Ellipse> planeter)
        {
            planeter.ForEach(planet => SolarSystem.Children.Remove(planet));
        }



        //Lager banen til alle planetene
      static private Ellipse OrbitMaker(Planet planet)
        {
     
            int divide = planet.orbital_radius < 200_000 ? gr1 :
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

        //Toggle for orbit
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

        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            hastighetTekst.Content = sender.ToString();
        }

        //Øker hastighet
        private void Button_Click_Positive(object sender, RoutedEventArgs e)
        {
          
            hastighetTekst.Content = "Hastighet: " + hastighet.ToString();
            Endre_Hastighet(-10);
        }

        //Minker hastighet
        private void Button_Click_Negative(object sender, RoutedEventArgs e)
        {
          
            hastighetTekst.Content = "Hastighet: " + hastighet.ToString();
            Endre_Hastighet(10);
          
        }

        //Liste der man kan velge planeter og zoome inn på
        private void liste_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBox comboBox = sender as ComboBox;


            if (comboBox.SelectedItem != null)
            {

                string selectedPlanet = ((ComboBoxItem)comboBox.SelectedItem).Content.ToString();
          

                switch (selectedPlanet)
                {
                    case "Earth (Ikke ferdig)":
                        vb.Visibility = Visibility.Visible;
                        SolarSystem.Visibility = Visibility.Collapsed;
                        break;

                    case "SolarSystem":
                        vb.Visibility = Visibility.Collapsed;
                        SolarSystem.Visibility = Visibility.Visible;
                        break;


                }
            }
        }
    }
}