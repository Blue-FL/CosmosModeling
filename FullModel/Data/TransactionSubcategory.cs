﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class TransactionSubcategory
	{
		public int SubcategoryId { get; set; }

		public string Name { get; set; }

		public DateTime CreatedDateTimeUTC { get; set; }

		public DateTime ModifiedDateTimeUTC { get; set; }
	}
}
