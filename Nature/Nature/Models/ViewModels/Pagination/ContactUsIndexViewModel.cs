using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class ContactUsIndexViewModel
	{
		public IEnumerable<ContactUs> ContactUs { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
