using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface ISessionManager : ISingleton
	{
		List<IAppSession> GetAllActiveSessions();

		bool RemoveSession(string session);
	}
}