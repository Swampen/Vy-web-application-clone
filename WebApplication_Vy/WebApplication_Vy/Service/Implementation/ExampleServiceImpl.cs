using System;
using System.Collections.Generic;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Db.Repositories.Implementation;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Service.Implementation
{
    public class ExampleServiceImpl : IExampleService
    {
        public List<ExampleEntityDTO> GetExampleEntityDto()
        {
            ExampleRepository repository = new ExampleRepositoryImpl();
            List<ExampleEntity> entities = repository.findAll();
            List<ExampleEntityDTO> dtos = new List<ExampleEntityDTO>();
            foreach (ExampleEntity entity in entities)
            {
                dtos.Add(MapExampleEntityDto(entity));
            }
            
            repository.findAll().ForEach(entity =>
            {
                
            });
            return dtos;
        }

        private ExampleEntityDTO MapExampleEntityDto(ExampleEntity entity)
        {
            ExampleEntityDTO dto = new ExampleEntityDTO();
            dto.id = entity.id;
            dto.name = entity.name;
            return dto;
        }
    }
}