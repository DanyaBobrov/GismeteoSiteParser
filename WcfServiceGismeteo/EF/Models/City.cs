using System.ComponentModel.DataAnnotations.Schema;

namespace EF.Models
{
    [Table("City")]
    public class City
    {
        public long? Id { get; set; }
        public string Name { get; set; }
    }
}
