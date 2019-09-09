using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tool_N_GOOD.Models
{
    public class ToolType
    {
        [Key]
        public int ToolTypeId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Tool> Tools { get; set; }

    }
}