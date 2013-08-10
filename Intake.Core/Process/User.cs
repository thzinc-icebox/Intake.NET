using System;
using System.Security.Cryptography;
using System.Text;
using MPRV.Process;
using System.Configuration;

namespace Intake.Core.Process
{
	public class User
	{
		#region Public Methods
		public static string GetPasswordDigest(string handle, string passwordPlaintext)
		{
			var salt = ConfigurationManager.AppSettings["Authorization.Salt"];
			var plaintext = string.Concat(handle, ":", passwordPlaintext, ":", salt);
			return Convert.ToBase64String(new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(plaintext)));
		}

		public static ProcessResult<bool> CreateUser(string handle, string name, string passwordPlaintext, out Model.User user)
		{
			ProcessResult<bool> result;

			if (Regulator.User.CanCreateUserWithHandle(handle))
			{
				user = new Model.User();
				user.Handle = handle;
				user.Name = name;
				user.PasswordDigest = GetPasswordDigest(handle, passwordPlaintext);
				user.Commit();

				result = new ProcessResult<bool>(true);
			}
			else
			{
				user = null;
				result = new ProcessResult<bool>(false);
				result.Messages.Add("USER_HANDLE_NOT_AVAILABLE");
			}

			return result;
		}

		public static ProcessResult<bool> GetUser(string handle, out Model.User user)
		{
			user = Model.Factory.UserFactory.Current.GetUser(handle);
			var result = new ProcessResult<bool>(user.IsPopulated);

			if (!result.Result)
			{
				result.Messages.Add("USER_HANDLE_NOT_FOUND");
			}

			return result;
		}
		#endregion
	}
}

