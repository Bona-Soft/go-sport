using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using System.Collections.Generic;
using System.Linq;

namespace MYB.BaseApplication.Domain.Sessions
{
	public class SessionManager : ISessionManager
	{
		#region " Shortcut Properties "

		private IDataBase DB
		{
			get
			{
				return BaseApp.DB;
			}
		}

		private IWSP WSP
		{
			get
			{
				return BaseApp.WSP;
			}
		}

		#endregion " Shortcut Properties "

		public List<IAppSession> GetAllActiveSessions()
		{
			return BaseApp.ContainerManager.ResolveAll<IAppSession>().ToList();
		}

		public bool RemoveSession(string sessionStr)
		{
			if (WSP.UserGroup.RemoveSession(sessionStr).Execute(DB))
			{
				List<IAppSession> sessionList = GetAllActiveSessions();

				foreach (IAppSession session in sessionList.Where(s => s.SessionString == sessionStr))
				{
					BaseApp.WSP.UserGroup.RemoveSession(sessionStr);
					BaseApp.ContainerManager.Release(session);
					return true;
				}
			}
			return false;
		}
	}
}