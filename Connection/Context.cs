using Microsoft.EntityFrameworkCore;
using WebApi_JWT.Models;

namespace WebApi_JWT.Connection
{
	public class Context : DbContext
	{
		public Context(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Users> users { get; set; }

		public DbSet<Products> products { get; set; }
	}
}
