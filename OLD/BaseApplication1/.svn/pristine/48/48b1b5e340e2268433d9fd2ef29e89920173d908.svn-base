using Castle.DynamicProxy;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseService : BaseLoggable
	{
		#region " Shortcuts "

		public static int CurrentImplementation
		{
			get
			{
				return BaseApp.CurrentImplementation;
			}
		}

		public static IContainerManager ContainerManager
		{
			get
			{
				return BaseApp.ContainerManager;
			}
		}

		public static IAppSession Session
		{
			get
			{
				return BaseApp.Session();
			}
		}

		public static IDataBase DB
		{
			get
			{
				return BaseApp.DB;
			}
		}

		public static IWSP WSP
		{
			get
			{
				return BaseApp.WSP;
			}
		}

		public static IVirtualPagesManager VirtualPageManager
		{
			get
			{
				return BaseApp.VirtualPageManager;
			}
		}

		public static IBaseUserManager BaseUserManager
		{
			get
			{
				return BaseApp.BaseUserManager;
			}
		}

		public static IBaseConfigurationManager ConfigManager
		{
			get
			{
				return BaseApp.BaseConfigManager();
			}
		}

		public static IErrorManager ErrorManager
		{
			get
			{
				return BaseApp.ErrorManager;
			}
		}

		public static ITranslationManager TranslationManager
		{
			get
			{
				return BaseApp.TranslationManager;
			}
		}

		public static IBaseUser BaseUser(long userID, int implementationID)
		{
			return BaseApp.BaseUser(userID, implementationID);
		}

		public static IBaseUser BaseUser(long userID)
		{
			return BaseApp.BaseUser(userID);
		}

		public static IBaseUser BaseUser()
		{
			return Session.User;
		}

		public static Interface Resolve<Interface>()
		{
			return BaseApp.Resolve<Interface>();
		}

		public static UIClass UI<UIClass>() where UIClass : IBaseUIService
		{
			return BaseApp.UI<UIClass>();
		}

		#endregion " Shortcuts "

		#region " Interceptors "

		public virtual void BeginInterceptor(string methodName)
		{
		}

		public virtual void AfterInterceptor(string methodName)
		{
		}

		public virtual void Interceptor(IInvocation invocation)
		{
		}

		#endregion " Interceptors "
	}
}