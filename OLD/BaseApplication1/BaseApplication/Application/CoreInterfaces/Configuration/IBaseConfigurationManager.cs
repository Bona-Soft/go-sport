using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseConfigurationManager<TConfigElement>
	{
		IEnumerable<TConfigElement> GetSectionElementList(string sectionName, string elementName);
	}

	public interface IBaseConfigurationManager : ISingleton
	{
		string PhysicalAppDir { get; }
		string VirtualAppDir { get; }
		string UrlHost { get; }
		string LogDir { get; }
		IConfigConnectionSection Connection { get; }

		string ConnectionString(string Host);

		string ConnectionString();

		string DomainID(string urlHost);
	}
}