using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Factories;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
	public static class Services
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

		#region "Repositories Services Shortcuts"

		public static IBaseRepositoryService BaseRepoService
			=> BaseApp.BaseRepoService;

		public static IFieldRepositoryService FieldRepoService
		{
			get
			{
				return BaseApp.Resolve<IFieldRepositoryService>();
			}
		}

		public static IHeadquarterRepositoryService HeadquarterRepoService
		{
			get
			{
				return BaseApp.Resolve<IHeadquarterRepositoryService>();
			}
		}

		public static IMatchRepositoryService MatchRepoService
		{
			get
			{
				return BaseApp.Resolve<IMatchRepositoryService>();
			}
		}

		public static IPlayerRepositoryService PlayerRepoService
		{
			get
			{
				return BaseApp.Resolve<IPlayerRepositoryService>();
			}
		}

		public static ISportRepositoryService SportRepoService
		{
			get
			{
				return BaseApp.Resolve<ISportRepositoryService>();
			}
		}

		public static ITeamRepositoryService TeamRepoService
		{
			get
			{
				return BaseApp.Resolve<ITeamRepositoryService>();
			}
		}

		public static IUserRepositoryService UserRepoService
		{
			get
			{
				return BaseApp.Resolve<IUserRepositoryService>();
			}
		}

		public static IMainRepositoryService MainRepoService
		{
			get
			{
				return BaseApp.Resolve<IMainRepositoryService>();
			}
		}

		#endregion "Repositories Shortcuts"
	}
}