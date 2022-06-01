using MYB.BaseApplication.Application.CoreApplication.AppInits;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Infrastructure.Windsor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace MYB.BaseApplication.Application.CoreApplication
{
   public class BaseApp : IBaseApp
   {
      private static IContainerManager _ContainerManager;

      #region " Constructors Methods "

      public BaseApp()
      {
      }

      #endregion " Constructors Methods "

      #region " Public Properties "

      public static int CurrentImplementation
      {
         get
         {
            try
            {
               return ContainerManager.Resolve<IImplementation>().GetImplementation();
            }
            catch
            {
               return 0;
            }
         }
      }

      public static string DefaultLanguage
      {
         get
         {
            try
            {
               return ContainerManager.Resolve<IImplementation>().GetDefaultLanguage();
            }
            catch
            {
               return null;
            }
         }
      }

		public static IBaseSchedulerJobManager JobManager
		{
			get
			{
				return ContainerManager.Resolve<IBaseSchedulerJobManager>();
			}
		}

		public static IContainerManager ContainerManager
      {
         get
         {
            if (_ContainerManager == null)
            {
               try
               {
                  InitGenericApplication.Application_Start();
               }
               catch (Exception ex)
               {
                  throw new Exception(TranslationManager.Get("ContainerManagerNotInitializated", "Initializate method must be called before using AppServerController. Error: ") + ex.Message);
               }
            }

            return _ContainerManager;
         }
      }

      public static IMailManager MailManager
      {
         get
         {
            return ContainerManager.Resolve<IMailManager>();
         }
      }

      public static IAppSession Session()
      {
			return ContainerManager.Resolve<IAppSession>();
      }

		public static IDataBase DB
      {
         get
         {
            return ContainerManager.Resolve<IDataBase>();
         }
      }


      public static IDataBase DataBase<T>(T connectionData)
      {
         Dictionary<string, T> constructParameters = new Dictionary<string, T>
          {
              {"connectionData" , connectionData},
          };
         return ContainerManager.Resolve<IDataBase>(constructParameters);
      }

      public static IWSP WSP
      {
         get
         {
            return ContainerManager.Resolve<IWSP>();
         }
      }

      public static IVirtualPagesManager VirtualPageManager
      {
         get
         {
            return ContainerManager.Resolve<IVirtualPagesManager>();
         }
      }

      public static IBaseUserManager BaseUserManager
      {
         get
         {
            return ContainerManager.Resolve<IBaseUserManager>();
         }
      }

      public static IErrorManager ErrorManager
      {
         get
         {
            return ContainerManager.Resolve<IErrorManager>();
         }
      }

      public static ITranslationManager TranslationManager
      {
         get
         {
            ITranslationManager tm = ContainerManager.Resolve<ITranslationManager>();
            if (!tm.TranslationLoad)
               tm.Init();
            return tm;
         }
      }
		public static IMemLogManager MemLogManager
		{
			get
			{
				return ContainerManager.Resolve<IMemLogManager>();

			}
		}

		public static IFileManager FileManager
      {
         get
         {
            return ContainerManager.Resolve<IFileManager>();
         }
      }

      public static IBaseRepositoryService BaseRepoService
      {
         get
         {
            return ContainerManager.Resolve<IBaseRepositoryService>();
         }
      }

      public static IGeneralParameters GeneralParameters
      {
         get
         {
            return BaseConfigManager().GeneralParameter();
         }
      }

      #endregion " Public Properties "

      #region " Public Methods "

      public static IMongoDataService MongoDB()
      {
         return ContainerManager.Resolve<IMongoDataService>();
      }

      public static IMongoDataService<TDocument> MongoDB<TDocument>()
      {
         return ContainerManager.Resolve<IMongoDataService<TDocument>>();
      }

      public static IBaseConfigurationManager<TConfigElement> BaseConfigManager<TConfigElement>() where TConfigElement : ConfigurationElement
      {
         return ContainerManager.Resolve<IBaseConfigurationManager<TConfigElement>>();
      }

      public static IBaseConfigurationManager BaseConfigManager()
      {
         return ContainerManager.Resolve<IBaseConfigurationManager>();
      }

      public static IBaseUser BaseUser(long userID, int implementationID)
      {
         Dictionary<string, long> constructParameters = new Dictionary<string, long>
            {
                { "userID", userID },
                { "implementationID", implementationID }
            };
         return ContainerManager.Resolve<IBaseUser>(constructParameters);
      }

		public static IAppSession Session(string sessionString)
		{
			Dictionary<string, string> constructParameters = new Dictionary<string, string>
				{
					 { "sessionString", sessionString}
				};
			return ContainerManager.Resolve<IAppSession>(constructParameters);
		}


		public static IBaseUser BaseUser(long userID)
      {
         Dictionary<string, long> constructParameters = new Dictionary<string, long>
            {
                { "userID", userID }
            };
         return ContainerManager.Resolve<IBaseUser>(constructParameters);
      }

      public static IBaseUser BaseUser()
      {
         return Session().User;
      }

		public static IHubService HubService()
		{
			return ContainerManager.Resolve<IHubService>();
		} 

		public static Interface Resolve<Interface>()
      {
         return ContainerManager.Resolve<Interface>();
      }

		public static Interface Resolve<KeyType, Interface>(string keyName, KeyType keyObject)
		{
			Dictionary<string, KeyType> constructParameters = new Dictionary<string, KeyType>()
			{
				{ keyName, keyObject }
			};

			return ContainerManager.Resolve<Interface>(constructParameters);
		}

		public static Interface Resolve<Interface>(IDictionary dictionary)
      {
         return ContainerManager.Resolve<Interface>(dictionary);
      }

      public static Interface Resolve<Interface,TID>(TID id)
      {
         Dictionary<string, TID> constructParameters = new Dictionary<string, TID>
            {
                { "id", id }
            };
         return ContainerManager.Resolve<Interface>(constructParameters);
      }

      public static T UI<T>() where T : IBaseUIService
      {
         return Resolve<T>();
      }

      public static void Initializate(IContainerManager containerManager)
      {
         _ContainerManager = containerManager;
      }

      public static void Initializate()
      {
         _ContainerManager = new ContainerManager();
      }

      #endregion " Public Methods "
   }
}