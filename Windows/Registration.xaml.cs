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
	/// Логика взаимодействия для Registration.xaml
	/// </summary>
	public partial class Registration : Window {
		public Registration() {
			InitializeComponent();
		}

		private void Est_Accaunt_Click(object sender, RoutedEventArgs e) {
			Authorization authorization = new Authorization();
			authorization.Show();
			this.Close();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {

		}

		private void RegBuuton_Click(object sender, RoutedEventArgs e) {
			var rez = App.Repo.RegisterNewUser(new User() { Name = txt_User.Text, Password = txt_Pwd.Password, EMail = txt_Email.Text });
			if (rez.Success) {
				Authorization authorization = new Authorization(rez.OutData);
				authorization.Show();
				this.Close();
				MessageBox.Show("Вы успешно зарегестрированы");
			} else {
				MessageBox.Show($"Не удалось зарегистрировать пользователя. \n {rez.Description}", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
