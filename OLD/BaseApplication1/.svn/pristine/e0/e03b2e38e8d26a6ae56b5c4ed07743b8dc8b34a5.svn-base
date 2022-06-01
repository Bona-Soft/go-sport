using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseRepoService : IBaseRepositoryService
	{
		public OleDbTransaction CurrentTransaction { get; private set; }
        protected TransactionState CurrentState { get; private set; }
        protected object CurrentOperationID;
        protected readonly string defaultOperationID = "56da39f6-43b6-4e05-8978-dc1eb45fd76f";

        protected enum TransactionState
		{
			NotStarted,
			Started,
			InProgress,
			Commited,
			Canceled,
			Error
		}

		public BaseRepoService()
		{
			CurrentTransaction = null;
			CurrentState = TransactionState.NotStarted;
		}

        public bool BeginTransaction() => BeginTransaction(defaultOperationID);

        public bool BeginTransaction<T>(T operationID)
        {
			Exception ex;
			if (CurrentTransaction == null)
			{
				try
				{
					CurrentTransaction = BaseRepo.NewTransaction();
                    BaseRepo.SetTransaction(CurrentTransaction);
					CurrentState = TransactionState.Started;
                    CurrentOperationID = operationID;
					return true;
				}
				catch (Exception exBegin)
				{
                    CurrentState = TransactionState.Error;
                    ex = exBegin;
                    throw exBegin;
				}
			}
			else
			{
                CurrentState = TransactionState.InProgress;
                Console.Write("There is a current transaction in progress. Current state: " + CurrentState.ToDefString("null"));
			}
            return false;	
		}

        public bool CommitTransaction() => CommitTransaction(defaultOperationID);

        public bool CommitTransaction<T>(T operationID)
		{
			Exception ex;
			if (CurrentTransaction != null && CurrentOperationID.Equals(operationID))
			{
				try
				{
					BaseRepo.Commit(CurrentTransaction);
					if (CurrentTransaction.Connection != null)
					{
						CurrentTransaction.Connection.Close();
					}
					CurrentTransaction = null;
                    BaseRepo.SetTransaction(null);
					CurrentState = TransactionState.Commited;
                    return true;
				}
				catch (Exception exCommit)
				{
                    CurrentState = TransactionState.Error;
                    ex = exCommit;
                    throw ex;
                }
			}
			else
			{
                CurrentState = TransactionState.InProgress;
                Console.Write("There is no current transaction in progress. Current state: " + CurrentState.ToDefString("null"));
			}
            return false;
		}

        public bool RollbackTransaction() => RollbackTransaction(defaultOperationID);
        public bool RollbackTransaction<T>(T operationID)
		{

			if (CurrentTransaction != null && CurrentOperationID.Equals(operationID))
			{
				try
				{
					BaseRepo.Rollback(CurrentTransaction);
					if (CurrentTransaction.Connection != null)
					{
						CurrentTransaction.Connection.Close();
					}
					CurrentTransaction = null;
                    BaseRepo.SetTransaction(null);
                    CurrentState = TransactionState.Canceled;
					return true;
				}
				catch (Exception exRollback)
				{
                    CurrentState = TransactionState.Error;
                    throw exRollback;
                }
			}
			else
			{
                CurrentState = TransactionState.InProgress;
                Console.Write("There is no current transaction in progress. Current state: " + CurrentState.ToDefString("null"));
			}
            return false;
		}

		#region " Shortcuts "

		public IBaseRepository BaseRepo
		{
			get
			{
				return BaseApp.Resolve<IBaseRepository>();
			}
		}

		public IBaseRepositoryService BaseRepositoryService
		{
			get
			{
				return BaseApp.Resolve<IBaseRepositoryService>();
			}
		}

		#endregion " Shortcuts "
	}
}