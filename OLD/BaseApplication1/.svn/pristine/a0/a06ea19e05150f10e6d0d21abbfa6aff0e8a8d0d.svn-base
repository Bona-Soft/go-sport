using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Collections.Generic;
using System.Data;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface ITranslationManager : ISingleton
	{
		/// <summary>
		///  The list of all Translation loaded. Use function Add to add translation to this list.
		/// </summary>
		List<ITranslation> Translations { get; }

		/// <summary>
		///   Add a translation to the Translations list.
		/// </summary>
		/// <param name="translation"> The translation that has the same struct of the ITranslation interfaz.</param>
		/// <returns> Bool: result if translation was added correctly. Exceptions controled as false </returns>
		bool Add(ITranslation translation);

		/// <summary>
		///   Add a translation to the Translations list.
		/// </summary>
		/// <param name="languageCode"> The code of the language from this translation. i.e. EN, ES, etc.</param>
		/// <param name="constantName"> The name of translation. i.e. Register_InvalidEmail, Login_PasswordInvalid.</param>
		/// <param name="translationString"> The string translated to the language that you want to show. i.e. This email is invalid, La contraseña es incorrecta</param>
		/// <returns> Bool: result if translation was added correctly. Exceptions controled as false </returns>
		bool Add(string languageCode, string constantName, string translationString);

		/// <summary>
		///   Add multiple translation to the Translations list.
		/// </summary>
		/// <param name="translation"> The dataset that want to add at translation list. Must has rows "LanguageCode", "ConstantName", "Translation" </param>
		/// <returns> Amount of translation added. 0 if no translation was added.</returns>
		long Add(DataSet translationDS);

		/// <summary>
		///   Get a specific translation with a name and language code
		/// </summary>
		/// <param name="languageCode"> The code of the language from this translation. i.e. EN, ES, etc. </param>
		/// <param name="module">  The module or group of translation. i.e. RegisterForm, UserGroup. </param>
		/// <param name="constantName">  The name of translation. i.e. Register_InvalidEmail, Login_PasswordInvalid. </param>
		/// <param name="defaultString">  The string to return if the language code and cosntant combination was not find. </param>
		/// <returns> string: the translated string </returns>
		string Get(string languageCode, string module, string constantName, string defaultString);

		/// <summary>
		///   Delete all translation of the Translation list.
		/// </summary>
		bool Clean();

		/// <summary>
		///   Flag: True if translations was already loaded at least one time.
		/// </summary>
		bool TranslationLoad { get; set; }
	}
}