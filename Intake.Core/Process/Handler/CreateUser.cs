using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	/// <summary>
	/// Handler to create a new <see cref="Model.User"/>
	/// </summary>
	public class CreateUser : ProducerHandler<Tuple<ProcessResult<bool>, Model.User>>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.Process.Handler.CreateUser"/> class
		/// </summary>
		/// <param name="response">Response to be called when the handler completes</param>
		public CreateUser(IHandlerResponse<Tuple<ProcessResult<bool>, Model.User>> response)
			: base(response)
		{
		}
		#endregion
		#region Protected Methods
		/// <summary>
		/// Produces the result of this handler
		/// </summary>
		/// <returns>The result</returns>
		/// <param name="context">Context</param>
		protected override Tuple<ProcessResult<bool>, Model.User> GetResult(HttpContext context)
		{
			var handle = context.Request.Form["handle"];
			var name = context.Request.Form["name"];
			var passwordPlaintext = context.Request.Form["password"];

			Model.User user;
			var result = Process.User.CreateUser(handle, name, passwordPlaintext, out user);

			return Tuple.Create(result, user);
		}
		#endregion
	}
}

