using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.FaltaUno.Application.MainApplication.Services;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;
using Newtonsoft.Json.Linq;

namespace MYB.FaltaUno.Application.UIService
{
	public abstract class UIServices : BaseUIService
	{
		#region "Factories Shortcuts"

		public static IFieldFactory FieldFactory
		{
			get
			{
				return BaseApp.Resolve<IFieldFactory>();
			}
		}

		public static IHeadquarterFactory HeadquarterFactory
		{
			get
			{
				return BaseApp.Resolve<IHeadquarterFactory>();
			}
		}

		public static IMatchFactory MatchFactory
		{
			get
			{
				return BaseApp.Resolve<IMatchFactory>();
			}
		}

		public static IPlayerFactory PlayerFactory
		{
			get
			{
				return BaseApp.Resolve<IPlayerFactory>();
			}
		}

		public static ISportFactory SportFactory
		{
			get
			{
				return BaseApp.Resolve<ISportFactory>();
			}
		}

		public static ITeamFactory TeamFactory
		{
			get
			{
				return BaseApp.Resolve<ITeamFactory>();
			}
		}

		public static IUserFactory UserFactory
		{
			get
			{
				return BaseApp.Resolve<IUserFactory>();
			}
		}

		public static ILocationFactory LocationFactory
		{
			get
			{
				return BaseApp.Resolve<ILocationFactory>();
			}
		}

		public static IMainFactory MainFactory
		{
			get
			{
				return BaseApp.Resolve<IMainFactory>();
			}
		}
		#endregion "Factories Shortcuts"

		#region " Shortcut UI Services "

		public MatchUIService MatchUIService
		{
			get
			{
				return MatchUIService.Instance;
			}
		}

		public PlayerUIService PlayerUIService
		{
			get
			{
				return PlayerUIService.Instancia;
			}
		}

		public UserUIService UserUIService
		{
			get
			{
				return UserUIService.Instancia;
			}
		}

		public MainUIService MainUIService
		{
			get
			{
				return MainUIService.Instance;
			}
		}
      
      public HeadquarterUIService HeadquarterUIService
      {
         get
         {
            return HeadquarterUIService.Instancia;
         }
      }
      #endregion " Shortcut UI Services "

      public static class UIMappers
      {
			public static IJMapperObject GetPrivacyMapper(long userID) => GetPrivacyMapper(UserService.GetUser(userID));

         public static IJMapperObject GetPrivacyMapper(IUser user)
         {
            IJMapperObject mapper = JTool.Mapper.NewJMapperObject();

				IUserPrivacy privacy = UserService.GetUserPrivacy(user);

				foreach (var entity in privacy)
				{
					foreach (var prop in entity.Value)
					{
						if (!UserService.IsUserPrivacyShowable(user, entity.Key, prop.Key.Item2, prop.Value))
						{
							mapper.Add(entity.Key, prop.Key.Item2);
						}
					}
				}

				return mapper;
			}

			public static IJMapperObject GetCurrentUserPrivacyMapper()
			{
				IJMapperObject mapper = JTool.Mapper.NewJMapperObject();

				IUserPrivacy privacy = UserService.GetCurrentUser().UserPrivacy;

				foreach (var entity in privacy)
				{
					foreach (var prop in entity.Value)
					{
						mapper.Add(entity.Key, prop.Key.Item2);
					}
				}
				return mapper;
			}

			public static IJMapperObject GetPublicPrivacyMapper()
			{
				IJMapperObject mapper = JTool.Mapper.NewJMapperObject();

				IUserPrivacy privacy = UserService.GetDefaultPrivacy();

				foreach (var entity in privacy)
				{
					foreach (var prop in entity.Value)
					{
						mapper.Add(entity.Key, prop.Key.Item2);
					}
				}
				return mapper;
			}
		}

   }
}