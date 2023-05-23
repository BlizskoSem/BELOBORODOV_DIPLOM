using BELOBORODOV_DIPLOM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
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
			var query = "select Id, Name from dbo.Users u where u.Name = @name and u.Password = @pwd";
			var cmd = new SqlCommand(query, new SqlConnection(_sb.ConnectionString));
			cmd.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50).Value = name;
			cmd.Parameters.Add("@pwd", System.Data.SqlDbType.NVarChar, 50).Value = pwd;
			try {
				cmd.Connection.Open();
				using (var reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						return Result<User>.OK(new User() { Id = (int)reader["Id"], Name = (string)reader["Name"], Password = pwd, IsValid = true });
					}
				}
				return Result<User>.OK(new User());
			} catch (Exception ex) {
				return Result<User>.Fail("GetUserByNameAndPwd", ex);
			} finally { cmd.Connection.Close(); }
		}
	}
}
