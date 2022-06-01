using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Data;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public static class TranslationManagerExt
	{
		/// <summary>
		///   Get a specific translation of an specific constant with the language of the session.
		/// </summary>
		/// <param name="constantName">  The name of translation. i.e. Register_InvalidEmail, Login_PasswordInvalid. </param>
		/// <returns> string: the translated string, null if not exists </returns>
		public static string Get(this ITranslationManager tm, string constantName)
		{
			return tm.Get(BaseApp.Session().Language, constantName, constantName);
		}

		/// <summary>
		///   Get a specific translation of an specific constant with the language of the session.
		/// </summary>
		/// <param name="constantName">  The name of translation. i.e. Register_InvalidEmail, Login_PasswordInvalid. </param>
		/// <param name="defaultString">  The string to return if the language code and cosntant combination was not find. </param>
		/// <returns> string: the translated string, return defaultString if not exists </returns>
		public static string Get(this ITranslationManager tm, string constantName, string defaultString)
		{
			return tm.Get(BaseApp.Session().Language, "", constantName, defaultString);
		}

		public static string Get(this ITranslationManager tm, string module, string constantName, string defaultString)
		{
			return tm.Get(BaseApp.Session().Language, module, constantName, defaultString);
		}
		/// <summary>
		///   Clean the translations and reload it from the table.
		/// </summary>
		/// <returns> The number of translations loaded. </returns>
		public static long Init(this ITranslationManager tm)
		{
			tm.Clean();
			try
			{
				long count = tm.Add(BaseApp.DB.StoredProcedure<DataSet>("GetTranslations").Execute());
				tm.TranslationLoad = true;
				return count;
			}
			catch (Exception ex)
			{
				return 0;
			}
		}
	}
}