using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.Entity.Implement
{
    public class ActivityEntity
    {
		public CrecheEntities context = new CrecheEntities();
		public void create(Activity newActivity, DateTime today)
		{
			try
			{
				Activity activity = new Activity
				{
					Photo = newActivity.Photo,
					Name = newActivity.Name,
					dayDate = today
				};
				context.Activity.Add(activity);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout de l'activité en DB");
			}
		}

		public void update(Activity activity)
		{
			try
			{
				Activity activityToUpdate = context.Activity.FirstOrDefault(r => r.ActivityID == activity.ActivityID);
				activityToUpdate.Name = activity.Name;
				activityToUpdate.Photo = activity.Photo;
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la modification de l'activité");
			}
		}

		public List<Activity> findAll(DateTime today)
		{
			List<Activity> activities = new List<Activity>();
			Activity activity;
			try
			{
				var query = from a in context.Activity
							where a.dayDate == today
							select new { a.ActivityID, a.Photo, a.Name, a.dayDate };

				foreach (var item in query)
				{
					activity = new Activity();
					activity.ActivityID = item.ActivityID;
					activity.Photo = item.Photo;
					activity.Name = item.Name;
					activity.dayDate = item.dayDate;
					activities.Add(activity);
				}
				return activities;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'activité dans la DB");
				return null;
			}
		}

		public Activity find (Activity selectedActivity)
		{
			try
			{
				var query = (from a in context.Activity
							where a.ActivityID == selectedActivity.ActivityID
							select a).FirstOrDefault();
				return query;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'activité dans la DB");
				return null;
			}
		}

		public void delete(int activityID)
		{
			try
			{
				var query = (from a in context.Activity
							 where a.ActivityID == activityID
							 select a).FirstOrDefault();

				context.Activity.Remove(query);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la suppression de l'activité en DB");
			}
		}
	}
}
