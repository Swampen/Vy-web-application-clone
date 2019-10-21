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
            throw new NotImplementedException();
        }

        public List<CustomerDto> getAllCustomerDtos()
        {
            var customersDtos = new List<CustomerDto>();
            _customerRepository.getAllCustomers().ForEach(c => {
                customersDtos.Add(MapCustomerDto(c));
            });
            return customersDtos;
        }

        private CustomerDto MapCustomerDto(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Address = customer.Address,
                Email = customer.Address,
                Zipcode = MapZipcodeDTO(customer.Zipcode)
            };
        }

        private ZipcodeDto MapZipcodeDTO(Zipcode entity)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Zipcode, ZipcodeDto>().ReverseMap());
            var mapper = config.CreateMapper();
            var dto = mapper.Map<ZipcodeDto>(entity);
            return dto;
        }
    }
}
