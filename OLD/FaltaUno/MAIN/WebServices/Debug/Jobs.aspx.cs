using MYB.FaltaUno.Application.UIService;
using MYB.BaseApplication.Application.CoreApplication;
using Newtonsoft.Json.Linq;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.BaseApplication.Framework.Helpers.JTools;

namespace MYB.FaltaUno.WebServices.Debug
{
	public partial class Jobs : AppWebServices
	{
		protected override void ProcessPOST()
		{
			BaseApp.JobManager.RunAll();
		}

		protected override void ProcessGET()
		{
			JArray jarr = new JArray();
			var jobs = BaseApp.JobManager.Jobs;
			foreach(var job in jobs)
			{
				JObject obj = new JObject();
				obj["BaseSchedulerJobID"] = job.BaseSchedulerJobID;
				obj["Name"] = job.Name;
				obj["StartDateTime"] = job.StartDateTime;
				obj["EndDateTime"] = job.EndDateTime;
				obj["Interval"] = job.Interval;
				obj["LastTimeExecuteStarted"] = job.LastTimeExecuteStarted;
				obj["LastTimeExecuteEnded"] = job.LastTimeExecuteEnded;
				obj["IsRunningNow"] = job.IsRunningNow;
				jarr.Add(obj);
			}

			sendSerializeObject(jarr);
		}
	}
}