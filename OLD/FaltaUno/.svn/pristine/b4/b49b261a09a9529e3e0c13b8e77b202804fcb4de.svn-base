using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Application.CoreApplication.BaseExtensions;
using MYB.BaseApplication.Framework.BaseSchedulerJobs.Interfaces;
using MYB.BaseApplication.Framework.Helpers.JTools;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.UIService;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static MYB.BaseApplication.Framework.Helpers.JTools.JTool;

namespace MYB.FaltaUno.WebServices.DebugWS
{
   public partial class Jobs : AppWebServices
   {
      protected override void ProcessPOST()
      {
        
      }

      protected override void ProcessGET()
      {
         List<IBaseSchedulerJob> jobs = BaseApp.JobManager.Jobs;
         JArray jarr = new JArray();
         foreach (var job in jobs)
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