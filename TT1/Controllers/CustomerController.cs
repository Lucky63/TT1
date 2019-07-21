using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TT1.Models;

namespace TT1.Controllers
{
	public class CustomerController : Controller
	{
		private CustomerContext db;
		public CustomerController(CustomerContext context)
		{
			db = context;
		}
		//Добавляем клиента
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Customer customer)
		{
			db.Customers.Add(customer);
			await db.SaveChangesAsync();
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		[ActionName("Delete")]
		public async Task<IActionResult> ConfirmDelete(int? id)
		{
			if (id != null)
			{
				Customer customer = await db.Customers.FirstOrDefaultAsync(p => p.Id == id);
				if (customer != null)
					return View(customer);
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id != null)
			{
				Customer customer = await db.Customers.FirstOrDefaultAsync(p => p.Id == id);
				if (customer != null)
				{
					db.Customers.Remove(customer);
					await db.SaveChangesAsync();
					return RedirectToAction("Index", "Home");
				}
			}
			return NotFound();
		}
	}
}
