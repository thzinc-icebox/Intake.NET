using System;
using System.Web;
using MPRV.Common;
using MPRV.Process;
using System.Collections.Generic;

namespace Intake.Core.Process.Handler
{
	/// <summary>
	/// Handler to retrieve paged enumerables of <see cref="Model.Datum"/> models
	/// </summary>
	public class Data : ProducerHandler<IPagedEnumerable<Model.Datum>>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Intake.Core.Process.Handler.Data"/> class
		/// </summary>
		/// <param name="response">Response</param>
		public Data(IHandlerResponse<IPagedEnumerable<Model.Datum>> response)
			: base(response)
		{
		}
		#endregion
		#region Public Properties
		/// <summary>
		/// Optional user identifier to filter by 
		/// </summary>
		public long? UserId { get; set; }

		/// <summary>
		/// Optional enumerable of tags to filter by
		/// </summary>
		public IEnumerable<string> Tags{ get; set; }
		#endregion
		#region Protected Methods
		/// <summary>
		/// Produces the result of this handler
		/// </summary>
		/// <returns>The result</returns>
		/// <param name="context">Context</param>
		protected override IPagedEnumerable<Model.Datum> GetResult(HttpContext context)
		{
			var data = Model.Factory.DatumFactory.Current.GetData(UserId, Tags);

			return data;
		}
		#endregion
	}
}

