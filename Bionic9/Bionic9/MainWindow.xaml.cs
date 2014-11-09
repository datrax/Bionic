using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Bionic9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateExpression(object sender, RoutedEventArgs e)
        {
            string str = CheckInputString();
            if (str == "error")
            {
                MessageBoxResult result = MessageBox.Show("Try input again", "Error", MessageBoxButton.OK);
                return;
            }
            else
            {
                ExprCalculator expcalculat = new ExprCalculator(str);
                double calculateResult = expcalculat.Calculate();
                double numerat,denominat;
                MakeFraction(calculateResult, out numerat, out denominat);


                wholepart.Content = (int)calculateResult;
                if (numerat != 1 || denominat != 1)
                {
                    numerator.Content = numerat;

                    denominator.Content = denominat;

                    separator.Content = "-----";
                }
            }
        }
        private void MakeFraction(double calculateResult, out double num, out double den)
        {
            num = 1;
            den = 1;
            if (Math.Abs((int)calculateResult - calculateResult) > 0)
            {
                num = Math.Abs((int)calculateResult - calculateResult);
                int counter = 0;
                while (num - (int)num != 0 && counter < 3)
                {
                    counter++;
                    num *= 10;
                    den *= 10;

                }
                ReduceFraction(ref num, ref den);
            }
           
        }
        private void ReduceFraction(ref double num, ref double den)
        {
            for (int i = 1; i <= num; i++)
                if (num % i == 0 && den % i == 0)
                {
                    num /= i;
                    den /= i;
                }
            num = (int)num;
            den = (int)den;
        }

        private string CheckInputString()
        {
            string checkedString = input.Text;
            if (checkedString[0].Equals('*') || checkedString[0].Equals('/') ||
                checkedString[checkedString.Length - 1].Equals('*') || checkedString[checkedString.Length - 1].Equals('/'))
                return "error";
            for (int i = 0; i < checkedString.Length - 1; i++)
            {
                if ((checkedString[i] == '*' || checkedString[i] == '/' || checkedString[i] == '+' || checkedString[i] == '-')&&
                    (checkedString[i+1] == '*' || checkedString[i+1] == '/' || checkedString[i+1] == '+' || checkedString[i+1] == '-'))
                    return "error";
            }
            return checkedString;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            input.Text += ((Button)sender).Content;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            input.Text = "";
            wholepart.Content = "";
            numerator.Content = "";
            denominator.Content = "";
            separator.Content = "";
        }

        private void DeleteLatSymbol(object sender, RoutedEventArgs e)
        {
            if (input.Text.Length > 0)
                input.Text = input.Text.Substring(0, input.Text.Length - 1);
        }
    }
}
