using Microsoft.Web.Http;
using Microsoft.Web.Http.Routing;
using Microsoft.Web.Http.Versioning;
using Newtonsoft.Json.Serialization;
using Microsoft.Web.Http.Versioning.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using TheCodeCamp.Controllers;

namespace TheCodeCamp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            AutofacConfig.Register();

            config.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;


                //cfg.ApiVersionReader = new UrlSegmentApiVersionReader(); //-- with URL versioning(not proper for maintenance/legacy support)

                cfg.ApiVersionReader = ApiVersionReader.Combine(
                new HeaderApiVersionReader("x-version"),
                new QueryStringApiVersionReader("ver"));

                // USING conventions.
                //cfg.Conventions.Controller<TalksController>()
                //.HasApiVersion(1, 0)
                //.HasApiVersion(1, 1)
                //.Action(m => m.Get(default(string), default(int), default(bool)))
                //.MapToApiVersion(2, 0);


            });


            // changing casing of json.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            // Web API routes
            config.MapHttpAttributeRoutes();


            //-- with URL versioning(not proper for maintenance/legacy support)
            //var constraintResolver = new DefaultInlineConstraintResolver()
            //{
            //    ConstraintMap =
            //    {
            //        ["apiVersion"] = typeof(ApiVersionRouteConstraint)
            //    }
            //};

            //config.MapHttpAttributeRoutes(constraintResolver); //-- with URL versioning(not proper for maintenance/legacy support)



            //hidding basic general routing 
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
