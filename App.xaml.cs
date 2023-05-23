using BELOBORODOV_DIPLOM.Ifrastructure;
using BELOBORODOV_DIPLOM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BELOBORODOV_DIPLOM {
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application {
		internal const string APP_CAPTION = "Wiki";
		internal static MSSQLRepository Repo { get; private set; }

		internal static User CurrentUser { get; set; }
		public App() { }
		protected override void OnStartup(StartupEventArgs e) {
			var rez = MSSQLRepository.GetNewRepository();
			if (!rez.Success) {
				string messageBoxText = $"Не удалось установить соединение с базой данных \n{rez.Description}";
				MessageBoxButton button = MessageBoxButton.OK;
				MessageBoxImage icon = MessageBoxImage.Error;

				MessageBox.Show(messageBoxText, APP_CAPTION, button, icon, MessageBoxResult.OK);
				Current.Shutdown();
			}
			Repo = rez.OutData;
			base.OnStartup(e);
		}

	}
}
