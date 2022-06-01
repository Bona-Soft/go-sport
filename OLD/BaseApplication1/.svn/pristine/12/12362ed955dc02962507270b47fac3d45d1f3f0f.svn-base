using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.DataBase;
using MYB.BaseApplication.Framework.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MYB.BaseApplication.Framework.Helpers.JTools;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.BaseApplication.Application.CoreApplication
{
   public abstract class BaseUIService : BaseLoggable, IBaseUIService
   {
      public Dictionary<UIView, JObject> Views;

      public JObject ObjToJObj<T>(T entity, UIView uiView)
      {
         if ((short)uiView <= 2 && (short)uiView >= 0)
         {
            return entity.ToJObject((UIView)uiView);
         }
         else
         {
            if (Views == null || !Views.ContainsKey(uiView))
               return entity.ToJObject(UIView.Short);
            return entity.ToJObject(Views[uiView]);
         }
      }

      public JObject Get<TEntity, TID>(Func<TID, TEntity> getMethod, TID id) => Get<TEntity, TID>(getMethod, id, UIView.Full);

      public JObject Get<TEntity, TID>(Func<TID, TEntity> getMethod, TID id, UIView uiView)
      {
         TEntity entity = getMethod(id);
         JObject jEntity = entity.ToJObject(uiView);
         return jEntity;
      }

      public JArray Get<TEntity>(Func<List<TEntity>> getMethod) => Get<TEntity>(getMethod, UIView.Full);

      public JArray Get<TEntity>(Func<List<TEntity>> getMethod, UIView uiView)
      {
         List<TEntity> entity = getMethod();
         JArray jEntity = entity.ToJArray(uiView);
         return jEntity;
      }

      #region " Public Properties "

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

      #endregion " Public Properties "

      #region " Public Methods "

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

      public static T UI<T>() where T : IBaseUIService
      {
         return BaseApp.UI<T>();
      }

      #region " Interceptors "

      public virtual void BeginInterceptor(string methodName)
      {
      }

      public virtual void AfterInterceptor(string methodName)
      {
      }

      //public virtual void Interceptor(IInvocation invocation)
      //{
      //}

      #endregion " Interceptors "

      #endregion " Public Methods "
   }
}