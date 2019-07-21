using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TT1.Models;

namespace TT1.Controllers
{
	public class HomeController : Controller
	{
		private CustomerContext db;
		public HomeController(CustomerContext context)
		{
			db = context;
		}
		
		public ActionResult Index()
		{
			return View(db.Customers.Include(x=>x.CustomerProducts).ThenInclude(y=>y.Product).ToList());
		}

		
		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
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

		//public IActionResult CreateProductCustomer()
		//{
		//	return View();
		//}
		//[HttpPost]
		//public async Task<IActionResult> CreateProductCustomer(Product product)
		//{
		//	db.Products.Add(product);
		//	await db.SaveChangesAsync();
		//	return RedirectToAction("Product");
		//}


		//Редактирование и добавление продуктов клиенту
		public ActionResult Edit(int? id)
		{
			Customer customer = db.Customers.Find(id);
			if (customer == null)
			{
				return NotFound();
			}
			ViewBag.Products = db.Products.ToList();
			return View(customer);
		}

		[HttpPost]
		public ActionResult Edit(Customer customer, int[] selectedProducts)
		{
			Customer newStudent = db.Customers.Find(customer.Id);
			newStudent.Name = customer.Name;
			newStudent.Address = customer.Address;
			newStudent.PhoneNumber = customer.PhoneNumber;

			newStudent.CustomerProducts.Clear();
			
			if (selectedProducts != null)
			{
				//получаем выбранные курсы
				foreach (var c in db.Products.Where(co => selectedProducts.Contains(co.Id)))
				{
					newStudent.CustomerProducts.Add(new CustomerProduct {ProductId = c.Id });
				}
			}
			db.Customers.Add(newStudent);
			db.Entry(newStudent).State = EntityState.Modified;
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		
	}
}
