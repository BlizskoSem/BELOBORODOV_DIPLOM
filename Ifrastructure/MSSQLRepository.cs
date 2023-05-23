using BELOBORODOV_DIPLOM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BELOBORODOV_DIPLOM.Ifrastructure {
	internal class MSSQLRepository {
		const string CONNECTION_STRING_NAME = "MsSQLConnStr";
		SqlConnectionStringBuilder _sb;
		private MSSQLRepository(SqlConnectionStringBuilder sb) {

			_sb = sb;
		}
		public static Result<MSSQLRepository> GetNewRepository() {
			try {
				Configuration _conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

				if (_conf.AppSettings.Settings[CONNECTION_STRING_NAME] == null) {
					SqlConnectionStringBuilder newsb = new SqlConnectionStringBuilder();
					newsb.DataSource = "127.0.0.1";
					newsb.InitialCatalog = "dbName";
					newsb.UserID = "User";
					newsb.Password = "***";
					newsb.IntegratedSecurity = false;

					_conf.AppSettings.Settings.Add(CONNECTION_STRING_NAME, newsb.ConnectionString);
					_conf.Save(ConfigurationSaveMode.Modified);
				}
				string connectionString = _conf.AppSettings.Settings[CONNECTION_STRING_NAME].Value;
				var sb = new SqlConnectionStringBuilder(connectionString);
				var repo = new MSSQLRepository(sb);
				var rez = repo.CheckConnection();
				if (rez.Success) {
					return Result<MSSQLRepository>.OK(repo);
				}
				return Result<MSSQLRepository>.Fail($"Не удалось соединиться с базой данных по причине: {rez.Description}");
			} catch (Exception ex) {
				return Result<MSSQLRepository>.Fail("GetNewRepository: ", ex);
			}


		}

		public Result CheckConnection() {
			SqlConnection conn = new SqlConnection(_sb.ConnectionString);
			try {
				conn.Open();
				return Result.OK();
			} catch (Exception ex) {
				return Result.Fail("CheckConnection", ex);
			} finally {
				conn.Close();
			}
		}

		public Result<User> GetUserByNameAndPwd(string name, string pwd) {
			var query = "select Id, Name, EMail from dbo.Users u where u.Name = @name and u.Password = @pwd";
			var cmd = new SqlCommand(query, new SqlConnection(_sb.ConnectionString));
			cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50).Value = name;
			cmd.Parameters.Add("@pwd", System.Data.SqlDbType.NVarChar, 50).Value = pwd;
			try {
				cmd.Connection.Open();
				using (var reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						return Result<User>.OK(new User() { Id = (int)reader["Id"], Name = (string)reader["Name"], EMail = (string)reader["EMail"], Password = pwd, IsValid = true });
					}
				}
				return Result<User>.OK(new User());
			} catch (Exception ex) {
				return Result<User>.Fail("GetUserByNameAndPwd", ex);
			} finally { cmd.Connection.Close(); }
		}

		public Result<User> RegisterNewUser(User user) {
			if (user is null) return Result<User>.Fail("RegisterNewUser: user is null");
			if (string.IsNullOrWhiteSpace(user.Name)) return Result<User>.Fail("RegisterNewUser: Недопустимое имя пользователя");
			if (string.IsNullOrWhiteSpace(user.Password)) return Result<User>.Fail("RegisterNewUser: Недопустимый пароль");
			if (EmailIsBad(user.EMail)) return Result<User>.Fail("RegisterNewUser: Недопустимый адрес почты");

			var query = @"
					INSERT INTO [dbo].[Users]
										 ([Name]
										 ,[Password]
										 ,[EMail])
							 VALUES
										 (@Name
										 ,@Password
										 ,@EMail)
					select @@IDENTITY
			";
			var cmd = new SqlCommand(query, new SqlConnection(_sb.ConnectionString));
			cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Value = user.Name;
			cmd.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar, 50).Value = user.Password;
			cmd.Parameters.Add("@EMail", System.Data.SqlDbType.NVarChar, 50).Value = user.EMail;
			try {
				cmd.Connection.Open();
				var n = cmd.ExecuteScalar();
				if (n is decimal id) {
					return Result<User>.OK(new User() { Name = user.Name, EMail = user.EMail, Id = (int)id, IsValid = true, Password = user.Password });
				} else {
					return Result<User>.Fail("RegisterNewUser: Не удалось зарегистрировать пользователя. Неверный тип ответа БД");
				}
			} catch (Exception ex) {
				return Result<User>.Fail("RegisterNewUser:", ex);
			} finally { cmd.Connection.Close(); }


		}

		internal Result ChangePassword(User user, string newPassword) {
			string query = "update dbo.Users set [Password] = @NewPassword where Id = @id and [Password] = @OldPassword";
			var cmd = new SqlCommand(query, new SqlConnection(_sb.ConnectionString));
			cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = user.Id;
			cmd.Parameters.Add("@OldPassword", System.Data.SqlDbType.NVarChar, 50).Value = user.Password;
			cmd.Parameters.Add("@NewPassword", System.Data.SqlDbType.NVarChar, 50).Value = newPassword;
			try {
				cmd.Connection.Open();
				var n = cmd.ExecuteNonQuery();
				if (n == 1) {
					return Result.OK();
				} else {
					return Result.Fail($"Затронуто неверное количечтво строк: {n}. Ожидалось: 1");
				}
			} catch (Exception ex) {
				return Result<User>.Fail("ChangePassword:", ex);
			} finally { cmd.Connection.Close(); }
		}

		internal Result<User> GetUserByEMail(string eMail) {
			if (EmailIsBad(eMail)) return Result<User>.Fail("Неверный формат почтового адреса");
			var query = "select Id, Name, Password from dbo.Users u where u.EMail = @EMail";
			var cmd = new SqlCommand(query, new SqlConnection(_sb.ConnectionString));
			cmd.Parameters.Add("@EMail", System.Data.SqlDbType.NVarChar, 50).Value = eMail;
			try {
				cmd.Connection.Open();
				using (var reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						return Result<User>.OK(new User() { EMail = eMail, Id = (int)reader["Id"], IsValid = true, Name = (string)reader["Name"], Password = (string)reader["Password"] });
					}
				}
				return Result<User>.OK(new User() { EMail = eMail });
			} catch (Exception ex) {
				return Result<User>.Fail("GetUserByEMail:", ex);
			} finally { cmd.Connection.Close(); }
		}

		private bool EmailIsBad(string eMail) {
			try {
				MailAddress m = new MailAddress(eMail);
				return false;
			} catch (FormatException) {
				return true;
			} catch (Exception) {
				return true;
			}
		}
	}
}
