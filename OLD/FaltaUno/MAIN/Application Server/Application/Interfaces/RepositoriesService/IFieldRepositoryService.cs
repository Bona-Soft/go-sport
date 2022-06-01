using MYB.BaseApplication.Application.CoreInterfaces.LifeStyles;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.Interfaces.RepositoriesService
{
	public interface IFieldRepositoryService : IPerWebRequest
	{
      List<IField> GetFields();
      List<IField> GetFields(short sportID);

      IField Fill(IField field, DataRow dr);
		IFieldPosition Fill(IFieldPosition fieldPosition, DataRow dr);

	}
}