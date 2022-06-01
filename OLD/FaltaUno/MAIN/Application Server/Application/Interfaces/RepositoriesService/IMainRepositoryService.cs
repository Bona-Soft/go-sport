using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
	public interface IMainRepositoryService : IPerWebRequest
	{
		List<ILocation> GetLocations(string searchText, short? sportID, int groupMember, long? userID);
      ILocation GetLocation(long locationID);
		long SaveLocation(ILocation location);
		IComment SaveComment(IComment comment);
	}
}
