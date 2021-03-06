using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Repositories;
using MYB.FaltaUno.Application.MainApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Application.MainApplication.Jobs
{
	public class RefreshMatchesStatusAndRequests : BaseJob
	{
		private bool internalRunning = false;

      public RefreshMatchesStatusAndRequests()
		{
			BaseSchedulerJobID = 1;
			Name = "RefreshMatchesStatusAndRequests";
			Interval = TimeSpan.FromMinutes(BaseApp.GeneralParameters.Get("JobCompleteMatchesInterval",15));
			IsRunningNow = false;

			Action = new Action(() =>
			{
				try
				{
					if (!internalRunning)
					{
						internalRunning = true;

						I($"Start RefreshStatusMatches", "RefreshMatchesStatusAndRequests");
						Services.Services.MatchRepoService.RefreshStatusMatches();

						I($"Start CloseExpiredMatchPlayerRequests", "RefreshMatchesStatusAndRequests");
						Services.Services.MatchRepoService.CloseExpiredMatchPlayerRequests();
						internalRunning = false;
					}
				}
				catch (Exception ex)
				{
					E($"Error while running job, message: {ex.Message}", "RefreshMatchesStatusAndRequests");
					internalRunning = false;
				}
							
			});

			BaseApp.MemLogManager.I($"Job Created");
		}

	}
}
