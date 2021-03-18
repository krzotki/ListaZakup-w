using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace ListaZakupów
{
    /// <summary>
    /// Logika interakcji dla klasy CaloriesWindow.xaml
    /// </summary>
    public partial class CaloriesWindow : UserControl
    {
        public CaloriesWindow()
        {
            InitializeComponent();

            //drawCalories();
            testSin();
        }

        public void testSin()
        {
            chart.MinX = -Math.PI;
            chart.MaxX = Math.PI * 2;

            chart.MinY = -2;
            chart.MaxY = 2;
            chart.DeltaY = 0.2;
            chart.DeltaX = 0.1;

            chart.LabelToDeltaRatio = 0.25;

            for (double i = chart.MinX; i <= chart.MaxX; i+=chart.DeltaX)
            {
                chart.AddPoint(i, Math.Sin(i));
            }

        }

        public void drawCalories()
        {
            chart.MinX = 0;
            chart.MaxX = 30;

            chart.MinY = 0;
            chart.MaxY = 2500;
            chart.DeltaY = 100;
            chart.DeltaX = 1;

            chart.LabelToDeltaRatio = 0.25;

            for (int i = 0; i < 30; i++)
            {
                CaloriesData data = CaloriesData.AllData[i];
                chart.AddPoint(i, data.Calories);
            }
        }
    }
}
