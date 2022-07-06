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

namespace Riemann_sum
{

    public partial class MainWindow : Window
    {

        const int STEPS_AMOUNT = 100;
        const double THETA = 1f / 3;
        public struct Tuple
        {
            public double integral;
            public int steps;
            public double error;

        }

        #region Methods

        public MainWindow()
        {
            InitializeComponent();
        }

        private double Parse(string str, string errMsg)
        {
            double parsedNum;
            bool result = Double.TryParse(str, out parsedNum);
            if (!result)
            {
                MessageBox.Show(errMsg);
                return Double.NaN;
            }
            return parsedNum;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            //OutputTextBox.Text = "";
            double leftLimit = Parse(LeftLimitTextBox.Text.Replace(".", ","), "Проверьте левый предел.");
            double rightLimit = Parse(RightLimitTextBox.Text.Replace(".", ","), "Проверьте левый предел."); 
            if (leftLimit > rightLimit)
            {
                MessageBox.Show("Так как левый лимит больше правого, программа автоматические поменяет их местами.");
                LeftLimitTextBox.Text = rightLimit.ToString();
                RightLimitTextBox.Text = leftLimit.ToString();
                double temp = leftLimit;
                leftLimit = rightLimit;
                rightLimit = temp;
            }
            double accuracy = Parse(AccuracyTextBox.Text.Replace(".", ","), "Проверьте точность.");
            Tuple result = Calculate(leftLimit, rightLimit, accuracy);
            Print(result);
        }

        private Tuple Calculate(double a, double b, double precision)
        {
            if (a == b) {
                Tuple result;
                result.integral = 0;
                result.steps = 0;
                result.error = 0;
                return result;
            }
            if (double.IsInfinity(f(a)) || double.IsNaN(f(a)))
                a += double.Epsilon;
            if (double.IsInfinity(f(b)) || double.IsNaN(f(b)))
                b -= double.Epsilon;
            double integral_n, error = precision;
            double step = (b - a) / STEPS_AMOUNT;
            do
            {
                integral_n = SolveIntegral(a, b, step);
                double integral_2n = SolveIntegral(a, b, step / 2);
                error = THETA * Math.Abs(integral_2n - integral_n);
                if (error >= precision) step /= 2;

            } while (error >= precision);
            Tuple tuple;
            if (double.IsInfinity(integral_n) || double.IsNaN(integral_n))
            {
                tuple.integral = 0;
                tuple.steps = 0;
                tuple.error = 0;
                MessageBox.Show("Функция не определена на даннном отрезке.");
                return tuple;
            }
            tuple.integral = integral_n;
            tuple.steps = (int)((b- a) / step);
            tuple.error = error;
            return tuple;
        }

        private double SolveIntegral(double leftLimit, double rightLimit, double step)
        {
            double x, result = 0;
            for (x = leftLimit; x <= rightLimit; x += step)
                result += Formula(x, x + step);
            return result;
        }

        private double Formula(double a, double b)
        {
            switch (Modifications.SelectedIndex)
            {
                case 0:
                    return f(a) * (b - a);
                case 1:
                    return f(b) * (b - a);
                case 2:
                    return f((a + b) / 2) * (b - a);
            }
            return Double.NaN;
        }

        private double f(double xCoordinate)
        {
            switch (Functions.SelectedIndex)
            {
                case 0:
                    return Math.Sin(xCoordinate);
                case 1:
                    return Math.Log(xCoordinate);
                case 2:
                    return Math.Exp(xCoordinate);
            }
            return Double.NaN;
        }

        

        private void Print(Tuple tuple)
        {
            OutputTextBox.Text += "Значение интеграла: " + tuple.integral.ToString() + "\n";
            OutputTextBox.Text += "Количество разбиений: " + tuple.steps.ToString() + "\n";
            OutputTextBox.Text += "Значение погрешности: " + tuple.error.ToString() + "\n";
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            Functions.SelectedIndex = 0;
            Modifications.SelectedIndex = 0;
            LeftLimitTextBox.Text = "";
            RightLimitTextBox.Text = "";
            AccuracyTextBox.Text = "";
            OutputTextBox.Text = "";
        }

        #endregion

    }
}
