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
	/// Логика взаимодействия для Authorization.xaml
	/// </summary>
	public partial class Authorization : Window {
		public Authorization() {
			InitializeComponent();
		}

		private void Vxod_Click(object sender, RoutedEventArgs e) {
			var rez = App.Repo.GetUserByNameAndPwd(txt_User.Text??"", txt_Password.Password??"");
			if (rez.Success) {
				if (rez.OutData.IsValid) {
					App.CurrentUser = rez.OutData;
					MainWindow mainWindow = new MainWindow();
					mainWindow.Show();
					Close();
				} else {
					MessageBox.Show("Неверный пользователь или пароль.", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
				}
			} else {
				MessageBox.Show("Не удалось авторизовать пользователя из-за ошибки", App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
			}
		}


		private void RegBtn_Click(object sender, RoutedEventArgs e) {
			Registration registration = new Registration();
			registration.Show();
			this.Close();
		}

		private void Zabili_Click(object sender, RoutedEventArgs e) {
			Parol parol = new Parol();
			parol.Show();
			this.Close();
		}
	}
}
