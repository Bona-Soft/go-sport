using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseRepositoryService : IPerWebRequest
	{
		OleDbTransaction CurrentTransaction { get; }


        /// <summary>
        ///   Start a new transaction/operation.
        /// </summary>
        /// <param name="operationID">  Any object used for identifier the transaction in order to not commit or rollback other transactions </param>
        /// <returns>Return true if a new transaction start, if there already is a transaction it will return false</returns>
        bool BeginTransaction<T>(T operationID);

        /// <summary>
		///   Start a new transaction/operation with a default operation ID
        ///   <para>Take care of call another method that already commit or rollback the default operation it will be execute for the current transaction</para>
		/// </summary>
        /// <returns>Return true if a new default transaction start, if there already is a transaction it will return false</returns>
        bool BeginTransaction();

        /// <summary>
		///   Commit the current transaction that was started with the same identifier
		/// </summary>
		/// <param name="operationID">  Any object used for identifier the transaction in order to not commit or rollback other transactions </param>
        /// <returns>Return true if the commit was successfully, if there already is a transaction it will return false</returns>
		bool CommitTransaction<T>(T operationID);

        /// <summary>
		///   Commit the current transaction that was started with the default identifier
		/// </summary>
		/// <param name="operationID">  Any object used for identifier the transaction in order to not commit or rollback other transactions </param>
        /// <returns>Return true if the commit was successfully, if there already is a transaction it will return false</returns>
		bool CommitTransaction();

        /// <summary>
		///   Rollback the current transaction that was started with the same identifier
		/// </summary>
		/// <param name="operationID">  Any object used for identifier the transaction in order to not commit or rollback other transactions </param>
        /// <returns>Return true if the rollback was successfully, if there already is a transaction it will return false</returns>
        bool RollbackTransaction<T>(T operationID);

        /// <summary>
		///   Rollback the current transaction that was started with the default identifier
		/// </summary>
		/// <param name="operationID">  Any object used for identifier the transaction in order to not commit or rollback other transactions </param>
        /// <returns>Return true if the rollback was successfully, if there already is a transaction it will return false</returns>
        bool RollbackTransaction();

    }
}
