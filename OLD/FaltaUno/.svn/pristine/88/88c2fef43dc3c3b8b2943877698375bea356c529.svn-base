using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
   public class FieldRepoService : RepoServices, IFieldRepositoryService
   {
      public List<IField> GetFields()
      {
         return Key.GetEntityList<IField, int>(
            FieldRepo.GetFields,
            FieldFactory.New,
            FieldRepoService.Fill,
            "FieldID");
      }

      public List<IField> GetFields(short sportID)
      {
         return Key.GetEntityList<IField, int, short>(sportID,
            FieldRepo.GetFields,
            FieldFactory.New,
            FieldRepoService.Fill,
            "FieldID");
      }

      #region "Fills Entity Methods"

      public IField Fill(IField field, DataRow dr)
      {
         Key.FillEntity(field, dr);
         field.Sport = SportFactory.NewDef(dr["SportID"], null);
         return field;
      }

		public IFieldPosition Fill(IFieldPosition fieldPosition, DataRow dr)
		{
			Key.FillEntity(fieldPosition, dr);
			fieldPosition.Field = FieldFactory.NewDef(dr["FieldID"], null);
			return fieldPosition;
		}

		#endregion "Fills Entity Methods"
	}
}