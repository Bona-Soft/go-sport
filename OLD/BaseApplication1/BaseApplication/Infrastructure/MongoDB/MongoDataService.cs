using MongoDB.Driver;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;

namespace MYB.BaseApplication.Infrastructure.MongoDB
{
	public class MongoDataService : IMongoDataService
	{
		public MongoDataService(IMongoConnectionData mongoConnectionData)
		{
			ConnectionData = mongoConnectionData;
			Client = new MongoClient(ConnectionData.ConnectionString);
			Database = Client.GetDatabase(ConnectionData.DataBase);
		}

		#region " Shortcut and Properties "

		public IMongoCollection<T> Collection<T>(string collectionName)
		{
			return Database.GetCollection<T>(collectionName);
		}

		public IMongoClient Client { get; private set; }
		public IMongoDatabase Database { get; private set; }
		public IMongoConnectionData ConnectionData { get; private set; }

		#endregion " Shortcut and Properties "
	}

	public class MongoDataService<T> : IMongoDataService<T>
	{
		public MongoDataService(IMongoConnectionData mongoConnectionData)
		{
			ConnectionData = mongoConnectionData;
			Client = new MongoClient(ConnectionData.ConnectionString);
			Database = Client.GetDatabase(ConnectionData.DataBase);
		}

		#region " Shortcut and Properties "

		public IMongoCollection<T> Collection(string collectionName)
		{
			return Database.GetCollection<T>(collectionName);
		}

		public IMongoClient Client { get; private set; }
		public IMongoDatabase Database { get; private set; }
		public IMongoConnectionData ConnectionData { get; private set; }

		#endregion " Shortcut and Properties "
	}
}