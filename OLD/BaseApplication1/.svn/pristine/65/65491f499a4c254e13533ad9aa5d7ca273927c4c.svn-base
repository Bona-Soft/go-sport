using System;
using System.Threading;

namespace MYB.BaseApplication.Framework.BaseSchedulerJobs.Interfaces
{
	public interface IBaseSchedulerJob
	{
		long BaseSchedulerJobID { get;  }

		string Name { get; set; }
		Action Action { get; set; }

		DateTime StartDateTime { get; set; }
		DateTime? EndDateTime { get; set; }
		TimeSpan Interval { get; set; }
		Timer Timer { get; set; }

		DateTime LastTimeExecuteStarted { get; set; }
		DateTime LastTimeExecuteEnded { get; set; }

		bool IsRunningNow { get; set; }
		void SetStartDateTimeHours(int hours, int minutes = 0, int seconds = 0);

		void SingleRun();
	};
}
