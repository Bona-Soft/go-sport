using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MYB.FaltaUno.Application.MainApplication.Services.Services;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
	public static class HeadquarterService
	{
		private static int SaveHeadquarter(IHeadquarter headquarter)
		{
			int result = 0;
			Key.SetEmptyObjectToNull(headquarter);

			try
			{
				Services.BaseRepoService.BeginTransaction();

				if (headquarter.Location != null)
				{
					if (headquarter.Location.LocationID == 0)
					{
						headquarter.Location = MainService.SaveLocation(headquarter.Location);
					}
				}

				result = Services.HeadquarterRepoService.Save(headquarter);

				Services.BaseRepoService.CommitTransaction();
			}
			catch (Exception ex)
			{
				try
				{
					Services.BaseRepoService.RollbackTransaction();
				}
				catch { }

				throw ex;
			}
			return result;
		}

		public static int CreateHeadquarter(IHeadquarter headquarter)
		{
			return SaveHeadquarter(headquarter);
		}

		public static int EditHeadquarter(IHeadquarter headquarter)
		{
			//Funcion en base que hay que testear,
			// le pasa el match, el id, y la funcion que get de match
			// ejecuta CheckKeys, si necesita hace el getMatch pasandole el ID
			// y luego mergea el match traido por getMatch sobre los nulls de la entidad match

			Key.RefillEntity<IHeadquarter, int>(headquarter, headquarter.HeadquarterID, GetHeadquarter);

			return SaveHeadquarter(headquarter);
		}

		public static IHeadquarter GetHeadquarter(int headquarterID) => GetHeadquarter(headquarterID, 0);

		public static IHeadquarter GetHeadquarter(int headquarterID, short viewType)
		{
			IHeadquarter headquarter = Services.HeadquarterRepoService.GetByID(headquarterID);

			if (viewType == 2)
			{
				//TODO: Traer las listas de match y asignarselas (Players, etc).
				//headquarter.Sports = Services.HeadquarterRepoService.GetSports(headquarterID);

			}
			if (viewType >= 1)
			{
				//TODO: Traer entidades bases completas OwnerPlayer en Match
			}
			//TODO: Traer objetos completos del subentities manager, no de la DB.
			return headquarter;
		}


		public static List<IHeadquarter> SearchHeadquarters(string headquarterNameSearch, float lat, float lng)
		{
         short? sportID = SportService.GetSelectedSportID();

         return Services.HeadquarterRepoService.SearchHeadquarter(sportID, headquarterNameSearch, lat, lng);
		}
	}
}
