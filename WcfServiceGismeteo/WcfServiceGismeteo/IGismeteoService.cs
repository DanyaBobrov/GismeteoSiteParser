using DataConnector.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace WcfServiceGismeteo
{
    [ServiceContract]
    public interface IGismeteoService
    {
        [OperationContract]
        IEnumerable<City> GetCitiesList();
        [OperationContract]
        IEnumerable<Forecast> GetForecastBySity(long cityId);
    }
}
