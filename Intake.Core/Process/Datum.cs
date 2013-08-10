using System;
using MPRV.Process;
using System.Collections.Generic;
using System.Linq;

namespace Intake.Core.Process
{
	public class Datum
	{
		#region Public Methods
		public static ProcessResult<bool> GetDatum(long? datumId, out Model.Datum datum)
		{
			ProcessResult<bool> result = null;

			if (datumId.HasValue)
			{
				datum = Model.Factory.DatumFactory.Current.GetDatum(datumId.Value);

				result = new ProcessResult<bool>(datum.IsPopulated);
			}
			else{
				datum = null;

				result = new ProcessResult<bool>(false);
				result.Messages.Add("DATUM_NOT_FOUND");
			}

			return result;
		}

		public static ProcessResult<bool> CreateDatum(Model.User user, string value, string description, IEnumerable<string> tags, decimal? latitude, decimal? longitude, decimal? accuracy, out Model.Datum datum)
		{
			datum = new Model.Datum() {
				Accuracy = accuracy,
				Date = DateTime.UtcNow,
				Description = description,
				Latitude = latitude,
				Longitude = longitude,
				Tags = tags.ToList(),
				User = user,
				Value = value
			};

			return new ProcessResult<bool>(datum.Commit());
		}
		#endregion
	}
}

