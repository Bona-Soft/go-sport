using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Entities.SubEntities
{
	public class Comment : Entity, IComment
	{
		public key<long> CommentID { get; private set; }
		public string Text { get; set; }
		public IUser User { get; set; }
		public DateTime Date { get; set; }

		public Comment(long id)
		{
			CommentID = id;
		}

		public Comment()
		{
			CommentID = 0;
		}
	}
}
