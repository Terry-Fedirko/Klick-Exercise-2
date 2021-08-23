using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CustomerMedicationModel
    {
        public CustomerInfoModel CustomerInfoModel { get; set; }
        public List<MedicationsModel> MedicationsModels { get; set; }

    }


}