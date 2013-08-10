using System;
using MPRV.Common;
using MPRV.Model;
using MPRV.Model.Data;
using System.Linq;

namespace Intake.Core.Model
{
	/// <summary>
	/// Represents a user of Intake
	/// </summary>
	public class User : MPRV.Model.WritableModel<User>
	{
		#region Public Properties
		/// <summary>
		/// User identifier
		/// </summary>
		[DataRowPopulator.Column("UserId")]
		[Equatable]
		[ParameterList.Parameter("@UserId")]
		public long? UserId { get; protected set; }

		/// <summary>
		/// Display name of the <see cref="User"/>
		/// </summary>
		[DataRowPopulator.Column("Name")]
		[Equatable]
		[ParameterList.Parameter("@Name")]
		public string Name { get; set; }

		/// <summary>
		/// Handle, or username, of the <see cref="User"/>
		/// </summary>
		[DataRowPopulator.Column("Handle")]
		[Equatable]
		[ParameterList.Parameter("@Handle")]
		public string Handle { get; set; }

		/// <summary>
		/// Password digest corresponding to the <see cref="User"/>'s securely-hashed password
		/// </summary>
		[DataRowPopulator.Column("PasswordDigest")]
		[Equatable]
		[ParameterList.Parameter("@PasswordDigest")]
		public string PasswordDigest { get; set; }
		#endregion
		#region Public Methods
		/// <summary>
		/// Commits this instance to the data store
		/// </summary>
		public override bool Commit()
		{
			UserId = Model.Data.User.Current.Commit(this.BuildParameterList());

			return UserId.HasValue;
		}
		#endregion
	}
}

