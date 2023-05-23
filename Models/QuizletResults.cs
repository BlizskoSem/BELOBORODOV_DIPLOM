using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELOBORODOV_DIPLOM.Models {
	internal class QuizletResults {
		public int TotalQuestions { get; set; } = 0;
		public int RightAnswers { get; set; } = 0;

		internal Grade GetGrade() {
			if (TotalQuestions == 0) return Grade.None;
			else if (RightAnswers > TotalQuestions) return Grade.Irrelevant;
			else if (RightAnswers == TotalQuestions) return Grade.Exelent;
			else if (RightAnswers < TotalQuestions / 2) return Grade.Bad;
			else return Grade.Good;

		}
	}

	internal enum Grade {
		None = 0,
		Irrelevant,
		Bad,
		Good,
		Exelent
	}
}
