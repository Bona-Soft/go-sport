using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat
{
	public class ChatHub : BaseUserHub
	{
		public void Send(string userID, string message)
		{
			string name = "Anonymous";
			IAppSession session = GetHubSession();

			if (session != null && session.User != null && session.User.Logins != null && session.User.Logins.Count > 0)
			{
				name = session.User.Logins.First().Username;
			}

			foreach (string connection in HubService.GetConnectionId("ChatHub", userID.ToDefType<long>()))
			{
				Clients.Client(connection).broadcastMessage(name, message);
			}

			if (userID.ToDefType<long>() != session.User.UserID)
			{
				foreach (string connection in HubService.GetConnectionId("ChatHub", session.User.UserID))
				{
					Clients.Client(connection).broadcastMessage(name, message);
				}
			}

		}
	}

	public class NotificationHub : BaseUserHub
	{
		
	}
}