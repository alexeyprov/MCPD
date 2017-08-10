using System;
using System.Web.Mvc;

namespace NorthwindMVC.Attributes
{
	public class ReportDurationAttribute : ActionFilterAttribute
	{
		private DateTime _startTime;

		public ReportDurationAttribute()
		{
			HeaderName = "ActionDuration";
		}

		public string HeaderName
		{
			get;
			set;
		}

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			_startTime = DateTime.Now;

			base.OnActionExecuting(filterContext);
		}

		public override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);

			filterContext.RequestContext.HttpContext.Response.AddHeader(HeaderName, 
				(DateTime.Now - _startTime).Milliseconds.ToString());
		}
	}
}