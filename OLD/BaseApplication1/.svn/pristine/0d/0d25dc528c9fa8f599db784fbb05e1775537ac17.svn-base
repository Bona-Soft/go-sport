using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.BaseApplication.Domain.Configuration
{
	public class GeneralParameters : IGeneralParameters
	{
		public int ImplementationID { get; private set; }

		private static Dictionary<string, Tuple<string, string>> DefaultGeneralParameterList;
		private Dictionary<string, Tuple<string, string>> GeneralParameterList;

		public GeneralParameters(int implementationID = 0)
		{
			ImplementationID = implementationID;

			DataSet ds = BaseApp.WSP
				.GetAllGeneralParameters(ImplementationID)
				.Execute(BaseApp.DB);

			GeneralParameterList = DataSetToGPList(ds);

			if (DefaultGeneralParameterList == null)
			{
				DefaultGeneralParameterList = new Dictionary<string, Tuple<string, string>>();
				if (ImplementationID == 0)
				{
					DefaultGeneralParameterList = GeneralParameterList;
					SetDomainDefaultGeneralParameters();
				}
				else
				{
					ds = BaseApp.WSP
						.GetAllGeneralParameters(0)
						.Execute(BaseApp.DB);
					DefaultGeneralParameterList = DataSetToGPList(ds);
				}
			}
		}

		private void SetDomainDefaultGeneralParameters()
		{
			SetIfNotExists("CookieExpireDay", 360);
		}

		private Dictionary<string, Tuple<string, string>> DataSetToGPList(DataSet ds)
		{
			Dictionary<string, Tuple<string, string>> gpList = new Dictionary<string, Tuple<string, string>>();

			foreach (DataRow row in ds.Tables[0].Rows)
			{
				gpList.Add(
					row["GeneralParameterID"].ToString(),
					new Tuple<string, string>(
						row["Type"].ToString(),
						row["Value"].ToString()));
			}

			return gpList;
		}

		#region " Others Public Methods "

		public void SetIfNotExists<T>(string parameterName, T value)
		{
			if (!Exists(parameterName)) Set(parameterName, value);
		}

		public bool Exists(string parameterName)
		{
			return Get(parameterName) != default(object) && Get(parameterName) != null;
		}

		#endregion " Others Public Methods "

		#region " Get Methods "

		public T Get<T>(string parameterName, T defValue)
		{
			T result = Get<T>(parameterName);
			if (result == null || result.Equals(default(T)))
			{
				
				return defValue;
			}
			return result;
		}
		public T Get<T>(string parameterName)
		{
			T result = default(T);
			if (GeneralParameterList.ContainsKey(parameterName))
			{
				result = (T)Convert.ChangeType(GeneralParameterList[parameterName].Item2, typeof(T));
			}
			else
			{
				if (DefaultGeneralParameterList.ContainsKey(parameterName))
				{
					result = (T)Convert.ChangeType(DefaultGeneralParameterList[parameterName].Item2, typeof(T));
				}
			}
			return result;
		}

		public object Get(string parameterName, object defValue)
		{
			object result = Get(parameterName);
			if (result == null || result.Equals(default(object)))
			{
				return defValue;
			}
			return result;
		}

		public object Get(string parameterName)
		{
			object result = default(object);
			if (GeneralParameterList.ContainsKey(parameterName))
			{
				result = Convert.ChangeType(GeneralParameterList[parameterName].Item2, Type.GetType(GeneralParameterList[parameterName].Item1));
			}
			else
			{
				if (DefaultGeneralParameterList.ContainsKey(parameterName))
				{
					result = Convert.ChangeType(DefaultGeneralParameterList[parameterName].Item2, Type.GetType(DefaultGeneralParameterList[parameterName].Item1));
				}
			}
			return result;
		}

		public T GetDefault<T>(string parameterName, T defValue)
		{
			T result = GetDefault<T>(parameterName);
			if (result == null || result.Equals(default(T)))
			{
				return defValue;
			}
			return result;
		}

		public T GetDefault<T>(string parameterName)
		{
			T result = default(T);
			if (DefaultGeneralParameterList.ContainsKey(parameterName))
			{
				result = (T)Convert.ChangeType(DefaultGeneralParameterList[parameterName].Item2, typeof(T));
			}

			return result;
		}

		public object GetDefault(string parameterName, object defValue)
		{
			object result = GetDefault(parameterName);
			if (result == null || result.Equals(default(object)))
			{
				return defValue;
			}
			return result;
		}

		public object GetDefault(string parameterName)
		{
			object result = default(object);
			if (DefaultGeneralParameterList.ContainsKey(parameterName))
			{
				result = Convert.ChangeType(DefaultGeneralParameterList[parameterName].Item2, Type.GetType(DefaultGeneralParameterList[parameterName].Item1));
			}
			return result;
		}

		#endregion " Get Methods "

		#region " Set Methods "

		public void Set<T>(string parameterName, T value)
		{
			if (GeneralParameterList.ContainsKey(parameterName))
			{
				GeneralParameterList[parameterName] = new Tuple<string, string>(typeof(T).ToString(), value.ToString());
			}
			else
			{
				GeneralParameterList.Add(parameterName, new Tuple<string, string>(typeof(T).ToString(), value.ToString()));
			}
			BaseApp.WSP.SetGeneralParameter(ImplementationID, parameterName, typeof(T).ToString(), value.ToString()).Execute(BaseApp.DB);
		}

		public void SetDefault<T>(string parameterName, T value)
		{
			if (GeneralParameterList.ContainsKey(parameterName))
			{
				DefaultGeneralParameterList[parameterName] = new Tuple<string, string>(typeof(T).ToString(), value.ToString());
			}
			else
			{
				DefaultGeneralParameterList.Add(parameterName, new Tuple<string, string>(typeof(T).ToString(), value.ToString()));
			}
			BaseApp.WSP.SetGeneralParameter(0, parameterName, typeof(T).ToString(), value.ToString()).Execute(BaseApp.DB);
		}

		#endregion " Set Methods "
	}
}