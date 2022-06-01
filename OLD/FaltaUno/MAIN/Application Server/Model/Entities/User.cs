using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Model.Entities
{
	public class User : Entity, IUser
	{
		public key<long> UserID { get; private set; }
		public ISport DefaultSport { get; set; }
		public ISport LastActiveSport { get; set; }
		public IUserPrivacy UserPrivacy { get; set; }
		public DateTime? BirthDate { get; set; }
		public short Height { get; set; }
		public short Weight { get; set; }

		public string Avatar { get; set; }

		public string Prefix { get; set; }
		public string Name { get; set; }
		public string SecondNames { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Alias { get; set; }
		public string MobileNumber { get; set; }
		public string LinePhoneNumber { get; set; }
		public string WebAddress { get; set; }
		public string DefaultLanguage
		{
			get
			{
				return Base.DefaultLanguage;
			}
			set
			{
				Base.DefaultLanguage = value;
			}
		}
		
		public string CompleteName
		{
			get
			{
				return this.Name + " " + this.LastName;
			}
		}

		public List<IPlayer> Players { get; set; }

		public bool Initialized { get; set; }

		public User(long userID)
		{
			UserID = userID;
			Initialized = false;
		}

		public void Init()
		{
			Initialized = true;
		}

		

		public IBaseUser Base
		{
			get
			{
				return BaseApp.BaseUser(UserID);
			}
		}

	}
}