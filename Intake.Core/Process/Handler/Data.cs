using System;
using System.Web;
using MPRV.Common;
using MPRV.Process;
using System.Collections.Generic;

namespace Intake.Core.Process.Handler
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

		public long? UserId {get;set;}
		public IEnumerable<string> Tags{get;set;}

		public IHandlerResponse<IPagedEnumerable<Model.Datum>> Response { get; protected set; }
		#endregion
		#region Public Methods
		public void ProcessRequest(HttpContext context)
		{
			var data = Model.Factory.DatumFactory.Current.GetData(UserId, Tags);

			Response.ProcessResponse(data);
		}
		#endregion
	}
}

