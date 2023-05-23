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
	/// Логика взаимодействия для ZABILI2.xaml
	/// </summary>
	public partial class ZABILI2 : Window {
		User _user;
		internal ZABILI2(User user) {
			InitializeComponent();
			_user = user;

		}
		private void Vxod_Click(object sender, RoutedEventArgs e) {
			if (string.IsNullOrEmpty(txt_newPwd.Password)) {
				MessageBox.Show("Введите новый пароль", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (string.IsNullOrEmpty(txt_RepeatNewPwd.Password)) {
				MessageBox.Show("Введите повтор нового пароля", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			if (txt_newPwd.Password != txt_RepeatNewPwd.Password) {
				MessageBox.Show("Пароли не совпадают", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}

			var rez = App.Repo.ChangePassword(_user, txt_newPwd.Password);
			if (rez.Success) {
				_user.Password = txt_newPwd.Password;
				SendUserPassword(_user);

				Authorization authorization = new Authorization(_user);
				authorization.Show();
				Close();
			} else {
				MessageBox.Show($"Не удалось сменить пароль.\n{rez.Description}", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
			}


		}

		private void SendUserPassword(User user) {
			MessageBox.Show($"Новый пароль отправлен на почту {user.EMail}", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Information);
		}
	}
}
