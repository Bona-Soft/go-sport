using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;

namespace MYB.FaltaUno.Model.Interfaces.Factories
{
	public interface IUserFactory : ISingleton
	{
		IUser CurrentUser();

		IUser User(long userID);
		IUserPrivacy NewUserPrivacy();
		IUserPrivacy NewUserPrivacy(long userID);
	}
}