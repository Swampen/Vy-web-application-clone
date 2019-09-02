using System.Collections.Generic;
using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface IExampleService
    {
        List<ExampleEntityDTO> GetExampleEntityDto();
    }
}