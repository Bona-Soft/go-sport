using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Framework.LogHandler;
using MYB.BaseApplication.Framework.LogHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Domain.LogManager
{
	public class LogManager : IMemLogManager
	{
		private class Config: IMemLogConfiguration
		{
			public int Capacity { get; set; }
			public string Level { get; set; }
		}


		public LogManager()
		{
			Config config = new Config();
			config.Capacity = BaseApp.GeneralParameters.Get("MemLogMaxQueue", 1000);
			config.Level = BaseApp.GeneralParameters.Get("MemLogLevel", "None");
			MemLog.Instance.RefreshConfgiuration((IMemLogConfiguration)config); 
		}

		public void SetLevel(string level) => MemLog.Instance.SetLevel(level);
		public void SetCapacity(int capacity) => MemLog.Instance.SetCapacity(capacity);

		public List<ILogMessage> ListLog() => MemLog.Instance.ListLog();
		public List<ILogMessage> ListLog(int capacity, string level) => MemLog.Instance.ListLog(capacity, level);
		public void ClearLog() => MemLog.Instance.ClearLog();

		public void U(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.U(message, memberName);
		public void C(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.C(message, memberName);
		public void D(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.D(message, memberName);
		public void I(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.I(message, memberName);
		public void W(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.W(message, memberName);
		public void E(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.E(message, memberName);
		public void F(string message, [CallerMemberName] string memberName = "") => MemLog.Instance.F(message, memberName);
	}
}
