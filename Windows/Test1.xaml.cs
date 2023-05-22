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
    /// Логика взаимодействия для Test1.xaml
    /// </summary>
    public partial class Test1 : Window
    {
        public Test1()
        {
            InitializeComponent();
        }

       
        

        private void test3Answer3_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void test1Answer2_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void test2Answer1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Выйти_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Выход_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void CheckAnswers_Click(object sender, RoutedEventArgs e)
        {
            Test1str2 test1Str2 = new Test1str2();
            test1Str2.Show();
            this.Close();
        }
    }
}
