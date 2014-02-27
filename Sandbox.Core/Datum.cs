using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sandbox.Core
{
	public class Datum
	{
		public virtual long DatumId { get; protected set; }
		public virtual string Descriptor { get; set; }
		public virtual decimal Latitute { get; set; }

		public virtual decimal Longitude { get; set; }

		public virtual User User { get; set; }

		public virtual string Value { get; set; }
	}
}
