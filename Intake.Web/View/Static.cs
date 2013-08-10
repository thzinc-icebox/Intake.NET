using System;
using MPRV.View;
using System.Web;

namespace Intake.Web.View
{
	/// <summary>
	/// Static path mappings
	/// </summary>
	public class Static : HandlerFactory
	{
		#region Request Handlers
		[RequestMapping(new string[] {"GET"}, "/")]
		public IHttpHandler Index(HttpContext context)
		{
			return new ServerTransferHandler("~/View/Pages/Default.aspx");
		}

		[RequestMapping(new string[] {"GET"}, "/about")]
		public IHttpHandler About(HttpContext context)
		{
			return new ServerTransferHandler("~/View/Pages/About.aspx");
		}

		[RequestMapping(new string[] {"GET"}, "/login")]
		public IHttpHandler LoginForm(HttpContext context)
		{
			return new ServerTransferHandler("~/View/Pages/Login.aspx");
		}

		[RequestMapping(new string[] {"GET"}, "/data/new")]
		public IHttpHandler CreateNewDatum(HttpContext context)
		{
			return new ServerTransferHandler("~/View/Pages/Datum/New.aspx");
		}

		[RequestMapping(new string[] {"GET"}, "/users/new")]
		public IHttpHandler SignUp(HttpContext context)
		{
			return new ServerTransferHandler("~/View/Pages/User/New.aspx");
		}
		#endregion
	}
}

