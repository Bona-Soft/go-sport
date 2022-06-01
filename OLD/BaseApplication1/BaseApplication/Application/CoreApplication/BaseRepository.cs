using MYB.BaseApplication.Application.CoreInterfaces;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreApplication
{
    public class BaseRepository : IBaseRepository
    {
        public void Commit(OleDbTransaction trx) => BaseApp.DB.CommitTransaction(trx);

        public OleDbTransaction NewTransaction() => BaseApp.DB.NewTransaction();

        public void SetTransaction(OleDbTransaction trx) => BaseApp.DB.SetTransaction(trx);
        public OleDbTransaction GetCurrentTransaction() => BaseApp.DB.GetCurrentTransaction();

        public void Rollback(OleDbTransaction trx) => BaseApp.DB.RollbackTransaction(trx);

    }
}