using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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

namespace Exam.Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Action = ""; // Последние действие(+, -, *, /, =)
        public string PrevAction = ""; // Предпоследнее действие
        public string FirstNumber = ""; // Хранит первое число
        public string SecondNumber = ""; // Хранит второе число
        public bool IsSecondNum; // Флаг для проверки готовности ввода второго числа
        public MainWindow()
        {
            InitializeComponent();
            IsSecondNum = false;

        }
        /// <summary>
        /// Действия для кнопок-цифр (ввод в главное поле)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Num_Click(object sender, RoutedEventArgs e)
        {
            result.FontSize = 30;
            if (IsSecondNum)
            {
                IsSecondNum=false;
                if (result.Text != "0") ;
                input.Text = result.Text;
                result.Text = "0";
            }

            Button B = (Button) sender;

            if (result.Text == "0" || !Double.TryParse(result.Text, out double temp))
                result.Text = (string)B.Content;
            else
                result.Text += (string)B.Content;
        }

        /// <summary>
        /// Действия для кнопки C (очищение главного поля)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "0";
        }

        /// <summary>
        /// Действия для кнопок-действий (запоминание действия + - * /)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Action_Click(object sender, RoutedEventArgs e)
        {
            Button B = (Button)sender;
            Action = (string)B.Content;

            FirstNumber = result.Text;
            IsSecondNum = true;
        }

        /// <summary>
        /// Действие для кнопки равно ( вычисление результата )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            double num1, num2, res = 0;
            //Проверка на нажатия 2 раза на равно (при таком действии должно повторяться предыдущие выражение)
            if(Action == "=")
            {
                Action = PrevAction;
                num1 = Convert.ToDouble(result.Text);
                num2 = Convert.ToDouble(SecondNumber);
            }
            else
            {
                //Если не было двойного нажатия на равно, то выполняется обычный алгоритм считывания
                if (!Double.TryParse(FirstNumber, out num1))
                {
                    result.Text = "Error";
                    return;
                }
                SecondNumber = result.Text;
                num2 = Convert.ToDouble(SecondNumber);
            }

            switch (Action)
            {
                case "+":
                    res = num1 + num2;
                    break;
                case "-":
                    res = num1 - num2;
                    break;
                case "×":
                    res = num1 * num2;
                    break;
                case "÷":
                    if (num2 == 0)
                    {
                        result.FontSize = 20;
                        result.Text = "Деление на 0!";
                        input.Text = "";
                        return;
                    }
                    res = num1 / num2;
                    break;
            }
            IsSecondNum = true;
            input.Text = String.Format("{0} {1} {2} = {3}", FirstNumber, Action, result.Text, res.ToString());
            result.Text = res.ToString();
            PrevAction = Action;
            Action = "=";

        }

        /// <summary>
        /// Нахождение квадратного корня
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sqrt_Click(object sender, RoutedEventArgs e)
        {
            double num, res = 0;
            if (!Double.TryParse(result.Text, out num))
            {
                result.Text = "Error";
                return;
            }
            if (num < 0)
            {
                result.FontSize = 20;
                result.Text = "Иррациональное число!";
                input.Text = "";
                return;
            }
                
            
            res = Math.Sqrt(num);
            input.Text = String.Format("√{0} = {1}", num, res);
            result.Text = res.ToString();
        }

        /// <summary>
        /// Возведение в квадрат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Square_Click(object sender, RoutedEventArgs e)
        {
            double num, res = 0;
            if (!Double.TryParse(result.Text, out num))
            {
                result.Text = "Error";
                return;
            }
            res = Math.Pow(num, 2);
            input.Text = String.Format("{0} × {0} = {1}", num, res);
            result.Text = res.ToString();
        }

        /// <summary>
        /// Деление 1 на введенное число
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void One_Div_By_Num_Click(object sender, RoutedEventArgs e)
        {
            double num, res = 0;
            if (!Double.TryParse(result.Text, out num))
            {
                result.Text = "Error";
                return;
            }
            if (num == 0)
            {
                result.FontSize = 30;
                result.Text = "Деление на 0!";
                FirstNumber = "0";
                input.Text = "";
                return;
            }
            res = 1 / num;
            input.Text = String.Format("1 / {0} = {1}", num, res);
            result.Text = res.ToString();
        }

        /// <summary>
        /// Изменения знака числа в главном поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rev_Sign_Click(object sender, RoutedEventArgs e)
        {
            if (!Double.TryParse(result.Text, out double num))
            {
                result.Text = "Error";
                return;
            }
            result.Text = (Convert.ToDouble(num) * (-1)).ToString();
        }

        /// <summary>
        /// Добавление запятой в числа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Point_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text.Contains(','))
                return;
            else
                result.Text += ",";
        }

        /// <summary>
        /// Очистка главного поля и поля истории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_All_Click(object sender, RoutedEventArgs e)
        {
            result.Text = "0";
            input.Text = "";
        }

        /// <summary>
        /// Работа с процентами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Perc_Click(object sender, RoutedEventArgs e)
        {
            double num1, num2, res = 0;
            if (!Double.TryParse(FirstNumber, out num1))
            {
                result.Text = "Error";
                return;
            }
            num2 = Convert.ToDouble(result.Text);

            switch (Action)
            {
                case "+":
                    res = num1 + num1*num2/100;
                    break;
                case "-":
                    res = num1 - num1 * num2 / 100;
                    break;
                case "×":
                    res = num1 * num2 / 100;
                    break;
                case "÷":
                    if (num2 / 100 == 0)
                    {
                        result.FontSize = 30;
                        result.Text = "Деление на 0!";
                        FirstNumber = "0";
                        input.Text = "";
                        return;
                    }
                    res = num1 / (num2 / 100);
                    break;
            }
            IsSecondNum = true;
            input.Text = String.Format("{0} {1} {2} = {3}", FirstNumber, Action, result.Text, res.ToString());
            result.Text = res.ToString();
            Action = "=";
        }

        /// <summary>
        /// Удаление последнего символа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text == "0")
                result.Text = "0";
            else
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
                if (result.Text == "")
                    result.Text = "0";
            }
        }
    }
    
}
