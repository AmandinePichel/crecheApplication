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
    public class UserEntity
    {

		public CrecheEntities context = new CrecheEntities();

		public UserEntity()
        {

        }

        public void create(User user)
        {
			try
			{
				User newUser = new User
				{
					Name = user.Name,
					Firstname = user.Firstname,
					Email = user.Email,
					Password = user.Password,
					Status = user.Status
				};
				context.User.Add(newUser);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de l'ajout de l'utilisateur en DB");
			}
		}

		public void updateFromID(User oldUser, User user)
		{
			try
			{
				User userToUpdated = context.User.FirstOrDefault(r => r.UserID == oldUser.UserID);
				userToUpdated.Name = user.Name;
				userToUpdated.Firstname = user.Firstname;
				userToUpdated.Email = user.Email;
				userToUpdated.Password = user.Password;
				userToUpdated.Status = user.Status;
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la modification de l'utilisateur");
			}
		}

		public void updateFromEmail(User user)
        {
			try
			{
				string emailUser = user.Email;
				User userToUpdated = context.User.FirstOrDefault(r => r.Email == emailUser);
				userToUpdated.Name = user.Name;
				userToUpdated.Firstname = user.Firstname;
				userToUpdated.Email = user.Email;
				userToUpdated.Password = user.Password;
				userToUpdated.Status = user.Status;
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la modification de l'utilisateur");
			}
		}

		public ObservableCollection<User> findAll()
		{
			ObservableCollection<User> usersList = new ObservableCollection<User>();
			try
			{
				var query = from item in context.User select item;
				usersList = new ObservableCollection<User>(query);
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de récupération de l'utilisateur");
			}
			return usersList;
		}

		public User find(int idUserToFind)
		{
			User user;
			try
			{
				var query = from u in context.User
							where u.UserID == idUserToFind
							select new { u.UserID, u.Name, u.Firstname, u.Email, u.Password, u.Status};

				foreach (var item in query)
				{
					user = new User(item.UserID, item.Name, item.Firstname, item.Email, item.Password, item.Status);
					return user;
				}

				return null;
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la récupération de l'utilisateur dans la DB");
				return null;
			}
		}

		public void delete(User user)
		{
			try
			{
				var query = (from u in context.User
							 where u.UserID == user.UserID
							 select u).FirstOrDefault();

				context.User.Remove(query);
				context.SaveChanges();
			}
			catch (SqlException e)
			{
				Console.WriteLine("Problème de la suppression de l'utilisateur en DB");
			}
		}

	}
}
