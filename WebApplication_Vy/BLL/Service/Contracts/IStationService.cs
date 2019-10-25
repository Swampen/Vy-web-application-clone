using DAL.DTO;
using System.Collections.Generic;

namespace BLL.Service.Contracts
{
    public interface IStationService
    {
        List<StationDTO> getAllStations();
        Dictionary<string, string> getAllKeyValueStations();
        bool deleteStation(int stationId);
        bool createStation(StationDTO stationDto);
        bool updateStation(StationDTO stationDto);
    }
}