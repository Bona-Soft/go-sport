using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.BaseSchedulerJobs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.BaseApplication.Application.CoreInterfaces
{
	public interface IBaseSchedulerJobManager : IBaseSchedulerJobService, ISingleton
	{

	}
}
