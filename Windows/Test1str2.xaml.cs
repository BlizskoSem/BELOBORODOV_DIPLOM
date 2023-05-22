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
using System.Windows.Shapes;

namespace BELOBORODOV_DIPLOM.Windows
{
    /// <summary>
    /// Логика взаимодействия для Test1str2.xaml
    /// </summary>
    public partial class Test1str2 : Window
    {
        public Test1str2()
        {
            InitializeComponent();
        }

    
       

  

        private void CheckAnswers_Click_1(object sender, RoutedEventArgs e)
        {
            {
                int correctAnswers = 0;

                if (test1Answer4_1.IsChecked == true) // проверяем, выбран ли второй вариант ответа в первом тесте 
                {
                    correctAnswers++;
                }

                if (test1Answer5_1.IsChecked == true) // проверяем, выбран ли первый вариант ответа во втором тесте
                {
                    correctAnswers++;
                }



                if (test1Answer6_2.IsChecked == true) // проверяем, выбран ли первый вариант ответа во втором тесте
                {
                    correctAnswers++;
                }


                MessageBox.Show($"Правильных ответов: {correctAnswers}/6"); // выводим сообщение с результатами 



            }
        }

        private void Выход_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void test1Answer5_1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void test1Answer6_3_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void test1Answer4_2_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
