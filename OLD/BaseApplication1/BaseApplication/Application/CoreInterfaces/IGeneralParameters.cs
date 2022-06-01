using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IGeneralParameters : IPerImplementation, IPerHost
	{
		T Get<T>(string parameterName);
		T GetDefault<T>(string parameterName);
		T Get<T>(string parameterName, T defValue);
		T GetDefault<T>(string parameterName, T defValue);

		object Get(string parameterName);
		object GetDefault(string parameterName);
		object Get(string parameterName, object defValue);
		object GetDefault(string parameterName, object defValue);

		void Set<T>(string parameterName, T value);
		void SetDefault<T>(string parameterName, T value);
	}
}