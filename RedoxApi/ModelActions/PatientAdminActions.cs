using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientAdmin = RedoxApi.Models.Patientadmin;

namespace RedoxApi.ModelActions
{
    public class PatientAdminActions : ModelActionsBase
    {
        internal PatientAdminActions(RedoxClient client) : base(client) { }

        public IRestResponse<PatientAdmin.Arrival.Arrival> Arrival(PatientAdmin.Arrival.Arrival patient)
        {
            patient.Meta = new Models.Patientadmin.Arrival.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "Arrival",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Cancel.Cancel> Cancel(PatientAdmin.Cancel.Cancel patient)
        {
            patient.Meta = new Models.Patientadmin.Cancel.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "Cancel",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Discharge.Discharge> Discharge(PatientAdmin.Discharge.Discharge patient)
        {
            patient.Meta = new Models.Patientadmin.Discharge.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "Discharge",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Newpatient.Newpatient> NewPatient(PatientAdmin.Newpatient.Newpatient patient)
        {
            patient.Meta = new Models.Patientadmin.Newpatient.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "NewPatient",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Patientmerge.Patientmerge> PatientMerge(PatientAdmin.Patientmerge.Patientmerge patient)
        {
            patient.Meta = new Models.Patientadmin.Patientmerge.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "PatientMerge",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Patientupdate.Patientupdate> PatientUpdate(PatientAdmin.Patientupdate.Patientupdate patient)
        {
            patient.Meta = new Models.Patientadmin.Patientupdate.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "PatientUpdate",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Preadmit.Preadmit> Preadmit(PatientAdmin.Preadmit.Preadmit patient)
        {
            patient.Meta = new Models.Patientadmin.Preadmit.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "Preadmit",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Registration.Registration> Registration(PatientAdmin.Registration.Registration patient)
        {
            patient.Meta = new Models.Patientadmin.Registration.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "Registration",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }
        public IRestResponse<PatientAdmin.Transfer.Transfer> Transfer(PatientAdmin.Transfer.Transfer patient)
        {
            patient.Meta = new Models.Patientadmin.Transfer.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "Transfer",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }

        public IRestResponse<PatientAdmin.Visitupdate.Visitupdate> VisitUpdate(PatientAdmin.Visitupdate.Visitupdate patient)
        {
            patient.Meta = new Models.Patientadmin.Visitupdate.Meta
            {
                DataModel = "PatientAdmin",
                EventType = "VisitUpdate",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(patient);
        }

    }

}
