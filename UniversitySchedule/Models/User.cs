using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Repository.Enums;

namespace UniversitySchedule.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }

		public string FirstName { get; set; }
		public string SecondName { get; set; }
		public string MiddleName { get; set; }

		public bool IsLocked { get; set; }
		public GenderType Gender { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}
