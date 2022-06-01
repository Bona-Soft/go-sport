using MYB.BaseApplication.Application.CoreInterfaces;

namespace MYB.BaseApplication.Infrastructure.TranslationManager
{
	public class Translation : ITranslation
	{
		public string LanguageCode { get; set; }
		public string Module { get; set; }
		public string ConstantName { get; set; }
		public string TranslationString { get; set; }

		public Translation()
		{

		}

		public Translation(string languageCode, string constantName, string translationString)
		{
			LanguageCode = languageCode;
			Module = "";
			ConstantName = constantName;
			TranslationString = translationString;
		}

		public Translation(string languageCode, string module, string constantName, string translationString)
		{
			LanguageCode = languageCode;
			Module = module;
			ConstantName = constantName;
			TranslationString = translationString;
		}
	}
}