using Microsoft.AspNet.SignalR.Hubs;
using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignalRChat
{
	public partial class PushClients : AppWebServices
	{
		protected override void ProcessGET()
		{
			foreach (dynamic conn in BaseApp.HubService().GetConnections("ChatHub", 40698))
			{
				conn.broadcastMessage("desde servicio", "mesaje");
			}

			foreach (dynamic conn in BaseApp.HubService().GetConnections("NotificationHub", 40698))
			{
				JObject test = JObject.Parse(@"{ test: 1, message: 'hola'}");
				conn.pushNotificaiton(test);
			}
		}
	}
}