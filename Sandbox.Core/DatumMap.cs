using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Sandbox.Core
{
	public class DatumMap : ClassMap<Datum>
	{
		public DatumMap()
		{
			Id(d => d.DatumId);
			Map(d => d.Descriptor);
			Map(d => d.Latitute);
			Map(d => d.Longitude);
			References(d => d.User);
			Map(d => d.Value);
		}
	}
}
