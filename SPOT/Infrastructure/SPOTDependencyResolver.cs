using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace SPOT.Infrastructure
{
    public class SPOTDependencyResolver : IDependencyResolver
    {
        private IUnityContainer _unitycontainer;

        public SPOTDependencyResolver(IUnityContainer unitycontainer)
        {
            _unitycontainer = unitycontainer;
        }
        public object GetService(Type serviceType)
        {
            try
            {
                return _unitycontainer.Resolve(serviceType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _unitycontainer.ResolveAll(serviceType);
            }
            catch (Exception ex)
            {
                return new List<object>();
            }
        }
    }
}