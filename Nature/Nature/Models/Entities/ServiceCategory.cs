using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class ServiceCategory
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(255)]
		[Display(Name = "Название")]
		public string Title { get; set; }
	}
}
