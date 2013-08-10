using System;
using System.Web;
using MPRV.View;
using MPRV.Process;
using System.Linq;
using MPRV.Common;

namespace Intake.Web.View
{
	/// <summary>
	/// Handler factory for user requestss
	/// </summary>
	public class Users : HandlerFactory
	{
		#region Request Handlers
		/// <summary>
		/// Creates a new <see cref="Core.Model.User"/> from the POSTed information
		/// </summary>
		/// <param name="context">Context</param>
		[RequestMapping(new string[] {"POST"}, "/users/new")]
		public IHttpHandler CreateUser(HttpContext context)
		{
			var passwordPlaintext = context.Request.Form["password"];
			var passwordConfirm = context.Request.Form["passwordConfirm"];

			IHttpHandler handler;
			if (StringComparer.Ordinal.Equals(passwordPlaintext, passwordConfirm))
			{
				var response = new DelegateHandlerResponse<Tuple<ProcessResult<bool>, Core.Model.User>>(t => {
					var result = t.Item1;
					var user = t.Item2;

					if (result.Result)
					{
						Authentication.SetAuthenticationCookie(user, context);

						context.Response.Redirect(string.Concat("/users/", user.Handle));
					}
					else
					{
						context.Items.Add("CreateUser.Error", result.Messages.FirstOrDefault());
						context.Items.Add("Handle", context.Request.Form["handle"]);
						context.Items.Add("Name", context.Request.Form["name"]);

						context.Server.Transfer("~/View/Pages/User/New.aspx");
					}
				});

				handler = new Core.Process.Handler.CreateUser(response);
			}
			else
			{
				context.Items.Add("CreateUser.Error", "PASSWORDS_DO_NOT_MATCH");
				context.Items.Add("Handle", context.Request.Form["handle"]);
				context.Items.Add("Name", context.Request.Form["name"]);

				handler = new ServerTransferHandler("~/View/Pages/User/New.aspx");
			}

			return handler;
		}

		/// <summary>
		/// Gets a <see cref="Core.Model.User"/> by handle
		/// </summary>
		/// <param name="context">Context</param>
		[RequestMapping(new string[] {"GET"}, "/users/:handle")]
		public IHttpHandler ShowUser(HttpContext context)
		{
			var response = new DelegateHandlerResponse<Tuple<ProcessResult<bool>, Core.Model.User>>(t => 
			{
				var result = t.Item1;
				var user = t.Item2;

				if (result.Result)
				{
					context.Items.Add("User", user);

					var dataResponse = new DelegateHandlerResponse<IPagedEnumerable<Core.Model.Datum>>(data => {
						context.Items.Add("Data", data);
						context.Server.Transfer("~/View/Pages/User/Show.aspx");
					});

					var handler = new Core.Process.Handler.Data(dataResponse) {
						UserId = user.UserId
					};

					handler.ProcessRequest(context);

				}
				else
				{
					context.Response.StatusCode = 404;
					context.Server.Transfer("~/View/Pages/User/NotFound.aspx");
				}
			});

			return new Core.Process.Handler.User(response);
		}

		/// <summary>
		/// Gets a list of <see cref="Core.Model.Datum"/> filted by user
		/// </summary>
		/// <param name="context">Context</param>
		[RequestMapping(new string[] {"GET"}, "/users/:handle/data")]
		[RequestMapping(new string[] {"GET"}, "/users/:handle/data/tag/:tag")]
		public IHttpHandler GetTaggedData(HttpContext context)
		{
			var response = new DelegateHandlerResponse<Tuple<ProcessResult<bool>, Core.Model.User>>(t => 
			{
				var result = t.Item1;
				var user = t.Item2;

				if (result.Result)
				{
					context.Items.Add("User", user);

					var dataResponse = new DelegateHandlerResponse<IPagedEnumerable<Core.Model.Datum>>(data => {
						context.Items.Add("Data", data);
						context.Server.Transfer("~/View/Pages/Datum/List.aspx");
					});

					var handler = new Core.Process.Handler.Data(dataResponse) {
						UserId = user.UserId,
						Tags = context.GetUrlParameter("tag").Split(new char[]{' ', '+'}, StringSplitOptions.RemoveEmptyEntries)
					};

					handler.ProcessRequest(context);
				}
				else
				{
					context.Response.StatusCode = 404;
					context.Server.Transfer("~/View/Pages/User/NotFound.aspx");
				}
			});

			return new Core.Process.Handler.User(response);
		}
		#endregion
	}
}

