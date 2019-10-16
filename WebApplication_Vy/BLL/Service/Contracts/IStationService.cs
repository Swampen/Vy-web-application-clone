using System.Collections.Generic;
using DAL.DTO;

namespace BLL.Service.Contracts
{
    public interface IStationService
    {
        List<StationDTO> getAllStations();
        Dictionary<string, string> getAllKeyValueStations();
    }
}