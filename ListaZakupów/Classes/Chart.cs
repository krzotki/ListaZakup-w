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
            Ellipse circle = new Ellipse();
            circle.Fill = (Brush)new BrushConverter().ConvertFromString(Color);
            circle.Width = 5;
            circle.Height = 5;

            double normalizedX = ((X - chart.MinX) / (chart.MaxX - chart.MinX)) * chart.Width;
            double normalizedY = -(((Y - chart.MinY) / (chart.MaxY - chart.MinY)) * chart.Height - chart.Height);

            Canvas.SetLeft(circle, normalizedX);
            Canvas.SetTop(circle, normalizedY);
            chart.Children.Add(circle);
        }
    }

    class Chart : Canvas
    {
        List<ChartPoint> Points = new List<ChartPoint>();
        public double MinX { get; set; }
        public double MinY { get; set; }

        public double MaxX { get; set; }
        public double MaxY { get; set; }

        public double DeltaX { get; set; }
        public double DeltaY { get; set; }

        public double LabelToDeltaRatio { get; set; }

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
            double dX = (Width / (MaxX - MinX)) * DeltaX / LabelToDeltaRatio;
            for (int i = 0; i <= (MaxX - MinX) / DeltaX * LabelToDeltaRatio; i++)
            {
                Label labelX = new Label();
                labelX.Content = (i * DeltaX / LabelToDeltaRatio + MinX).ToString("0.0");
                labelX.Height = 25;
                Canvas.SetLeft(labelX, dX * i);
                Canvas.SetTop(labelX, Height + 5);
                Children.Add(labelX);
            }

            double dY = (Height / (MaxY - MinY)) * DeltaY / LabelToDeltaRatio;
            for (int i = 0; i <= (MaxY - MinY) / DeltaY * LabelToDeltaRatio; i++)
            {
                Label labelY = new Label();
                labelY.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                labelY.Width = 50;
                labelY.Height = 25;
                labelY.Content = (i * DeltaY / LabelToDeltaRatio + MinY).ToString("0.0");
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
