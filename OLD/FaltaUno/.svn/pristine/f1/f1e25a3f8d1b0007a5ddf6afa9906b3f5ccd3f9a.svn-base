using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
	public interface IHeadquarterRepositoryService : IPerWebRequest
	{
		int Save(IHeadquarter entity);

		IHeadquarter GetByID(int id);

		IHeadquarter Fill(IHeadquarter entity, DataRow dr);

		List<IHeadquarter> SearchHeadquarter(short? sportID, string searchText, float lat, float lng);
	}
}
