using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Infrastructure.Interfaces.Repositories
{
	public interface IMainRepository : ISingleton
	{
      DataTable GetLocations(string searchText, short? sportID, int groupMember, long? userID);
      DataTable GetLocation(long locationID);
      long AddLocation(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long UpdateLocation(Dictionary<string, Type, object> dic, OleDbTransaction trx = null);
		long AddComment(Dictionary<string, Type, object> dic);
   }
}
