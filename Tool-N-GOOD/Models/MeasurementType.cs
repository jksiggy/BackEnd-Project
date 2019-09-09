using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tool_N_GOOD.Models
{
    public class MeasurementType
    {
        [Key]
        public int MeasurementTypeId { get; set; }
        [Required]
        public string Type { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }
    }
}