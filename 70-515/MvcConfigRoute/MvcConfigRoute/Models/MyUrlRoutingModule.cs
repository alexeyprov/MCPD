using System;
using System.Web.Routing;
using System.Web;
using Nsquared2.Web.Mvc;

public class MyUrlRouteingModule : UrlRoutingModule
{
    protected override void Init(HttpApplication application)
    {
        RouteTableManager.RegisterRoutes();
        base.Init(application);
    }
}