using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.BaseApplication.Framework.LogHandler.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Web;

namespace MYB.BaseApplication.Framework.LogHandler
{
	public class MemLog : IMemLog
	{

		private static IMemLog _instance { get; set; }
		public Queue<ILogMessage> LogQueue { get; private set; }
		public LogLevel CurrentLevel { get; private set; }

		public int Capacity { get; private set; }

		public static IMemLog Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (IMemLog)new MemLog();
				}
				return _instance;
			}
		}

		public IMemLog CloneMemLog()
		{
			IMemLog memLog = (IMemLog)new MemLog();
			memLog.SetCapacity(memLog.Capacity);
			memLog.SetLevel(memLog.CurrentLevel);
			return memLog;
		}

		public MemLog()
		{
			RefreshConfgiuration();
		}

		public MemLog(IMemLogConfiguration config)
		{
			RefreshConfgiuration(config);
		}

		private void AddMessage(string message, string memberName, LogLevel level)
		{
			ILogMessage logMessage = (ILogMessage)new LogMessage();
			logMessage.OccurDateTime = DateTime.UtcNow;
			logMessage.Caller = memberName;
			logMessage.Level = level;
			logMessage.Message = message;
			if (LogQueue.Count >= Capacity)
			{
				LogQueue.Dequeue();
			}
			LogQueue.Enqueue(logMessage);
		}

		public void ClearLog()
		{
			LogQueue = new Queue<ILogMessage>(Capacity);
			CurrentLevel = LogLevel.None;
		}

		public void RefreshConfgiuration()
		{

			SetCapacity(100);
			SetLevel(LogLevel.None);
			LogQueue = new Queue<ILogMessage>(Capacity);
		}
		public void RefreshConfgiuration(IMemLogConfiguration config)
		{

			SetCapacity(config.Capacity);
			SetLevel(config.Level);
			LogQueue = new Queue<ILogMessage>(Capacity);
		}

		public void SetLevel(LogLevel level)
		{
			CurrentLevel = level;
		}
		public void SetLevel(string level)
		{
			switch (level.ToUpper())
			{
				case "UNIQUE":
					CurrentLevel = LogLevel.Unique; break;
				case "CORE":
					CurrentLevel = LogLevel.Core; break;
				case "DEBUG":
					CurrentLevel = LogLevel.Debug; break;
				case "INFO":
					CurrentLevel = LogLevel.Info; break;
				case "WARN":
					CurrentLevel = LogLevel.Warn; break;
				case "ERROR":
					CurrentLevel = LogLevel.Error; break;
				case "FATAL":
					CurrentLevel = LogLevel.Fatal; break;
				case "ALL":
					CurrentLevel = LogLevel.All; break;
				default:
					CurrentLevel = LogLevel.None; break;
			}

		} 

		public void SetCapacity(int capacity)
		{
			Capacity = capacity;
		}

		public List<ILogMessage> ListLog()
		{
			var list = new List<ILogMessage>();
			
			foreach (ILogMessage logMessage in LogQueue)
			{
				list.Add(logMessage);
			}

			return list;
		}
		public List<ILogMessage> ListLog(int capacity, string level)
		{
			SetCapacity(capacity);
			SetLevel(level);
			return ListLog();
		}

		public Queue<ILogMessage> ReadLog()
		{
			return LogQueue;
		}

		public void U(string message, [CallerMemberName] string memberName = "")
		{
			if (CurrentLevel == LogLevel.Debug || CurrentLevel == LogLevel.Core || CurrentLevel == LogLevel.Unique || CurrentLevel == LogLevel.All)
			{
				AddMessage(message, memberName, LogLevel.Unique);
			}
		}
		public void C(string message, [CallerMemberName] string memberName = "")
		{
			if (CurrentLevel == LogLevel.Core || CurrentLevel == LogLevel.All)
			{
				AddMessage(message, memberName, LogLevel.Core);
			}
		}

		public void D(string message, [CallerMemberName] string memberName = "")
		{
			if(CurrentLevel >= LogLevel.Debug || CurrentLevel == LogLevel.Core)
			{
				AddMessage(message, memberName, LogLevel.Debug);
			}	
		}
		

		public void I(string message, [CallerMemberName] string memberName = "")
		{
			if (CurrentLevel >= LogLevel.Info)
			{
				AddMessage(message, memberName, LogLevel.Info);
			}
		}

		public void W(string message, [CallerMemberName] string memberName = "")
		{
			if (CurrentLevel >= LogLevel.Warn)
			{
				AddMessage(message, memberName, LogLevel.Warn);
			}
		}

		public void E(string message, [CallerMemberName] string memberName = "")
		{
			if (CurrentLevel >= LogLevel.Error)
			{
				AddMessage(message, memberName, LogLevel.Error);
			}
		}

		public void F(string message, [CallerMemberName] string memberName = "")
		{
			if (CurrentLevel >= LogLevel.Fatal)
			{
				AddMessage(message, memberName, LogLevel.Fatal);
			}
		}

	}
}