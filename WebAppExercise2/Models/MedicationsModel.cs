using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Medications")]
    public class MedicationsModel
    {
        [Required] public Guid Id { get; set; }
         public string Medication { get; set; }
    }
}