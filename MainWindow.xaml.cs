using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Ueb23a_Grafik_Maus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<double> reactionTimes = new List<double>();
        private Stopwatch timer = new Stopwatch();
        private int circleCounter = 0;
        private Ellipse circle = new Ellipse();
        private Brush filling = Brushes.White;

        public MainWindow()
        {
            InitializeComponent();

            circle.PreviewMouseDown += circle_PreviewMouseDown;

            CircleSpawner();

            Thread.Sleep(1000);
        }

        public void CircleSpawner()
        {
            // Drawing circle
            circle.Width = 50;
            circle.Height = 50;
            circle.Fill = filling;

            // Setting circle on random position
            Canvas.SetLeft(circle, RandomPosition(0, 733));
            Canvas.SetTop(circle, RandomPosition(0,361));


            // Display circle on canvas
            myCanvas.Children.Add(circle);

            // Start stopwatch
            timer.Start();
        }

        private void circle_PreviewMouseDown(object sender, RoutedEventArgs e)
        {
            circleCounter++;
            e.Handled = true;

            myCanvas.Children.Remove(circle);

            timer.Stop();

            TimeSpan ts = timer.Elapsed;

            reactionTimes.Add(ts.TotalSeconds);

            if (circleCounter < 10)
            {
                timer.Reset();
                CircleSpawner();
            }
            else
            {
                AverageReactionTime();
            }
        }

        public static double RandomPosition(double min, double max)
        {
            Random random = new Random();
            return (random.NextDouble() * (max - min)) + min;
        }

        public void AverageReactionTime()
        {
            string sAvg = "Reaction time average: ";
            double rTimes = reactionTimes.Sum();
            double dAvg = Math.Round(rTimes / reactionTimes.Count, 3);
            if (dAvg < 1)
            {
                sAvg += (dAvg * 1000) + " milliseconds";
                lblreactionTime.Content = sAvg;
            }
            else
            {
                sAvg += dAvg + " seconds";
                lblreactionTime.Content = sAvg;
            }
        }

    }
}