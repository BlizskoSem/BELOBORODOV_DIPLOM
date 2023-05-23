using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELOBORODOV_DIPLOM.Models {
	internal class Question {
		public string Text { get; set; }
		public List<Variant> Variants { get; set; } = new List<Variant>();
		public bool Passed { get; private set; } = false;

		public void CheckAnswer(string question, string answer) {
			if (question == Text)
				foreach (var variant in Variants) {
					if (variant.Text == answer) Passed = variant.IsCorrect;
				}
		}
	}
}
