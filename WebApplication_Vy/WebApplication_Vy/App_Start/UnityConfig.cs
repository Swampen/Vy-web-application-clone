using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApplication_Vy.Service.Contracts;
using WebApplication_Vy.Service.Implementation;

namespace WebApplication_Vy 
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IVyService, VyServiceImpl>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}