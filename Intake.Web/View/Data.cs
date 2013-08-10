using System;
using System.Web;
using MPRV.View;
using Intake.Core;
using MPRV.Process;
using MPRV.Common;
using System.Linq;

namespace Intake.Web.View
{
	/// <summary>
	/// Handler factory for data requests
	/// </summary>
	public class Data : HandlerFactory
	{
		#region Request Handlers
		/// <summary>
		/// Creates a new <see cref="Core.Model.Datum"/> from the POSTed information
		/// </summary>
		/// <param name="context">Context</param>
		[RequestMapping(new string[] {"POST"}, "/data/new")]
		public IHttpHandler CreateDatum(HttpContext context)
		{
			var response = new DelegateHandlerResponse<Tuple<ProcessResult<bool>, Core.Model.Datum>>(t => {
				var result = t.Item1;
				var datum = t.Item2;

				if (result.Result)
				{
					context.Response.Redirect(string.Concat("/data/", datum.DatumId));
				}
				else
				{
					context.Items.Add("CreateDatum.Error", result.Messages.FirstOrDefault());

					context.Items.Add("Value", context.Request.Form["value"]);
					context.Items.Add("Description", context.Request.Form["description"]);
					context.Items.Add("TagNames", context.Request.Form.GetValues("tagName"));
					context.Items.Add("LocationName", context.Request.Form["locationName"]);
					context.Items.Add("Latitude", context.Request.Form["latitude"]);
					context.Items.Add("Longitude", context.Request.Form["longitude"]);
					context.Items.Add("Accuracy", context.Request.Form["accuracy"]);

					context.Server.Transfer("~/View/Pages/Datum/New.aspx");
				}
			});

			return new Core.Process.Handler.CreateDatum(response);
		}

		/// <summary>
		/// Gets a <see cref="Core.Model.Datum"/> by datumId
		/// </summary>
		/// <param name="context">Context</param>
		[RequestMapping(new string[] {"GET"}, "/data/:(number)datumId")]
		public IHttpHandler ShowDatum(HttpContext context)
		{
			var response = new DelegateHandlerResponse<Tuple<ProcessResult<bool>, Core.Model.Datum>>(t => 
			{
				var result = t.Item1;
				var datum = t.Item2;

				if (result.Result)
				{
					context.Items.Add("Datum", datum);
					context.Items.Add("User", datum.User);
					context.Server.Transfer("~/View/Pages/Datum/Show.aspx");
				}
				else
				{
					context.Response.StatusCode = 404;
					context.Server.Transfer("~/View/Pages/Datum/NotFound.aspx");
				}
			});

			return new Core.Process.Handler.Datum(response);
		}

		/// <summary>
		/// Gets a list of <see cref="Core.Model.Datum"/>
		/// </summary>
		/// <param name="context">Context</param>
		[RequestMapping(new string[] {"GET"}, "/data")]
		[RequestMapping(new string[] {"GET"}, "/data/tag/:tag")]
		public IHttpHandler GetRecentData(HttpContext context)
		{
			var response = new DelegateHandlerResponse<IPagedEnumerable<Core.Model.Datum>>(data => {
				context.Items.Add("Data", data);
				context.Server.Transfer("~/View/Pages/Datum/List.aspx");
			});

			var handler = new Core.Process.Handler.Data(response) {
				Tags = context.GetUrlParameter("tag").Split(new char[]{' ', '+'}, StringSplitOptions.RemoveEmptyEntries)
			};

			return handler;
		}
		#endregion
	}
}

