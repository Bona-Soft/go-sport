using MYB.BaseApplication.Application.CoreApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Application.MainApplication
{
	public static class Log
	{
		public static void C(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.C(message, memberName);
		public static void D(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.D(message, memberName);
		public static void U(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.U(message, memberName);
		public static void W(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.W(message, memberName);
		public static void E(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.E(message, memberName);
		public static void F(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.F(message, memberName);
		public static void I(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.I(message, memberName);
	}
}
