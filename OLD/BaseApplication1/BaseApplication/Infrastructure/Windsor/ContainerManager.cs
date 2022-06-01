using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom;
using MYB.BaseApplication.Infrastructure.Windsor.LifeStylesCustom.LifestylesParents;
using MYB.BaseApplication.Security.Configuration;
using System;
using System.Collections;

namespace MYB.BaseApplication.Infrastructure.Windsor
{
   public class ContainerManager : IContainerManager
   {
      private static IWindsorContainer _WindsorContainer;
      private static bool _initialized = false;
      private static readonly object _locker = new object();

      #region " Public Properties "

      public static IWindsorContainer WindsorContainer
      {
         get
         {
            if (_initialized && _WindsorContainer != null)
            {
               return _WindsorContainer;
            }

            lock (_locker)
            {
               if (!_initialized || _WindsorContainer == null)
               {
                  _WindsorContainer = new WindsorContainer();

                  #region " Interceptor registrations "

                  _WindsorContainer.Register(
                      Classes.FromAssemblyInThisApplication()
                      .BasedOn<IInterceptor>()
                      .WithService.FromInterface()
                      .LifestyleTransient());

                  #endregion " Interceptor registrations "

                  #region " All classes with normals lifestyles registrations "

                  FromAssemblyDescriptor fad = Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath));

                  _WindsorContainer.Register(fad
                      .BasedOn<ITransient>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestyleTransient().Interceptors<BaseInterceptor>()));


						_WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerWebRequest>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifeStyle.HybridPerWebRequestPerThread().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerThread>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestylePerThread().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerPooled>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestylePooled().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerSession>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestyleCustom<PerSessionLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerUser>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestyleCustom<PerUserLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerImplementation>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestyleCustom<PerImplementationLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IPerConstructor>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestyleCustom<PerConstructorLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                            .BasedOn<IAsEnum>()
                            .WithService.FromInterface()
                            .Configure(c => c.LifestyleCustom<PerConstructorLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                            .BasedOn<IPerState>()
                            .WithService.FromInterface()
                            .Configure(c => c.LifestyleCustom<PerConstructorLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                            .BasedOn<IPerHost>()
                            .WithService.FromInterface()
                            .Configure(c => c.LifestyleCustom<PerHostLifestyleManager>().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IBaseUIService>()
                      .Configure(c => c.LifestyleSingleton().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<ISingleton>()
                      .WithService.FromInterface()
                      .Configure(c => c.LifestyleSingleton().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IBaseUIService>()
                      .Configure(c => c.LifestyleSingleton().Interceptors<BaseInterceptor>()));

                  _WindsorContainer.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter(AppDomain.CurrentDomain.RelativeSearchPath))
                      .BasedOn<IInterceptorService>()
                      .Configure(c => c.LifestyleTransient().Interceptors<BaseInterceptor>()));

                  #endregion " All classes with normals lifestyles registrations "

                  #region " Parent Manager Registration "

                  #region " One parent "

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerSession>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerSession>))
                      .LifeStyle.Custom<PerSessionLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser>))
                      .LifeStyle.Custom<PerUserLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerImplementation>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerImplementation>))
                      .LifeStyle.Custom<PerImplementationLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerHost>))
                      .LifeStyle.Custom<PerHostLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor>))
                      .LifeStyle.Custom<PerConstructorLifestyleManager>());

                  #endregion " One parent "

                  #region " Two Parents"

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerImplementation, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerImplementation, IPerHost>))
                      .LifeStyle.Custom<PerHostImplementationLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerHost>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerHost>))
                    .LifeStyle.Custom<PerHostUserLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerImplementation>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerImplementation>))
                    .LifeStyle.Custom<PerImplementationUserLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerSession, IPerImplementation>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerSession, IPerImplementation>))
                    .LifeStyle.Custom<PerImplementationSessionLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerImplementation, IPerUser>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerImplementation, IPerUser>))
                    .LifeStyle.Custom<PerUserImplementationLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerHost>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerHost>))
                    .LifeStyle.Custom<PerHostConstructorLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerImplementation, IPerConstructor>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerImplementation, IPerConstructor>))
                    .LifeStyle.Custom<PerConstructorImplementationLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerImplementation>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerImplementation>))
                    .LifeStyle.Custom<PerConstructorImplementationLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerConstructor>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerConstructor>))
                    .LifeStyle.Custom<PerConstructorUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerUser>))
                    .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerUser>))
                    .LifeStyle.Custom<PerConstructorUserLifestyleManager>());

                  #endregion " Two Parents"

                  #region " Three Parents"

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerImplementation, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerImplementation, IPerHost>))
                      .LifeStyle.Custom<PerHostImplementationUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerImplementation, IPerUser, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerImplementation, IPerUser, IPerHost>))
                      .LifeStyle.Custom<PerHostUserImplementationLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerImplementation, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerImplementation, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorImplementationLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerImplementation, IPerConstructor, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerImplementation, IPerConstructor, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorImplementationLifestyleManager>());

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerUser, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerUser, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerConstructor, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerConstructor, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorUserLifestyleManager>());

                  #endregion " Three Parents"

                  #region " Four Parents "

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerUser, IPerImplementation, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerUser, IPerImplementation, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorImplementationUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerConstructor, IPerImplementation, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerConstructor, IPerImplementation, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorImplementationUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerImplementation, IPerConstructor, IPerHost>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerImplementation, IPerConstructor, IPerHost>))
                      .LifeStyle.Custom<PerHostConstructorImplementationUserLifestyleManager>());

                  #endregion " Four Parents "

                  #region " Five Parents - First BaseEntity "

                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerConstructor, IPerUser, IPerImplementation, IPerHost, IBaseEntity>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerConstructor, IPerUser, IPerImplementation, IPerHost, ISingleton>))
                      .LifeStyle.Custom<PerHostConstructorImplementationUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerConstructor, IPerImplementation, IPerHost, IBaseEntity>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerConstructor, IPerImplementation, IPerHost, ISingleton>))
                      .LifeStyle.Custom<PerHostConstructorImplementationUserLifestyleManager>());
                  _WindsorContainer.Register(Component.For(typeof(IParentManager<IPerUser, IPerImplementation, IPerConstructor, IPerHost, IBaseEntity>))
                      .ImplementedBy(typeof(ParentLifestyleManager<IPerUser, IPerImplementation, IPerConstructor, IPerHost, ISingleton>))
                      .LifeStyle.Custom<PerHostConstructorImplementationUserLifestyleManager>());

                  #endregion " Five Parents - First BaseEntity "

                  #endregion " Parent Manager Registration "

                  _WindsorContainer.Register(Component
                      .For(typeof(IBaseConfigurationManager<>))
                      .ImplementedBy(typeof(BaseConfigurationManager<>))
                      .LifestyleCustom<PerUserLifestyleManager>());

                  _initialized = true;
               }
            }
            return _WindsorContainer;
         }
      }

      #endregion " Public Properties "

      #region " Public Methods "

      public IWindsorContainer Extended
      {
         get
         {
            return WindsorContainer;
         }
      }

      public void Register<T>() where T : class
      {
         WindsorContainer.Register(Component.For<T>().LifestyleTransient());
      }

      public T Resolve<T>()
      {
         return WindsorContainer.Resolve<T>();
      }

      public T Resolve<T>(string key)
      {
         return WindsorContainer.Resolve<T>(key);
      }

      public T Resolve<T>(IDictionary constructParameters)
      {
         return WindsorContainer.Resolve<T>(constructParameters);
      }

      public T Resolve<T>(object argumentsAsAnonymousType)
      {
         return WindsorContainer.Resolve<T>(argumentsAsAnonymousType);
      }

      public object Resolve(string key, Type service)
      {
         return WindsorContainer.Resolve(key, service);
      }

      public T[] ResolveAll<T>()
      {
         return WindsorContainer.ResolveAll<T>();
      }

      public T[] ResolveAll<T>(string key)
      {
         return WindsorContainer.ResolveAll<T>(key);
      }

      public object ResolveAll(string key, Type service)
      {
         return WindsorContainer.ResolveAll(service, key);
      }

      public void Release(object obj)
      {
         WindsorContainer.Release(obj);
      }

      #endregion " Public Methods "
   }
}