using System;
using System.Security.Principal;

namespace Intake.Core.View
{
	/// <summary>
	/// Implementation of <see cref="IIdentity"/> for use with ASP.NET's authentication model
	/// </summary>
	public class UserIdentity : IIdentity
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.View.UserIdentity"/> class
		/// </summary>
		/// <param name="user"><see cref="Model.User"/> representing the currently-logged-in user</param>
		public UserIdentity(Model.User user)
		{
			User = user;
			AuthenticationType = "Intake.Core.Model.User";
			IsAuthenticated = user.IsPopulated;
			Name = user.Handle;
		}
		#endregion
		#region Public Properties
		/// <summary>
		/// Description of authentication used
		/// </summary>
		public string AuthenticationType{ get; protected set; }

		/// <summary>
		/// Determines whether the current user is authenticated
		/// </summary>
		public bool IsAuthenticated{ get; protected set; }

		/// <summary>
		/// Username of the current user
		/// </summary>
		public string Name{ get; protected set; }

		/// <summary>
		/// Instance of <see cref="Model.User"/> corresponding to this <see cref="IIdenttiy"/>
		/// </summary>
		public Core.Model.User User;
		#endregion
	}
}

