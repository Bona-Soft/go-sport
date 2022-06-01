using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.FaltaUno.Application.MainApplication.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Application.MainApplication.Services
{
	public static class JobService
	{
		public static void ScheduleJobs()
		{

			BaseApp.JobManager.ScheduleJob(new RefreshMatchesStatusAndRequests());
		}


	}
}
