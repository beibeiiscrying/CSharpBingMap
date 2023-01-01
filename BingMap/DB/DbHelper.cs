using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BingMap.DB
{
    internal class DbHelper
    {
		private readonly static string ConStr;

		private readonly static string TABLE_NAME;

		private readonly static string TABLE_USER_NAME;

		private readonly static string TABLE_USER_PW;

		private readonly static string TABLE_USER_SETTING;

		private readonly static string TABLE_USER_VIEW_STATUS;

		public static SqlConnection SqlCon;

		static DbHelper()
		{
			DbHelper.ConStr = ConfigurationManager.ConnectionStrings["LoginDB"].ConnectionString;
			DbHelper.TABLE_NAME = "UserTable";
			DbHelper.TABLE_USER_NAME = "UserName";
			DbHelper.TABLE_USER_PW = "UserPw";
			DbHelper.TABLE_USER_SETTING = "Setting";
			DbHelper.TABLE_USER_VIEW_STATUS = "ViewStatus";
		}

		public DbHelper()
		{
		}

		public static int AddUser(string name, string pw)
		{
			int num1;
			DbHelper.Open();
			if (DbHelper.CheckUserRegistered(name))
			{
				num1 = -1;
			}
			else
			{
				using (SqlCommand cmd = new SqlCommand(string.Concat(new string[] { "INSERT INTO ", DbHelper.TABLE_NAME, "(", DbHelper.TABLE_USER_NAME, ", ", DbHelper.TABLE_USER_PW, ") VALUES(@param1,@param2)" }), DbHelper.SqlCon))
				{
					cmd.Parameters.Add("@param1", SqlDbType.VarChar, 50).Value = name;
					cmd.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = pw;
					cmd.CommandType = CommandType.Text;
					int num = cmd.ExecuteNonQuery();
					DbHelper.Close();
					num1 = num;
				}
			}
			return num1;
		}

		public static bool CheckUserLogin(string name, string pw)
		{
			bool flag;
			DbHelper.Open();
			using (SqlCommand cmd = new SqlCommand(string.Concat(new string[] { "SELECT COUNT(", DbHelper.TABLE_USER_NAME, ") FROM ", DbHelper.TABLE_NAME, " WHERE ", DbHelper.TABLE_USER_NAME, " = @checkName AND ", DbHelper.TABLE_USER_PW, " = @checkPw" }), DbHelper.SqlCon))
			{
				cmd.Parameters.Add("@checkName", SqlDbType.VarChar, 50).Value = name;
				cmd.Parameters.Add("@checkPw", SqlDbType.VarChar, 50).Value = pw;
				flag = ((int)cmd.ExecuteScalar() <= 0 ? false : true);
			}
			return flag;
		}

		public static bool CheckUserRegistered(string name)
		{
			bool flag;
			DbHelper.Open();
			using (SqlCommand cmd = new SqlCommand(string.Concat(new string[] { "SELECT COUNT(", DbHelper.TABLE_USER_NAME, ") FROM ", DbHelper.TABLE_NAME, " WHERE ", DbHelper.TABLE_USER_NAME, " = @checkName" }), DbHelper.SqlCon))
			{
				cmd.Parameters.Add("@checkName", SqlDbType.VarChar, 50).Value = name;
				flag = ((int)cmd.ExecuteScalar() <= 0 ? false : true);
			}
			return flag;
		}

		public static void Close()
		{
			if ((DbHelper.SqlCon == null ? false : DbHelper.SqlCon.State == ConnectionState.Open))
			{
				DbHelper.SqlCon.Close();
			}
		}

		public void Dispose()
		{
			if (DbHelper.SqlCon != null)
			{
				DbHelper.SqlCon.Dispose();
				DbHelper.SqlCon = null;
			}
		}

		public int ExecuteNonQuery(string query)
		{
			int num;
			SqlConnection sqlConnection = new SqlConnection(DbHelper.ConStr);
			DbHelper.SqlCon = sqlConnection;
			using (sqlConnection)
			{
				DbHelper.SqlCon.Open();
				using (SqlCommand cmd = DbHelper.SqlCon.CreateCommand())
				{
					cmd.CommandText = query;
					num = cmd.ExecuteNonQuery();
				}
			}
			return num;
		}

		public static object ExecuteScalar(string query)
		{
			object obj;
			SqlConnection sqlConnection = new SqlConnection(DbHelper.ConStr);
			DbHelper.SqlCon = sqlConnection;
			using (sqlConnection)
			{
				DbHelper.SqlCon.Open();
				using (SqlCommand cmd = DbHelper.SqlCon.CreateCommand())
				{
					cmd.CommandText = query;
					obj = cmd.ExecuteScalar();
				}
			}
			return obj;
		}

		~DbHelper()
		{
			this.Dispose();
		}

		public static string LoadUserSettingInfo(string name)
		{
			string str;
			DbHelper.Open();
			using (SqlCommand cmd = new SqlCommand(string.Concat(new string[] { "SELECT ", DbHelper.TABLE_USER_SETTING, " FROM ", DbHelper.TABLE_NAME, " WHERE ", DbHelper.TABLE_USER_NAME, " = @checkName" }), DbHelper.SqlCon))
			{
				cmd.Parameters.Add("@checkName", SqlDbType.VarChar, 50).Value = name;
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					if (!Convert.IsDBNull(reader[0]))
					{
						string setting = (string)reader[0];
						DbHelper.Close();
						str = setting;
						return str;
					}
					else
					{
						DbHelper.Close();
						str = "";
						return str;
					}
				}
			}
			DbHelper.Close();
			str = "";
			return str;
		}

		public static string LoadUserViewStatusInfo(string name)
		{
			string str;
			DbHelper.Open();
			using (SqlCommand cmd = new SqlCommand(string.Concat(new string[] { "SELECT ", DbHelper.TABLE_USER_VIEW_STATUS, " FROM ", DbHelper.TABLE_NAME, " WHERE ", DbHelper.TABLE_USER_NAME, " = @checkName" }), DbHelper.SqlCon))
			{
				cmd.Parameters.Add("@checkName", SqlDbType.VarChar, 50).Value = name;
				SqlDataReader reader = cmd.ExecuteReader();
				if (reader.Read())
				{
					if (!Convert.IsDBNull(reader[0]))
					{
						string viewstatus = (string)reader[0];
						DbHelper.Close();
						str = viewstatus;
						return str;
					}
					else
					{
						DbHelper.Close();
						str = "";
						return str;
					}
				}
			}
			DbHelper.Close();
			str = "";
			return str;
		}

		public static void Open()
		{
			if (DbHelper.SqlCon == null)
			{
				DbHelper.SqlCon = new SqlConnection(DbHelper.ConStr);
			}
			if (DbHelper.SqlCon.State == ConnectionState.Closed)
			{
				try
				{
					DbHelper.SqlCon.Open();
				}
				catch (Exception exception)
				{
					throw new Exception(exception.Message);
				}
			}
		}

		public static int UpdateUserSettingAndViewInfo(string name, string settingInfo, string viewInfo)
		{
			int num1;
			DbHelper.Open();
			using (SqlCommand cmd = new SqlCommand(string.Concat(new string[] { "UPDATE ", DbHelper.TABLE_NAME, " SET ", DbHelper.TABLE_USER_SETTING, " = @SetInfo , ", DbHelper.TABLE_USER_VIEW_STATUS, " = @ViewInfo  WHERE ", DbHelper.TABLE_USER_NAME, " = @checkName" }), DbHelper.SqlCon))
			{
				cmd.Parameters.Add("@checkName", SqlDbType.VarChar, 50).Value = name;
				cmd.Parameters.Add("@SetInfo", SqlDbType.VarChar, 50).Value = settingInfo;
				cmd.Parameters.Add("@ViewInfo", SqlDbType.VarChar, 50).Value = viewInfo;
				int num = cmd.ExecuteNonQuery();
				DbHelper.Close();
				num1 = num;
			}
			return num1;
		}
	}
}
