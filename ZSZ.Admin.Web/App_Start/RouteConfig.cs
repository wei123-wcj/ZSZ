﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZSZ.Admin.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdminRole",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AdminRole", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
