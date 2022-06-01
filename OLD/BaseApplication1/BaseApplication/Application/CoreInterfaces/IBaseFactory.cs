namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseFactory
	{
		TCustomInterface New<TCustomInterface>();
		TCustomInterface New<TCustomInterface, T>(T ID);
		TCustomInterface Resolve<TCustomInterface>();
		TCustomInterface Resolve<TCustomInterface, T>(T id);
	}

	public interface IBaseFactory<TInterface>
	{
		TInterface New();
      TCustomInterface New<TCustomInterface>();
      TInterface New<T>(T ID);

		TInterface NewDef<T>(T ID, TInterface defaulValue);
		TInterface NewDef<T>(object ID, TInterface defaultValue);
		TInterface NewDefDefault<T>(T ID, TInterface defaultValue);

		TCustomInterface New<TCustomInterface, T>(T ID);

		TInterface Resolve();
		TInterface Resolve<T>(T id);
		TCustomInterface Resolve<TCustomInterface>();
		TCustomInterface Resolve<TCustomInterface, T>(T id);

	}
}