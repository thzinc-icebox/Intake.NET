using System;
using MPRV.Common;

namespace Intake.Core.Model
{
	public class Location : MPRV.Model.WritableModel<Location>
	{
		#region Public Properties

		public long LocationId {get;set;}

		public User User {get;set;}
		public string Name {get;set;}

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

