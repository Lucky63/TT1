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
		
		public async Task <ActionResult> Index(SortState sortOrder = SortState.NameAsc, int page = 1)
		{
			//сортировка
			IQueryable<Customer> customers = db.Customers;

			ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
			ViewData["PhoneNumberSort"] = sortOrder == SortState.PhoneNumberAsc ? SortState.PhoneNumberDesc : SortState.PhoneNumberAsc;
			ViewData["AddressSort"] = sortOrder == SortState.AddressAsc ? SortState.AddressDesc : SortState.AddressAsc;

			switch (sortOrder)
			{
				case SortState.NameDesc:
					customers = customers.OrderByDescending(s => s.Name);
					break;
				case SortState.PhoneNumberAsc:
					customers = customers.OrderBy(s => s.PhoneNumber);
					break;
				case SortState.PhoneNumberDesc:
					customers = customers.OrderByDescending(s => s.PhoneNumber);
					break;
				case SortState.AddressAsc:
					customers = customers.OrderBy(s => s.Address);
					break;
				case SortState.AddressDesc:
					customers = customers.OrderByDescending(s => s.Address);
					break;
				default:
					customers = customers.OrderBy(s => s.Name);
					break;
			}

			//Пагинация..............................................................
			int pageSize = 3;   // количество элементов на странице

			var count = await customers.CountAsync();
			var items = await customers.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().Include(x => x.CustomerProducts).ThenInclude(y => y.Product).ToListAsync();

			PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
			IndexViewModel viewModel = new IndexViewModel
			{
				PageViewModel = pageViewModel,
				Customers = items
			};

			return View(viewModel);
		}

		
		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}
		

		//Редактирование и добавление продуктов клиенту
		public ActionResult Edit(int? id)
		{
			Customer customer = db.Customers.Include(x=>x.CustomerProducts).ThenInclude(x=>x.Product).First(x=>x.Id==id);
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
		//Тестовая версия удаления продукта у клиента
		public ActionResult DeleteCustomerProduct(int id)
		{
			Customer customer = db.Customers.Include(x=>x.CustomerProducts).ThenInclude(x=>x.Product).First(x => x.Id == id);
			ViewBag.Cusprod = customer.CustomerProducts.ToList();
			CustomerProduct customerProduct = new CustomerProduct { CustomerId = id};
			return View(customerProduct);
		}
		[HttpPost]
		public ActionResult DeleteCustomerProduct(CustomerProduct customerProduct, int selectList)
		{
			Customer customer = db.Customers.Include(s => s.CustomerProducts).FirstOrDefault(s => s.Id == customerProduct.CustomerId);
			Product product = db.Products.FirstOrDefault(c => c.Id == selectList);
			if (customer != null && product != null)
			{
				var customerproductdel = customer.CustomerProducts.FirstOrDefault(sc => sc.ProductId == product.Id);
				customer.CustomerProducts.Remove(customerproductdel);
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return NotFound();
		}

	}
}
