using MongoDB.Driver;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;

namespace MYB.BaseApplication.Application.CoreInterfaces.DataBase
{
	public interface IMongoDataService : IPerWebRequest
	{
		IMongoClient Client { get; }
		IMongoDatabase Database { get; }
		IMongoConnectionData ConnectionData { get; }

		IMongoCollection<T> Collection<T>(string collectionName);
	}

	public interface IMongoDataService<T> : IPerWebRequest
	{
		IMongoClient Client { get; }
		IMongoDatabase Database { get; }
		IMongoConnectionData ConnectionData { get; }

		IMongoCollection<T> Collection(string collectionName);
	}
}