using Newtonsoft.Json.Linq;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseEntity
	{
		void FillFrom<T>(T source);

	}
}