using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;

namespace MYB.BaseApplication.Infrastructure.DB
{
	public class ParametersProvider
	{
		private static Dictionary<string, OleDbParameterCollection> _oleDbParameterCollections;

		private static void DeriveParameters(OleDbCommand dbCommand)
		{
			try
			{
				if (_oleDbParameterCollections == null)
				{
					_oleDbParameterCollections = new Dictionary<string, OleDbParameterCollection>();
				}

				if (_oleDbParameterCollections.ContainsKey(dbCommand.CommandText))
				{
					var paramCollection = _oleDbParameterCollections[dbCommand.CommandText];
					try
					{
						dbCommand.Parameters.AddRange(
						 paramCollection.OfType<OleDbParameter>()
							  .Select(x => new OleDbParameter
							  {
								  ParameterName = x.ParameterName,
								  OleDbType = x.OleDbType,
								  Size = x.Size,
								  Direction = x.Direction
							  })
							  .ToArray()
						 );
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("B'Warning - Exception Controlled - DeriveParameters: " + ex.Message);
						System.Diagnostics.Debug.WriteLine("B'Warning - Exception Controlled - DeriveParameters: Tying derivate each parameter in single transaction");
						try
						{
							paramCollection.OfType<OleDbParameter>().Select(x =>
								TryAddDerivateParameter(x, dbCommand.Parameters)
							);
						}
						catch
						{
						}
					}
					return;
				}

				OleDbCommandBuilder.DeriveParameters(dbCommand);
				lock (_oleDbParameterCollections)
				{
					if (!_oleDbParameterCollections.ContainsKey(dbCommand.CommandText))
					{
						_oleDbParameterCollections.Add(dbCommand.CommandText, dbCommand.Parameters);
					}
				}
				
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine("B'Warning - Exception Controlled - DeriveParameters: " + ex.Message);
			}
		}

		private static OleDbParameter TryAddDerivateParameter(OleDbParameter odp, OleDbParameterCollection prm)
		{
			OleDbParameter _odp = new OleDbParameter();
			try
			{
				_odp = new OleDbParameter
				{
					ParameterName = odp.ParameterName,
					OleDbType = odp.OleDbType,
					Size = odp.Size,
					Direction = odp.Direction
				};
				prm.Add(_odp);
			}
			catch
			{
			}
			return _odp;
		}

		public static void FillParameters(OleDbCommand dbCommand, OleDbParameter[] dbParams)
		{
			DeriveParameters(dbCommand);

			if (dbParams != null && dbCommand.Parameters != null)
			{
				for (int jCount = 0; jCount < dbCommand.Parameters.Count; jCount++)
				{
					for (int iCount = 0; iCount < dbParams.Length; iCount++)
					{
						if (dbCommand.Parameters[jCount].ParameterName == dbParams[iCount].ParameterName.Replace("@", String.Empty))
						{
							dbCommand.Parameters[jCount].Value = dbParams[iCount].Value;
							break;
						}
					}
				}
			}
		}

		public static void CleanOleDbParameterCollections()
		{
			_oleDbParameterCollections = new Dictionary<string, OleDbParameterCollection>();
		}
	}
}