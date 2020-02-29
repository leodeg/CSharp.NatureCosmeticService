using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<Service> Services { get; set; }
		public IEnumerable<News> News { get; set; }
		public IEnumerable<Doctor> Doctors { get; set; }

	}
}
