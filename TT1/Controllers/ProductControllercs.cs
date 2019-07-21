using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT1.Models;

namespace TT1.Controllers
{
	public class ProductController : Controller
	{
		private CustomerContext db;
		public ProductController(CustomerContext context)
		{
			db = context;
		}

		//Отображение всех продуктов
		public async Task<IActionResult> Product()
		{
			return View(await db.Products.ToListAsync());
		}
		//Добавление Продуктов в БД
		public IActionResult CreateProduct()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> CreateProduct(Product product)
		{
			db.Products.Add(product);
			await db.SaveChangesAsync();
			return RedirectToAction("Product");
		}
	}
}
