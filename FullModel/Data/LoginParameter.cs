using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullModel.Data
{
	public class LoginParameter
	{
		public int ParamId { get; set; }
		public string ParamNumber { get; set; }
		public string ParamType { get; set; }
		public int? ParamMaxLength { get; set; }
		public int? ParamSize { get; set; }
		public string ParamCaption { get; set; }
		public string ParamVariableName { get; set; }
		public string ParamDefaultValue { get; set; }
		public bool ParamEditable { get; set; }
		public ParameterSensitivityCodeType ParamSensitivityCode { get; set; }
		public DateTime CreatedDateTimeUtc { get; set; }
		public DateTime ModifiedDateTimeUtc { get; set; }
		public bool Archived { get; set; }
	}
}
