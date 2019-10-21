using DAL.DTO;
using System.Collections.Generic;

namespace BLL.Service.Contracts
{
    public interface ICustomerService
    {
        List<CustomerDto> getAllCustomerDtos();

        bool changeCustomer(CustomerDto customer);
    }
}
