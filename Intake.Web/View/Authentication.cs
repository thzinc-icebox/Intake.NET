using System;
using MPRV.View;
using System.Web;
using System.Configuration;

namespace Intake.Web.View
{
	public class Authentication : HandlerFactory
	{
		#region Public Methods
		public static string EncryptAuthenticationCookieValue(string handle)
		{
			// HACK: This is egregious and must be changed
			return handle;
		}

		public static string DecryptAuthenticationCookieValue(string ciphertext)
		{
			// HACK: This is egregious and must be changed
			return ciphertext;
		}

		public static void SetAuthenticationCookie(Core.Model.User user, HttpContext context)
		{
			var name = ConfigurationManager.AppSettings["Authentication.Cookie.Name"];
			var path = ConfigurationManager.AppSettings["Authentication.Cookie.Path"];

			var cookie = new HttpCookie(name) {
				Path =  path,
				Value =  EncryptAuthenticationCookieValue(user.Handle)
			};

			context.Response.AppendCookie(cookie);
		}

		public static void ClearAuthenticationCookie(HttpContext context)
		{
			var name = ConfigurationManager.AppSettings["Authentication.Cookie.Name"];
			var path = ConfigurationManager.AppSettings["Authentication.Cookie.Path"];

			var cookie = new HttpCookie(name) {
				Expires = DateTime.UtcNow,
				Path =  path,
				Value =  string.Empty
			};

			context.Response.SetCookie(cookie);
		}
		#endregion
		#region Request Handlers
		[RequestMapping(new string[]{"POST"}, "/login")]
		public IHttpHandler Authenticate(HttpContext context)
		{
			var handle = context.Request.Form["handle"];
			var passwordPlaintext = context.Request.Form["password"];
			var salt = ConfigurationManager.AppSettings["Authentication.Salt"];
			var passwordDigest = Core.Process.User.GetPasswordDigest(handle, passwordPlaintext);

			var user = Core.Model.Factory.UserFactory.Current.GetUser(handle, passwordDigest);

			IHttpHandler handler;
			if (user.IsPopulated)
			{
				SetAuthenticationCookie(user, context);

				handler = new MPRV.View.RedirectHandler("/");
			}
			else
			{
				context.Items.Add("Authentication.Error", "INVALID_HANDLE_OR_PASSWORD");

				handler = new ServerTransferHandler("~/View/Pages/Login.aspx");
			}

			return handler;
		}

		[RequestMapping(new string[] {"GET"}, "/logout")]
		public IHttpHandler Logout(HttpContext context)
		{
			ClearAuthenticationCookie(context);

			return new RedirectHandler("/");
		}
		#endregion
	}
}

