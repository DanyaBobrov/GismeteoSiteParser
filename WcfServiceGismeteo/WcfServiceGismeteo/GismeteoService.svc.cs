using DataConnector;
using DataConnector.Models;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;

namespace WcfServiceGismeteo
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class GismeteoService : IGismeteoService
    {
        public IEnumerable<City> GetCitiesList()
        {
            return DataBase.GetInstance(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
                .GetCities();
        }

        public IEnumerable<Forecast> GetForecastBySity(long cityId) 
        {
            return DataBase.GetInstance(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString)
                .GetForecastBySity(cityId);
        }
    }
}
