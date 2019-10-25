using System.Text.RegularExpressions;
using AutoMapper;
using BLL.Service.Contracts;
using DAL.Db.Repositories.Contracts;
using DAL.DTO;
using MODEL.Models.Entities;

namespace BLL.Service.Implementation
{
    public class ZipSearchServiceImpl : IZipSearchService
    {
        private readonly IVyRepository _vyRepository;

        public ZipSearchServiceImpl(IVyRepository vyRepository)
        {
            _vyRepository = vyRepository;
        }

        public string GetPostaltown(string postalcode)
        {
            var match = Regex.Match(postalcode, "[0-9]{4}");
            if (!match.Success) return "";

            var zipcode = _vyRepository.FindZipcode(postalcode);
            if (zipcode == null) return "";
            return zipcode.Postaltown;
        }
    }
}