using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Entities.Filters;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.Application.UIService
{
	public class MatchUIService : UIServices
	{
		#region "Instance"

		private static MatchUIService m_Instance;

		public static MatchUIService Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new MatchUIService();
				}
				return m_Instance;
			}
		}

		public MatchUIService()
		{
			Views = new Dictionary<UIView, JObject>{
				{ UIView.Custom1, JObject.Parse(@"{
						MatchID: 3,
						Name: 'Nombre de Match',
						MaxPlayers: 24,
						Players: [{PlayerID: 1, Name: 'Juancito'}],
						Oportunidades: [],
						Field: { FieldID: null },
						CuentaCategoria: { CuentaCategoriaId: null, Nombre: null },
						Nombre: null,
						Codigo: null,
						Website: null,
						Empleados: 'un pebete',
						Propietario: null,
						Telefono: null,
						Descripcion: null,
						CodigoSic: null
					}")
				}
			};
		}

		#endregion "Instance"

		public JObject GetMatch(long matchID, UIView uiView)
		{
			IMatch match = MatchService.GetMatch(matchID);
			JObject obj = match.ToJObject(uiView);

			return obj;
		}

		public JObject GetCompleteMatch(long matchID)
		{
			IMatch match = MatchService.GetFullMatch(matchID);
			JObject obj = match.ToJObject(UIView.Full);

			//TODO: HIGH: Pasar a teamRequest y Verificar el ObjListToJObject + summary

			
         obj["MatchPlayerOwner"] = match.MatchPlayerOwner.ToJObject(UIView.Full, UIMappers.GetPrivacyMapper(match.MatchPlayerOwner.User));
         obj["TeamsRequest"] = match.TeamsRequest.ToJArray(UIView.Full);
			obj["PlayersRequest"] = Fill(match.PlayersRequest, UIView.Full);

			return obj;
		}

		public JArray Fill(List<IMatchPlayerRequest> matchPlayerRequests, UIView uiView)
		{
			JArray jar = new JArray();
			foreach (IMatchPlayerRequest _mpr in matchPlayerRequests)
			{
				JObject obj = new JObject();
				if (_mpr.Comment != null && !(_mpr.Comment.CommentID > 0))
				{
					_mpr.Comment = null;
				}
				obj = ObjToJObj(_mpr, UIView.Full);
				obj["PlayerReceiver"]["User"] = _mpr.PlayerReceiver.User.ToJObject(UIView.Full, UIMappers.GetPrivacyMapper(_mpr.PlayerReceiver.User));
				obj["PlayerSender"]["User"] = _mpr.PlayerSender.User.ToJObject(UIView.Full, UIMappers.GetPrivacyMapper(_mpr.PlayerSender.User));
				jar.Add(obj);
			}

			return jar;
		}

		public DateTime ConcatDateTime(DateTime date, DateTime time)
		{
			DateTime dt = new DateTime();

			return dt;
		}

		public JArray SearchMatches(Filter filter, UIView uiView = UIView.Full)
		{
			Filter<FMatchSearch> filterMatch = filter.Convert<FMatchSearch>();

			return MatchService.FilterMatches(filterMatch).ToJArray(uiView);
		}

		public JArray GetCurrentUserMatches(Filter filter)
		{
         Filter<FUserMatches> _filter = filter.Convert<FUserMatches>();
			return MatchService.GetCurrentUserMatches(_filter).ToJArray();
		}
		public JArray GetCurrentUserOwnerMatches()
		{
			return MatchService.GetCurrentUserOwnerMatches().ToJArray();
		}

		public JObject CreateMatch(JObject joMatch)
		{
			JObject joResult = new JObject();
			IMatch matchEntity = Fill(joMatch);

			joResult["MatchID"] = MatchService.CreateMatch(matchEntity);

			return joResult;
		}

		public JObject UpdateMatch(JObject joMatch)
		{
			JObject joResult = new JObject();
			IMatch matchEntity = Fill(joMatch, joMatch["MatchID"].Value<long>());

			//matchEntity.MatchPlayerOwner = PlayerUIService.Fill((JObject)joMatch["MatchPlayerOwner"]);

			joResult["MatchID"] = MatchService.UpdateMatch(matchEntity);

			return joResult;
		}

		public JArray GetMatchPlayerRequests(long matchID)
		{
			List<IMatchPlayerRequest> mpRequests = MatchService.GetMatchPlayerRequests(matchID);
			JArray jarr = mpRequests.ToJArray();

			return jarr;
		}

		public JArray SendMatchPlayerRequests(JArray data)
		{
			List<IMatchPlayerRequest> matchPlayerRequests = new List<IMatchPlayerRequest>();
			foreach (JObject jMpr in data)
			{
				if (jMpr.IsNullOrEmpty("MatchPlayerRequestID"))
					jMpr["MatchPlayerRequestID"] = 0;

				IMatchPlayerRequest mpr = jMpr.ToClass<IMatchPlayerRequest, long>("MatchPlayerRequestID", MatchFactory.NewMatchPlayerRequest);

				Fill(jMpr, mpr);

				matchPlayerRequests.Add(mpr);
			}
			matchPlayerRequests = MatchService.SendMatchPlayerRequests(matchPlayerRequests);

			return Fill(matchPlayerRequests, UIView.Full);
		}

		public JObject SendMatchPlayerRequest(JObject data)
		{
         //TODO: Pasar a metodo privada
         if (data.IsNullOrEmpty("MatchPlayerRequestID"))
            data["MatchPlayerRequestID"] = 0;

         IMatchPlayerRequest mpr = data.ToClass<IMatchPlayerRequest, long>("MatchPlayerRequestID", MatchFactory.NewMatchPlayerRequest);

			Fill(data, mpr);

         mpr = MatchService.SendMatchPlayerRequest(mpr);

			return mpr.ToJObject(UIView.Full);
		}

		public IMatch Fill(JObject joMatch) => Fill(joMatch, MatchFactory.New());

		public IMatch Fill(JObject joMatch, long matchID) => Fill(joMatch, MatchFactory.New(matchID));

		public IMatch Fill(JObject joMatch, IMatch matchEntity)
		{
			matchEntity = joMatch.ToClass(matchEntity);

			matchEntity.Sport = joMatch["Sport"].ToClass<ISport, short>("SportID", SportFactory.New);
			matchEntity.Field = joMatch["Field"].ToClass<IField, int>("FieldID", FieldFactory.New);
			matchEntity.MatchType = joMatch["MatchType"].ToClass<IMatchType, short>( "MatchTypeID", MatchFactory.NewMatchType);
			matchEntity.ChallengeType = joMatch["ChallengeType"].ToClass<IChallengeType, short>("ChallengeTypeID", MatchFactory.NewChallengeType);

			if (!joMatch["LocationGroup"].IsNullOrEmpty())
			{
				matchEntity.Location = MainUIService.FillLocation((JObject)(joMatch["LocationGroup"]["Location"]));
				if (!joMatch["LocationGroup"]["Headquarter"].IsNullOrEmpty())
					matchEntity.Headquarter = HeadquarterUIService.Fill((JObject)(joMatch["LocationGroup"]["Headquarter"]));
			}

			if (!joMatch["playersQty"].IsNullOrEmpty())
			{
				matchEntity.MaxPlayers = joMatch["playersQty"]["MaxPlayers"].Value<short>();
				matchEntity.MinPlayers = joMatch["playersQty"]["MinPlayers"].Value<short>();
			}
			else
			{
				matchEntity.MaxPlayers = 0;
				matchEntity.MinPlayers = 0;
			}

			matchEntity.MatchDateTime = matchEntity.MatchDateTime.ToUniversalTime();

			return matchEntity;
		}

		public IMatchPlayerRequest Fill(JObject source, IMatchPlayerRequest destination)
		{
			if (!source.IsNullOrEmpty("MatchPlayerRequestState", "RequestStateID"))
			{
				if (source["MatchPlayerRequestState"]["RequestStateID"].IsNumber())
				{
					destination.MatchPlayerRequestState = source["MatchPlayerRequestState"].ToClass<IRequestStates, short>("RequestStateID", MainFactory.NewRequestState);
				}
				else
				{
					destination.MatchPlayerRequestState = source["MatchPlayerRequestState"].ToClass<IRequestStates, string>("RequestStateID", MainFactory.NewRequestState);
				}
			}

			if (!source.IsNullOrEmpty("Comment", "Text")
				&& source["Comment"]["CommentID"].ValueDef<long>() != -1)
			{
				destination.Comment = source["Comment"].ToClass<IComment, long>("CommentID", MainFactory.NewComment);
			}
			else
			{
				destination.Comment = null;
			}

			destination.Match = MatchFactory.New(source["Match"]["MatchID"].Value<long>());

			destination.PlayerReceiver = PlayerFactory.New(source["PlayerReceiver"]["PlayerID"].Value<long>());

			return destination;
		}
	}
}