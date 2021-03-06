using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.Application.UIService
{
	public class PlayerUIService : UIServices
	{
		private static PlayerUIService m_Instancia;

		public static PlayerUIService Instancia
		{
			get
			{
				if (m_Instancia == null)
				{
					m_Instancia = new PlayerUIService();
				}
				return m_Instancia;
			}
		}

		public PlayerUIService()
		{
			Views = new Dictionary<UIView, JObject>{
				{ UIView.Custom1, JObject.Parse(@"{
						PlayerID: 3,
						Name: 'Nombre de Match'
					}")
				}
			};
		}

		public JArray GetTopPlayers()
		{
			return PlayerService.GetTopPlayers().ToJArray(UIMappers.GetCurrentUserPrivacyMapper());
		}

		public JObject GetPlayer(long playerID, UIView uiView)
		{
			IPlayer player = PlayerService.GetPlayer(playerID);
			JObject jobj = player.ToJObject(uiView, UIMappers.GetPrivacyMapper(player.User));
			
			//TODO: Corregir ToJObject para que pueda tomar fechas tipo null DateTime?
			if (player.User.BirthDate != null)
			{
				jobj["User"]["BirthDate"] = player.User.BirthDate.ToDefType<DateTime>();
			}

			return jobj;
		}

		public JArray GetFrecuentlyPlayers()
		{
			return PlayerService.GetFrecuentlyPlayers().ToJArray(UIMappers.GetCurrentUserPrivacyMapper());
		}

		public JArray GetRecommendedPlayers(long matchID)
		{
			return PlayerService.GetRecommendedPlayers(MatchFactory.New(matchID)).ToJArray(UIMappers.GetCurrentUserPrivacyMapper());
		}

		public JObject CreatePlayer(JObject joPlayer)
		{
			IPlayer entity = Fill(joPlayer);
			entity = PlayerService.CreatePlayer(entity);

			return entity.ToJObject();
		}

		public JArray SearchPlayers(Filter filter) => SearchPlayers(filter, UIView.Full);

		public JArray SearchPlayers(Filter filter, UIView uiView)
		{
			Filter<FPlayerSearch> filterPlayers = filter.Convert<FPlayerSearch>();

			var players = PlayerService.SearchPlayers(filterPlayers);
			var jPlayers = new JArray();

			foreach (var player in players)
			{
				var jPlayer = player.ToJObject(uiView, UIMappers.GetPublicPrivacyMapper()); //UIMappers.GetPrivacyMapper(player.User)
				jPlayers.Add(jPlayer);
			}
			return jPlayers;
		}

		public JObject GetPlayer(long userID = 0, short sportID = 0, UIView uiView = UIView.Full)
		{
			return PlayerService.GetPlayer(userID, sportID).ToJObject(uiView, UIMappers.GetPrivacyMapper(userID));
		}

		public JArray GetCurrentUserMatchPlayerRequests()
		{
			List<IMatchPlayerRequest> mprs = PlayerService.GetCurrentUserMatchPlayerRequests();
			JArray arr = mprs.ToJArray();
			for (int i = 0; i < mprs.Count; i++)
			{
				arr[i]["Match"]["PlayersRequest"] = mprs[i].Match.PlayersRequest.ToJArray();
			}
			return arr;
		}

		public JObject EnablePlayer(long playerID, bool enabled)
		{
         JObject obj = new JObject();
         obj["PlayerID"] = playerID;
         obj["Active"] = PlayerService.EnablePlayer(playerID, enabled).ToJObject();
			return obj;
		}

		public IPlayer Fill(JObject joPlayer)
		{
			IPlayer entity = UIServices.PlayerFactory.New();
			entity = joPlayer.ToClass(entity);
			return entity;
		}

		public JObject Fill(IPlayer player, UIView uiView = UIView.Full)
		{
			JObject jPlayer = player.ToJObject(uiView);
			jPlayer["User"] = player.User.ToJObject(uiView);
			return jPlayer;
		}
	}
}