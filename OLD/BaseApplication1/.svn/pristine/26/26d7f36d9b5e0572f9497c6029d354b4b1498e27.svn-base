using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(MYB.BaseApplication.Domain.HubService.HubStartup))]

namespace MYB.BaseApplication.Domain.HubService
{
	public class HubStartup
	{
		public void Configuration(IAppBuilder app)
		{
			//Any coonection or hub wire up and configuration should go here
			app.MapSignalR();
		}
	}
}
