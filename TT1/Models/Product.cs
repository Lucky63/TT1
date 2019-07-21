using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT1.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string NameProduct { get; set; }
		public string Description { get; set; }
		public int Price { get; set; }
		public List<CustomerProduct> CustomerProducts { get; set; }

		public Product()
		{
			CustomerProducts = new List<CustomerProduct>();
		}
	}
}
