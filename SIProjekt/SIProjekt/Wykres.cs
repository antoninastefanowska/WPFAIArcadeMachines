using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Configurations;

namespace SIProjekt
{
    public class Wykres
    {
        public ChartValues<Point> Wartosci { get; set; }
        public SeriesCollection Serie { get; set; }
        public Func<double, string> Formatowanie { get; set; }
        public double Zakres { get; set; }

        public Wykres()
        {
            Formatowanie = value => value.ToString();
            var mapper = Mappers.Xy<Point>();
            mapper.X(model => model.X);
            mapper.Y(model => model.Y);
            Charting.For<Point>(mapper); 
            Wartosci = new ChartValues<Point>();

            Serie = new SeriesCollection
            {
                new StepLineSeries
                {
                    Title = "Iteracja",
                    Values = Wartosci,
                    PointGeometrySize = 10,
                    PointForeground = Brushes.Blue
                }
            };
        }
    }
}
