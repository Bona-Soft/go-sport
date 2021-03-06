using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System.Collections.Generic;
using System.Linq;
using static MYB.FaltaUno.Application.MainApplication.Services.Services;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
   public class MainService 
	{

      public static short? GetActiveSport()
      {
         if (UserService.GetCurrentUser() != null && UserService.GetCurrentUser().LastActiveSport != null)
         {
            return UserService.GetCurrentUser().LastActiveSport.SportID;
         }
         return null;
      }

		public static List<object> GetUserNews()
		{
         short sportID = SportService.GetSelectedSportID();

         Filter<FMatchSearch> matchSearch = Filter.New<FMatchSearch>();
			Filter<FPlayerSearch> playerSearch = Filter.New<FPlayerSearch>();
						
			matchSearch["SportID"] = sportID;

			playerSearch["UserID"] = UserService.GetCurrentUser().UserID;
			playerSearch["UserID"] = sportID;

			Log.I($"Calling SearchMatch");

			List<IMatch> matches = MatchService.SearchMatch(matchSearch);
			List<IPlayer> players = PlayerService.SearchPlayers(playerSearch);

			Log.I($"SearchMatches Executed: {matches.Count} - SearchPlayers Executed: {players.Count}");

			List<object> result = new List<object>();
			result.AddRange(matches);
			result.AddRange(players);

			return result;
		}

		public static List<ILocation> GetLocations(string searchText, int groupNumber)
      {
         long? userID = null;
         short? sportID = SportService.GetSelectedSportID();

         if (sportID == 0) sportID = null;

         return Services.MainRepoService.GetLocations(searchText, sportID, groupNumber, userID);
      }

      public static ILocation GetLocation(long locationID)
      {
         return Services.MainRepoService.GetLocation(locationID);
      }

      public static ILocation SaveLocation(ILocation location)
      {
         long locationID = Services.MainRepoService.SaveLocation(location);
         ILocation auxLocation = Services.LocationFactory.New(locationID);

         //TODO: Hacer en base un ObjectToObject O merge o extend the entidades, o mejor darle un factory a Location que le pases un ILocation y un id.
         auxLocation.Sport = location.Sport;
         auxLocation.User = location.User;
         auxLocation.GroupMemberID = location.GroupMemberID;
         auxLocation.Lat = location.Lat;
         auxLocation.Lng = location.Lng;
         auxLocation.PostalCode = location.PostalCode;
         auxLocation.City = location.City;
         auxLocation.Country = location.Country;
         auxLocation.Address = location.Address;
         auxLocation.Value = location.Value;
         auxLocation.Display = location.Display;
         auxLocation.State = location.State;

         return auxLocation;
      }

		public static IComment SaveComment(IComment comment)
		{
			return Services.MainRepoService.SaveComment(comment);
		}

		#region "Request State"
		private static List<IRequestStates> _RequestStates;

		public static List<IRequestStates> GetRequestStates()
		{
			if (_RequestStates == null)
			{
				LoadRequestStates();
			}
			return _RequestStates;
		}
		public static IRequestStates GetRequestState(RequestState id)
		{
			if (_RequestStates == null)
			{
				LoadRequestStates();
			}
			IRequestStates state = _RequestStates.FirstOrDefault(request => request.RequestStateID == id);

         if(state != null)
         {
            state.Description = BaseApp.TranslationManager.Get("RequestStates", "Description" + state.Value, state.Description);
            state.DefaultComment = BaseApp.TranslationManager.Get("RequestStates", "DefaultComment" + state.Value, state.DefaultComment);
         }

         return state;
		}

		public static void LoadRequestStates()
		{
			_RequestStates = Services.MatchRepoService.GetRequestStates();
		}
		#endregion

		#region "Challenge Types"

		private static List<IChallengeType> _ChallengeTypes;

      public static List<IChallengeType> GetChallengeTypes()
      {
         if (_ChallengeTypes == null)
         {
            LoadChallengeTypes();
         }

         return _ChallengeTypes;
      }

      public static IChallengeType GetChallengeType(short id)
      {
         if (_ChallengeTypes == null)
         {
            LoadChallengeTypes();
         }

         return _ChallengeTypes.FirstOrDefault(challenge => challenge.ChallengeTypeID == id);
      }

      public static void LoadChallengeTypes()
      {
         _ChallengeTypes = Services.MatchRepoService.GetChallegeTypes();
      }

      #endregion "Challenge Types"
   }
}