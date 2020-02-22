using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class Service
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[ForeignKey("ServiceCategory")]
		[Display(Name = "Категория")]
		public int ServiceCategoryId { get; set; }
		public virtual ServiceCategory ServiceCategory { get; set; }

		[Required]
		[MaxLength(255)]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Required]
		[Display(Name = "Цена")]
		public decimal Price { get; set; }

		[Display(Name = "Описание")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Display(Name = "Изображение")]
		public string ImagePath { get; set; }
	}
}
