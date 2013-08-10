using System;
using MPRV.Common;
using MPRV.Model;
using System.Collections.Generic;
using System.Linq;

namespace Intake.Core.Model.Factory
{
	public class DatumFactory : MPRV.Common.Factory<DatumFactory>
	{
		#region Public Methods

		public IPagedEnumerable<Datum> GetData(long? userId = null, IEnumerable<string> tags = null)
		{
			var data = new PagedReadableModelEnumerable<Datum>(
				delegate(long page, long perPage, out long totalItems, out long totalPages)
				{
					return Data.Datum.Current
						.GetLatestPaged(userId, tags, page, perPage, out totalItems, out totalPages)
						.Select(r => (ReadableModelPopulator)new DataRowPopulator(r).Populator);
				}
			);

			return data;
		}

		public Datum GetDatum(long datumId)
		{
			var row = Data.Datum.Current.Get(datumId);
			var populator = new MPRV.Model.DataRowPopulator(row);

			var datum = new Model.Datum();
			datum.Populate(populator.Populator);

			return datum;
		}
		#endregion
	}
}

