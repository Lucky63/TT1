using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TT1.Models;

namespace TT1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
			//using (CustomerContext db = new CustomerContext())
			//{
			//	Customer s1 = new Customer { Name = "Tom", PhoneNumber = 123321, Address = "Лермонтова" };
			//	Customer s2 = new Customer { Name = "Григорий", PhoneNumber = 221133, Address = "Нижняя Луговая" };
			//	Customer s3 = new Customer { Name = "Федор", PhoneNumber = 1221367, Address = "Ленина" };
			//	db.Customers.AddRange(new List<Customer> { s1, s2, s3 });

			//	Product c1 = new Product { NameProduct = "Алгоритмы", Description = "Базовые", Price = 1000 };
			//	Product c2 = new Product { NameProduct = "Телефон", Description = "С чехлом", Price = 2000 };
			//	db.Products.AddRange(new List<Product> { c1, c2 });

			//	db.SaveChanges();

			//	// добавляем к студентам курсы
			//	s1.CustomerProducts.Add(new CustomerProduct { ProductId = c1.Id, CustomerId = s1.Id });
			//	s2.CustomerProducts.Add(new CustomerProduct { ProductId = c1.Id, CustomerId = s2.Id });
			//	s2.CustomerProducts.Add(new CustomerProduct { ProductId = c2.Id, CustomerId = s2.Id });
			//	db.SaveChanges();

			//	var courses = db.Products.Include(c => c.CustomerProducts).ThenInclude(sc => sc.Customer).ToList();
			//	// выводим все курсы
			//	foreach (var c in courses)
			//	{
			//		Console.WriteLine($"\n Product: {c.NameProduct}");
			//		// выводим всех студентов для данного кура
			//		var customers = c.CustomerProducts.Select(sc => sc.Customer).ToList();
			//		foreach (Customer s in customers)
			//			Console.WriteLine($"{s.Name}");
			//	}
			//}
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
