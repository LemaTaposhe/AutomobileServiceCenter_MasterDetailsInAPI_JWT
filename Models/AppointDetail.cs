using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AutomobileServiceCenter_MasterDetailsInAPI.Models
{
    public class AppointDetail
    {
        [Key]
        public int AppointDetailId { get; set; }
        public int AppointId { get; set; }
        [JsonIgnore]
        public AppointMaster AppointMaster { get; set; }
        public int ServiceId { get; set; }
        public Service Service { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
