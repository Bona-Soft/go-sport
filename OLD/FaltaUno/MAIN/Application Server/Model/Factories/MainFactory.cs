using MYB.BaseApplication.Application.CoreApplication;
using MYB.FaltaUno.Model.Entities.SubEntities;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.Factories;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Factories
{
   public class MainFactory : BaseFactory, IMainFactory
   {
		public IRequestStates NewRequestState(short id)
		{
			return base.Resolve<IRequestStates, short>(id);
		}

		public IRequestStates NewRequestState(string id)
		{
			return base.Resolve<IRequestStates, string>(id);
		}

		public IComment NewComment(long id)
		{
			return base.Resolve<IComment, long>(id);
		}
	}
}
