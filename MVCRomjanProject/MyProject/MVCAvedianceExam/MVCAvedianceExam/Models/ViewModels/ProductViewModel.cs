using MVCAvedianceExam.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCAvedianceExam.Models.ViewModels
{
    public class ProductViewModel
    {
        [Display(Name = "Employee Id")]
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Require Product Name")]
        [DataType(DataType.Text)]
        public string ProductName { get; set; }

        [Display(Name = "Purchase Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [Required(ErrorMessage = "Require Purchase Date")]
        [ValidateJoinDate]
        [DataType(DataType.DateTime)]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please Select a Supplier")]
        public int SupplierId { get; set; }

        [Display(Name = "Supplier Email")]
        [Required(ErrorMessage = "Require Supplier Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Require Quantity")]
        [Range(1, 100, ErrorMessage = "Enter Number between 1 to 100")]
        public int Quantity { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Require Unit Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public string PageTitle { get; set; }

        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
    }
}