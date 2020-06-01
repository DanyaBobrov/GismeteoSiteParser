using System.Runtime.Serialization;

namespace DataConnector.Models
{
    [DataContract]
    public class City
    {
        [DataMember]
        public long? Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
