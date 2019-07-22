using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TT1.Models
{
	public class IndexViewModel
	{
		public IEnumerable<Customer> Customers { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
