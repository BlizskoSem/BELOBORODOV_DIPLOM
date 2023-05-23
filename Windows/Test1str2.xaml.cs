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
	/// Логика взаимодействия для Test1str2.xaml
	/// </summary>
	public partial class Test1str2 : Window {
		Quizlet _quizlet;
		internal Test1str2(Quizlet quizlet) {
			_quizlet = quizlet;
			InitializeComponent();
		}

		private void CheckAnswers_Click_1(object sender, RoutedEventArgs e) {
			{
				foreach (var item in grd_Main.Children) {
					if (item is StackPanel panel) {
						var question = "";
						var answer = "";
						foreach (var element in panel.Children) {
							if (element is TextBlock textBlock)
								question = textBlock.Text;
							if (element is RadioButton radioButton) {
								if (radioButton.IsChecked.HasValue && radioButton.IsChecked.Value) {
									answer = (string)radioButton.Content;
									break;
								}
							}
						}
						_quizlet.CheckAnswer(question, answer);
					}
				}

				var rez = _quizlet.CalculateResults();
				string msg;
				switch (rez.GetGrade()) {
					case Grade.None:
						msg = "В тесте не было вопросов.";
						break;
					case Grade.Irrelevant:
						msg = "Неверные результаты теста.";
						break;
					case Grade.Bad:
						msg = "Вы плохо знаете тему.";
						break;
					case Grade.Good:
						msg = "Ваши знания выше среднего";
						break;
					case Grade.Exelent:
						msg = "Ваши знания совершенны.";
						break;
					default:
						msg = "Неопределенная оценка теста.";
						break;
				}
				msg += $"\nПравильных ответов: {rez.RightAnswers} из {rez.TotalQuestions}";
				MessageBox.Show(msg, App.APP_CAPTION, MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void Выход_Click(object sender, RoutedEventArgs e) {
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			this.Close();
		}
	}
}
