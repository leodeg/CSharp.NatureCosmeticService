using Nature.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nature.Models
{
	interface IRepository<T> : IDisposable where T : class
	{
		IEnumerable<T> Get();

		T Get(int id);

		void Create(T item);

		void Update(T item);

		bool Delete(int id);

	}
}
