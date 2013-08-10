using System;
using System.Security.Principal;
namespace Intake.Core.View
{
	public class UserIdentity : IIdentity
	{
		#region Constructors
		public UserIdentity(Core.Model.User user)
		{
			User = user;
			AuthenticationType = "Intake.Core.Model.User";
			IsAuthenticated = user.IsPopulated;
			Name = user.Handle;
		}
		#endregion
		#region Public Properties
		public string AuthenticationType{ get; protected set; }

		public bool IsAuthenticated{ get; protected set; }

		public string Name{ get; protected set; }

		public Core.Model.User User;
		#endregion
	}
}

