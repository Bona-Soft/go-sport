using MYB.BaseApplication.Framework.Helpers;
using System;

namespace MYB.FaltaUno.Model.Interfaces.Entities
{
	public interface IContactInformation : IEntity
	{
		key<long> ContactInfoID { get; }
		string Prefix { get; set; }
		string Name { get; set; }
		string SecondNames { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string Alias { get; set; }
		string MobilNumber { get; set; }
		string LinePhoneNumber { get; set; } 
		string WebAddress { get; set; }
		DateTime BirthDate { get; set; }
	}
}