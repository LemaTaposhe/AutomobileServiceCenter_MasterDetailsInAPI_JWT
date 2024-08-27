using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutomobileServiceCenter_MasterDetailsInAPI.Models
{
    public class AppointMaster
    {
        [Key]
        public int AppointId { get; set; }
        public string CustomerName { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime? AppointDate { get; set; }
        public bool? IsComplete { get; set; }

        public virtual List<AppointDetail> AppointDetail { get; set; } = new List<AppointDetail>();

    }
}
