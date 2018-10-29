using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Eq5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TextBox[] arrayBoxs = { CoeffA0, CoeffA1, CoeffA2, CoeffA3, CoeffA4, CoeffA5 };
            Label[] arrayLabels = { CoeffLabelA0, CoeffLabelA1, CoeffLabelA2, CoeffLabelA3, CoeffLabelA4, CoeffLabelA5 };
            for (int j = 0; j <= 5; j++)
            {
                arrayBoxs[j].Visibility = Visibility.Collapsed;
                arrayLabels[j].Visibility = Visibility.Collapsed;
            }

        }
        public void Pop_DiAnswers()
        {
            TextBlock[] answersFields = { DigitalRootX1, DigitalRootX2, DigitalRootX3, DigitalRootX4, DigitalRootX5 };
            var roots = Computing.Roots;
            var i = 0;
            foreach (var root in roots)
            {
                answersFields[i].Text = root;
                i++;
            }
        }
        public void Pop_AnAnswers()
        {
            TextBlock[] answersFields = { AnalizeRootX1, AnalizeRootX2, AnalizeRootX3, AnalizeRootX4, AnalizeRootX5 };
            var roots = Computing.Roots;
            var i = 0;
            foreach (var root in roots)
            {
                answersFields[i].Text = root;
                i++;
            }
        }

        private void GetSolveA_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Computing.Coefficients = new[]
                {
                    Convert.ToDouble(CoeffA0.Text), Convert.ToDouble(CoeffA1.Text), Convert.ToDouble(CoeffA2.Text),
                    Convert.ToDouble(CoeffA3.Text), Convert.ToDouble(CoeffA4.Text), Convert.ToDouble(CoeffA5.Text)
                };
                switch (Convert.ToByte(PowerComboBox.Text))
                {
                    case 2:
                        Computing.GetSolveSecondPowEq();
                        Pop_AnAnswers();
                        break;
                    case 3:
                        Computing.GetSolveThirdPowEq();
                        Pop_AnAnswers();
                        break;
                    case 4: 
                    case 5:
                        MessageBox.Show("Тут только численные решения :(");
                        break;
                }
            }

            catch (FormatException exception)
            {
                MessageBox.Show("Проверьте правильно ли введены степень и коэффициенты?");
            }
        }

        private void GetSolveD_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Computing.Coefficients = new[]
                {
                    Convert.ToDouble(CoeffA0.Text), Convert.ToDouble(CoeffA1.Text), Convert.ToDouble(CoeffA2.Text),
                    Convert.ToDouble(CoeffA3.Text), Convert.ToDouble(CoeffA4.Text), Convert.ToDouble(CoeffA5.Text)
                };
                Computing.GetIntervals(Convert.ToDouble(IntervalA.Text), Convert.ToDouble(IntervalB.Text));
                Computing.HalfDivision(Convert.ToDouble(Accurate.Text));
            }
            catch (FormatException exception)
            {
                MessageBox.Show("Ошибка ввода.");
            }
            Pop_DiAnswers();
        }

        private void GetGraph_OnClick(object sender, RoutedEventArgs e)
        {
            var power = Convert.ToByte(PowerComboBox.Text);
            TextBox[] textBoxs = {CoeffA0, CoeffA1, CoeffA2, CoeffA3, CoeffA4, CoeffA5};
            List<double> A = new List<double>();
            for (int k = 0; k <= power; k++)
            {
                A.Add(Convert.ToDouble(textBoxs[k].Text));
            }
            Computing T = new Computing(A,power);
            var newModel = new PlotModel();
            double xMin, xMax, yMin, yMax;
            xMax = Convert.ToDouble(XMAX.Text);
            xMin = Convert.ToDouble(XMIN.Text);
            yMax = Convert.ToDouble(YMAX.Text);
            yMin = Convert.ToDouble(YMIN.Text);
            newModel.Axes.Clear();
            newModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                FilterMaxValue = xMax,
                FilterMinValue = xMin
            });
            newModel.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                PositionAtZeroCrossing = true,
                AxislineStyle = LineStyle.Solid,
                FilterMaxValue = yMax,
                FilterMinValue = yMin
            });
            newModel.Series.Add(new FunctionSeries(T.FxP,xMin,xMax,0.1));
            PlotView.Model = newModel;
        }
        
        private void PowerComboBox_OnDropDownClosed(object sender, EventArgs e)
        {
            TextBox[] arrayBoxs = { CoeffA0, CoeffA1, CoeffA2, CoeffA3, CoeffA4, CoeffA5 };
            Label[] arrayLabels = { CoeffLabelA0, CoeffLabelA1, CoeffLabelA2, CoeffLabelA3, CoeffLabelA4, CoeffLabelA5 };
            int power;
            try
            {
                power = Convert.ToInt16(PowerComboBox.Text);
            }
            catch (FormatException exception)
            {
                Console.WriteLine(exception);
                power = 0;
            }
            switch (power)
            {
                case 0:
                    ImagesBox.Source = new BitmapImage(new Uri("Images/PowerEmpty.png", UriKind.Relative));
                    break;
                case 1:
                    ImagesBox.Source = new BitmapImage(new Uri("Images/Power1.png", UriKind.Relative));
                    break;
                case 2:
                    ImagesBox.Source = new BitmapImage(new Uri("Images/Power2.png", UriKind.Relative));
                    break;
                case 3:
                    ImagesBox.Source = new BitmapImage(new Uri("Images/Power3.png", UriKind.Relative));
                    break;
                case 4:
                    ImagesBox.Source = new BitmapImage(new Uri("Images/Power4.png", UriKind.Relative));
                    break;
                case 5:
                    ImagesBox.Source = new BitmapImage(new Uri("Images/Power5.png", UriKind.Relative));
                    break;
            }

            if (power != 0)
                for (int j = 0; j <= 5; j++)
                {
                    if (j <= power)
                    {
                        arrayBoxs[j].Visibility = Visibility.Visible;
                        arrayLabels[j].Visibility = Visibility.Visible;
                    }

                    if (j > power)
                    {
                        arrayBoxs[j].Visibility = Visibility.Collapsed;
                        arrayBoxs[j].Text ="0";
                        arrayLabels[j].Visibility = Visibility.Collapsed;
                    }
                }
        }
    }
    public class MainViewModel
    {
        public MainViewModel()
        {
            this.MyModel = new PlotModel { Title = "Example 1" };
            this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel MyModel { get; private set; }
    }
}
