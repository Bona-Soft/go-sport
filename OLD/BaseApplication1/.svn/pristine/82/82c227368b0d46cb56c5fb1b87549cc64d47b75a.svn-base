using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Domain.HubService
{
	public class HubService : IHubService
	{
		public int ImplementationID { get; private set; }
		public Dictionary<string, long, List<string>> UserHubConnections { get; set; }

		public HubService(int implementationID = 0)
		{
			ImplementationID = implementationID;
			UserHubConnections = new Dictionary<string, long, List<string>>();
		}

		public List<string> GetConnectionId(string hubname, long userID)
		{
			if (!UserHubConnections.ContainsKey(hubname, userID))
			{
				return new List<string>();
			}
			return UserHubConnections[hubname,userID];
		}

		#region GetConnections

		public List<dynamic> GetConnections<T>(long userID) where T : IHub
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext<T>();

			return GetConnections(typeof(T).Name, context, userID);
		}

		public List<dynamic> GetConnections<T>(List<long> usersID) where T : IHub
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext<T>();

			return GetConnections(typeof(T).Name, context, usersID);
		}

		public List<dynamic> GetConnections(string hubname, long userID)
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext(hubname);

			return GetConnections(hubname, context, userID);
		}

		public List<dynamic> GetConnections(string hubname, List<long> usersID)
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext(hubname);

			return GetConnections(hubname, context, usersID);
		}

		public List<dynamic> GetConnections<T>(IBaseUser user) where T : IHub
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext<T>();

			return GetConnections(typeof(T).Name, context, user.UserID);
		}

		public List<dynamic> GetConnections<T>(List<IBaseUser> users) where T : IHub
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext<T>();

			return GetConnections(typeof(T).Name, context, users);
		}

		public List<dynamic> GetConnections(string hubname, IBaseUser user)
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext(hubname);

			return GetConnections(hubname, context, user.UserID);
		}

		public List<dynamic> GetConnections(string hubname, List<IBaseUser> users)
		{
			IHubContext context = GlobalHost.ConnectionManager.GetHubContext(hubname);

			return GetConnections(hubname, context, users);
		}

		private List<dynamic> GetConnections(string hubname, IHubContext context, long userId)
		{
			List<dynamic> connections = new List<dynamic>();

			foreach (string connection in GetConnectionId(hubname, userId))
			{
				connections.Add(context.Clients.Client(connection));
			}

			return connections;
		}

		private List<dynamic> GetConnections(string hubname, IHubContext context, List<IBaseUser> users)
		{
			List<dynamic> connections = new List<dynamic>();

			foreach (IBaseUser user in users)
			{
				foreach (string connection in GetConnectionId(hubname, user.UserID))
				{
					connections.Add(context.Clients.Client(connection));
				}
			}

			return connections;
		}

		private List<dynamic> GetConnections(string hubname, IHubContext context, List<long> usersID)
		{
			List<dynamic> connections = new List<dynamic>();

			foreach (long userid in usersID)
			{
				foreach (string connection in GetConnectionId(hubname, userid))
				{
					connections.Add(context.Clients.Client(connection));
				}
			}

			return connections;
		}

		#endregion GetConnections
	}

}