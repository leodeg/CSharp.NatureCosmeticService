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
		public string Title { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public string ImagePath { get; set; }

		[DataType(DataType.Date)]
		public DateTime UploadDate { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime EditDate { get; set; }

		public string ShortDescription
		{
			get
			{
				if (Description.Length > 200)
					return Description.Substring(0, 200) + "...";
				return Description;
			}
		}
	}
}
