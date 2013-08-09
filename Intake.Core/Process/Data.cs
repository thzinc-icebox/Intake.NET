using System;
using System.Web;
using MPRV.Common;
using MPRV.Process;
using System.Collections.Generic;

namespace Intake.Core.Process
{
	public class Data : IHttpHandler
	{
		#region Constructors
		public Data(IHandlerResponse<IPagedEnumerable<Model.Datum>> response)
		{
			Response = response;
		}
		#endregion
		#region Public Properties
		public bool IsReusable
		{
			get{ return false; }
		}

		public ISearchableParameter SearchableParameter { get; set; }

		public IEnumerable<ISortParameter> SortParameters { get; set; }

		public IHandlerResponse<IPagedEnumerable<Model.Datum>> Response { get; protected set; }
		#endregion
		#region Public Methods
		public void ProcessRequest(HttpContext context)
		{
			IPagedEnumerable<Model.Datum> data = Model.Factory.DatumFactory.Current.GetData(SearchableParameter, SortParameters);

			Response.ProcessResponse(data);
		}
		#endregion
	}
}

