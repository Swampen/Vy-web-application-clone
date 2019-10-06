﻿using System.Collections.Generic;
using WebApplication_Vy.Models.DTO;

namespace WebApplication_Vy.Service.Contracts
{
    public interface IVyService
    {
        List<CustomerDto> GetCustomerDtos();

        List<TicketDto> GetTicketDtos();

        bool CreateTicket(TicketDto ticketDto);

        bool DeleteTicket(int ticketId);

        void MaskCreditCardNumber(CardDto cardDto);
    }
}