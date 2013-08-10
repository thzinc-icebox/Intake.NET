using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	public class Datum : IHttpHandler
	{
		#region Constructors
		public Datum(IHandlerResponse<Tuple<ProcessResult<bool>, Model.Datum>> response)
		{
			Response = response;
		}
		#endregion
		#region Public Properties
		public bool IsReusable
		{
			get{ return false;}
		}

		public IHandlerResponse<Tuple<ProcessResult<bool>, Model.Datum>> Response { get; protected set; }
		#endregion
		#region Public Methods
		public void ProcessRequest(HttpContext context)
		{
			long dummy;
			var datumId = long.TryParse(context.GetUrlParameter("datumId"), out dummy) ? (long?)dummy : null;

			Model.Datum datum;
			var result = Process.Datum.GetDatum(datumId, out datum);

			Response.ProcessResponse(Tuple.Create(result, datum));
		}
		#endregion
	}
}

