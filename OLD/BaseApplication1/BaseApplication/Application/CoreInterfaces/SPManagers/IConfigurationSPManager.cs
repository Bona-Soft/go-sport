using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces.SPManagers
{
	public interface IConfigurationSPManager
	{
		IStoredProcedure<DataSet> GetMailServerConfiguration(OleDbTransaction Trx = null);
	}
}
