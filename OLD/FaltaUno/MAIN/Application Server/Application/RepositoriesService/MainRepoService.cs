using MYB.BaseApplication.Framework.Helpers;
using MYB.BaseApplication.Framework.Helpers.TypesExt;
using MYB.FaltaUno.Application.Interfaces.RepositoriesService;
using MYB.FaltaUno.Model.Interfaces.Entities;
using MYB.FaltaUno.Model.Interfaces.SubEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace MYB.FaltaUno.Application.RepositoriesService
{
	public class MainRepoService : RepoServices, IMainRepositoryService
	{
		public List<ILocation> GetLocations(string searchText, short? sportID, int groupMember, long? userID)
		{
			List<ILocation> locations = new List<ILocation>();
			DataTable dt = MainRepo.GetLocations(searchText, sportID, groupMember, userID);
			foreach (DataRow row in dt.Rows)
			{
				ILocation location = LocationFactory.New(row["LocationID"]);
				FillLocation(location, row);
				locations.Add(location);
			}
			return locations;
		}

      public ILocation GetLocation(long id)
      {
         DataTable dt = MainRepo.GetLocation(id);
         if (dt == null || dt.Rows.Count == 0)
            return null;

         ILocation location = LocationFactory.New(id);
         FillLocation(location, dt.Rows[0]);
         return location;
      }

		public long SaveLocation(ILocation location)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(location);

			if (location.LocationID > 0)
				return MatchRepo.Update(dic, BaseRepoService.CurrentTransaction);
			dic.Remove<long>("LocationID");
			return MainRepo.AddLocation(dic, BaseRepoService.CurrentTransaction);
		}

		public IComment SaveComment(IComment comment)
		{
			Dictionary<string, Type, object> dic = Key.EntityToDictionary(comment);

			dic.Remove<long>("CommentID");
			long result = MainRepo.AddComment(dic);
			if(result > 0)
			{
				IComment _comment = MainFactory.NewComment(result);
				_comment.FillFrom(comment);
				return _comment;
			}
			return null;
		}



		#region "Fills Entity Methods"

		public ILocation FillLocation(ILocation location, DataRow dr)
		{

			Key.FillEntity(location, dr);
			
			return location;
		}

      #endregion "Fills Entity Methods"
   }
}