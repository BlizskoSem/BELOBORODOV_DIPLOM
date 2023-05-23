using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELOBORODOV_DIPLOM.Models {
	/// <summary>
	/// Викторина
	/// </summary>
	internal class Quizlet {
		public List<Question> Questions { get; set; } = new List<Question>();
		public QuizletResults CalculateResults() {
			var rez = new QuizletResults();
			foreach (var question in Questions) {
				rez.TotalQuestions++;
				if (question.Passed) {
					rez.RightAnswers++;
				}
			}
			return rez;
		}

		public void CheckAnswer(string question, string answer) {
			foreach (var quest in Questions) {
				quest.CheckAnswer(question, answer);	
			}
		}

		public static Quizlet GetFormula1Quizlet() {
			var quizlet = new Quizlet();
			var question = new Question {
				Text = "Вопрос 1: Кто победил в гонке Формулы 1 в Абу Даби в 2020 году?"
			};
			question.Variants.Add(new Variant() { Text = "Льюис Хэмилтон", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Макс Ферстаппен", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Себастьян Феттель", IsCorrect = true });
			quizlet.Questions.Add(question);

			question = new Question() {
				Text = "Вопрос  2: Какое из этих мест является самой сложной трассой Формулы 1?"
			};
			question.Variants.Add(new Variant() { Text = "Монте-Карло", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Сильверстоун", IsCorrect = true });
			question.Variants.Add(new Variant() { Text = "Спа-Франкошам", IsCorrect = false });
			quizlet.Questions.Add(question);

			question = new Question() {
				Text = "Вопрос 3: На какой трассе Михаэль Шумахер одержал свою первую победу?"
			};
			question.Variants.Add(new Variant() { Text = "Монте-Карло", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Сильверстоун", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Спа-Франкошам", IsCorrect = true });
			quizlet.Questions.Add(question);

			question = new Question {
				Text = "Вопрос 4: Кто победил в гонке Формулы 1 в Сильверстоуне в 2020 году?"
			};
			question.Variants.Add(new Variant() { Text = "Льюис Хэмилтон", IsCorrect = true });
			question.Variants.Add(new Variant() { Text = "Макс Ферстаппен", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Себастьян Феттель", IsCorrect = false });
			quizlet.Questions.Add(question);

			question = new Question() {
				Text = "Вопрос  5: Какое из этих мест является самой сложной трассой Формулы 1?"
			};
			question.Variants.Add(new Variant() { Text = "Монте-Карло", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Сильверстоун", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Спа-Франкошам", IsCorrect = true });
			quizlet.Questions.Add(question);

			question = new Question() {
				Text = "Вопрос 6: На какой трассе Михаэль Шумахер одержал свою первую победу?"
			};
			question.Variants.Add(new Variant() { Text = "Монте-Карло", IsCorrect = false });
			question.Variants.Add(new Variant() { Text = "Сильверстоун", IsCorrect = true });
			question.Variants.Add(new Variant() { Text = "Спа-Франкошам", IsCorrect = false });
			quizlet.Questions.Add(question);

			return quizlet;
		}
	}
}
