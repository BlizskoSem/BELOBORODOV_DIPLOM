using BELOBORODOV_DIPLOM.Models;
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

namespace BELOBORODOV_DIPLOM.Windows {
	/// <summary>
	/// Логика взаимодействия для Parol.xaml
	/// </summary>
	public partial class Parol : Window {
		public Parol() {
			InitializeComponent();
		}

		private void Dalee_Click(object sender, RoutedEventArgs e) {
			var rez = App.Repo.GetUserByEMail(txt_Email.Text);
			if (rez.Success) {
				if (rez.OutData.IsValid) {
					ZABILI2 zABILI2 = new ZABILI2(rez.OutData);
					zABILI2.Show();
					Close();
				} else {
					MessageBox.Show("Такой адрес почты не зарегистрированю", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			} else {
				MessageBox.Show($"Ошибка при поиске зарегистрированного адреса \n {rez.Description}", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
