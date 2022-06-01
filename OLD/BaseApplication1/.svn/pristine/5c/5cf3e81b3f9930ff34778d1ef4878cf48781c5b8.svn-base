using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MYB.BaseApplication.DB
{
	public class SQLDB
	{

		private string _connString = ConfigurationManager.AppSettings["connectionstring"];
		public SqlDataReader DataReader = null;
		//public SqlConnection sqlConnection = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=SimpsonNames;User Id=web_admin;Password = 2dsg34425H!_as;");
		public SqlConnection sqlConnection;

		public void open()
		{
			sqlConnection = new SqlConnection(_connString);
			sqlConnection.Open();
		}
		public void close()
		{
			sqlConnection.Close();
		}
		public void conn(string strconn)
		{
			sqlConnection.ConnectionString = strconn;
		}

		public void OnLogRequest(Object source, EventArgs e)
		{
			//Aquí puede poner la lógica de registro personalizado
		}

		public string run(string consulta)
		{
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = consulta;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Connection = sqlConnection;

			try
			{
				this.open();
				DataReader = cmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				sqlConnection.Close();
				return ex.ToString();
			}
			finally
			{

			}
			return null;
		}


		public string FIL(string tabla, string criterio, params object[] parametros)
		{
			if (parametros.Length <= 0)
			{
				parametros[0] = "";
			}
			var query = tabla + "FIL" + criterio + " " + parametros[0];
			for (int i = 1; i < parametros.Length; i++)
			{
				if (parametros[i].GetType().ToString() == "System.String" || parametros[i].GetType().ToString() == "System.Char")
				{
					query = query + ",'" + parametros[i] + "'";
				}
				else
				{
					query = query + "," + parametros[i];
				}
			}
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = query;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Connection = sqlConnection;

			try
			{
				this.open();
				DataReader = cmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				sqlConnection.Close();
				return ex.ToString();
			}
			finally
			{

			}
			return null;
		}

		public string MODI(string tabla, params object[] parametros)
		{
			if (parametros.Length <= 0)
			{
				parametros[0] = "";
			}
			Console.WriteLine(parametros[0].GetType().ToString());
			var query = tabla + "MODI ";
			/*if (parametros[0].GetType().ToString()== "System.Array")
			{
				 query = query + parametros[0][0];
				 for (int i = 1; i < parametros[0].Length; i++)
				 {
					  if (parametros[0][i].GetType().ToString() == "System.String" || parametros[0][i].GetType().ToString() == "System.Char")
					  {
							query = query + ",'" + parametros[0][i] + "'";
					  }
					  else
					  {
							query = query + "," + parametros[0][i];
					  }
				 }
			}
			else
			{*/
			query = query + parametros[0];
			for (int i = 1; i < parametros.Length; i++)
			{
				if (parametros[i].GetType().ToString() == "System.String" || parametros[i].GetType().ToString() == "System.Char")
				{
					query = query + ",'" + parametros[i] + "'";
				}
				else
				{
					query = query + "," + parametros[i];
				}
			}



			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = query;
			cmd.CommandType = System.Data.CommandType.Text;
			cmd.Connection = sqlConnection;

			try
			{
				this.open();
				DataReader = cmd.ExecuteReader();
			}
			catch (Exception ex)
			{
				sqlConnection.Close();
				return ex.ToString();
			}
			finally
			{

			}
			return null;
		}

		public string next()
		{
			if (DataReader.HasRows)
			{
				DataReader.Read();
			}
			else
			{
				return "No rows found.";
			}
			return null;
		}

	}
}
