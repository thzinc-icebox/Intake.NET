using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	public class CreateUser : IHttpHandler
	{
		#region Constructors
		public CreateUser(IHandlerResponse<Tuple<ProcessResult<bool>, Model.User>> response)
		{
			Response = response;
		}
		#endregion
		#region Public Properties
		public IHandlerResponse<Tuple<ProcessResult<bool>, Model.User>> Response { get; protected set; }

		public bool IsReusable
		{
			get{ return false;}
		}
		#endregion
		#region Public Methods
		public void ProcessRequest(HttpContext context)
		{
			var handle = context.Request.Form["handle"];
			var name = context.Request.Form["name"];
			var passwordPlaintext = context.Request.Form["password"];

			Model.User user;
			var result = Process.User.CreateUser(handle, name, passwordPlaintext, out user);

			Response.ProcessResponse(Tuple.Create(result, user));
		}
		#endregion
	}
}

