using Repository.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.MsSqlRepository
{
	public class MsSqlPostRepository : MsSqlBaseRepository, IPostRepository
	{
		public MsSqlPostRepository(ConnectionOptions options) 
			: base(options) { }

		public Task<Post> AddAsync(Post item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Posts.Add(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}

		public Task AddRangeAsync(IEnumerable<Post> items)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Posts.AddRange(items);
					dbContext.SaveChanges();
				}
			});
		}

		public Task Clear()
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Posts.RemoveRange(dbContext.Posts);
					dbContext.SaveChanges();
				}
			});
		}

		public Task DeleteAsync(int id)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					var item = dbContext.Posts.Find(id);
					if (item != null)
					{
						dbContext.Posts.Remove(item);
						dbContext.SaveChanges();
					}
				}
			});
		}

		public Task<Post> GetEntityAsync(int id)
		{
			return Task.Run(() =>
			{
				Post post;

				using (var dbContext = GetDbContext())
				{
					post = dbContext.Posts.FirstOrDefault(x => x.Id == id);
				}

				return post;
			});
		}

		public Task<List<Post>> GetEntityListAsync()
		{
			return Task.Run(() =>
			{
				var posts = new List<Post>();

				using (var dbContext = GetDbContext())
				{
					posts = dbContext.Posts.OrderBy(x => x.Name).ToList();
				}

				return posts;
			});
		}

		public Task<Post> UpdateAsync(Post item)
		{
			return Task.Run(() =>
			{
				using (var dbContext = GetDbContext())
				{
					dbContext.Posts.Update(item);
					dbContext.SaveChanges();
				}

				return item;
			});
		}
	}
}
