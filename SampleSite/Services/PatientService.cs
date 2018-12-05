using RedoxApi;
using SampleSite.Models;
using System.Collections.Generic;
using System.Linq;
using PatientAdmin = RedoxApi.Models.Patientadmin;


namespace SampleSite.Services
{
    public static class PatientService
    {
        private static RedoxClient redox = new RedoxApi.RedoxClient("0fa96c7a-5f80-4249-bfce-7644d483ef44", "LSjjwJDhlBVZ7W5zdJ9lBhMwz1zkhrWpMsUOkwyj9XBl5DalQ0FRRWJ19QoTVw5SGQlPRZUo");

        public static IEnumerable<Patient> GetAllPatients()
        {
            var entities = new PatientsDBEntities();
            return entities.Patients.ToList();
        }

        public static Patient GetPatient(int patientId)
        {
            var entities = new PatientsDBEntities();
            return entities.Patients.SingleOrDefault(x => x.Id == patientId);
        }

        public static void SavePatient(Patient patient)
        {
            var entities = new PatientsDBEntities();
            if (!entities.Patients.Any(x => x.First == patient.First &&
                                            x.Last == patient.Last &&
                                            x.BirthDate == patient.BirthDate &&
                                            x.SocialSecurityNumber == patient.SocialSecurityNumber))
            {
                entities.Patients.Add(patient);
                entities.SaveChanges();

                //send the new patient to Redox
                var newPatient = new PatientAdmin.Newpatient.Newpatient();
                newPatient.Patient.Identifiers.Add(new PatientAdmin.Newpatient.Anonymous2() { ID = patient.MRN, IDType = "MRN" });
                newPatient.Patient.Demographics.FirstName = patient.First;
                newPatient.Patient.Demographics.LastName = patient.Last;
                newPatient.Patient.Demographics.DOB = patient.BirthDate.ToString();
                newPatient.Patient.Demographics.Sex = patient.Gender;
                newPatient.Patient.Demographics.EmailAddresses.Add(patient.Email);
                newPatient.Patient.Demographics.Address.StreetAddress = patient.AddressLine1 + patient.AddressLine2;
                newPatient.Patient.Demographics.Address.City = patient.City;
                newPatient.Patient.Demographics.Address.State = patient.State;
                newPatient.Patient.Demographics.Address.ZIP = patient.ZipCode;
                newPatient.Patient.Demographics.SSN = patient.SocialSecurityNumber.ToString();
                newPatient.Patient.Demographics.PhoneNumber.Home = patient.HomePhone.ToString();
                newPatient.Patient.Demographics.PhoneNumber.Mobile = patient.CellPhone.ToString();

                var response = redox.PatientAdmin.NewPatient(newPatient);
            }
        }

        public static void SaveRedoxPatient(PatientAdmin.Newpatient.Newpatient redoxPatient)
        {

            // Save the new patient
            var newPatient = new Patient();
            newPatient.MRN = redoxPatient.Patient.Identifiers?.Where(x => x.IDType == "MRN").Select(x => x.ID).FirstOrDefault();
            var demographics = redoxPatient.Patient.Demographics;
            newPatient.First = demographics.FirstName;
            newPatient.Last = demographics.LastName;

            System.DateTime birthDate;
            if (System.DateTime.TryParse(demographics.DOB, out birthDate))
            {
                newPatient.BirthDate = birthDate;
            }

            newPatient.Gender = demographics.Sex;
            newPatient.Email = demographics.EmailAddresses?.FirstOrDefault()?.ToString();
            newPatient.AddressLine1 = demographics.Address?.StreetAddress;
            newPatient.City = demographics.Address?.City;
            newPatient.State = demographics.Address?.State;
            newPatient.ZipCode = demographics.Address?.ZIP;
            newPatient.SocialSecurityNumber = decimal.Parse(demographics.SSN ?? "");
            newPatient.HomePhone = decimal.Parse(demographics.PhoneNumber?.Home ?? "");
            newPatient.CellPhone = decimal.Parse(demographics.PhoneNumber?.Mobile ?? "");

            var entities = new PatientsDBEntities();
            entities.Patients.Add(newPatient);
            entities.SaveChanges();
        }

        public static void UpdatePatient(Patient patient)
        {
            var entities = new PatientsDBEntities();
            var dbPatient = entities.Patients.Single(x => x.Id == patient.Id);
            dbPatient.First = patient.First;
            dbPatient.Last = patient.Last;
            dbPatient.MRN = patient.MRN;
            dbPatient.Gender = patient.Gender;
            dbPatient.Email = patient.Email;
            dbPatient.HomePhone = patient.HomePhone;
            dbPatient.BirthDate = patient.BirthDate;
            dbPatient.CellPhone = patient.CellPhone;
            dbPatient.AddressLine1 = patient.AddressLine1;
            dbPatient.AddressLine2 = patient.AddressLine2;
            dbPatient.City = patient.City;
            dbPatient.State = patient.State;
            dbPatient.ZipCode = patient.ZipCode;
            dbPatient.SocialSecurityNumber = patient.SocialSecurityNumber;

            entities.SaveChanges();

            //send updated info to Redox
            var patientUpdate = new PatientAdmin.Patientupdate.Patientupdate();
            patientUpdate.Patient.Identifiers.Add(new PatientAdmin.Patientupdate.Anonymous2() { ID = patient.MRN, IDType = "MRN" });
            patientUpdate.Patient.Demographics.FirstName = patient.First;
            patientUpdate.Patient.Demographics.LastName = patient.Last;
            patientUpdate.Patient.Demographics.DOB = patient.BirthDate.ToString();
            patientUpdate.Patient.Demographics.Sex = patient.Gender;
            patientUpdate.Patient.Demographics.EmailAddresses.Add(patient.Email);
            patientUpdate.Patient.Demographics.Address.StreetAddress = patient.AddressLine1 + patient.AddressLine2;
            patientUpdate.Patient.Demographics.Address.City = patient.City;
            patientUpdate.Patient.Demographics.Address.State = patient.State;
            patientUpdate.Patient.Demographics.Address.ZIP = patient.ZipCode;
            patientUpdate.Patient.Demographics.SSN = patient.SocialSecurityNumber.ToString();
            patientUpdate.Patient.Demographics.PhoneNumber.Home = patient.HomePhone.ToString();
            patientUpdate.Patient.Demographics.PhoneNumber.Mobile = patient.CellPhone.ToString();

            var response = redox.PatientAdmin.PatientUpdate(patientUpdate);
        }

        public static void DeletePatient(int patientId)
        {
            var entities = new PatientsDBEntities();
            var patient = entities.Patients.Single(x => x.Id == patientId);
            entities.Patients.Remove(patient);
            entities.SaveChanges();
        }
    }
}