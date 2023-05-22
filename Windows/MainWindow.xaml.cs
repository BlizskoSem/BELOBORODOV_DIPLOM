using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace BELOBORODOV_DIPLOM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Perexod_na_Test_Click(object sender, RoutedEventArgs e)
        {
            Windows.VIBIRAEM_TEST vIBIRAEM_TEST = new Windows.VIBIRAEM_TEST();
            vIBIRAEM_TEST.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.BazaZnaniy bazaZnaniy = new Windows.BazaZnaniy();
            bazaZnaniy.Show();
            this.Close();
        }

       

        private void Выйти_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Уйти_Click(object sender, RoutedEventArgs e)
        {
            Windows.Authorization authorization = new Windows.Authorization();
            authorization.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Windows.MyStats myStats = new Windows.MyStats();
            myStats.Show();
            this.Close();
        }
    }
}
