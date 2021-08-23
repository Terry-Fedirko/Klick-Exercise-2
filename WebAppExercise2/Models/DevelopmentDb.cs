using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Text;

namespace WebApplication1.Models
{
    public class DevelopmenDb : DbContext
    {
       
        public DevelopmenDb() : base("name=DevelopmentDatabase")
        {
           
            Database.SetInitializer<DevelopmenDb>(null);
         
        }

        public DbSet<CustomerInfoModel> CustomerInfo { get; set; }
        public DbSet<MedicationsModel> Medications { get; set; }

        //// Another Method to prevent table pluralization (adding s or es to the end of a model name)
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    //modelBuilder.Entity<CustomerInfo>().ToTable("CustomerInfo");
        //}

        // BEST METHOD --- 
        //  Apply Attribute `[Table("table name")]` to the model (using System.ComponentModel.DataAnnotations.Schema;)
        //  this way you don't have to use the OnModelCreating protected Model.
    }
}