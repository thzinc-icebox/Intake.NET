using System;
using System.Web;
using System.Configuration;
using System.Security.Principal;

namespace Intake.Web.View
{
	public class AuthenticationModule : IHttpModule
	{

		#region Public Methods
		public void Dispose()
		{
			// NOOP: Nothing to dispose of
		}

		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += AuthenticateRequest;
		}

		public static void AuthenticateRequest(object sender, EventArgs e)
		{
			var application = sender as HttpApplication;
			var context = application.Context;
			var name = ConfigurationManager.AppSettings["Authentication.Cookie.Name"];
			var cookie = context.Request.Cookies[name];
			if (cookie != null)
			{
				var handle = Authentication.DecryptAuthenticationCookieValue(cookie.Value);

				Core.Model.User user;
				if (Core.Process.User.GetUser(handle, out user).Result)
				{
					var identity = new UserIdentity(user);
					var principal = new GenericPrincipal(identity, new string[] { });

					context.User = principal;
				}
			}
		}
		#endregion
	}
}

