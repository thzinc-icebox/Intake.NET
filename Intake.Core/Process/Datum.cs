using System;
using MPRV.Process;
using System.Collections.Generic;
using System.Linq;

namespace Intake.Core.Process
{
	/// <summary>
	/// Library of methods for retrieving and manipulating <see cref="Model.Datum"/>
	/// </summary>
	public static class Datum
	{
		#region Public Methods
		/// <summary>
		/// Creates a new <paramref name="datum"/>
		/// </summary>
		/// <returns>Process result indicating whether the request was successful</returns>
		/// <param name="user">User responsible for entering this <paramref name="datum"/></param>
		/// <param name="value">Value</param>
		/// <param name="description">Optional description of the <paramref name="value"/></param>
		/// <param name="tags">List of textual tags that apply to the <paramref name="value"/></param>
		/// <param name="latitude">Latitude corresponding to the <paramref name="value"/></param>
		/// <param name="longitude">Longitude corresponding to the <paramref name="value"/></param>
		/// <param name="accuracy">Accuracy (in meters) of the <paramref name="latitude"/> and <paramref name="longitude"/> coordinates</param>
		/// <param name="datum">Instance of <see cref="Model.Datum"/></param>
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

		/// <summary>
		/// Gets a <paramref name="datum"/>
		/// </summary>
		/// <returns>Process result indicating whether the request was successful</returns>
		/// <param name="datumId">Datum identifier</param>
		/// <param name="datum">Instance of <see cref="Model.Datum"/></param>
		public static ProcessResult<bool> GetDatum(long? datumId, out Model.Datum datum)
		{
			ProcessResult<bool> result = null;

			if (datumId.HasValue)
			{
				datum = Model.Factory.DatumFactory.Current.GetDatum(datumId.Value);

				result = new ProcessResult<bool>(datum.IsPopulated);
			}
			else
			{
				datum = null;

				result = new ProcessResult<bool>(false);
				result.Messages.Add("DATUM_NOT_FOUND");
			}

			return result;
		}
		#endregion
	}
}

