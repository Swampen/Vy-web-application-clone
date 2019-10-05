using System;
using AutoMapper;
using WebApplication_Vy.Db.Repositories.Contracts;
using WebApplication_Vy.Models.DTO;
using WebApplication_Vy.Models.Entities;
using WebApplication_Vy.Service.Contracts;

namespace WebApplication_Vy.Service.Implementation
{
    public class CreditCardServiceImpl : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardServiceImpl(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public CardDTO GetCreditCard(int id)
        {
            return mapCardDto(_creditCardRepository.getCardById(id));
        }

        public CardDTO GetCreditCardFromTicketId(int ticketId)
        {
            return mapCardDto(_creditCardRepository.getCardByTicketId(ticketId));
        }

        private CardDTO mapCardDto(CreditCard creditCard)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CreditCard, CardDTO>().ReverseMap(); });
            var mapper = config.CreateMapper();
            var dto = mapper.Map<CardDTO>(creditCard);
            return dto;
        }
    }
}