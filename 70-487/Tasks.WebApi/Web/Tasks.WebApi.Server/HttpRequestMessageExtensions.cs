using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Tasks.WebApi.Models;

namespace Tasks.WebApi.Server
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpResponseMessage CreatePostResponse<T>(this HttpRequestMessage request, T entity)
            where T : IWebApiEntity
        {
            HttpResponseMessage response = request.CreateResponse(HttpStatusCode.Created, entity);

            Link link = entity.Links.Single(l => l.Rel == "self");

            response.Headers.Add("Location", link.Href);

            return response;
        }
    }
}