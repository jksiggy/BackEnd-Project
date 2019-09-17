
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tool_N_GOOD.Models
{
    public class UsageHistory
    {


        [Key]
        public int UsageHistoryId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Display(Name = "Borrower")]
        public ApplicationUser User { get; set; }

        public int ToolId { get; set; }
        public Tool Tool { get; set; }
        [Required]
        [Display(Name = "Work")]
        public string TaskFor { get; set; }


        [Required]
        [InspectionValidation(ErrorMessage ="bad")]
        public bool Inspection { get; set; }


        [Required]
        public bool Serviceable { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Checkout Date")]
        public DateTime? CheckoutTime { get; set; }

        [Required]
        [Display(Name = "Expected Return Date")]
        public DateTime? ExpectedReturn { get; set; }

     
        [Display(Name = "Actual Return Date")]
        [MyDate(ErrorMessage = "Put Todays Date")]
        public DateTime? PromiseReturn { get; set; }
    }

    public class InspectionValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null) return false;
            if (value.GetType() != typeof(bool)) throw new InvalidOperationException("bad choice");
            return (bool)value;
        }
    }

    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now; //Dates  equal to today are valid (true)

        }
    }
}