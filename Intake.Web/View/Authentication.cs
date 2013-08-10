using System;
using MPRV.View;
using System.Web;
using System.Configuration;

namespace Intake.Web.View
{
	/// <summary>
	/// Handler factory for authentication
	/// </summary>
	public class Authentication : HandlerFactory
	{
		#region Public Methods
		/// <summary>
		/// Encrypts the authentication cookie value
		/// </summary>
		/// <returns>The authentication cookie value</returns>
		/// <param name="handle">Handle</param>
		public static string EncryptAuthenticationCookieValue(string handle)
		{
			return string.Concat(Core.Process.User.GetPasswordDigest(handle, string.Empty), ":", handle);
		}

		/// <summary>
		/// Decrypts the authentication cookie value into a <see cref="Core.Model.User.Handle"/>
		/// </summary>
		/// <returns>The authentication cookie value</returns>
		/// <param name="ciphertext">Ciphertext</param>
		public static string DecryptAuthenticationCookieValue(string ciphertext)
		{
			string handle = null;

			var atoms = ciphertext.Split(new char[] { ':' }, 2);
			if (atoms.Length == 2)
			{
				var reportedHandle = atoms[1];
				var digest = EncryptAuthenticationCookieValue(reportedHandle);

				if (ciphertext.Equals(digest))
				{
					handle = reportedHandle;
				}
			}

			return handle;
		}

		/// <summary>
		/// Sets the authentication cookie
		/// </summary>
		/// <param name="user"><see cref="Core.Model.User"/> for whom to set the authentication cookie</param>
		/// <param name="context">Context</param>
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

		/// <summary>
		/// Clears the authentication cookie
		/// </summary>
		/// <param name="context">Context</param>
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
		/// <summary>
		/// Authenticate the POSTed user credentials
		/// </summary>
		/// <param name="context">Context</param>
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

		/// <summary>
		/// "Log out" the current user
		/// </summary>
		/// <param name="context">Context.</param>
		[RequestMapping(new string[] {"GET"}, "/logout")]
		public IHttpHandler Logout(HttpContext context)
		{
			ClearAuthenticationCookie(context);

			return new RedirectHandler("/");
		}
		#endregion
	}
}

