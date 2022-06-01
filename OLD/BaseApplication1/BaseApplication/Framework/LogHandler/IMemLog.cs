using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.LogHandler.Interfaces
{
	public interface IMemLog
	{
		Queue<ILogMessage> LogQueue { get; }
		LogLevel CurrentLevel { get; }
		int Capacity { get; }

		void SetLevel(string level);
		void SetLevel(LogLevel level);
		void SetCapacity(int capacity);

		List<ILogMessage> ListLog();
		List<ILogMessage> ListLog(int capacity, string level);
		Queue<ILogMessage> ReadLog();
		void ClearLog();
		void RefreshConfgiuration();
		void RefreshConfgiuration(IMemLogConfiguration config);

		void U(string message, [CallerMemberName] string memberName = "");
		void C(string message, [CallerMemberName] string memberName = "");
		void D(string message, [CallerMemberName] string memberName = "");
		void I(string message, [CallerMemberName] string memberName = "");
		void W(string message, [CallerMemberName] string memberName = "");
		void E(string message, [CallerMemberName] string memberName = "");
		void F(string message, [CallerMemberName] string memberName = "");
	}

	public enum LogLevel
	{
		None,
		Core,
		Fatal,
		Error,
		Warn,
		Info,
		Debug,
		Unique,
		All
	}
}
