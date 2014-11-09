using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Bionic9
{
    public class ExprCalculator
    {
        string expression;
        private int pos;
        public ExprCalculator(string expression)
        {
            this.expression = expression;
            pos = 0;
        }
        public double Calculate()
        {
            //As calculate method works only with unary minus, we replace all patterns ...-a into ...+(-1)*(a)
            for (int i = 0; i < expression.Length; i++)
                if (expression[i].Equals('-'))
                {
                    expression = expression.Substring(0, i) + "+(-1)*" + expression.Substring(i + 1);
                    i += 5;
                }

            //Counting 
            return Calculation();
        }
        private char GetNextChar()
        {
            if (pos >= expression.Length)
                return '!';
            return expression[pos++];
        }
        private char TryNextChar()
        {
            if (pos >= expression.Length)
                return '!';
            return expression[pos];
        }
    
        private bool TryGetNum(out double number)
        {
            bool find = false;

            string number_buffer = "";
            while (Char.IsDigit(TryNextChar()))
            {
                find = true;
                char c = GetNextChar();
                number_buffer += c;

            }
            if (find)
            {
                number = Convert.ToDouble(number_buffer);
                return true;
            }
            else
            {
                number = 0;
                return false;
            }
        }
        private double Calculation()
        {
            try
            {
                double number = 0;
                TryGetNum(out number);
                while (pos < expression.Length)
                {
                    char c = GetNextChar();


                    if (c.Equals('+'))
                    {
                        number += Calculation();
                    }
                    if (c.Equals('-'))
                    {

                        number -= Calculation();
                    }
                    if (c.Equals('('))
                    {
                        number = Calculation();
                    }
                    if (c.Equals(')'))
                    {
                        return number;
                    }

                    if (c.Equals('*'))
                    {
                        double nextlexem = 0;
                        if (TryGetNum(out nextlexem))
                        {
                            number *= nextlexem;
                        }
                        else
                        {
                            number *= Calculation();
                        }
                    }
                    if (c.Equals('/'))
                    {
                        double nextlexem = 0;
                        if (TryGetNum(out nextlexem))
                        {
                            if (nextlexem != 0)
                                number /= nextlexem;
                            else throw new Exception("Divided by zero");
                        }
                        else
                        {
                            double result = Calculation();
                            if (result != 0)
                                number /= result;
                            else throw new Exception("Divided by zero");
                        }
                    }
                    if (c.Equals('!')) return number;
                }
                return number;
            }
            catch (Exception e)
            {
                MessageBoxResult result = MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                return 0;
            }

        }
    }
}
