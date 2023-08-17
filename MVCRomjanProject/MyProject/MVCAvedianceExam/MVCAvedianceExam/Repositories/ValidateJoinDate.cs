using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCAvedianceExam.Repositories
{
    public class ValidateJoinDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime CurrentDate = DateTime.Now;
            string message = string.Empty;
            if (Convert.ToDateTime(value) <= CurrentDate)
            {
                return ValidationResult.Success;
            }
            else
            {
                message = "Purchase Date can not be getter than Current date";
                return new ValidationResult(message);
            }
        }
    }
}