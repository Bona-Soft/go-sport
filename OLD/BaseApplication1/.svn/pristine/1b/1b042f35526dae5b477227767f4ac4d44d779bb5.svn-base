using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Application.CoreInterfaces.SPManagers;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IWSP : IPerHost
	{
		ISessionSPManager SessionGroup { get; }
		IUserSPManager UserGroup { get; }
		IConfigurationSPManager ConfigurationGroup { get; }

		IStoredProcedure<DataSet> GetAllGeneralParameters(int implementationID, OleDbTransaction Trx = null);

		IStoredProcedure<int> SetGeneralParameter(int implementationID, string parameterName, string type, string value, OleDbTransaction Trx = null);

		IStoredProcedure<DataSet> GetTranslations(OleDbTransaction Trx = null);
	}
}