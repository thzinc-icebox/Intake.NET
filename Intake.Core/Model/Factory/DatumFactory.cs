using System;
using MPRV.Common;
using System.Collections.Generic;

namespace Intake.Core.Model.Factory
{
	public class DatumFactory : MPRV.Common.Factory<DatumFactory>
	{
		#region Public Methods

		public IPagedEnumerable<Datum> GetData(ISearchableParameter searchableParameter, IEnumerable<ISortParameter> sortParameters)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

