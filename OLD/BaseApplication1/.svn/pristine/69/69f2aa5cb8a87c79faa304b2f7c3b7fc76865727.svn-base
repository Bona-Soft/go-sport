using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.LogHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IMemLogManager : ISingleton
	{
		void SetLevel(string level);
		void SetCapacity(int capacity);

		List<ILogMessage> ListLog();
		List<ILogMessage> ListLog(int capacity, string level);
		void ClearLog();

		void U(string message, [CallerMemberName] string memberName = "");
		void C(string message, [CallerMemberName] string memberName = "");
		void D(string message, [CallerMemberName] string memberName = "");
		void I(string message, [CallerMemberName] string memberName = "");
		void W(string message, [CallerMemberName] string memberName = "");
		void E(string message, [CallerMemberName] string memberName = "");
		void F(string message, [CallerMemberName] string memberName = "");
	}
}
