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
using System.Windows.Threading;

namespace canvas_sketch04
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Brush _stroke = Brushes.BlueViolet;
        List<Point> pointList;
        Point origin = new Point(0, 0);
        //int ticks = 0;
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            pointList = new List<Point>();
            pointList.Add(origin);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
            timer.Interval = TimeSpan.FromSeconds(0.2);              // Timer will tick evert second
            timer.Start();                              // Start the timer
        }

        void timer_Tick(object sender, EventArgs e)
        {
            mainCanvas.Children.Clear();
            timerStamp.Content = DateTime.Now.ToString();
           
            //if (pointList.Count < 45)

            // this 'ticks' variable didn't do what I wanted.  pointList.Count works much better
            // ====================
            // ticks += 1; 
            Point thisPoint = new Point(pointList.Count * 10, random.Next(0, 200));
            pointList.Add(thisPoint);
            pointStamp.Content = pointList.Count;
            xPoint.Content = "x:" + thisPoint.X.ToString();
            yPoint.Content = "y:" + thisPoint.Y.ToString();

            if (pointList.Count >= 44)
            {
                // remove first element
                pointList.RemoveAt(0);
                for (int i = 0; i < pointList.Count; i++)
                {
                    Point oldPoint = pointList[i];
                    Point newPoint = new Point(oldPoint.X - 10, oldPoint.Y);
                    pointList[i] = newPoint;
                }
            }


            // loop over list of points and draw lines
            for (int i = 1; i < pointList.Count; i++)
            {
                // TODO: look into using a polyline instead
                Polyline thisLine = new Polyline();
                thisLine.Points.Add(pointList[i - 1]);
                thisLine.Points.Add(pointList[i]);

                thisLine.Stroke = _stroke;
                thisLine.StrokeThickness = 2;
                mainCanvas.Children.Add(thisLine);

                // draw ellipses at points
                Ellipse el = new Ellipse();
                el.Width = 10;
                el.Height = 10;
                el.Stroke = Brushes.BlueViolet;
                el.StrokeThickness = 3;
                el.Fill = Brushes.Violet;
                //el.Margin = new Thickness(pointList[i].X, pointList[i].Y, 0, 0);
                Canvas.SetLeft(el, pointList[i].X - el.Width / 2);
                Canvas.SetTop(el, pointList[i].Y - el.Height / 2);
                mainCanvas.Children.Add(el);
            }
        }
    }
}
