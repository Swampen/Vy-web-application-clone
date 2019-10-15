using AutoMapper;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using DAL.Service.Contracts;
using MODEL.Models.Entities;

namespace DAL.Service.Implementation
{
    public class CreditCardServiceImpl : ICreditCardService
    {
        private readonly ICreditCardRepository _creditCardRepository;

        public CreditCardServiceImpl(ICreditCardRepository creditCardRepository)
        {
            _creditCardRepository = creditCardRepository;
        }

        public CardDto GetCreditCard(int id)
        {
            return mapCardDto(_creditCardRepository.GetCardById(id));
        }

        public CardDto GetCreditCardFromTicketId(int ticketId)
        {
            return mapCardDto(_creditCardRepository.GetCardByTicketId(ticketId));
        }

        private CardDto mapCardDto(CreditCard creditCard)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<CreditCard, CardDto>().ReverseMap(); });
            var mapper = config.CreateMapper();
            var dto = mapper.Map<CardDto>(creditCard);
            return dto;
        }
    }
}