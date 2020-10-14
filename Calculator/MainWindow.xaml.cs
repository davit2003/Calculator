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

namespace Calculator
{
    public partial class MainWindow : Window
    {
        public static int prevAct = 1, Act = 1;
        public static double PreviousNumber = 0, CurrentNumber;
        public bool WritingState = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        public void NumberClick(object sender, RoutedEventArgs e)
        {
            string obj = MainDisplay.Text.ToString();
            if (!WritingState && (sender as Button).Content.ToString() != "0")
            {
                WritingState = true;
                
                if((sender as Button).Content.ToString() == ".")
                    MainDisplay.Text = "0.";
                else 
                    MainDisplay.Text = (sender as Button).Content.ToString();
            }
            else if ((double.Parse(MainDisplay.Text) != 0 || MainDisplay.Text.Contains("0.")) && WritingState)
                MainDisplay.Text += (sender as Button).Content.ToString();
        }
        public void ActionClick(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "AC")
            {
                MainDisplay.Text = "0";
                PreviousNumber = 0;
                CurrentNumber = 0;
                WritingState = false;
                Act = 1;
            }
            else if ((sender as Button).Content.ToString() == "DEL")
            {
                string tmp = MainDisplay.Text.ToString();
                if(double.Parse(tmp) == PreviousNumber)
                {
                    if (tmp.Length >= 1)
                        MainDisplay.Text = tmp.Remove(tmp.Length - 1, 1);
                    else MainDisplay.Text = "0";
                    PreviousNumber = double.Parse(MainDisplay.Text);
                }
                else
                {
                    if (tmp.Length >= 1)
                        MainDisplay.Text = tmp.Remove(tmp.Length - 1, 1);
                    else MainDisplay.Text = "0";
                }
            }
            else
            {
                if (WritingState)
                {
                    WritingState = false;
                    CurrentNumber = double.Parse(MainDisplay.Text);
                    MainDisplay.Text = Calc(Act, PreviousNumber, CurrentNumber).ToString();
                    PreviousNumber = double.Parse(MainDisplay.Text);
                }


                if ((sender as Button).Content.ToString() == "√")
                {
                    MainDisplay.Text = Math.Sqrt(double.Parse(MainDisplay.Text)).ToString();
                    PreviousNumber = 0;
                    Act = 1;
                }

                /*
                if ((sender as Button).Content.ToString() == "%")
                {
                    MainDisplay.Text = (PreviousNumber * CurrentNumber / 100).ToString();
                    PreviousNumber = 0;
                    Act = 1;
                }
                */
                switch ((sender as Button).Content.ToString()) {
                    case "+":
                        Act = 1;
                        prevAct = Act;
                        break;
                    case "-":
                        Act = 2;
                        prevAct = Act;
                        break;
                    case "*":
                        Act = 3;
                        prevAct = Act;
                        break;
                    case "/":
                        Act = 4;
                        prevAct = Act;
                        break;
                    case "=":
                        if (Act == 0 && !WritingState) {
                            PreviousNumber = double.Parse(MainDisplay.Text);
                            MainDisplay.Text = Calc(prevAct, PreviousNumber, CurrentNumber).ToString();
                        }
                        else { 
                            MainDisplay.Text = PreviousNumber.ToString();
                            Act = 0;
                        }
                        break;
                }
            }
        }
        public double Calc(int x, double a, double b)
        {
            if (x == 1) return a + b;
            else if (x == 2) return a - b;
            else if (x == 3) return a * b;
            else if (x == 4) return a / b;
            else return a;
        }
    }
}
