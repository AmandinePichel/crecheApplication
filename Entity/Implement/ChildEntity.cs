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
    public class ChildEntity
    {
		public CrecheEntities context = new CrecheEntities();

		public void create(Child child)
		{
			try
			{
				Child newChild = new Child
				{
					Firstname = child.Firstname,
					Name = child.Name,
					Birthdate = child.Birthdate,
					Address = child.Address,
					fk_parent = child.fk_parent,
					Photo = child.Photo
				};
				context.Child.Add(newChild);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout de l'enfant en DB");
			}
		}

		public void update(Child oldChild, Child newChild)
		{
			try
			{
				Child childToUpdate = context.Child.FirstOrDefault(r => r.KidID == oldChild.KidID);

				childToUpdate.Name = newChild.Name;
				childToUpdate.Firstname = newChild.Firstname;
				childToUpdate.fk_parent = newChild.fk_parent;
				childToUpdate.Birthdate = newChild.Birthdate;
				childToUpdate.Address = newChild.Address;
				childToUpdate.Photo = newChild.Photo;
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la modification de l'enfant");
			}
		}


		public Child find (Child childToFind)
		{
            Child child;
			try
			{
				var query = from c in context.Child
							where c.Name == childToFind.Name && c.Firstname == childToFind.Firstname
							select new { c.KidID, c.Firstname, c.Name, c.Address, c.Birthdate, c.Photo, c.fk_parent };

				foreach (var item in query)
				{
					child = new Child(item.KidID, item.Firstname, item.Name, item.fk_parent, item.Birthdate, item.Address, item.Photo);
					return child;
				}

				return null;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'enfant dans la DB");
				return null;
			}
		}

		public ObservableCollection<Child> findAll()
		{
			ObservableCollection<Child> children = new ObservableCollection<Child>();
			Child kid;
			try
			{
				var query = from c in context.Child
							select new { c.KidID, c.Firstname, c.Name, c.Address, c.Birthdate, c.Photo, c.fk_parent };

				foreach (var item in query)
				{
					kid = new Model.Child();
					kid.KidID = item.KidID;
					kid.Firstname = item.Firstname;
					kid.Name = item.Name;
					kid.fk_parent = item.fk_parent;
					kid.Address = item.Address;
					kid.Birthdate = item.Birthdate;
					children.Add(kid);
				}
				return children;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'enfant dans la DB");
				return null;
			}
		}

		public List<Model.Child> findWithParent(int userID)
		{
            List<Model.Child> children = new List<Model.Child>();
            Model.Child kid;
			try
			{
				var query =
					from user in context.User
					join child in context.Child on user.UserID equals child.fk_parent
					where user.UserID == userID
					select new { child.KidID, child.Firstname, child.Name };

				foreach (var item in query)
				{
					kid = new Model.Child();
					kid.KidID = item.KidID;
					kid.Firstname = item.Firstname;
					kid.Name = item.Name;
					children.Add(kid);
				}
				return children;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération des enfants dans la DB");
				return null;
			}
		}

		public List<Child> findWithFood(int selectedFoodID)
		{
			List<Child> children = new List<Child>();
			Child kid;
			try
			{
				var query = from child in context.Child
							join cf in context.ChildFood on child.KidID equals cf.Fk_Child
							join f in context.Food on cf.Fk_Food equals f.FoodID
							where f.FoodID == selectedFoodID
							select new { child.KidID, child.Firstname, child.Name};

				foreach (var item in query)
				{
					kid = new Child();
					kid.KidID = item.KidID;
					kid.Firstname = item.Firstname;
					kid.Name = item.Name;
					children.Add(kid);
				}
				return children;
			}
			catch (SqlException e)
			{
				Console.WriteLine($"Problème de la récupération des enfants dans la DB lié à l'aliment {selectedFoodID}");
				return null;
			}
		}

		public List<Child> findForADay(DateTime dateToFind)
		{
			List<Child> children = new List<Child>();
			Child child;
			try
			{
				var query = from c in context.Child
							join cs in context.ChildSchedule on c.KidID equals cs.fk_Child
							join s in context.Schedule on cs.fk_Schedule equals s.ScheduleID
							where s.Date == dateToFind
							select new { c.KidID, c.Firstname, c.Name };

				foreach (var item in query)
				{
					child = new Child();
					child.KidID = item.KidID;
					child.Firstname = item.Firstname;
					child.Name = item.Name;
					children.Add(child);
				}
				return children;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'horaire de l'enfant dans la DB");
				return null;
			}
		}

		public void delete(Child child)
		{
			try
			{
				var query = (from c in context.Child
							 where c.KidID == child.KidID
							 select c).FirstOrDefault();

				context.Child.Remove(query);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la suppression de la DB dans la table child");
			}
		}
	}
}
