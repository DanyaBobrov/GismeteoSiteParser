using System.Runtime.Serialization;

namespace DataConnector.Models
{
    [DataContract]
    public class Forecast
    {
        [DataMember]
        public int Time { get; set; }
        [DataMember]
        public string Temperature { get; set; }
        [DataMember]
        public string Wind { get; set; }
        [DataMember]
        public string Pressure { get; set; }
    }
}
