using System;
using System.Web;
using MPRV.View;
using Intake.Core;
using MPRV.Process;
using MPRV.Common;

namespace Intake.Web.View
{
	public class Data : HandlerFactory
	{
		#region Request Handlers
		[RequestMapping(new string[] {"GET"}, "/data/:(number)datumId")]
		public IHttpHandler GetDatum(HttpContext context)
		{
			throw new NotImplementedException();
		}

		[RequestMapping(new string[] {"GET"}, "/data")]
		public IHttpHandler GetRecentData(HttpContext context)
		{
			var response = new DelegateHandlerResponse<IPagedEnumerable<Core.Model.Datum>>(data => {
				context.Items.Add("Data", data);
				context.Server.Transfer("~/View/Pages/Datum/Data.aspx");
			});

			var handler = new Core.Process.Handler.Data(response);

			return handler;
		}

		[RequestMapping(new string[] {"GET"}, "/data/tag/:tag")]
		public IHttpHandler GetTaggedData(HttpContext context)
		{
			var response = new DelegateHandlerResponse<IPagedEnumerable<Core.Model.Datum>>(data => {
				context.Items.Add("Data", data);
				context.Server.Transfer("~/View/Pages/Datum/Data.aspx");
			});

			var tags = context.GetUrlParameter("tag").Split(new char[] { '+' }, StringSplitOptions.RemoveEmptyEntries);



			var handler = new Core.Process.Handler.Data(response) {

			};

			return handler;
		}
		#endregion
	}
}

