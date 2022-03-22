using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.Entity.Implement
{
    public class ChildFoodEntity
    {
		public CrecheEntities context = new CrecheEntities();

		public void create(int kidID, int allergyID)
		{
			try
			{
				ChildFood newChildFood = new ChildFood
				{
					Fk_Child = kidID,
					Fk_Food = allergyID
				};
				context.ChildFood.Add(newChildFood);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout de l'allergie à un enfant en DB");
			}
		}

		public void update(Child child, int foodId)
		{
			try
			{
				var query = from cf in context.ChildFood
							join c in context.Child on cf.Fk_Child equals c.KidID
							join f in context.Food on cf.Fk_Food equals f.FoodID
							where cf.Fk_Child == child.KidID
							select cf;

				foreach (var item in query)
                {
					context.ChildFood.Remove(item);
				}
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la modification de l'enfant");
			}
		}

		public List<Food> findForAChild(Child child)
		{
			List<Food> foods = new List<Food>();
			Food food;
			try
			{
				var query = from cf in context.ChildFood
							join c in context.Child on cf.Fk_Child equals c.KidID
							join f in context.Food on cf.Fk_Food equals f.FoodID
							where cf.Fk_Child == child.KidID
							select new { f.FoodID, f.Name };

				foreach (var item in query)
				{
					food = new Food();
					food.FoodID = item.FoodID;
					food.Name = item.Name;
					foods.Add(food);
				}
				return foods;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'aliment lié à l'enfant dans la DB");
				return null;
			}
		}

		public void delete(Child child)
		{
			try
			{
				var query = (from cf in context.ChildFood
							 where cf.Fk_Child == child.KidID
							 select cf);


                foreach (var item in query)
				{
					Console.WriteLine(item.ChildFoodID);
					context.ChildFood.Remove(item);
				}

				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la suppression de la DB dans la table child");
			}
		}
	}
}
