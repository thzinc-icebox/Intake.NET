using System;

namespace Intake.Core.Regulator
{
	public class User
	{
		#region Public Methods

		public static bool CanCreateUserWithHandle(string handle)
		{
			var user = Model.Factory.UserFactory.Current.GetUser(handle);

			return !user.IsPopulated;
		}

		#endregion
	}
}

