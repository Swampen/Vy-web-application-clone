using System.Web.Mvc;
using BLL.Service.Contracts;
using BLL.Service.Implementation;
using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
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
            
            //Repositories
            container.RegisterType<IVyRepository, VyRepositoryImpl>();
            container.RegisterType<ICreditCardRepository, CreditCardRepositoryImpl>();
            container.RegisterType<IStationRepository, StationRepositoryImpl>();
            
            //Services
            container.RegisterType<IVyService, VyServiceImpl>();
            container.RegisterType<IZipSearchService, ZipSearchServiceImpl>();
            container.RegisterType<ICreditCardService, CreditCardServiceImpl>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}