using System;

namespace Intake.Core.Model
{
	public class Tag : MPRV.Model.WritableModel<Tag>
	{
		#region Public Properties
		public long TagId { get; protected set; }
		public string Name{ get; set; }

		#endregion

		#region Public Methods

		public override bool Commit()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}

