using MYB.BaseApplication.Application.CoreInterfaces;
using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.Factories
{
   public interface IMainFactory : IBaseFactory, ISingleton
   {
		IRequestStates NewRequestState(short id);
		IRequestStates NewRequestState(string id);
		IComment NewComment(long id);
	}
}
