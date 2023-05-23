using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BELOBORODOV_DIPLOM.Models {
	internal class User {
		public int Id { get; set; } = 0;

		public string Name { get; set; } = string.Empty;

		public string Password { get; set; } = string.Empty;

		public string EMail { get; set; } = string.Empty;

		/// <summary>
		/// Флаг валидности пользователя
		/// </summary>
		public bool IsValid { get; set; } = false;
	}
}
