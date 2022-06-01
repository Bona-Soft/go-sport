using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Framework.BaseSchedulerJobs.Interfaces
{
	public interface IBaseSchedulerJobService
	{
		List<IBaseSchedulerJob> Jobs { get; set; }

		void ScheduleJob(long jobID, DateTime startDateTime, TimeSpan interval, Action jobAction);
		void ScheduleJob(long jobID, DateTime startDateTime, double intervalInHour, Action jobAction);
		void ScheduleJob(long jobID, DateTime startDateTime, DateTime? endDateTime, double intervalInHour, Action jobAction);
		void ScheduleJob(long jobID, string name, DateTime startDateTime, double intervalInHour, Action jobAction);

		void ScheduleJob(long jobID, string name, DateTime startDateTime, DateTime? endDateTime, double intervalInHour, Action jobAction);

		void ScheduleJob(long jobID, string name, DateTime startDateTime, DateTime? endDateTime, TimeSpan interval, Action jobAction);
		void ScheduleJob(IBaseSchedulerJob job);

		void RunAll();

		bool StopAll();

		bool StopJob(long jobID);

		bool StopJob(string jobName);

	}
}
