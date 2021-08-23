using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using System.Data.Entity.Migrations;
using System.Text;
using System.Dynamic;

namespace WebApplication1.Controllers
{
    public class TerryController : Controller
    {
        public CommonModel Common = new CommonModel();
        public List
            <CustomerInfoModel> Model = new List<CustomerInfoModel>();
        public TerryController()
        {
            Common.FirstName = "TEST";
        }

        // GET: Terry
        [Route("Terry/HomeView")]
        public ActionResult Home()
        {
            return View("~/views/Terry/HomeView.cshtml");
        }

        [Route("")]
        [Route("Terry")]
        [Route("Terry/ListView")]
        public ActionResult ListView()
        {
            ViewBag.Description = "List of Patients";

            List<CustomerInfoModel2> Model2 = new List<CustomerInfoModel2>(); // This model replace the MedicationId (Guid) to Medication (String)

            Model2 = GetAllDataSetItems();
            //using (var ctx = new DevelopmenDb())
            //{

            //    List<CustomerInfoModel> custs = ctx.CustomerInfo.ToList();
            //    List<MedicationsModel> meds = ctx.Medications.ToList();

            //    // LINQ Using "Left" Join
            //    var query = (from c in custs
            //                 join m in meds on c.MedicationId equals m.Id into temp
            //                 from subm in temp.DefaultIfEmpty()
            //                 select new
            //                 {
            //                     Id = c.Id,
            //                     FirstName = c.FirstName,
            //                     LastName = c.LastName,
            //                     Address = c.Address,
            //                     PostalCode = c.PostalCode,
            //                     Phone = c.Phone,
            //                     City = c.City,
            //                     Province = c.Province,
            //                     Country = c.Country,
            //                     Email = c.Email,
            //                     Medication = subm?.Medication ?? string.Empty,
            //                     DeleteFlag = c.DeleteFlag
            //                 });

            //    // Copy Anonomys types to list
            //    Model2 = query.Select(x => new CustomerInfoModel2()
            //    {
            //        Id = x.Id,
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        Address = x.Address,
            //        PostalCode = x.PostalCode,
            //        Phone = x.Phone,
            //        City = x.City,
            //        Province = x.Province,
            //        Country = x.Country,
            //        Email = x.Email,
            //        Medication = x.Medication,
            //        DeleteFlag = x.DeleteFlag
            //    }).OrderBy(o => o.LastName).ToList();

            //}


            //using (var ctx = new DevelopmenDb())
            //{
            //    Model = ctx.CustomerInfo.ToList();
            //    Model = Model.OrderBy(o => o.LastName).ToList();
            //}

            return View(Model2);
        }

        private List<CustomerInfoModel2> GetAllDataSetItems()
        {
            List<CustomerInfoModel2> model2 = new List<CustomerInfoModel2>(); // This model replace the MedicationId (Guid) to Medication (String)

            using (var ctx = new DevelopmenDb())
            {

                List<CustomerInfoModel> custs = ctx.CustomerInfo.ToList();
                List<MedicationsModel> meds = ctx.Medications.ToList();

                // LINQ Using "Left" Join
                var query = (from c in custs
                             join m in meds on c.MedicationId equals m.Id into temp
                             from subm in temp.DefaultIfEmpty()
                             select new
                             {
                                 Id = c.Id,
                                 FirstName = c.FirstName,
                                 LastName = c.LastName,
                                 Address = c.Address,
                                 PostalCode = c.PostalCode,
                                 Phone = c.Phone,
                                 City = c.City,
                                 Province = c.Province,
                                 Country = c.Country,
                                 Email = c.Email,
                                 Medication = subm?.Medication ?? string.Empty,
                                 DeleteFlag = c.DeleteFlag
                             });

                // Copy Anonomys types to list
                model2 = query.Select(x => new CustomerInfoModel2()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address,
                    PostalCode = x.PostalCode,
                    Phone = x.Phone,
                    City = x.City,
                    Province = x.Province,
                    Country = x.Country,
                    Email = x.Email,
                    Medication = x.Medication,
                    DeleteFlag = x.DeleteFlag
                }).OrderBy(o => o.LastName).ToList();

            }

            return model2;
        }

        private CustomerInfoModel2 GetDataSetItemsById(Guid id)
        {
            var model2 = GetAllDataSetItems();
            var model2byId = model2.Where(x => x.Id == id).First();
            return model2byId;
        }

        [Route("Terry/EditView/{id}")]
        public ActionResult EditView(Guid id)
        {
            CustomerMedicationModel custMedModel = new CustomerMedicationModel();

            List<MedicationsModel> medModelList = new List<MedicationsModel>();
            CustomerInfoModel custInfoModel;

            using (var ctx = new DevelopmenDb())
            {
                medModelList = ctx.Medications.ToList();
                custInfoModel = ctx.CustomerInfo.Where(x => x.Id == id).First();

                custMedModel.CustomerInfoModel = custInfoModel;
                custMedModel.MedicationsModels = medModelList;
            }
            ViewBag.CustomerMedicationModel = custMedModel;
            return View(custMedModel);
        }

        [HttpPost]
        public ActionResult EditUpdate(CustomerMedicationModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new DevelopmenDb())
                {
                    using (var t = ctx.Database.BeginTransaction())
                    {
                        var meds = ctx.Medications.ToList();
                        if (model.CustomerInfoModel.Id == null || model.CustomerInfoModel.Id == Guid.Empty)
                        {
                            model.CustomerInfoModel.Id = Guid.NewGuid();
                        }

                        if (model.CustomerInfoModel.MedicationId == null)
                        {
                            model.CustomerInfoModel.MedicationId = meds.Where(x => x.Medication == "None").Select(y => y.Id).FirstOrDefault();
                        }

                        ctx.CustomerInfo.AddOrUpdate(model.CustomerInfoModel);
                        ctx.SaveChanges();

                        t.Commit();
                    }
                }
                return RedirectToAction("ListView");
            }

            using (var ctx = new DevelopmenDb())
            {
                model.MedicationsModels = ctx.Medications.ToList();
            }

            return View("EditView", model);
        }

        [Route("Terry/DetailView/{id}")]
        public ActionResult DetailView(Guid id)
        {
            var model2 = GetDataSetItemsById(id);
            return View(model2);
        }


        [Route("Terry/DeleteView/{id}")]
        public ActionResult DeleteView(Guid id)
        {
            var model2 = GetDataSetItemsById(id);
            return View(model2);

        }

        [HttpPost]
        [Route("Terry/DelectItem/{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            using (var ctx = new DevelopmenDb())
            {
                using (var t = ctx.Database.BeginTransaction())
                {

                    CustomerInfoModel m = ctx.CustomerInfo.Where(x => x.Id == id).Single();

                    ctx.CustomerInfo.Remove(m);
                    ctx.SaveChanges();

                    t.Commit();
                }
            }
            return RedirectToAction("ListView");
        }

        [Route("Terry/RemoveItem/{id}")]
        public ActionResult RemoveItem(Guid id)
        {
            using (var ctx = new DevelopmenDb())
            {
                using (var t = ctx.Database.BeginTransaction())
                {

                    CustomerInfoModel m = ctx.CustomerInfo.Where(x => x.Id == id).Single();
                    m.DeleteFlag = true;

                    ctx.CustomerInfo.AddOrUpdate(m);
                    ctx.SaveChanges();

                    t.Commit();
                }
            }
            return RedirectToAction("ListView");
        }



        [Route("Terry/CreateView")]
        public ActionResult CreateView()
        {

            var model = new CustomerMedicationModel();
            using (var ctx = new DevelopmenDb())
            {               
                model.CustomerInfoModel = new CustomerInfoModel();
                model.MedicationsModels = ctx.Medications.ToList();
            }
            return View(model);
        }


        [HttpPost]
        public ActionResult CreateItem(CustomerMedicationModel model)
        {
            if (ModelState.IsValid)
            {
                using (var ctx = new DevelopmenDb())
                {
                    using (var t = ctx.Database.BeginTransaction())
                    {
                        var meds = ctx.Medications.ToList();
                        if (model.CustomerInfoModel.Id == null || model.CustomerInfoModel.Id == Guid.Empty)
                        {
                            model.CustomerInfoModel.Id = Guid.NewGuid();
                        }

                        if (model.CustomerInfoModel.MedicationId == null)
                        {
                            model.CustomerInfoModel.MedicationId = meds.Where(x => x.Medication == "None").Select(y => y.Id).FirstOrDefault();
                        }

                        ctx.CustomerInfo.Add(model.CustomerInfoModel);

                        ctx.SaveChanges();
                        t.Commit();
                    }
                }
                return RedirectToAction("ListView");
            }

            using (var ctx = new DevelopmenDb())
            {
                model.MedicationsModels = ctx.Medications.ToList();
            }
            return View("CreateView", model);
        }



        // ====================================================================================================

        [Route("GetFirst")]
        public string GetFirst()
        {
            CustomerInfoModel cim;
            using (var ctx = new DevelopmenDb())
            {
                cim = ctx.CustomerInfo.First();
            }
            return $"{cim.FirstName} {cim.LastName}";
        }

        [Route("GetCustInfo/{id}")]
        public string GetCustInfo(Guid id)
        {
            // GetCustInfo/07C06D7B-EE4B-4BDE-A406-4C0B05FAD891
            // GetCustInfo/A204FE54-1978-4D95-B4D4-DFDC6428F9F8


            CustomerInfoModel cim;

            using (var ctx = new DevelopmenDb())
            {
                cim = ctx.CustomerInfo.Where(el => el.Id == id).First();

            }

            return $"{cim.FirstName} {cim.LastName}";
        }

        //    [Route("AddCustInfo")]
        //    public void AddCustInfo()
        //    {
        //        var model = new CustomerInfoModel()
        //        {
        //            Id = Guid.NewGuid(),
        //            FirstName = "BABY",
        //            LastName = "BLUE",
        //            Address = "Blue Street",
        //            PostalCode = "BLU123",
        //            Phone = "123-123-BLUE",
        //            City = "Blue City",
        //            Province = "Blue Province",
        //            Country = "ALL BLUE"
        //        };

        //        using (var ctx = new DevelopmenDb())
        //        {
        //            using (var transaction = ctx.Database.BeginTransaction())
        //            {
        //                ctx.CustomerInfo.Add(model);
        //                ctx.SaveChanges();

        //                transaction.Commit();
        //            }

        //        }

        //    }

    }

}