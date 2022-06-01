using MYB.BaseApplication.Framework.BaseSchedulerJobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MYB.BaseApplication.Framework.BaseSchedulerJobs
{
	public class BaseSchedulerJobService : IBaseSchedulerJobService
	{
		private static BaseSchedulerJobService _instance;

		public List<IBaseSchedulerJob> Jobs { get; set; }

		public BaseSchedulerJobService()
		{
			Jobs = new List<IBaseSchedulerJob>();
		}


		public static BaseSchedulerJobService Instance => _instance ?? (_instance = new BaseSchedulerJobService());

		public void ScheduleJob(long jobID, DateTime startDateTime, TimeSpan interval, Action jobAction)
			=> ScheduleJob(jobID, null, startDateTime, null, interval, jobAction);

		public void ScheduleJob(long jobID, DateTime startDateTime, double intervalInHour, Action jobAction)
			=> ScheduleJob(jobID, null, startDateTime, null, intervalInHour, jobAction);

		public void ScheduleJob(long jobID, DateTime startDateTime, DateTime? endDateTime, double intervalInHour, Action jobAction)
			=> ScheduleJob(jobID, null, startDateTime, endDateTime, intervalInHour, jobAction);

		public void ScheduleJob(long jobID, string name, DateTime startDateTime, double intervalInHour, Action jobAction)
			=> ScheduleJob(jobID, name, startDateTime, null, intervalInHour, jobAction);

		public void ScheduleJob(long jobID, string name, DateTime startDateTime, DateTime? endDateTime, double intervalInHour, Action jobAction)
			=> ScheduleJob(jobID, name, startDateTime, endDateTime, TimeSpan.FromHours(intervalInHour), jobAction);

		public void ScheduleJob(long jobID, string name, DateTime startDateTime, DateTime? endDateTime, TimeSpan interval, Action jobAction)
		{
			DateTime now = DateTime.Now;
			if (now > startDateTime)
			{
				var auxDay = now.Day;

				if (now.Hour > startDateTime.Hour || (now.Hour == startDateTime.Hour && now.Minute > startDateTime.Minute))
				{
					auxDay++;
				}

				startDateTime = new DateTime(now.Year, now.Month, auxDay, startDateTime.Hour, startDateTime.Minute, startDateTime.Second);
			}

			TimeSpan timeToGo = startDateTime - now;
			if (timeToGo <= TimeSpan.Zero)
			{
				timeToGo = TimeSpan.Zero;
			}

			IBaseSchedulerJob bsj = new BaseSchedulerJob(jobID);
			bsj.Name = name;
			bsj.EndDateTime = endDateTime;
			bsj.StartDateTime = startDateTime;
			bsj.Timer = new Timer(x =>
		  {
			  bsj.IsRunningNow = true;
			  bsj.LastTimeExecuteStarted = DateTime.Now;
			  jobAction.Invoke();
			  bsj.LastTimeExecuteEnded = DateTime.Now;
			  bsj.IsRunningNow = false;
		  }, null, timeToGo, interval);
			Jobs.Add(bsj);
		}

		public void ScheduleJob(IBaseSchedulerJob job)
		{
			DateTime now = DateTime.Now;
			if(job.StartDateTime == default(DateTime))
			{
				job.StartDateTime = now;
			}
			if (now > job.StartDateTime)
			{
				var auxDay = now.Day;

				if (now.Hour > job.StartDateTime.Hour || (now.Hour == job.StartDateTime.Hour && now.Minute > job.StartDateTime.Minute))
				{
					auxDay++;
				}

				job.StartDateTime = new DateTime(now.Year, now.Month, auxDay, job.StartDateTime.Hour, job.StartDateTime.Minute, job.StartDateTime.Second);
			}

			TimeSpan timeToGo = job.StartDateTime - now;
			if (timeToGo <= TimeSpan.Zero)
			{
				timeToGo = TimeSpan.Zero;
			}

			if (job.Interval == null)
			{
				job.Interval = TimeSpan.FromDays(1);
			}

			if (job.Timer == null)
			{
				job.Timer = new Timer(x =>
				{
					job.IsRunningNow = true;
					job.LastTimeExecuteStarted = DateTime.Now;
					job.Action.Invoke();
					job.LastTimeExecuteEnded = DateTime.Now;
					job.IsRunningNow = false;
				}, null, timeToGo, job.Interval);
			}

			Jobs.Add(job);
		}

		private void SingleRun(IBaseSchedulerJob job)
		{
			job.IsRunningNow = true;	
			if(job.Action != null)
			{
				job.LastTimeExecuteStarted = DateTime.Now;
				job.Action.Invoke();
				job.LastTimeExecuteEnded = DateTime.Now;
			}	
			job.IsRunningNow = false;
		}

		public void RunAll()
		{
			Jobs.ForEach(x => SingleRun(x));
		}

		public bool StopAll()
		{
			return Jobs.Select(x => x.Timer.Change(Timeout.Infinite, Timeout.Infinite)).Count() > 0;
		}

		public bool StopJob(long jobID)
		{
			return Jobs.Where(x => x.BaseSchedulerJobID == jobID).Select(x => x.Timer.Change(Timeout.Infinite, Timeout.Infinite)).Count() > 0;
		}

		public bool StopJob(string jobName)
		{
			return Jobs.Where(x => x.Name == jobName).Select(x => x.Timer.Change(Timeout.Infinite, Timeout.Infinite)).Count() > 0;
		}
	}
}