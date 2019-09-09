using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tool_N_GOOD.Models
{
    public class Tool
    {
        [Key]
        public int ToolId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Measurement { get; set; }
       
        public int? BrandTypeId { get; set; }
        
        public BrandType BrandType { get; set; }
        
        public int? ToolTypeId { get; set; }
        
        public ToolType ToolType { get; set; }
        [Required]
        public string UserId { get; set; }
       
        public ApplicationUser User{ get; set; }
        
        public int? MeasurementTypeId { get; set; }
        
        public MeasurementType MeasurementType { get; set; }

        public virtual ICollection<UsageHistory> UsageHistories { get; set; }


    }
}