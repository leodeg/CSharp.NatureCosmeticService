using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class DoctorsIndexViewModel
	{
		public IEnumerable<Doctor> Doctors { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
