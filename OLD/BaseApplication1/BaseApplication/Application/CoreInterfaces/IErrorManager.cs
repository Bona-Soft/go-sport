using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using Newtonsoft.Json.Linq;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IErrorManager : IPerWebRequest
	{
		JArray jsonError { get; }

		void AddError(string errorMesssage);

		void AddError(string errorMesssage, int errorCode);

		bool IsNotLoggedError(string errorMessage);

		bool MinCountError(JArray data, int minCount);
		bool MinCountError(JArray data, int minCount, string errorMessage);

		bool MandatoryError<T>(T field);
		bool MandatoryError<T>(T field, string errorMessage);
		bool MandatoryError<T>(Filter filter, string keyName, string errorMessage);
		bool MandatoryError<T>(JObject data, string propertyName, string errorMessage);
		bool MandatoryError<T, TID>(JObject data, string objectName, string propertyName , string errorMessage);

		bool MandatoryEmailError<T>(T field, string errorMandatoryMessage, string errorFormatMessage);
		bool MandatoryEmailError<T>(JObject data, string propertyName, string errorMandatoryMessage, string errorFormatMessage);
		

		bool EmailError(string email, string errorMessage);

		void Verify();
	}
}