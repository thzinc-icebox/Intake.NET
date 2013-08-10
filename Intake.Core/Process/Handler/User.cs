using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	/// <summary>
	/// Handler to retrieve a <see cref="Model.User"/>
	/// </summary>
	public class User : ProducerHandler<Tuple<ProcessResult<bool>, Model.User>>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.Process.Handler.User"/> class
		/// </summary>
		/// <param name="response">Response</param>
		public User(IHandlerResponse<Tuple<ProcessResult<bool>, Model.User>> response)
			: base(response)
		{
		}
		#endregion
		#region Public Methods
		/// <summary>
		/// Produces the result of this handler
		/// </summary>
		/// <returns>The result</returns>
		/// <param name="context">Context</param>
		protected override Tuple<ProcessResult<bool>, Model.User> GetResult(HttpContext context)
		{
			var handle = context.GetUrlParameter("handle");

			Model.User user;
			var result = Process.User.GetUser(handle, out user);

			return Tuple.Create(result, user);
		}
		#endregion
	}
}

