using GismeteoPresentation.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GismeteoPresentation
{
    public class GismeteoViewModel : BaseViewModel
    {
        private string _information;
        private City _currentCity;

        public ICommand FindForecast { get; set; }

        public string Information
        {
            get => _information;
            set => SetProperty(ref _information, value);
        }

        public ObservableCollection<City> Cities { get; private set; }
        public ObservableCollection<Forecast> ForecastList { get; private set; }

        public City CurrentCity 
        {
            get => _currentCity;
            set 
            {
                if (SetProperty(ref _currentCity, value))
                {
                    ForecastList.Clear();
                    Information = null;
                }
            }
        }

        public void Init()
        {
            FillCitiesList();
            FindForecast = new RelayCommand(OnFindForecast, () => CurrentCity != null);
            Cities = new ObservableCollection<City>();
            ForecastList = new ObservableCollection<Forecast>();
        }

        private async void FillCitiesList()
        {
            using (var client = new Gismeteo.GismeteoServiceClient("BasicHttpBinding_IGismeteoService"))
            {
                Gismeteo.City[] result = await client.GetCitiesListAsync();
                foreach (Gismeteo.City item in result)
                {
                    Cities.Add(new City()
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }
        }

        private async void OnFindForecast() 
        {
            ForecastList.Clear();
            if (!CurrentCity.Id.HasValue)
                throw new ApplicationException("CityId is null");

            Information = "Загрузка...";
            using (var client = new Gismeteo.GismeteoServiceClient("BasicHttpBinding_IGismeteoService"))
            {
                Gismeteo.Forecast[] result = await client.GetForecastBySityAsync(CurrentCity.Id.Value);
                foreach (Gismeteo.Forecast item in result.OrderBy(x=> x.Time).ToList())
                {
                    ForecastList.Add(new Forecast()
                    {
                        Time = item.Time,
                        Temperature = item.Temperature,
                        Pressure = item.Pressure,
                        Wind = item.Wind
                    });
                }
            }
            Information = $"Погода для города {CurrentCity.Name}";
        }
    }
}
