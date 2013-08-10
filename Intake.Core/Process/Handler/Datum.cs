using System;
using System.Web;
using MPRV.Process;

namespace Intake.Core.Process.Handler
{
	/// <summary>
	/// Handler to retrieve a <see cref="Model.Datum"/>
	/// </summary>
	public class Datum : ProducerHandler<Tuple<ProcessResult<bool>, Model.Datum>>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.Process.Handler.Datum"/> class
		/// </summary>
		/// <param name="response">Response</param>
		public Datum(IHandlerResponse<Tuple<ProcessResult<bool>, Model.Datum>> response)
			:base(response)
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
			long dummy;
			var datumId = long.TryParse(context.GetUrlParameter("datumId"), out dummy) ? (long?)dummy : null;

			Model.Datum datum;
			var result = Process.Datum.GetDatum(datumId, out datum);

			return Tuple.Create(result, datum);
		}
		#endregion
	}
}

