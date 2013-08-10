using System;

namespace Intake.Core.Model.Factory
{
	/// <summary>
	/// Factory to build instances of <see cref="Model.User"/>
	/// </summary>
	public class UserFactory : MPRV.Common.Factory<UserFactory>
	{
		#region Public Methods
		/// <summary>
		/// Gets a <see cref="User"/> by <paramref name="handle"/> and <paramref name="passwordDigest"/>
		/// </summary>
		/// <returns>Instance of <see cref="User"/> if it was found by matching both <paramref name="handle"/> and <paramref name="passwordDigest"/></returns>
		/// <param name="handle">Handle</param>
		/// <param name="passwordDigest">Password digest</param>
		public User GetUser(string handle, string passwordDigest)
		{
			var row = Data.User.Current.Get(handle, passwordDigest);
			var populator = new MPRV.Model.DataRowPopulator(row);

			var user = new User();
			user.Populate(populator.Populator);

			return user;
		}

		/// <summary>
		/// Gets a <see cref="User"/> by <paramref name="handle"/>
		/// </summary>
		/// <returns>Instance of <see cref="User"/> if it was found by matching both <paramref name="handle"/> and <paramref name="passwordDigest"/></returns>
		/// <param name="handle">Handle</param>
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

