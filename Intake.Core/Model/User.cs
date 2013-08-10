using System;
using MPRV.Common;
using MPRV.Model;
using MPRV.Model.Data;
using System.Linq;

namespace Intake.Core.Model
{
	public class User : MPRV.Model.WritableModel<User>
	{
		#region Constructors
		public User()
		{

		}
		#endregion
		#region Public Properties
		[DataRowPopulator.Column("UserId")]
		[Equatable]
		[ParameterList.Parameter("@UserId")]
		public long? UserId { get; protected set; }

		[DataRowPopulator.Column("Name")]
		[Equatable]
		[ParameterList.Parameter("@Name")]
		public string Name { get; set; }

		[DataRowPopulator.Column("Handle")]
		[Equatable]
		[ParameterList.Parameter("@Handle")]
		public string Handle { get; set; }

		[DataRowPopulator.Column("PasswordDigest")]
		[Equatable]
		[ParameterList.Parameter("@PasswordDigest")]
		public string PasswordDigest { get; set; }
		#endregion
		#region Public Methods
		public override bool Commit()
		{
			UserId = Model.Data.User.Current.Commit(this.BuildParameterList());

			return UserId.HasValue;
		}
		#endregion
	}
}

