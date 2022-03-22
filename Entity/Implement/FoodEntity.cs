using CrecheApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrecheApplication.Entity.Implement
{
    public class FoodEntity
	{
		public CrecheEntities context = new CrecheEntities();

		public void create(Food food)
        {
			try
			{
				Food newFood = new Food
				{
					Name = food.Name,
				};
				context.Food.Add(newFood);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout de l'utilisateur en DB");
			}
		}

		public Food find(string foodEntry)
		{
			Food food = new Food();
			try
			{
				var query = from f in context.Food
							where f.Name == foodEntry
							select new { f.FoodID, f.Name};

				foreach (var item in query)
				{
					food.FoodID = item.FoodID;
					food.Name = item.Name;
				}
				return food;
			}

			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'enfant dans la DB");
				return null;
			}
		}

		public ObservableCollection<Food> findAll()
		{
			ObservableCollection<Food> foods = new ObservableCollection<Food>();
			Food food;
			try
			{
				var query = from f in context.Food
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
				Console.WriteLine("Problème de la récupération de l'aliment dans la DB");
				return null;
			}
		}
	}
}
