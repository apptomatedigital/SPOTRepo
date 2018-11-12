using SPOT.Infrastructure;
using SPOT.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace SPOT.App_Start
{
    public class IOCconfigurator
    {
        public static void Registercomponents()
        {
            IUnityContainer container = new UnityContainer();
            registerServices(container);
            DependencyResolver.SetResolver(new SPOTDependencyResolver(container));
        }
        private static void registerServices(IUnityContainer container)
        {
            container.RegisterType<IPagingService, PagingService>();

        }
    }
}