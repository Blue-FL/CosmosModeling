using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Model
{
	public class EntityResponse<T> where T : class
	{
		public List<T> Entities { get; set; }

		public long ElapsedTimeMs { get; set; }
	}
}
