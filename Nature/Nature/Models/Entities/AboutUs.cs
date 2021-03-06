﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class AboutUs
	{
		[Key]
		public int Id { get; set; }

		[ForeignKey("Contacts")]
		public int ContactsId { get; set; }
		public Contacts Contacts { get; set; }

		[Required]
		[MaxLength(255)]
		[Display(Name = "Название")]
		public string Title { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Описание")]
		public string Description { get; set; }


	}
}
