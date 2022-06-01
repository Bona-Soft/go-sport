using MYB.BaseApplication.Framework.Helpers;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
   public class SportRepoService : RepoServices, ISportRepositoryService
   {
      public ISport GetByID(short sportID)
      {
         DataTable dt = SportRepo.Get(sportID);
         if (dt == null || dt.Rows.Count == 0)
            throw new Exception("Sport can not be found");

         ISport sport = SportFactory.New(sportID);
         Fill(sport, dt.Rows[0]);
         return sport;
      }

      public List<ISport> GetAll()
      {
         return Key.GetEntityList<ISport, short>(
            SportRepo.GetAll,
            SportFactory.New,
            Fill,
            "SportID");

      }

      #region "Fills Entity Methods"

      private ISport Fill(ISport sport, DataRow dr)
      {
         Key.FillEntity(sport, dr);

         return sport;
      }

      #endregion "Fills Entity Methods"
   }
}