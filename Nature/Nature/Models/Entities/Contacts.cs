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

		[Display(Name ="Номер телефона")]
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Display(Name = "Почтовый адрес")]
		public string Address { get; set; }

		[Display(Name = "Страна")]
		public string Country { get; set; }

		[Display(Name = "Город")]
		public string City { get; set; }

		public string Twitter { get; set; }
		public string Facebook { get; set; }
		public string LinkedIn { get; set; }
		public string VKontakte { get; set; }
		public string YouTube { get; set; }
		public string Website { get; set; }
	}
}
