using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIProjekt
{
    public partial class Ustawienia : Window
    {
        public AlgorytmGenetyczny AktualnyAlgorytm { get; set; }
        public Ustawienia()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Wczytaj_ok_Click(object sender, RoutedEventArgs e)
        {
            Rozmiar_populacji.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Max_rozmiar_populacji.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Rozmiar_turnieju.GetBindingExpression(Slider.ValueProperty).UpdateSource();
            Elitaryzm.GetBindingExpression(Slider.ValueProperty).UpdateSource();
            Prawd_krzyzowania.GetBindingExpression(Slider.ValueProperty).UpdateSource();
            Prawd_mutacji.GetBindingExpression(Slider.ValueProperty).UpdateSource();
            DialogResult = true;
            this.Close();
        }

        private void Wczytaj_anuluj_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
