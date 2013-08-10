
namespace Intake.Web
{
	using System;
	using System.Web;
	using System.Web.UI;

	public partial class Default : System.Web.UI.MasterPage
	{
		public bool IsLoggedIn
		{
			get
			{ 
				bool result;
				if (CurrentUser != null)
				{
					// TODO: Update to use CurrentUser, which will implement IPrincipal or IIdentity
					result = Context.User.Identity.IsAuthenticated;
				}
				else
				{
					result = false;
				}

				return result;
			}
		}

		public string CurrentHandle
		{
			get
			{ 
				string handle;
				if (CurrentUser != null)
				{
					handle = CurrentUser.Handle;
				}
				else
				{
					handle = null;
				}

				return handle;
			}
		}

		public Core.Model.User CurrentUser
		{
			get
			{
				Core.Model.User user = null;
				if (Context.User != null && Context.User.Identity is View.UserIdentity)
				{
					user = (Context.User.Identity as View.UserIdentity).User;
				}

				return user;
			}
		}

		public string CurrentDisplayName
		{
			get
			{
				string displayName;
				if (CurrentUser != null)
				{
					displayName = CurrentUser.Handle;

					if (!string.IsNullOrEmpty(CurrentUser.Name))
					{
						displayName += " (" + CurrentUser.Name + ")";
					}
				}
				else
				{
					displayName = null;
				}

				return displayName;
			}
		}
	}
}

