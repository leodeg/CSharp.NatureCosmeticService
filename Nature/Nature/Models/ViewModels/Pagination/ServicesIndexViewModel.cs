using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class ServicesIndexViewModel
	{
		public IEnumerable<Service> Services { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
