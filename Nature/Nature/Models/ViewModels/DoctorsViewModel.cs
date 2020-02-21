using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models.ViewModels
{
	public class DoctorsViewModel
	{
		public Doctor Doctor { get; set; }
		public IEnumerable<DoctorCategory> DoctorCategories { get; set; }
	}
}
