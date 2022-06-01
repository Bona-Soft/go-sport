using MYB.BaseApplication.Framework.LogHandler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.LogHandler
{
	public class LogMessage : ILogMessage
	{
		public DateTime OccurDateTime { get; set; }

		public LogLevel Level { get; set; }
		public string Caller { get; set; }
		public string Message { get; set; }
	}

}
