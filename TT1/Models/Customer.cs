using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT1.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int PhoneNumber { get; set; }
		public string Address { get; set; }
		public List<CustomerProduct> CustomerProducts { get; set; }

		public Customer()
		{
			CustomerProducts = new List<CustomerProduct>();
		}
	}
}
