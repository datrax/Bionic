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

            try
            {
                ExprCalculator expcalculat = new ExprCalculator(CheckInputString(input.Text));
                double calculateResult = expcalculat.Calculate();
                double numerat, denominat;
                MakeFraction(calculateResult, out numerat, out denominat);


                wholepart.Content = (int)calculateResult;
                if (numerat != 1 || denominat != 1)
                {
                    numerator.Content = numerat;

                    denominator.Content = denominat;

                    separator.Content = "-----";
                }

            }
            catch (Exception ex)
            {
                ErrorBlock.Text = ex.Message;
                wholepart.Content = "";
                numerator.Content = "";
                denominator.Content = "";
                separator.Content = "";
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
            num = (int)num;
            den = (int)den;
            for (int i = (int)num; i >= 1; i--)
                if (num % i == 0 && den % i == 0)
                {
                    num /= i;
                    den /= i;
                }
           
        }

        private string CheckInputString(string checkedString)
        {
            try
            {

                if (checkedString[0].Equals('*') || checkedString[0].Equals('/') ||
                    checkedString[checkedString.Length - 1].Equals('*') || checkedString[checkedString.Length - 1].Equals('/'))
                    throw new Exception("Wrong input format");


                for (int i = 0; i < checkedString.Length - 1; i++)
                {
                    if ((checkedString[i] == '*' || checkedString[i] == '/' || checkedString[i] == '+' || checkedString[i] == '-') &&
                        (checkedString[i + 1] == '*' || checkedString[i + 1] == '/' || checkedString[i + 1] == '+' || checkedString[i + 1] == '-'))
                    {
                        throw new Exception("Wrong input format");
                    }

                }
                char LastSymbol = '-';
                int leftScopeAmount = 0;
                int rightScopeAmount = 0;
                foreach (char sym in checkedString)
                {
                    if ((sym == '('  && LastSymbol==')')||(sym == ')'  && LastSymbol=='('))
                        throw new Exception("Wrong input format");
                    if (sym == '(')
                    {
                        leftScopeAmount++;
                    }
                    else
                        if (sym == ')')
                        {
                            if (leftScopeAmount > 0 && (rightScopeAmount - leftScopeAmount != 0))
                                rightScopeAmount++;
                            else
                            {
                                throw new Exception("Wrong input format");
                            }
                        }
 
                    LastSymbol=sym;
                }
                if (leftScopeAmount != rightScopeAmount)
                    throw new Exception("Some scopes aren't close");
                return checkedString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
          
            ErrorBlock.Text = "";
            input.Text += ((Button)sender).Content;
        }

        private void Reset(object sender, RoutedEventArgs e)
        {
            input.Text = "";
            wholepart.Content = "";
            numerator.Content = "";
            denominator.Content = "";
            separator.Content = "";
            ErrorBlock.Text = "";
        }

        private void DeleteLatSymbol(object sender, RoutedEventArgs e)
        {
            ErrorBlock.Text = "";
            if (input.Text.Length > 0)
                input.Text = input.Text.Substring(0, input.Text.Length - 1);
        }
    }
}
