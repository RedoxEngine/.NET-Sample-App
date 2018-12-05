using SampleSite.Models;
using SampleSite.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleSite.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            var patients = PatientService.GetAllPatients();
            return View(patients);
        }

        // GET: Patient/Details/5
        public ActionResult Details(int id)
        {
            var patient = PatientService.GetPatient(id);
            return View(patient);
        }

        // GET: Patient/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patient/Create
        [HttpPost]
        public ActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                //save a patient here
                try
                {
                    PatientService.SavePatient(patient);
                    return RedirectToAction("Index");
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var err in e.EntityValidationErrors)
                    {
                        foreach (var msg in err.ValidationErrors)
                        {
                            ModelState.AddModelError(msg.PropertyName, msg.ErrorMessage);
                        }
                    }
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        // GET: Patient/Edit/5
        public ActionResult Edit(int id)
        {
            var patient = PatientService.GetPatient(id);
            return View(patient);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Patient patient)
        {
            try
            {
                PatientService.UpdatePatient(patient);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Patient/Delete/5
        public ActionResult Delete(int id)
        {
            var patient = PatientService.GetPatient(id);
            return View(patient);
        }

        // POST: Patient/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Patient patient)
        {
            try
            {
                PatientService.DeletePatient(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
