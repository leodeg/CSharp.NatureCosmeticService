using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models.ViewModels
{
	public class ServicesViewModel
	{
		public Service Service { get; set; }
		public IEnumerable<ServiceCategory> ServiceCategories { get; set; }
	}
}
