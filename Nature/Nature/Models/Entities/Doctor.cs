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
		public int DoctorCategoryId { get; set; }
		public virtual DoctorCategory DoctorCategory { get; set; }

		[ForeignKey("Contacts")]
		public int ContactsId { get; set; }
		public virtual Contacts Contacts { get; set; }

		[Required]
		[MaxLength(50)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		public string LastName { get; set; }

		[MaxLength(50)]
		public string MiddleName { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		public string ImagePath { get; set; }
	}
}
