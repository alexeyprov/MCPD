//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace Orders.Website.CustomAttributes
{
    using System.Web.Mvc;
    using Orders.Shared;
    using Orders.Shared.Helpers;

    public class LogActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Guard.CheckArgumentNull(filterContext, "filterContext");

            TraceHelper.TraceInformation(
                "Executing Action '{0}', from controller '{1}'",
                filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Guard.CheckArgumentNull(filterContext, "filterContext");

            TraceHelper.TraceInformation(
                "Action '{0}', from controller '{1}' has been executed",
                filterContext.ActionDescriptor.ActionName,
                filterContext.ActionDescriptor.ControllerDescriptor.ControllerName);
        }
    }
}