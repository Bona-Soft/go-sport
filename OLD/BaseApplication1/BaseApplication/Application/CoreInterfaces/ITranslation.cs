namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface ITranslation
	{
		string LanguageCode { get; set; }
		string Module { get; set; }
		string ConstantName { get; set; }
		string TranslationString { get; set; }
	}
}