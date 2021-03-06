﻿using GuestBook.Models;
using GuestBook.Models.DAL.EF;
using GuestBook.Models.DI;
using System.Web.Mvc;
using System.Web.Routing;

namespace GuestBook
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            GuestBookDependencyResolver.Bind<IRepository, EfRepository>();
            DependencyResolver.SetResolver(new GuestBookDependencyResolver());
        }
    }
}
