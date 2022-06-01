using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseUserHub : BaseHub, IBaseUserHub
	{

		public override Task OnConnected()
		{
			IAppSession session = GetHubSession();

			if (!session.IsLogged || session.User == null || session.User.UserID == 0)
			{
				return base.OnConnected();
			}

			long userID = session.User.UserID;

			List<string> userHubConnectionIds = new List<string>();

			if (!HubService.UserHubConnections.ContainsKey(SubClassName, userID))
			{
				HubService.UserHubConnections.Add(SubClassName, userID, userHubConnectionIds);
			}
			HubService.UserHubConnections[SubClassName, userID].TryAdd(Context.ConnectionId.ToString());

			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			IAppSession session = GetHubSession();

			if (stopCalled)
			{
				if (!session.IsLogged || session.User == null || session.User.UserID == 0)
				{
					return base.OnConnected();
				}

				long userID = session.User.UserID;

				if (HubService.UserHubConnections.ContainsKey(SubClassName, userID))
				{
					HubService.UserHubConnections[SubClassName, userID].Remove(Context.ConnectionId);
				}
			}
			else
			{
				// This server hasn't heard from the client in the last ~35 seconds.
				// If SignalR is behind a load balancer with scaleout configured,
				// the client may still be connected to another SignalR server.
			}

			return base.OnDisconnected(stopCalled);
		}


	}

}
