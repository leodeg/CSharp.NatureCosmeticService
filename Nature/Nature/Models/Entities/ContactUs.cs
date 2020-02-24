using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	public class ContactUs
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "Имя")]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(50)]
		[Display(Name = "Фамилия")]
		public string LastName { get; set; }

		[Required]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Номер телефона")]
		public string Phone { get; set; }

		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.MultilineText)]
		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Прочитано?")]
		public bool HasBeenRead { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Время заявки")]
		public DateTime UploadTime { get; set; }

		[DataType(DataType.Date)]
		[Display(Name = "Время последнего редактирования")]
		public DateTime EditTime { get; set; }

		[NotMapped]
		public string GetReadState
		{
			get
			{
				return HasBeenRead ? "Не прочитано" : "Прочитано";
			}
		}
	}
}
