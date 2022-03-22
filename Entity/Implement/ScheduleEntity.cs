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
    public class ScheduleEntity
	{
		public CrecheEntities context = new CrecheEntities();

		public void create(Schedule schedule)
		{
			try
			{
				Schedule newSchedule = new Schedule
				{
					Moment = schedule.Moment,
					Date = schedule.Date
				};
				context.Schedule.Add(newSchedule);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout dans la table Schedule");
			}
		}

		public int Find(Schedule schedule)
		{
			try
			{
				var query = from s in context.Schedule
							where s.Moment == schedule.Moment && s.Date == schedule.Date
							select new { s.ScheduleID };

				foreach (var item in query)
				{
					return item.ScheduleID;
				}

				return 0;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout dans la table Schedule");
				return 0;
			}
		}
	}
}
