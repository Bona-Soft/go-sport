using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using System;
using System.Collections.Generic;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IUser : IEntity, IPerUser
	{
		key<long> UserID { get; }

		IBaseUser Base { get; }
		ISport DefaultSport { get; set; }
		ISport LastActiveSport { get; set; }

		IUserPrivacy UserPrivacy { get; set; }
		DateTime? BirthDate { get; set; }
		short Height { get; set; }
		short Weight { get; set; }
	
		string Avatar { get; set; }

		string Prefix { get; set; }
		string Name { get; set; }
		string SecondNames { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string Alias { get; set; }
		string MobileNumber { get; set; }
		string LinePhoneNumber { get; set; }
		string WebAddress { get; set; }
		string DefaultLanguage { get; set; }

		List<IPlayer> Players { get; set; }

		string CompleteName { get; }

		void Init();

		bool Initialized { get; set; }
	}
}