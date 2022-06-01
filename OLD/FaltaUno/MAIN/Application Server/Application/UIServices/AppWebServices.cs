using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;
using static MYB.FaltaUno.Application.UIService.UIServices;

namespace MYB.FaltaUno.Application.UIService
{
	public abstract class AppWebServices : BaseWebService
	{
		#region " Override Methods "

		//TODO: Interceptor para manejar excepciones de servidor.

		protected override void OnLoad(EventArgs e)
		{
			if(BaseApp.ContainerManager == null)
			{
				BaseApp.Initializate();
			}

			if (!AppSession.IsLogged)
			{
				/*
				 * TODO:
				string session = Request[Usuario.SessionName];
				if (session != null && session != string.Empty)
				{
					if (AppSession.SetUserBySession(session))
					{
						base.OnLoad(e);
						return;
					}
				}
				if (!Request.Path.Contains("Login.aspx"))
				{
					Response.Redirect("/", true);
				}
				*/
			}

         MainUIService.SetSelectedSport(GetDataSportID());
			base.OnLoad(e);
		}

		protected override void OnError(EventArgs e)
		{
			base.OnError(e);
		}

		#endregion " Override Methods "

		public UIView RequestView
		{
			get
			{
				return GetRequestView();
			}
		}

      public short GetMandatoryDataSportID()
      {
         short _sportID = GetDataSportID();
         if(_sportID == 0)
         {
            ErrorManager.AddError(TranslationManager.Get("PlayerEnable", "SportMandatory", "Sport not selected"));
         }
         return _sportID;
      }

		public short GetDataSportID()
		{
			short _sportID = default(short);
			try
			{
            _sportID = getRequestParam<short>("sportID",0);
            if(_sportID == 0)
            {
               if (Data.ContainsKey("SportID"))
               {
                  _sportID = Data["SportID"].Value<short>();
               }
               else
               {
                  _sportID = Data["sport"] != null && Data["sport"].ToString() != String.Empty ? Data["sport"]["SportID"].ToDefType<short>() : default(short);
                  if (_sportID == default(short))
                  {
                     _sportID = Data["Sport"] != null && Data["Sport"].ToString() != String.Empty ? Data["Sport"]["SportID"].ToDefType<short>() : default(short);
                  }
               }
            }				
			}
			catch
			{
				return default(short);
			}
			return _sportID;
		}

		public UIView GetRequestView()
		{
			UIView viewName = UIView.Full;
			if (Request.QueryString["ViewType"] != null)
			{
				try
				{
					viewName = (UIView)(short)Convert.ChangeType(Request.QueryString["ViewType"], typeof(short));
				}
				catch
				{
					viewName = UIView.Short;
				}
			}

			return viewName;
		}

		#region " Shortcut UI Services "

		public MatchUIService MatchUIService
		{
			get
			{
				return MatchUIService.Instance;
			}
		}

		public PlayerUIService PlayerUIService
		{
			get
			{
				return PlayerUIService.Instancia;
			}
		}

		public UserUIService UserUIService
		{
			get
			{
				return UserUIService.Instancia;
			}
		}

		public MainUIService MainUIService
		{
			get
			{
				return MainUIService.Instance;
			}
		}
        public HeadquarterUIService HeadquarterUIService
        {
            get
            {
                return HeadquarterUIService.Instancia;
            }
        }

		public DebugUIService DebugUIService
		{
			get
			{
				return DebugUIService.Instance;
			}
		}
		#endregion " Shortcut UI Services "
	}
}