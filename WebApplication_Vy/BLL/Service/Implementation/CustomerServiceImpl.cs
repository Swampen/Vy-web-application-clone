using AutoMapper;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service.Implementation
{
    public class CustomerServiceImpl : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServiceImpl(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public bool updateCustomer(CustomerDto customer)
        {
            return _customerRepository.updateCustomer(MapCustomerEntity(customer));
        }

        public List<CustomerDto> getAllCustomerDtos()
        {
            var customersDtos = new List<CustomerDto>();
            _customerRepository.getAllCustomers().ForEach(c =>
            {
                customersDtos.Add(MapCustomerDto(c));
            });
            return customersDtos;
        }

        private CustomerDto MapCustomerDto(Customer entity)
        {
            return new CustomerDto
            {
                Id = entity.Id,
                Givenname = entity.Givenname,
                Surname = entity.Surname,
                Address = entity.Address,
                Email = entity.Email,
                Zipcode = MapZipcodeDTO(entity.Zipcode)
            };
        }

        private ZipcodeDto MapZipcodeDTO(Zipcode entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDto>().ReverseMap());
            var mapper = config.CreateMapper();
            var dto = mapper.Map<ZipcodeDto>(entity);
            return dto;
        }

        private Customer MapCustomerEntity(CustomerDto dto)
        {
            var customer = new Customer
            {
                Id = dto.Id,
                Givenname = dto.Givenname,
                Surname = dto.Surname,
                Address = dto.Address,
                Zipcode = mapZipCodeEntity(dto.Zipcode),
                Email = dto.Email
            };
            return customer;
        }

        private Zipcode mapZipCodeEntity(ZipcodeDto dto)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ZipcodeDto, Zipcode>(); });
            var mapper = new Mapper(config);
            return mapper.Map<Zipcode>(dto);
        }
    }
}
