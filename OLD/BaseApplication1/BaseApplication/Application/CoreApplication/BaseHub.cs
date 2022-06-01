using Microsoft.AspNet.SignalR;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseHub : Hub, IBaseHub
	{
		protected string SubClassName
		{
			get
			{
				Type type = this.GetType().UnderlyingSystemType;
				return type.Name;
			}
		}

		protected IHubService HubService
		{
			get
			{
				return BaseApp.Resolve<IHubService>();
			}
		}

		public void SignalConnectionId(string signalConnectionId)
		{
			Clients.Client(signalConnectionId).signalConnectionId(signalConnectionId);
		}

		protected IAppSession GetHubSession()
		{
			IAppSession session = BaseApp.Session();
			if ((session == null || session.User == null || session.User.UserID == 0) && Context.Request.Cookies["BaseAppSession"] != null)
			{
				session = BaseApp.Session(Context.Request.Cookies["BaseAppSession"].Value.ToDefString());
			}
			return session;
		}
	}
}
