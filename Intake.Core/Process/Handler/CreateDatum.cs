using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	/// <summary>
	/// Handler to create a new <see cref="Model.Datum"/>
	/// </summary>
	public class CreateDatum : ProducerHandler<Tuple<ProcessResult<bool>, Model.Datum>>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.Process.Handler.CreateDatum"/> class
		/// </summary>
		/// <param name="response">Response to be called when the handler completes</param>
		public CreateDatum(IHandlerResponse<Tuple<ProcessResult<bool>, Model.Datum>> response)
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
		protected override Tuple<ProcessResult<bool>, Model.Datum> GetResult(HttpContext context)
		{
			var value = context.Request.Form["value"];
			var description = context.Request.Form["description"];
			var tags = context.Request.Form.GetValues("tagName");

			double dummy;
			var latitude = double.TryParse(context.Request.Form["latitude"], out dummy) ? (double?)dummy : null;
			var longitude = double.TryParse(context.Request.Form["longitude"], out dummy) ? (double?)dummy : null;
			var accuracy = double.TryParse(context.Request.Form["accuracy"], out dummy) ? (double?)dummy : null;

			Model.Datum datum;
			Model.User user;
			ProcessResult<bool> result;
			if (Process.User.GetCurrentUser(out user).Result)
			{
				result = Process.Datum.CreateDatum(user, value, description, tags, latitude, longitude, accuracy, out datum);
			}
			else
			{
				datum = null;

				result = new ProcessResult<bool>(false);
				result.Messages.Add("CURRENT_USER_NOT_FOUND");
			}

			return Tuple.Create(result, datum);
		}
		#endregion
	}
}

