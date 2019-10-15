using System.Web.Mvc;
using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
using DAL.Service.Contracts;
using DAL.Service.Implementation;
using Unity;
using Unity.Mvc5;

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
            container.RegisterType<IZipSearchService, ZipSearchServiceImpl>();
            container.RegisterType<ICreditCardRepository, CreditCardRepositoryImpl>();
            container.RegisterType<ICreditCardService, CreditCardServiceImpl>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}