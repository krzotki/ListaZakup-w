using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ListaZakupów
{
    class ChartPoint
    {
        double X { get; set; }
        double Y { get; set; }

        string Color { get; set; }

        public ChartPoint(double x, double y, string color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void display(Chart chart)
        {
            Rectangle rect = new Rectangle();
            rect.Fill = (Brush)new BrushConverter().ConvertFromString(Color);
            rect.Width = 5;
            rect.Height = 5;

            double normalizedX = (X /(chart.MaxX - chart.MinX)) * chart.Width - rect.Width / 2;
            double normalizedY = chart.Height - rect.Height / 2 - (Y / (chart.MaxY - chart.MinY)) * chart.Height;

            Canvas.SetLeft(rect, normalizedX);
            Canvas.SetTop(rect, normalizedY);
            chart.Children.Add(rect);
        }
    }

    class Chart : Canvas
    {
        List<ChartPoint> Points = new List<ChartPoint>();
        public double MinX { get; set; }
        public double MinY { get; set; }

        public double MaxX { get; set; }
        public double MaxY { get; set; }

        public int AxisXDivider { get; set; }
        public int AxisYDivider { get; set; }

        private void draw()
        {
            Children.Clear();
            drawAxes();
            foreach (ChartPoint point in Points)
            {
                point.display(this);
            }
        }

        private void drawAxes()
        {
            double dX = (Width / (MaxX - MinX)) * AxisXDivider;
            for (int i = 0; i <= (MaxX - MinX) / AxisXDivider; i++)
            {
                Label labelX = new Label();
                labelX.Content = i * AxisXDivider + MinX;
                labelX.Height = 25;
                Canvas.SetLeft(labelX, dX * i);
                Canvas.SetTop(labelX, Height + 5);
                Children.Add(labelX);
            }

            double dY = (Height / (MaxY - MinY)) * AxisYDivider;
            for (int i = 0; i <= (MaxY - MinY) / AxisYDivider; i++)
            {
                Label labelY = new Label();
                labelY.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                labelY.Width = 50;
                labelY.Height = 25;
                labelY.Content = i * AxisYDivider + MinY;
                Canvas.SetLeft(labelY, -labelY.Width - 5);
                Canvas.SetTop(labelY, Height - labelY.Height / 2 - dY * i);
                Children.Add(labelY);
            }
        }

        public void AddPoint(double x, double y, string color = "#000000")
        {
            ChartPoint point = new ChartPoint(x, y, color);
            Points.Add(point);
            draw();
        }
    }
}
