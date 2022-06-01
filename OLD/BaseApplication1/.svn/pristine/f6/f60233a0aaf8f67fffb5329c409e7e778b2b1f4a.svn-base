using MYB.BaseApplication.Application.CoreInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreApplication
{
	public class BaseJob : BaseLoggable, IBaseJob
	{
		private DateTime _StartDateTime { get; set; }

		public BaseJob()
		{

		}
		public BaseJob(long id)
		{
			BaseSchedulerJobID = id;
		}

		public long BaseSchedulerJobID { get; set; }

		public string Name { get; set; }
		public Action Action { get; set; }

		public DateTime StartDateTime
		{
			get
			{
				if (_StartDateTime == null)
				{
					return DateTime.Now;
				}
				return _StartDateTime;
			}
			set
			{
				 _StartDateTime = value;
			}
		}

		public DateTime? EndDateTime { get; set; }
		public TimeSpan Interval { get; set; }
		public Timer Timer { get; set; }

		public DateTime LastTimeExecuteStarted { get; set; }
		public DateTime LastTimeExecuteEnded { get; set; }

		public bool IsRunningNow { get; set; }
		public void SetStartDateTimeHours(int hours, int minutes = 0, int seconds = 0)
		{
			DateTime now = DateTime.Now;
			var auxDay = now.Day;

			if (now.Hour > hours || (now.Hour == hours && now.Minute > minutes))
			{
				auxDay++;
			}

			StartDateTime = new DateTime(now.Year, now.Month, auxDay, hours, minutes, seconds);
		}

		public void SingleRun()
		{
			Action.Invoke();
		}
	}
}