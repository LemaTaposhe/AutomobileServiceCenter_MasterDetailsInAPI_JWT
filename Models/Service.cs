using System.ComponentModel.DataAnnotations;

namespace AutomobileServiceCenter_MasterDetailsInAPI.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public virtual ICollection<AppointDetail> AppointDetail { get; set; }
    }
}
