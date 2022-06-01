using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;

namespace MYB.FaltaUno.Model.Factories
{
	public class PlayerFactory : BaseFactory<Player, IPlayer>, IPlayerFactory
	{

	}
}