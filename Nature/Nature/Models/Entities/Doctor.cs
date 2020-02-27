using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class Doctor
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("DoctorCategory")]
		[Display(Name = "Категория")]
		public int DoctorCategoryId { get; set; }
		public virtual DoctorCategory DoctorCategory { get; set; }

		[ForeignKey("Contacts")]
		public int ContactsId { get; set; }
		public virtual Contacts Contacts { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[MaxLength(50)]
		[Display(Name = "Отчество")]
		public string MiddleName { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Изображение")]
		public string ImagePath { get; set; }

		public string ImagePathLink
		{
			get
			{
				if (ImagePath != null)
					return ImagePath.Replace("\\", "/");
				return ImagePath;
			}
		}

		public string ShortDescription
		{
			get
			{
				if (Description != null)
					if (Description.Length >= 100)
						return Description.Substring(0, 100) + "...";
				return Description;
			}
		}

	}
}
