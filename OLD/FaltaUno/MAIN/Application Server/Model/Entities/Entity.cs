using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using MYB.FaltaUno.Model.Interfaces.Entities;

namespace MYB.FaltaUno.Model.Entities
{
	public class Entity : BaseEntity, IEntity
	{
		#region "Repositories Shortcuts"

		public IFieldRepository FieldRepository
		{
			get
			{
				return BaseApp.Resolve<IFieldRepository>();
			}
		}

		public IHeadquarterRepository IHeadquarterRepository
		{
			get
			{
				return BaseApp.Resolve<IHeadquarterRepository>();
			}
		}

		public IMatchRepository MatchRepository
		{
			get
			{
				return BaseApp.Resolve<IMatchRepository>();
			}
		}

		public IPlayerRepository PlayerRepository
		{
			get
			{
				return BaseApp.Resolve<IPlayerRepository>();
			}
		}

		public ISportRepository SportRepository
		{
			get
			{
				return BaseApp.Resolve<ISportRepository>();
			}
		}

		public ITeamRepository TeamRepository
		{
			get
			{
				return BaseApp.Resolve<ITeamRepository>();
			}
		}

		public IUserRepository UserRepository
		{
			get
			{
				return BaseApp.Resolve<IUserRepository>();
			}
		}

		#endregion "Repositories Shortcuts"
	}
}