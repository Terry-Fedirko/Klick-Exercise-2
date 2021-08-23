using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("CustomerInfo")]
    public class CustomerInfoModel
    {

        [Required]public Guid Id { get; set; }
        [Required]public string FirstName { get; set; }
        [Required]public string LastName { get; set; }
        [Required]public string Address { get; set; }
        [Required]public string PostalCode { get; set; }
        [Required]public string Phone { get; set; }
        [Required]public string City { get; set; }
        [Required]public string Province { get; set; }
        [Required]public string Country { get; set; }
        [Required] public string Email { get; set; }
        public Guid? MedicationId { get; set; }
        [Required]public bool DeleteFlag { get; set; } = false;
    }

}