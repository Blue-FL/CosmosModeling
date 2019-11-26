using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class MutualFundPosition
	{
		public string PositionId { get; set; }

		public SecurityInfo SecurityInfo { get; set; }

		public SecurityPosition SecurityPosition { get; set; }

		public string AchIndicator { get; set; }
	}
}
