using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class NewsIndexViewModel
	{
		public IEnumerable<News> News { get; set; }
		public PageViewModel PageViewModel { get; set; }
	}
}
