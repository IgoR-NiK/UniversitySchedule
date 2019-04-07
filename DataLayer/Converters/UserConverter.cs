using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using DataLayer.Models;
using DbUser = Repository.Entities.User;

namespace DataLayer.Converters
{
	public static class UserConverter
	{
		public static User Convert(DbUser dbUser)
		{
			if (dbUser == null) return null;

			return new User()
			{
				Id = dbUser.Id,
				Login = dbUser.Login,
				FirstName = dbUser.FirstName,
				SecondName = dbUser.SecondName,
				MiddleName = dbUser.MiddleName,				
				IsLocked = dbUser.IsLocked,
				Gender = dbUser.Gender,
				RoleId = dbUser.RoleId,
				Role = RoleConverter.Convert(dbUser.Role)
			};
		}

		public static DbUser Convert(User user)
		{
			if (user == null) return null;

			return new DbUser()
			{
				Id = user.Id,
				Login = user.Login,
				Password = MD5.Create().ComputeHash(Encoding.Default.GetBytes(user.Password)),
				FirstName = user.FirstName,
				SecondName = user.SecondName,
				MiddleName = user.MiddleName,
				IsLocked = user.IsLocked,
				Gender = user.Gender,
				RoleId = user.RoleId,
				Role = RoleConverter.Convert(user.Role)
			};
		}
	}
}
