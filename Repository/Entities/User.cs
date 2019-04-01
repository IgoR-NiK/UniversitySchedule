using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

using Repository.Enums;

namespace Repository.Entities
{
	public class User
	{
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Login { get; set; }

		[Required]
		[MaxLength(128)]
		public byte[] Password { get; set; }

		[Required]
		[StringLength(100)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(100)]
		public string SecondName { get; set; }

		[StringLength(100)]
		public string MiddleName { get; set; }

		public bool IsLocked { get; set; }

		public GenderType Gender { get; set; }

		public int RoleId { get; set; }
		public Role Role { get; set; }
	}
}