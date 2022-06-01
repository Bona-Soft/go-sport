using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Interfaces.SubEntities
{
	public interface IComment : IEntity, ITransient
	{
		key<long> CommentID { get; }
		string Text { get; set; }
		IUser User { get; set; }
		DateTime Date { get; set; }
	}
}
