using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.SPManagers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Domain.WarehouseStoredProcedure
{
	public class ConfigurationSPManager : StoredProceduresManager, IConfigurationSPManager
	{
		public IStoredProcedure<DataSet> GetMailServerConfiguration(OleDbTransaction Trx = null)
		{
			return DB.StoredProcedure<DataSet>("GetMailServerConfiguration", Trx);
		}
	}
}
