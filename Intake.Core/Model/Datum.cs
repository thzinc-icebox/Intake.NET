using System;
using System.Collections.Generic;

namespace Intake.Core.Model
{
	public class Datum : MPRV.Model.WritableModel<Datum>
	{
		#region Public Properties
		public long DatumId { get; protected set; }

		public string Value { get; set; }

		public DateTime Date { get; set; }

		public string Description { get; set; }

		public User User { get; set; }

		public decimal Latitude { get; set; }

		public decimal Longitude { get; set; }

		public decimal Accuracy { get; set; }

		public IEnumerable<Tag> Tags { get; protected set; }

		public Location Location { get; set; }
		#endregion
		#region Public Methods

		public override bool Commit()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

