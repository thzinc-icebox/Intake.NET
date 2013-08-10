using System;
using System.Web;
using System.Web.UI;

namespace Intake.Web.View.Pages.User
{
	public partial class Result : System.Web.UI.UserControl
	{
		public Result()
		{
			this.Load += (sender, e) => {
				if (User == null)
				{
					User = Context.Items["User"] as Core.Model.User;
					if (User == null)
					{
						Core.Model.User currentUser;
						if (Core.Process.User.GetCurrentUser(out currentUser).Result)
						{
							User = currentUser;
						}
					}
				}
			};
		}

		public Core.Model.User User { get; set; }
	}
}

