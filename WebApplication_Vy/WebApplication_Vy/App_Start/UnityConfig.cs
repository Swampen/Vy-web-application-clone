using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
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
            container.RegisterType<IVyRepository, VyRepositoryImpl>();
            container.RegisterType<ITripRepository, TripRepositoryImpl>();
            container.RegisterType<IVyService, VyServiceImpl>();
            container.RegisterType<ITripService, TripServiceImpl>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}