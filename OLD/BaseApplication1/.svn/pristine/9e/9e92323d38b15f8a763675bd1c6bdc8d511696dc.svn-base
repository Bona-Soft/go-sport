using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Application.CoreInterfaces.SPManagers;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Domain.WarehouseStoredProcedure
{
	public class WSP : IWSP
	{
		#region " Shortcut "

		private IDataBase DB
		{
			get
			{
				return BaseApp.DB;
			}
		}

		#endregion " Shortcut "

		public WSP()
		{
		}

		private ISessionSPManager _Session;

		public ISessionSPManager SessionGroup
		{
			get
			{
				if (_Session == null)
					_Session = new SessionSPManager();
				return _Session;
			}
		}

		private IUserSPManager _Users;

		public IUserSPManager UserGroup
		{
			get
			{
				if (_Users == null)
					_Users = new UserSPManager();
				return _Users;
			}
		}

		private IConfigurationSPManager _Configuration;

		public IConfigurationSPManager ConfigurationGroup
		{
			get
			{
				if (_Configuration == null)
					_Configuration = new ConfigurationSPManager();
				return _Configuration;
			}
		}

		public IStoredProcedure<DataSet> GetAllGeneralParameters(int implementationID, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer)
			};
			return DB.StoredProcedure<DataSet>("GetAllGeneralParameters", dbParams, Trx);
		}

		public IStoredProcedure<int> SetGeneralParameter(int implementationID, string parameterName, string type, string value, OleDbTransaction Trx = null)
		{
			List<OleDbParameter> dbParams = new List<OleDbParameter>
			{
				DB.CreateParameter("ImplementationID", implementationID, OleDbType.Integer),
				DB.CreateParameter("GeneralParameterID", parameterName, OleDbType.VarChar),
				DB.CreateParameter("Type", type, OleDbType.VarChar),
				DB.CreateParameter("Value", value, OleDbType.VarChar)
			};
			return DB.StoredProcedure<int>("SetGeneralParameter", dbParams, Trx);
		}

		public IStoredProcedure<DataSet> GetTranslations(OleDbTransaction Trx = null)
		{
			return DB.StoredProcedure<DataSet>("GetTranslations", Trx);
		}
	}
}