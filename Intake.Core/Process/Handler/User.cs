using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	public class User : IHttpHandler
	{
		#region Constructors
		public User(IHandlerResponse<Tuple<ProcessResult<bool>, Model.User>> response)
		{
			Response = response;
		}
		#endregion
		#region Public Properties
		public bool IsReusable
		{
			get{ return false;}
		}

		public IHandlerResponse<Tuple<ProcessResult<bool>, Model.User>> Response { get; protected set; }
		#endregion
		#region Public Methods
		public void ProcessRequest(HttpContext context)
		{
			var handle = context.GetUrlParameter("handle");

			Model.User user;
			var result = Process.User.GetUser(handle, out user);

			Response.ProcessResponse(Tuple.Create(result, user));
		}
		#endregion
	}
}

