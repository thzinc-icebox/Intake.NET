using System;

namespace Intake.Core.Regulator
{
	/// <summary>
	/// Library of methods for regulating <see cref="Model.User"/>
	/// </summary>
	public static class User
	{
		#region Public Methods
		/// <summary>
		/// Determines if can create user with handle the specified handle.
		/// </summary>
		/// <returns><c>true</c> if can create user with handle the specified handle; otherwise, <c>false</c>.</returns>
		/// <param name="handle">Handle.</param>
		public static bool CanCreateUserWithHandle(string handle)
		{
			var user = Model.Factory.UserFactory.Current.GetUser(handle);

			return !user.IsPopulated;
		}
		#endregion
	}
}

