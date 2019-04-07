using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DataLayer.Models;
using DbRole = Repository.Entities.Role;

namespace DataLayer.Converters
{
	public static class RoleConverter
	{
		public static Role Convert(DbRole dbRole)
		{
			if (dbRole == null) return null;

			return new Role()
			{
				Id = dbRole.Id,
				Name = dbRole.Name,
				Description = dbRole.Description
			};
		}

		public static DbRole Convert(Role role)
		{
			if (role == null) return null;

			return new DbRole()
			{
				Id = role.Id,
				Name = role.Name,
				Description = role.Description
			};
		}
	}
}
