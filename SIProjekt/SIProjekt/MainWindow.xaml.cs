using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SIProjekt
{
    public partial class MainWindow : Window
    {

        private RaportWindow oknoRaportu;
        private AlgorytmGenetyczny ag;

        public MainWindow()
        {
            InitializeComponent();
        }

        /* do okna z raportem */
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ag = new AlgorytmGenetyczny(5, 100, 1, 3, 80, 10);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            Wykres wykres = new Wykres();

            /*
            for (int i = 0; i < 2000; i++)
                ZaktualizujWykres(wykres); 
            */

            oknoRaportu = new RaportWindow();
            oknoRaportu.Owner = this;
            oknoRaportu.AktualnyWykres = wykres;
            oknoRaportu.Show();

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            ZaktualizujWykres(oknoRaportu.AktualnyWykres);
        }

        private void ZaktualizujWykres(Wykres wykres)
        {
            //Console.WriteLine(ag.NajlepszyOsobnik().ToString());
            int x, y, n = wykres.Wartosci.Count;
            Point nowy, ostatni, przedostatni;
            x = ag.NumerIteracji;
            y = ag.NajlepszyOsobnik().Przystosowanie;
            nowy = new Point(x, y);

            wykres.Wartosci.Add(nowy);

            if (n > 0)
            {
                ostatni = wykres.Wartosci[n - 1];
                przedostatni = wykres.Wartosci[n - 2];

                wykres.Zakres = x;
                if (nowy.Y == ostatni.Y && ostatni.Y == przedostatni.Y)
                    wykres.Wartosci.Remove(ostatni);

                n = wykres.Wartosci.Count;
            }
            else wykres.Wartosci.Add(nowy);

            ag.Iteracja();
        }
    }
}
