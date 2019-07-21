using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT1.Models
{
	public class CustomerContext : DbContext
	{
		public DbSet<Customer> Customers { get; set; }//Student
		public DbSet<Product> Products { get; set; }//Course

		public CustomerContext()
		{
			Database.EnsureCreated();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<CustomerProduct>()
				.HasKey(t => new { t.CustomerId, t.ProductId });

			modelBuilder.Entity<CustomerProduct>()
				.HasOne(sc => sc.Customer)
				.WithMany(s => s.CustomerProducts)
				.HasForeignKey(sc => sc.CustomerId);

			modelBuilder.Entity<CustomerProduct>()
				.HasOne(sc => sc.Product)
				.WithMany(c => c.CustomerProducts)
				.HasForeignKey(sc => sc.ProductId);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=relationsdb2;Trusted_Connection=True;");
		}


	}
}
