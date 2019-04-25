using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOAprototype.Models
{
    [NotMapped]
    public class PurchaseOrderCSVModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }
        
        [Display(Name = "Entity")]
        public Entities Entity { get; set; }
        
        [Display(Name = "Business Function")]
        public string BusinessFunction { get; set; }

        [Display(Name = "Finance (Cost Pool)")]
        public string Finance { get; set; }
        
        [Display(Name = "IT Tower")]
        public string ITTower { get; set; }
        
        [Display(Name = "IT Function")]
        public string ITFunction { get; set; }

        [Display(Name = "TBM IT Service")]
        public string TBMITService { get; set; }

        [Display(Name = "TBM Category")]
        public string TBMCategory { get; set; }

        [Display(Name = "TBM Name")]
        public string TBMName { get; set; }

        [Display(Name = "E-GOV BRM")]
        public string EgovBRM { get; set; }
        
        [Display(Name = "Technology Name")]
        public string Application { get; set; }
        
        [Range(1, 10000)]
        [Display(Name = "Units")]
        public int Units { get; set; } = 0;
        
        [Range(0.01, 5000000)]
        [Display(Name = "Unit Price")]
        public decimal UnitPrice { get; set; } = 0;
        
        [Display(Name = "Total Cost")]
        public decimal TotalCost { get; set; }

        [Display(Name = "Seats/License")]
        public int? SeatsPerLicense { get; set; } = null;

        [Range(1, 1000)]
        [Display(Name = "Seats Used")]
        public int? SeatsUsed { get; set; } = null;

        [Display(Name = "Description")]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Entry Date")]
        public DateTime EntryDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Approval Date")]
        public DateTime? ApprovalDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Purchase Date")]
        public DateTime? PurchaseDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        public PurchaseOrderCSVModel ()
        {

        }

        public PurchaseOrderCSVModel (PurchaseOrder purchaseOrder)
        {
            this.Id = purchaseOrder.Id;
            this.Application = purchaseOrder.Application;
            this.Description = purchaseOrder.Description;
            this.Entity = purchaseOrder.Entity;
            this.ITFunction = purchaseOrder.ITFunction;
            this.ITTower = purchaseOrder.ITTower;
            this.Units = purchaseOrder.Units;
            this.UnitPrice = purchaseOrder.UnitPrice;
            this.TotalCost = purchaseOrder.TotalCost;
            this.SeatsPerLicense = purchaseOrder.SeatsPerLicense;
            this.SeatsUsed = purchaseOrder.SeatsUsed;
            this.BusinessFunction = purchaseOrder.BusinessFunction;
            this.Finance = purchaseOrder.Finance;
            this.TBMITService = purchaseOrder.TBMITService;
            this.TBMCategory = purchaseOrder.TBMCategory;
            this.TBMName = purchaseOrder.TBMName;
            this.EgovBRM = purchaseOrder.EgovBRM;
            this.EntryDate = purchaseOrder.EntryDate;
            this.PurchaseDate = purchaseOrder.PurchaseDate;
            this.ApprovalDate = purchaseOrder.ApprovalDate;
            this.ExpirationDate = purchaseOrder.ExpirationDate;
        }

        public PurchaseOrder ToNewPurchaseOrder()
        {
            PurchaseOrder purchaseOrder = new PurchaseOrder()
            {
                Id = this.Id,
                Application = this.Application,
                Description = this.Description,
                Entity = this.Entity,
                ITFunction = this.ITFunction,
                ITTower = this.ITTower,
                Units = this.Units,
                UnitPrice = this.UnitPrice,
                SeatsPerLicense = this.SeatsPerLicense,
                SeatsUsed = this.SeatsUsed,
                BusinessFunction = this.BusinessFunction,
                Finance = this.Finance,
                TBMITService = this.TBMITService,
                TBMCategory = this.TBMCategory,
                TBMName = this.TBMName,
                EgovBRM = this.EgovBRM,
                EntryDate = this.EntryDate == null ? DateTime.Now : this.EntryDate,
                PurchaseDate = this.PurchaseDate,
                ApprovalDate = this.ApprovalDate,
                ExpirationDate = this.ExpirationDate
            };

            return purchaseOrder;
        }

        public PurchaseOrder UpdatePurchaseOrder(PurchaseOrder order)
        {
            if (order.Id.Equals(this.Id))
            {
                order.Application = this.Application;
                order.Description = this.Description;
                order.Entity = this.Entity;
                order.ITFunction = this.ITFunction;
                order.ITTower = this.ITTower;
                order.Units = this.Units;
                order.UnitPrice = this.UnitPrice;
                order.SeatsPerLicense = this.SeatsPerLicense;
                order.SeatsUsed = this.SeatsUsed;
                order.BusinessFunction = this.BusinessFunction;
                order.Finance = this.Finance;
                order.TBMITService = this.TBMITService;
                order.TBMCategory = this.TBMCategory;
                order.TBMName = this.TBMName;
                order.EgovBRM = this.EgovBRM;
                order.EntryDate = this.EntryDate == null ? DateTime.Now : this.EntryDate;
                order.PurchaseDate = this.PurchaseDate;
                order.ApprovalDate = this.ApprovalDate;
                order.ExpirationDate = this.ExpirationDate;
            }

            return order;
        }

        public class PurchaseOrderCSVMap : ClassMap<PurchaseOrderCSVModel>
        {
            public PurchaseOrderCSVMap()
            {
                AutoMap();
                Map(po => po.Application).Name("Technology Name");
                Map(po => po.ITFunction).Name("IT Function");
                Map(po => po.ITTower).Name("IT Tower");
                Map(po => po.UnitPrice).Name("Unit Price");
                Map(po => po.TotalCost).Name("Total Cost");
                Map(po => po.SeatsPerLicense).Name("Seats Per License");
                Map(po => po.SeatsUsed).Name("Seats Used");
                Map(po => po.BusinessFunction).Name("Business Function (LOB)");
                Map(po => po.Finance).Name("Finance (Cost Pool)");
                Map(po => po.TBMITService).Name("TBM IT Service");
                Map(po => po.TBMCategory).Name("TBM Category");
                Map(po => po.TBMName).Name("TBM Name");
                Map(po => po.EgovBRM).Name("E-GOV BRM #");
                Map(po => po.EntryDate).Name("Entry Date");
                Map(po => po.ApprovalDate).Name("Approval Date");
                Map(po => po.ApprovalDate).Name("Purchase Date");
                Map(po => po.ExpirationDate).Name("Expiration Date");
            }
        }
    }
}
