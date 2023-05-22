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
    /// Логика взаимодействия для VIBIRAEM_TEST.xaml
    /// </summary>
    public partial class VIBIRAEM_TEST : Window
    {
        public VIBIRAEM_TEST()
        {
            InitializeComponent();
        }

        private void Трассы_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Правила_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Команды_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Трассы_Click_1(object sender, RoutedEventArgs e)
        {
            Windows.Test1Str1 test1 = new Windows.Test1Str1();
            test1.Show();
            this.Close();
        }

        private void Машина_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Выход_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
