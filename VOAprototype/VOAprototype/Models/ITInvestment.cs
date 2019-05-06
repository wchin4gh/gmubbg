using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VOAprototype.Models
{
    public class ITInvestment
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Technology Name")]
        public string Application { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Entity")]
        public Entities Entity { get; set; }

        [Required]
        [Display(Name = "IT Function")]
        public string ITFunction { get; set; }

        [Display(Name = "IT Tower")]
        public string ITTower { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = "Units")]
        public int Units { get; set; } = 0;

        [Required]
        [Range(0.00, 5000000)]
        [Display(Name = "Unit Price")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; } = 0;

        [NotMapped]
        [Display(Name = "Total Cost")]
        public decimal TotalCost
        {
            get
            {
                return Units * UnitPrice;
            }
        }

        [Display(Name = "Seats/License")]
        public int? SeatsPerLicense { get; set; } = null;

        [Range(1, 1000)]
        [Display(Name = "Seats Used")]
        public int? SeatsUsed { get; set; } = null;

        [Required]
        [Display(Name = "Business Function")]
        public string BusinessFunction { get; set; }

        [Display(Name = "Finance (Cost Pool)")]
        public string Finance { get; set; }

        [Display(Name = "TBM IT Service")]
        public string TBMITService { get; set; }

        [Display(Name = "TBM Category")]
        public string TBMCategory { get; set; }

        [Display(Name = "TBM Name")]
        public string TBMName { get; set; }

        [Display(Name = "E-GOV BRM")]
        public string EgovBRM { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Approval Date")]
        public DateTime? ApprovalDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "1st Classification")]
        public string FirstClassification { get; set; }

        [Display(Name = "2nd Classification")]
        public string SecondClassification { get; set; }

        [Display(Name = "3rd Classification")]
        public string ThirdClassification { get; set; }
    }
}
