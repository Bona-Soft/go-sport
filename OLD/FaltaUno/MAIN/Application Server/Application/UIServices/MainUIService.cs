using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Model.Interfaces.Entities;
using Newtonsoft.Json.Linq;
using System.Linq;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.Application.UIService
{
	public class MainUIService : UIServices
	{
		#region "Instance"

		private static MainUIService m_Instance;

		public static MainUIService Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new MainUIService();
				}
				return m_Instance;
			}
		}

		public MainUIService()
		{
		}

		#endregion "Instance"

		#region "Locations"

		public JArray GetLocations(string searchText, int groupNumber)
			=> GetLocations(searchText, groupNumber, UIView.Full);

		public JArray GetLocations(string searchText, int groupNumber, UIView uiView)
		{
			return MainService.GetLocations(searchText, groupNumber).ToJArray(uiView);
		}

		#endregion "Locations"

		#region "Sports"

      public void SetSelectedSport(short sportID)
      {
         SportService.SetSelectedSport(sportID);
      }

      public JArray GetSports() => GetSports(UIView.Normal);

		public JArray GetSports(UIView uiView)
		{
			JArray arr = Get(SportService.GetSports, uiView);

			return arr;
		}

		public JObject GetSport(short sportID) => GetSport(sportID, UIView.Normal);

		public JObject GetSport(short sportID, UIView uiView)
		{
			JObject obj = Get(SportService.GetSport, sportID, uiView);

			return obj;

		}

		#endregion "Sports"

		#region "Fields"

		public JObject GetField(int id) => GetField(id, UIView.Full);

		public JObject GetField(int id, UIView uiView)
		{
			return Get(SportService.GetField, id);
		}

		public JArray GetFields() => GetFields(UIView.Full);

		public JArray GetFields(UIView uiView)
		{
			return Get(SportService.GetFields);
		}

		#endregion "Fields"

		#region "ChallengeTypes"

		public JArray GetChallengeTypes() => GetChallengeTypes(UIView.Full);

		public JArray GetChallengeTypes(UIView uiView)
		{
			return Get(MainService.GetChallengeTypes);
		}

		#endregion "ChallengeTypes"

		#region "MatchTypes"

		public JArray GetMatchTypes() => GetMatchTypes(UIView.Full);

		public JArray GetMatchTypes(UIView uiView)
		{
			return Get(MatchService.GetMatchTypes);
		}

		#endregion "MatchTypes"

		#region "News"

		public JArray GetUserNews()
		{
			return MainService.GetUserNews().ToJArray();
		}

		#endregion
		#region "Fills"

		public ILocation FillLocation(JObject joLocation)
		{
			ILocation location;
			if (joLocation["LocationID"] != null && joLocation["LocationID"].Value<long>() != 0)
			{
				location = LocationFactory.New(joLocation["LocationID"].Value<long>());
			}
			else
			{
				location = LocationFactory.New();
			}
			location = JObjectToClass(joLocation, location);
			if (joLocation["User"] != null && joLocation["User"].HasValues)
				location.User = UserFactory.User(((JObject)joLocation["User"])["UserID"].Value<long>());
			if (joLocation["Sport"] != null && joLocation["Sport"].HasValues)
				location.Sport = SportFactory.New(((JObject)joLocation["Sport"])["SportID"].Value<short>());

			return location;
		}

		#endregion "Fills"
	}
}