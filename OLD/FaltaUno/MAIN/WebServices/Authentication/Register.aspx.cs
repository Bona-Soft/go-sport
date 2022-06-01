using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.MainApplication;
using MYB.FaltaUno.Application.UIService;
using System;

namespace MYB.FaltaUno.WebServices.Authentication
{
	public partial class Register : AppWebServices
	{
		protected override void ProcessPOST()
		{
			//Hacer register MainApplication.RegisterUser(username, password);
			string mail, password, name, lastName;
			short sportID;

			Tuple<int, string> passwordErrorMessage;

			name = Data["name"].ToDefString();
			lastName = Data["lastName"].ToDefString();
			mail = Data["mail"].ToDefString();
			sportID = GetDataSportID();
			password = Data["password"].ToDefString();

			ErrorManager.MandatoryError(name, TranslationManager.Get("MandatoryName"));
			ErrorManager.MandatoryEmailError(mail, TranslationManager.Get("MandatoryEmail"), TranslationManager.Get("InvalidEmail"));
			ErrorManager.MandatoryError(sportID, TranslationManager.Get("MandatorySport"));
			if(!ErrorManager.MandatoryError(password, TranslationManager.Get("MandatoryPassword")))
			{
				if (!Validator.ValidatePassword(password, out passwordErrorMessage))
				{
					ErrorManager.AddError(TranslationManager.Get(passwordErrorMessage.Item2));
				}
			}

         ErrorManager.Verify();

			long resultCode = UserUIService.RegisterUser(mail, password, name, lastName, sportID);

			switch (resultCode)
			{
				case -1:
					ErrorManager.AddError(TranslationManager.Get("EmailAlreadyRegister"), 1);
					break;
			}
			
			ErrorManager.Verify();
		}
	}
}