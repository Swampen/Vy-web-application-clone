using BLL.Service.Contracts;
using BLL.Service.Implementation;
using DAL.Db.Repositories.Contracts;
using DAL.Db.Repositories.Implementation;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace WebApplication_Vy
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            //Repositories
            container.RegisterType<IVyRepository, VyRepositoryImpl>();
            container.RegisterType<ICreditCardRepository, CreditCardRepositoryImpl>();
            container.RegisterType<IStationRepository, StationRepositoryImpl>();
            container.RegisterType<ILoginRepository, LoginRepositoryImpl>();
            container.RegisterType<ICustomerRepository, CustomerRepositoryImpl>();

            //Services
            container.RegisterType<IVyService, VyServiceImpl>();
            container.RegisterType<IZipSearchService, ZipSearchServiceImpl>();
            container.RegisterType<ICreditCardService, CreditCardServiceImpl>();
            container.RegisterType<IStationService, StationServiceImpl>();
            container.RegisterType<ILoginService, LoginServiceImpl>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}