using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
	public interface IMongoConnectionData : IPerHost
	{
		string ConnectionString { get; }
		bool CloseConnection { get; }
		string DataBase { get; }
	}
}