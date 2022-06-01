using MYB.BaseApplication.Application.CoreInterfaces;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public static class StoredProcedureExt
	{
		public static T Execute<T>(this IStoredProcedure<T> sp)
		{
			return BaseApp.DB.Execute(sp);
		}
	}
}