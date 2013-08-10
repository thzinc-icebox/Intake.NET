using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	public class CreateDatum : IHttpHandler
	{
		#region Constructors
		public CreateDatum(IHandlerResponse<Tuple<ProcessResult<bool>, Model.Datum>> response)
		{
			Response = response;
		}
		#endregion
		#region Public Properties
		public IHandlerResponse<Tuple<ProcessResult<bool>, Model.Datum>> Response { get; protected set; }

		public bool IsReusable
		{
			get{ return false;}
		}
		#endregion
		#region Public Methods
		public void ProcessRequest(HttpContext context)
		{
			var value = context.Request.Form["value"];
			var description = context.Request.Form["description"];
			var tags = context.Request.Form.GetValues("tagName");

			decimal dummy;
			var latitude = decimal.TryParse(context.Request.Form["latitude"], out dummy) ? (decimal?)dummy : null;
			var longitude = decimal.TryParse(context.Request.Form["longitude"], out dummy) ? (decimal?)dummy : null;
			var accuracy = decimal.TryParse(context.Request.Form["accuracy"], out dummy) ? (decimal?)dummy : null;

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

			Response.ProcessResponse(Tuple.Create(result, datum));
		}
		#endregion
	}
}

