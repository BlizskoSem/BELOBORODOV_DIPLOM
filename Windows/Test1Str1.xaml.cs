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
	/// Логика взаимодействия для Test1.xaml
	/// </summary>
	public partial class Test1Str1 : Window {
		Quizlet _quizlet =  Quizlet.GetFormula1Quizlet();
		public Test1Str1() {
			InitializeComponent();
		}

		private void Выход_Click(object sender, RoutedEventArgs e) {
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			Close();
		}

		private void CheckAnswers_Click(object sender, RoutedEventArgs e) {
			foreach (var item in grd_Main.Children) {
				if (item is StackPanel panel) {
					var question = "";
					var answer = "";
					foreach (var element in panel.Children) {
						if(element is TextBlock textBlock)
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

			Test1str2 test1Str2 = new Test1str2(_quizlet);
			test1Str2.Show();
			this.Close();
		}
	}
}
