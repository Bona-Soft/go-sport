using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System.Collections.Generic;
using System.Data;

namespace MYB.BaseApplication.Infrastructure.TranslationManager
{
	public class TranslationManager : ITranslationManager
	{
		public List<ITranslation> Translations { get; private set; }
		public bool TranslationLoad { get; set; }
		
		
		public TranslationManager()
		{
			Translations = new List<ITranslation>();
			TranslationLoad = false;
		}

		public bool Add(ITranslation translation)
		{
			try
			{
				if (translation != null && translation.LanguageCode != null && translation.Module != null && translation.ConstantName != null)
				{
					Translations.Add(translation);
					return true;
				}
			}
			catch { }
			return false;
		}

		public bool Add(string languageCode, string constantName, string translationString)
			=> Add(languageCode, "", constantName, translationString);

		public bool Add(string languageCode, string module, string constantName, string translationString)
		{
			try
			{
				if (languageCode != null && module != null && constantName != null)
				{
					Translations.Add(new Translation(languageCode, module, constantName, translationString));
					return true;
				}
			}
			catch { }
			return false;
		}

		public long Add(DataSet translationDS)
		{
			long rowCount = 0;
			foreach (DataRow row in translationDS.Tables[0].Rows)
			{
				if (row["LanguageCode"] != null && row["ConstantName"] != null)
				{
					Add(row["LanguageCode"].ToDefString(), row["Module"].ToDefString(), row["ConstantName"].ToDefString(), row["Translation"].ToDefString());
					rowCount++;
				}
			}
			return rowCount;
		}

		public string Get(string languageCode, string module, string constantName, string defaultString)
		{
			ITranslation trans = Translations.Find(x => x.ConstantName == constantName && x.LanguageCode == languageCode);
			if (trans != null)
				return trans.TranslationString;
			return defaultString;
		}

		public bool Clean()
		{
			try
			{
				Translations = new List<ITranslation>();
			}
			catch
			{
				return false;
			}
			return true;
		}
	}
}