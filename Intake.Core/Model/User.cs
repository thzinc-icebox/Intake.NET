using System;
using MPRV.Common;

namespace Intake.Core.Model
{
	public class User : MPRV.Model.WritableModel<User>
	{
		#region Public Properties
		public long UserId { get; protected set; }

		public string Name { get; set; }

		public string Handle { get; set; }

		public IPagedEnumerable<Location> Locations { get; protected set; }
		public IPagedEnumerable<Datum> Data {get;protected set;}
	
		#endregion
		#region Public Methods

		public override bool Commit()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

