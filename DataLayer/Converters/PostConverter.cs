using System;
using System.Collections.Generic;
using System.Text;

using DataLayer.Models;
using DbPost = Repository.Entities.Post;

namespace DataLayer.Converters
{
	public static class PostConverter
	{
		public static Post Convert(DbPost dbPost)
		{
			if (dbPost == null) return null;

			return new Post()
			{
				Id = dbPost.Id,
				Name = dbPost.Name,
				Description = dbPost.Description
			};
		}

		public static DbPost Convert(Post post)
		{
			if (post == null) return null;

			return new DbPost()
			{
				Id = post.Id,
				Name = post.Name,
				Description = post.Description
			};
		}
	}
}
