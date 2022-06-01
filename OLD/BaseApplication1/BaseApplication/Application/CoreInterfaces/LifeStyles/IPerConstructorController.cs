namespace MYB.BaseApplication.Application.CoreInterfaces.LifeStyles
{
	public interface IPerConstructorController : ISingleton
	{
		object[] GetConstructor(ref object[] constructArguments);
	}
}