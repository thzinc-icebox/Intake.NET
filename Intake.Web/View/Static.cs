using System;
using MPRV.View;
using System.Web;

namespace Intake.Web.View
{
	public class Static : HandlerFactory
	{
		#region Request Handlers

		[RequestMapping(new string[] {"GET"}, "/")]
		public IHttpHandler Index(HttpContext context)
		{
			return new ServerTransferHandler("~/Default.aspx");
		}

		[RequestMapping(new string[] {"GET"}, "/about")]
		public IHttpHandler About(HttpContext context)
		{
			return new ServerTransferHandler("~/About.aspx");
		}

		#endregion
	}
}

