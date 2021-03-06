using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
	public interface IUserRepositoryService : IPerWebRequest
	{
		long Save(IUser user);
		bool SaveAvatar(long userID, string fileName);

		IUser GetByID(long userID);
		IUser GetByEmail(string email);
		IUser GetCurrent();

		IUserPrivacy GetPrivacy(long id);
		IUserPrivacy GetDefaultPrivacy();
		IUserPrivacy SavePrivacy(IUserPrivacy up, IUser user);
	}
}