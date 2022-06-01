using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
    public interface IBaseRepository : ISingleton
    {
        OleDbTransaction NewTransaction();

        OleDbTransaction GetCurrentTransaction();

        void SetTransaction(OleDbTransaction trx);

        void Commit(OleDbTransaction trx);

        void Rollback(OleDbTransaction trx);
    }
}