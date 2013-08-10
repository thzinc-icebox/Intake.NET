using System;

namespace Intake.Core.Model.Factory
{
	public class UserFactory : MPRV.Common.Factory<UserFactory>
	{
		#region Public Methods

		public User GetUser(string handle, string passwordDigest)
		{
			var row = Data.User.Current.Get(handle, passwordDigest);
			var populator = new MPRV.Model.DataRowPopulator(row);

			var user = new User();
			user.Populate(populator.Populator);

			return user;
		}

		public User GetUser(string handle)
		{
			var row = Data.User.Current.Get(handle);
			var populator = new MPRV.Model.DataRowPopulator(row);

			var user = new User();
			user.Populate(populator.Populator);

			return user;
		}

		#endregion
	}
}

