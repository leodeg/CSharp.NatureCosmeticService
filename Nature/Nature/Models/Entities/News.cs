using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class News
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(255)]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Изображение")]
		public string ImagePath { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Время добавления")]
		public DateTime UploadDate { get; set; }

		[DataType(DataType.DateTime)]
		[Display(Name = "Время редактирования")]
		public DateTime EditDate { get; set; }

		public string ShortDescription
		{
			get
			{
				if (!string.IsNullOrEmpty(Description))
					if (Description.Length > 200)
						return Description.Substring(0, 200) + "...";
				return Description;
			}
		}

		public string ImagePathLink { get { return ImagePath.Replace("\\", "/"); } }

	}
}
