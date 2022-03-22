using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.Entity.Implement
{
   public class MenuEntity
	{
		public CrecheEntities context = new CrecheEntities();
		public void create(Menu menu)
		{
			try
			{
				Menu newMenu = new Menu
				{
					Description = menu.Description,
					dayDate = menu.dayDate
				};
				context.Menu.Add(newMenu);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout du menu en DB");
			}
		}

		public Menu find(DateTime today)
		{
			Menu menu = new Menu();
			try
			{
				var query = from m in context.Menu
							where m.dayDate == today.Date
							select new { m.MenuID, m.Description, m.dayDate };

				foreach (var item in query)
				{
					menu.MenuID = item.MenuID;
					menu.Description = item.Description;
					menu.dayDate = item.dayDate;
				}

				return menu;
			}

			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'enfant dans la DB");
				return null;
			}
		}

		public void delete(DateTime today)
		{
			try
			{
				var query = (from m in context.Menu
							 where m.dayDate == today
							 select m).FirstOrDefault();

				context.Menu.Remove(query);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la suppression de la DB dans la table menu");
			}
		}
	}
}
