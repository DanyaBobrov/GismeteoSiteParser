using DataConnector.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace DataConnector
{
    public class DataBase
    {
        private static readonly DataBase Instance = new DataBase();
        private static string _connectionString;

        private DataBase() { }

        public static DataBase GetInstance(string connectionString)
        {
            _connectionString = connectionString;
            return Instance;
        }

        public IEnumerable<City> GetCities() 
        {
            IList<City> result = new List<City>(); 
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string citiesList = "CitiesList";
                using (MySqlCommand command = new MySqlCommand(citiesList, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            City city = new City()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            result.Add(city);
                        }
                    }
                };
            }
            return result;
        }

        public IEnumerable<Forecast> GetForecastBySity(long cityId)
        {
            IList<Forecast> result = new List<Forecast>();
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string forecastBySity = "ForecastBySity";
                using (MySqlCommand command = new MySqlCommand(forecastBySity, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    MySqlParameter parameter = new MySqlParameter("@CityId", MySqlDbType.Int32)
                    {
                        Value = cityId
                    };
                    command.Parameters.Add(parameter);
                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Forecast city = new Forecast()
                            {
                                Time = reader.GetInt32(0),
                                Temperature = reader.GetString(1),
                                Pressure = reader.GetString(2),
                                Wind = reader.GetString(3)
                            };
                            result.Add(city);
                        }
                    }
                };
            }
            return result;
        }
    }
}
