using System;
using System.Web;
using System.Configuration;
using System.Security.Principal;

namespace Intake.Web.View
{
	/// <summary>
	/// Module to authenticate <see cref="Core.Model.User"/>s based on vaidating a cookie
	/// </summary>
	public class AuthenticationModule : IHttpModule
	{
		#region Public Methods
		/// <summary>
		/// Releases all resource used by the <see cref="Intake.Web.View.AuthenticationModule"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="Intake.Web.View.AuthenticationModule"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="Intake.Web.View.AuthenticationModule"/> in an unusable state.
		/// After calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="Intake.Web.View.AuthenticationModule"/> so the garbage collector can reclaim the memory that the
		/// <see cref="Intake.Web.View.AuthenticationModule"/> was occupying.</remarks>
		public void Dispose()
		{
			// NOOP: Nothing to dispose of
		}

		/// <summary>
		/// Init the specified context
		/// </summary>
		/// <param name="context">Context</param>
		public void Init(HttpApplication context)
		{
			context.AuthenticateRequest += AuthenticateRequest;
		}

		/// <summary>
		/// Authenticates the request
		/// </summary>
		/// <param name="sender"><see cref="HttpApplication"/> instance</param>
		/// <param name="e"><see cref="EventArgs"/> corresponding to this event</param>
		public static void AuthenticateRequest(object sender, EventArgs e)
		{
			var application = sender as HttpApplication;
			var context = application.Context;
			var name = ConfigurationManager.AppSettings["Authentication.Cookie.Name"];
			var cookie = context.Request.Cookies[name];
			if (cookie != null)
			{
				var handle = Authentication.DecryptAuthenticationCookieValue(cookie.Value);

				// If the handle is null or empty, the authentication failed
				if (string.IsNullOrEmpty(handle))
				{
					Authentication.ClearAuthenticationCookie(context);
				}
				else
				{
					Core.Model.User user;
					if (Core.Process.User.GetUser(handle, out user).Result)
					{
						var identity = new Core.View.UserIdentity(user);
						var principal = new GenericPrincipal(identity, new string[] { });

						context.User = principal;
					}
				}
			}
		}
		#endregion
	}
}

