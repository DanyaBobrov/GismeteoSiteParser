using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace GismeteoParser
{
    public static class SaveForecast
    {
        private const string InsertCityProc = "InsertCity";
        private const string InsertDateForecast = "INSERT DateForecast (CityId, DateForecast) " +
                                                  "VALUES (@Id, @Date)";
        private const string InsertForecast = "INSERT Forecast (DateForecastId, TimeForecast, Temperature, Wind, Pressure) " +
                                              "VALUES (@DateForecastId, @TimeForecast, @Temperature, @Wind, @Pressure)";
        private const string CheckDate = "SELECT D.DateForecast " +
                                         "FROM DateForecast D " +
                                         "Where D.CityId = @Id " +
                                         "ORDER BY D.DateForecast DESC";

        public static void Save(IEnumerable<City> сities)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (City item in сities)
                    {
                        SaveCity(item, connection, transaction);

                        if (CheckDateForecast(item, connection, transaction))
                            SaveForecastBySity(item, connection, transaction);
                    }
                    transaction.Commit();
                }
            }
        }

        private static void SaveCity(City item, MySqlConnection connection, MySqlTransaction transaction) 
        {
            using (MySqlCommand command = new MySqlCommand(InsertCityProc, connection, transaction))
            {
                command.CommandType = CommandType.StoredProcedure;
                MySqlParameter parameter = new MySqlParameter("@CityId", MySqlDbType.Int32)
                {
                    Value = item.Id
                };
                MySqlParameter parameterName = new MySqlParameter("@CityName", MySqlDbType.VarChar)
                {
                    Value = item.Name
                };
                command.Parameters.Add(parameter);
                command.Parameters.Add(parameterName);

                command.ExecuteNonQuery();
            }
        }

        private static void SaveForecastBySity(City item, MySqlConnection connection, MySqlTransaction transaction) 
        {
            using (MySqlCommand command = new MySqlCommand(InsertDateForecast, connection, transaction))
            {
                MySqlParameter parameterId = new MySqlParameter("@Id", MySqlDbType.Int32)
                {
                    Value = item.Id
                };
                MySqlParameter parameterDate = new MySqlParameter("@Date", MySqlDbType.DateTime)
                {
                    Value = item.DateForecast
                };
                command.Parameters.Add(parameterId);
                command.Parameters.Add(parameterDate);
                command.ExecuteNonQuery();

                SaveForecastInterval(item, connection, transaction);
            }
        }

        private static void SaveForecastInterval(City item, MySqlConnection connection, MySqlTransaction transaction) 
        {
            long lastId = default;
            using (MySqlCommand insertLastId = new MySqlCommand("SELECT LAST_INSERT_ID()", connection, transaction))
                lastId = long.Parse(insertLastId.ExecuteScalar().ToString());

            foreach (KeyValuePair<Time, ForecastByСity> forecast in item.Forecast)
            {
                using (MySqlCommand commandForecast = new MySqlCommand(InsertForecast, connection, transaction))
                {
                    MySqlParameter parameterForecastId = new MySqlParameter("@DateForecastId", MySqlDbType.Int32)
                    {
                        Value = lastId
                    };
                    MySqlParameter parameterTime = new MySqlParameter("@TimeForecast", MySqlDbType.Int32)
                    {
                        Value = (int)forecast.Key
                    };
                    MySqlParameter parameterTemperature = new MySqlParameter("@Temperature", MySqlDbType.VarChar)
                    {
                        Value = forecast.Value.Temperature
                    };
                    MySqlParameter parameterWind = new MySqlParameter("@Wind", MySqlDbType.VarChar)
                    {
                        Value = forecast.Value.Wind
                    };
                    MySqlParameter parameterPressure = new MySqlParameter("@Pressure", MySqlDbType.VarChar)
                    {
                        Value = forecast.Value.Pressure
                    };
                    commandForecast.Parameters.Add(parameterForecastId);
                    commandForecast.Parameters.Add(parameterTime);
                    commandForecast.Parameters.Add(parameterTemperature);
                    commandForecast.Parameters.Add(parameterWind);
                    commandForecast.Parameters.Add(parameterPressure);
                    commandForecast.ExecuteNonQuery();
                }
            }
        }

        private static bool CheckDateForecast(City item, MySqlConnection connection, MySqlTransaction transaction) 
        {
            using (MySqlCommand command = new MySqlCommand(CheckDate, connection))
            {
                MySqlParameter parameter = new MySqlParameter("@Id", MySqlDbType.Int32)
                {
                    Value = item.Id
                };
                command.Parameters.Add(parameter);
                DateTime? date = (DateTime?)command.ExecuteScalar();
                return date == null || date <= DateTime.Today;   
            }
        }
    }
}
