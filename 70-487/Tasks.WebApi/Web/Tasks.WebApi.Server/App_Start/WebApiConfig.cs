using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Tasks.WebApi.Common;

namespace Tasks.WebApi.Server
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.AddRange(
                new IFilter[]
                {
                    new HostAuthenticationFilter(OAuthDefaults.AuthenticationType),
                    new NhibernateSessionAttribute()
                });

            config.MessageHandlers.Add(
                new BasicAuthenticationMessageHandler());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "Task Status",
                "api/tasks/{taskId}/status/{id}",
                new
                {
                    id = RouteParameter.Optional,
                    controller = "TaskStatus"
                });

            config.Routes.MapHttpRoute(
                "Task Priority",
                "api/tasks/{taskId}/priority/{id}",
                new
                {
                    id = RouteParameter.Optional,
                    controller = "TaskPriority"
                });

            config.Routes.MapHttpRoute(
                "Task Categories",
                "api/tasks/{taskId}/categories/{id}",
                new
                {
                    id = RouteParameter.Optional,
                    controller = "TaskCategories"
                });

            config.Routes.MapHttpRoute(
                "Task Assignees",
                "api/tasks/{taskId}/users/{id}",
                new
                {
                    id = RouteParameter.Optional,
                    controller = "TaskUsers"
                });

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new 
                {
                    id = RouteParameter.Optional 
                });
        }
    }
}
