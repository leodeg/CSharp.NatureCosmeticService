using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class Contacts
	{
		[Key]
		public int Id { get; set; }

		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		public string Address { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
	}
}
