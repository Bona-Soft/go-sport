using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Infrastructure.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.FaltaUno.Model.Repositories
{
	public class FieldRepository : IFieldRepository
	{
      public DataTable GetFields()
      {
         List<OleDbParameter> dbParams = new List<OleDbParameter>();
         return BaseApp.DB.Execute<DataTable>("GetFields", dbParams.ToArray());
      }
      public DataTable GetFields(short sportID)
      {
         List<OleDbParameter> dbParams;

         dbParams = new List<OleDbParameter>();

         dbParams.Add(BaseApp.DB.CreateParameter("@SportID", sportID, OleDbType.TinyInt));

         return BaseApp.DB.Execute<DataTable>("GetFields", dbParams.ToArray());
      }

   }
}