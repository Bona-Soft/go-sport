using MYB.BaseApplication.Framework.BaseSchedulerJobs.Interfaces;
using System;
using System.Threading;

namespace MYB.BaseApplication.Framework.BaseSchedulerJobs
{
	public class BaseSchedulerJob : IBaseSchedulerJob
	{
		public BaseSchedulerJob(long id)
		{
			BaseSchedulerJobID = id;
		}
		public long BaseSchedulerJobID { get; }
		public string Name { get; set; }
		public Action Action { get; set; }
		public DateTime StartDateTime { get; set; }
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

			if (now.Hour > hours|| (now.Hour == hours && now.Minute > minutes))
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
