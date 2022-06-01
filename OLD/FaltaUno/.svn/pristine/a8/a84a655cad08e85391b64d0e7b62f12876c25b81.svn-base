using MYB.BaseApplication.Application.CoreApplication;
using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYB.FaltaUno.Model.Entities
{
	public class FieldPosition : BaseEntity, IFieldPosition
	{
		public key<int> FieldPositionID { get; }
		public IField Field { get; set; }

		public string Description { get; set; }
		public string Abbreviation { get; set; }

		public FieldPosition(int id)
		{
			FieldPositionID = id;
		}
	}
}
