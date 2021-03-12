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

            chart.MinX = 1;
            chart.MaxX = 30;

            chart.MinY = 0;
            chart.MaxY = 2500;
            chart.AxisYDivider = 500;
            chart.AxisXDivider = 7;

            generatePoints();
        }

        public void generatePoints()
        {
            for (int i = 0; i < 30; i++)
            {
                chart.AddPoint(i, Math.Log(i+1)*500);
            }

        }
    }
}
