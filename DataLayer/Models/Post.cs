using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
	public class Post
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object obj)
		{
			return obj is Post post ? Id == post.Id : false;
		}
	}
}
