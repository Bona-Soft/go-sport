using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface IUserRepository : ISingleton
	{
		DataTable Get(long userId);
		DataTable GetByEmail(string email);
		long Update(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Add(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long Delete(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);

      bool UpdateAvatar(long userID, string fileName, OleDbTransaction trx = null);

		DataTable GetPrivacy(long id);
		DataTable GetDefaultPrivacy();
      long UpdatePrivacy(long userPrivacyID, long userID, char value, OleDbTransaction trx = null);
      long AddPrivacy(long userID, string entity, string property, char value, OleDbTransaction trx = null);

   }
}