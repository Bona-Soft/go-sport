using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
   public class HeadquarterRepoService : RepoServices, IHeadquarterRepositoryService
   {
      public int Save(IHeadquarter entity)
      {
         Dictionary<string, Type, object> dic = Key.EntityToDictionary(entity);

         if (entity.HeadquarterID > 0)
            return HeadquarterRepo.Update(dic, BaseRepoService.CurrentTransaction);
         dic.Remove<int>("HeadquarterID");
         return HeadquarterRepo.Add(dic, BaseRepoService.CurrentTransaction);
      }

      public IHeadquarter GetByID(int id)
      {
         DataTable dt = HeadquarterRepo.Get(id);
         if (dt == null || dt.Rows.Count == 0)
            throw new Exception("Headquarter can not be found");

         IHeadquarter entity = HeadquarterFactory.New(id);
         Fill(entity, dt);
         return entity;
      }

      public List<IHeadquarter> SearchHeadquarter(short? sportID, string searchText, float lat, float lng)
      {
         List<IHeadquarter> headquarters = new List<IHeadquarter>();
         DataTable dt = HeadquarterRepo.SearchHeadquarters(sportID, searchText, lat, lng);
         foreach (DataRow row in dt.Rows)
         {
            IHeadquarter headquarter = HeadquarterFactory.New(row["HeadquarterID"]);
            Fill(headquarter, row);
            headquarters.Add(headquarter);
         }
         return headquarters;
      }

      #region "Fills Entity Methods"

      private void Fill(IHeadquarter entity, DataTable dt)
         => Fill(entity, dt.Rows[0]);

      public IHeadquarter Fill(IHeadquarter entity, DataRow dr)
      {
         Key.FillEntity(entity, dr);

         if (dr["LocationID"] != null && dr["LocationLat"] != null && dr["LocationLng"] != null)
         {
            entity.Location = LocationFactory.New(dr["LocationID"].ToDefType<long>(0));
            entity.Location.Lat = dr["LocationLat"].ToDefType<float>(0);
            entity.Location.Lng = dr["LocationLng"].ToDefType<float>(0);
            entity.Location.Display = dr["LocationDisplay"].ToDefType("");
            entity.Location.Value = dr["LocationValue"].ToDefType("");
         }

         return entity;
      }

      #endregion "Fills Entity Methods"
   }
}