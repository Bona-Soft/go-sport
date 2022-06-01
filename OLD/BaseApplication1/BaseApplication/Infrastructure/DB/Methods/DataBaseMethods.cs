using MYB.BaseApplication.Application.CoreInterfaces;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Infrastructure.DB.Methods
{
	public abstract class DataBaseMethods
	{
		public DB ParentDataBase { get; set; }
		
		public int CommandTimeOut
		{
			get
			{
				return ParentDataBase.CommandTimeOut;
			}
		}

		public OleDbTransaction Transaction
		{
			get
			{
				return ParentDataBase.Transaction;
			}
		}

		public OleDbConnection Connection
		{
			get
			{
				return ParentDataBase.Connection;
			}
			set
			{
				ParentDataBase.Connection = value;
			}
		}

		public OleDbConnection CreateOleDbConnection()
		{
			return new OleDbConnection(ConnectionData.ConnectionString);
		}

		public IConnectionData ConnectionData
		{
			get
			{
				return ParentDataBase.ConnectionData;
			}
		}

      public OleDbConnection ResolveConnection()
      {
         if (Connection != null)
         {
            if (Connection.State == ConnectionState.Closed)
            {
               Connection = CreateOleDbConnection();
               if(Connection.State != ConnectionState.Open)
                  Connection.Open();
            }
            return  Connection;
         }
         else
         {
            OleDbConnection dbConnection = CreateOleDbConnection();
            dbConnection.Open();
            return dbConnection;
         }
      }

	}
}