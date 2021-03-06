﻿using System;
using System.Collections.Generic;
using System.Text;

using Repository.Enums;

namespace DataLayer.Models
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

		public override string ToString()
		{
			return $"{SecondName} {FirstName}" + (String.IsNullOrWhiteSpace(MiddleName) ? "" : $" {MiddleName}");
		}

		public override bool Equals(object obj)
		{
			return obj is User user ? Id == user.Id : false;
		}
	}
}
