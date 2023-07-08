using MNC.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity;

namespace MNC
{
    public class MNCDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer container;

        public MNCDependencyResolver()
        {
            container = new UnityContainer();
            RegisterDependencies();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        private void RegisterDependencies()
        {
            // Register your dependencies using the container
            container.RegisterType<DepartmentRepository>();
            container.RegisterType<CustomerRepository>();
            container.RegisterType<ItemRepository>();
            container.RegisterType<EmployeeRepository>();
            container.RegisterType<OrderRepository>();
            // Register other dependencies
        }
    }
}