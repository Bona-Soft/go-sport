using MYB.BaseApplication.Application.CoreApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public abstract class BaseLoggable
	{
		public void U(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.U(message, memberName);
		public void C(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.C(message, memberName);
		public void D(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.D(message, memberName);
		public void I(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.I(message, memberName);
		public void W(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.W(message, memberName);
		public void E(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.E(message, memberName);
		public void F(string message, [CallerMemberName] string memberName = "") => BaseApp.MemLogManager.F(message, memberName);
	}

}
