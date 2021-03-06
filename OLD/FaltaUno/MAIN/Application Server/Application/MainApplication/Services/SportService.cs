using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System.Collections.Generic;
using System.Linq;
using static MYB.FaltaUno.Application.MainApplication.Services.Services;

namespace MYB.FaltaUno.Application.MainApplication.Services
{


   public static class SportService
	{
      #region "Sport"

      public static void SetSelectedSport(short sportID)
      {
         Dictionary<string, short> constructParameters = new Dictionary<string, short>
          {
              {"sportID" , sportID}
          };
         BaseApp.Resolve<ISportSelected>(constructParameters);
      }

      public static short GetSelectedSportID()
      {
         return BaseApp.Resolve<ISportSelected>().SportID;
      }

      public static ISport GetSelectedSport()
      {
         var isp = BaseApp.Resolve<ISportSelected>();
         return GetSport(isp.SportID);
      }

      private static List<ISport> _Sports;

		public static ISport GetSport(short sportID)
		{
			if (_Sports == null)
			{
				LoadSports();
			}

			return _Sports.FirstOrDefault(sport => sport.SportID == sportID);
		}

		public static void LoadSports()
		{
			_Sports = Services.SportRepoService.GetAll();
			List<IField> fields = SportService.GetFields();
			List<IMatchType> matchTypes = MatchService.GetMatchTypes();

			foreach (ISport sport in _Sports)
			{
				sport.Fields = fields.Where(field => field.Sport != null && field.Sport.SportID == sport.SportID).ToList();
				sport.MatchTypes = matchTypes.Where(matchType => matchType.Sport != null && matchType.Sport.SportID == sport.SportID).ToList();
			}
		}

		public static List<ISport> GetSports()
		{
			if (_Sports == null)
			{
				LoadSports();
			}
			return _Sports;
		}

		#endregion "Sport"

		#region "Fields"
		private static List<IField> _Fields;

		public static List<IField> GetFields(short sportID)
		{
			if (_Fields == null)
			{
				LoadFields();
			}

			return _Fields.Where(field => field.Sport != null && field.Sport.SportID == sportID).ToList();
		}

		public static IField GetField(int fieldID)
		{
			if (_Fields == null)
			{
				LoadFields();
			}

			return _Fields.FirstOrDefault(field => field.FieldID == fieldID);
		}

		public static void LoadFields()
		{
			_Fields = Services.FieldRepoService.GetFields();
		}

		public static List<IField> GetFields()
		{
			if (_Fields == null)
			{
				LoadFields();
			}
			return _Fields;
		}

		#endregion "Fields"
	}
}