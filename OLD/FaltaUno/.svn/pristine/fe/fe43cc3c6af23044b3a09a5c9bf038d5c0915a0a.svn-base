using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers.JTools;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Application.UIService
{
	public class DebugUIService : UIServices
	{
		#region "Instance"

		private static DebugUIService m_Instance;

		public static DebugUIService Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new DebugUIService();
				}
				return m_Instance;
			}
		}

		public DebugUIService()
		{
		}

		#endregion "Instance"


		public JArray GetMemLog(int capacity, string level)
		{
			BaseApp.MemLogManager.D($"------------------------------------------------------", "Refresh Debug");
			return BaseApp.MemLogManager.ListLog(capacity, level).ToJArray();
		}
	}
}
