using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Data;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface ISportRepository : ISingleton
	{
		DataTable Get(short sportID);
		DataTable GetAll();
	}
}