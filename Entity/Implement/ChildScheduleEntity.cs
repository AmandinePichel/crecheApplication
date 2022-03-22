using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.Entity.Implement
{
    public class ChildScheduleEntity
    {
		public CrecheEntities context = new CrecheEntities();
		public List<object> scheduleList = new List<object>();
		DateTimeFormatInfo dateTimeFormats;
		public void create(ChildSchedule childSchedule)
		{
			try
			{
				ChildSchedule newChildSchedule = new ChildSchedule
				{
					fk_Child = childSchedule.fk_Child,
					fk_Schedule = childSchedule.fk_Schedule
				};
				context.ChildSchedule.Add(newChildSchedule);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout dans la table ChildSchedule");
			}
		}

		public void delete(ChildSchedule childSchedule)
		{
			try
			{
				var query = (from cs in context.ChildSchedule
							where cs.fk_Child == childSchedule.fk_Child && cs.fk_Schedule == childSchedule.fk_Schedule
							select cs).FirstOrDefault();


				context.ChildSchedule.Remove(query);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la suppression dans la table ChildSchedule");
			}
		}

		public ChildSchedule find(ChildSchedule scheduleToFind)
		{
			ChildSchedule childSchedule;
			try
			{
				var query = from cs in context.ChildSchedule
							where cs.fk_Child == scheduleToFind.fk_Child && cs.fk_Schedule == scheduleToFind.fk_Schedule
							select new { cs.ChildScheduleID };

				foreach (var item in query)
				{
					childSchedule = new ChildSchedule(item.ChildScheduleID);
					return childSchedule;
				}

				return null;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'horaire-enfant dans la DB");
				return null;
			}
		}

		public List<ChildSchedule> findForAKid(Child child)
		{
			List<ChildSchedule> childScheduleList = new List<ChildSchedule>();
			ChildSchedule childSchedule;
			try
			{
				var query = from cs in context.ChildSchedule
							where cs.fk_Child == child.KidID
							select new { cs.ChildScheduleID, cs.fk_Child, cs.fk_Schedule };

				foreach (var item in query)
				{
					childSchedule = new ChildSchedule(item.fk_Child, item.fk_Schedule);
					childScheduleList.Add(childSchedule);
				}

				return childScheduleList;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'horaire de l'enfant dans la DB");
				return null;
			}
		}


		/*public List<object> FindKidsStayingMoment()
		{
			try
			{
				var query = from cs in context.ChildSchedule
							join child in context.Child on cs.fk_Child equals child.KidID
							join sch in context.Schedule on cs.fk_Schedule equals sch.ScheduleID
							select new { child.Firstname, child.Name, sch.Date, sch.Moment };

				foreach (var item in query)
				{
					DateTime date = (DateTime)item.Date;
					dateTimeFormats = new CultureInfo("fr-FR").DateTimeFormat;
					string dateString = date.ToString("dddd", dateTimeFormats);
					scheduleList.Add(dateString);
					scheduleList.Add(item.Moment);
					scheduleList.Add(item.Name);
					scheduleList.Add(item.Firstname);

					return scheduleList;
				}
				return null;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout dans la table Schedule");
				return null;
			}
		}*/
	}
}
