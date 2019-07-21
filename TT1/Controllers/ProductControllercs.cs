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

		//Редактирование продуктов
		public async Task<IActionResult> EditProduct(int? id)
		{
			if (id != null)
			{
				Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
				if (product != null)
					return View(product);
			}
			return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> EditProduct(Product product)
		{
			db.Products.Update(product);
			await db.SaveChangesAsync();
			return RedirectToAction("Product");
		}

		//Удаление продуктов из БД
		[HttpGet]
		[ActionName("DeleteProduct")]
		public async Task<IActionResult> ConfirmDeleteProduct(int? id)
		{
			if (id != null)
			{
				Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
				if (product != null)
					return View(product);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> DeleteProduct(int? id)
		{
			if (id != null)
			{
				Product product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
				if (product != null)
				{
					db.Products.Remove(product);
					await db.SaveChangesAsync();
					return RedirectToAction("Product");
				}
			}
			return NotFound();
		}
	}
}
