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
        public double X { get; set; }
        public double Y { get; set; }

        string Color { get; set; }

        public ChartPoint(double x, double y, string color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public void display(Chart chart, ChartPoint previous)
        {
            Ellipse circle = new Ellipse();
            circle.Fill = (Brush)new BrushConverter().ConvertFromString(Color);
            circle.Width = 8;
            circle.Height = 8;
            circle.ToolTip = string.Format("X: {0}, Y: {1}", X, Y);
            Canvas.SetZIndex(circle, 1);

            double normalizedX = ((X - chart.MinX) / (chart.MaxX - chart.MinX)) * chart.Width;
            double normalizedY = -(((Y - chart.MinY) / (chart.MaxY - chart.MinY)) * chart.Height - chart.Height);

            Canvas.SetLeft(circle, normalizedX - circle.Width / 2);
            Canvas.SetTop(circle, normalizedY - circle.Height / 2);

            if (previous != null)
            { 
                double previousX = ((previous.X - chart.MinX) / (chart.MaxX - chart.MinX)) * chart.Width;
                double previousY = -(((previous.Y - chart.MinY) / (chart.MaxY - chart.MinY)) * chart.Height - chart.Height);

                Line line = lineBetweenPoints(previousX, normalizedX, previousY, normalizedY);
                chart.Children.Add(line);
            }

            chart.Children.Add(circle);
        }

        private Line lineBetweenPoints(double X1, double X2, double Y1, double Y2)
        {
            Line line = new Line();

            line.X1 = X1;
            line.X2 = X2;

            line.Y1 = Y1;
            line.Y2 = Y2;

            line.Stroke = (Brush)new BrushConverter().ConvertFromString("#EE7799");
            line.StrokeThickness = 2;

            Canvas.SetZIndex(line, 0);

            return line;
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

        public string FormatX { get; set; }
        public string FormatY { get; set; }

        public double LabelToDeltaRatio { get; set; }

        public Chart() 
        {
            FormatX = "0.00";
            FormatY = "0.00";
            LabelToDeltaRatio = 1;
        }

        private void drawGrid()
        {
            DrawingBrush grid = new DrawingBrush();
            {
                double normalizedX = (Width / (MaxX - MinX)) * DeltaX / LabelToDeltaRatio;
                double normalizedY = (Height / (MaxY - MinY)) * DeltaY / LabelToDeltaRatio;

                grid.TileMode = TileMode.Tile;
                grid.Viewport = new System.Windows.Rect(0, Height % normalizedY, normalizedX, normalizedY);
                grid.ViewportUnits = BrushMappingMode.Absolute;
                {
                    GeometryDrawing geometryDrawing = new GeometryDrawing();
                    geometryDrawing.Geometry = new RectangleGeometry(new System.Windows.Rect(0, 0, normalizedX, normalizedY));

                    Pen pen = new Pen();
                    pen.Brush = (Brush)new BrushConverter().ConvertFromString("#50505055");
                    pen.Thickness = 1;
                    geometryDrawing.Pen = pen;

                    grid.Drawing = geometryDrawing;
                }

            }
            Background = grid;
        }

        public void draw()
        {
            drawGrid();
            Children.Clear();
            drawAxes();

            ChartPoint previous = null;

            foreach (ChartPoint point in Points)
            {
                point.display(this, previous);
                previous = point;
            }
        }

        private void drawAxes()
        {
            double dX = (Width / (MaxX - MinX)) * DeltaX / LabelToDeltaRatio;
            for (int i = 0; i <= (MaxX - MinX) / DeltaX * LabelToDeltaRatio; i++)
            {
                Label labelX = new Label();
                labelX.Content = (i * DeltaX / LabelToDeltaRatio + MinX).ToString(FormatX);
                labelX.Height = 25;
                labelX.Width = 25;
                labelX.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                Canvas.SetLeft(labelX, dX * i - labelX.Width / 2);
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
                labelY.Content = (i * DeltaY / LabelToDeltaRatio + MinY).ToString(FormatY);
                Canvas.SetLeft(labelY, -labelY.Width - 5);
                Canvas.SetTop(labelY, Height - labelY.Height / 2 - dY * i);
                Children.Add(labelY);
            }
        }

        public void AddPoint(double x, double y, string color = "#000000")
        {
            ChartPoint point = new ChartPoint(x, y, color);
            Points.Add(point);
        }
    }
}
