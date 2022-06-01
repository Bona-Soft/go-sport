using Microsoft.AspNet.SignalR.Hubs;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IHubService : ISingleton, IPerImplementation
	{
		Dictionary<string, long, List<string>> UserHubConnections { get; set; }

		List<string> GetConnectionId(string hubname, long userID);

		#region GetConnections

		List<dynamic> GetConnections<T>(long userID) where T : IHub;
		List<dynamic> GetConnections<T>(List<long> usersID) where T : IHub;
		List<dynamic> GetConnections(string hubname, long userID);
		List<dynamic> GetConnections(string hubname, List<long> usersID);
		List<dynamic> GetConnections<T>(IBaseUser user) where T : IHub;
		List<dynamic> GetConnections<T>(List<IBaseUser> users) where T : IHub;
		List<dynamic> GetConnections(string hubname, IBaseUser user);
		List<dynamic> GetConnections(string hubname, List<IBaseUser> users);

		#endregion
	}

}
