using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scheduling = RedoxApi.Models.Scheduling;

namespace RedoxApi.ModelActions
{
    public class SchedulingActions : ModelActionsBase
    {
        internal SchedulingActions(RedoxClient client) : base (client) { }

        public IRestResponse<Scheduling.Availableslotsresponse.Availableslotsresponse> AvailableSlots(Scheduling.Availableslots.Availableslots scheduling)
        {
            scheduling.Meta = new Scheduling.Availableslots.Meta
            {
                DataModel = "Scheduling",
                EventType = "AvailableSlots",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post<Scheduling.Availableslotsresponse.Availableslotsresponse, Scheduling.Availableslots.Availableslots>(scheduling);
        }

        public IRestResponse<Scheduling.Bookedresponse.Bookedresponse> Booked(Scheduling.Booked.Booked scheduling)
        {
            scheduling.Meta = new Scheduling.Booked.Meta
            {
                DataModel = "Scheduling",
                EventType = "Booked",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post<Scheduling.Bookedresponse.Bookedresponse, Scheduling.Booked.Booked>(scheduling);
        }

        public IRestResponse<Scheduling.Cancel.Cancel> Cancel(Scheduling.Cancel.Cancel scheduling)
        {
            scheduling.Meta = new Scheduling.Cancel.Meta
            {
                DataModel = "Scheduling",
                EventType = "Cancel",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(scheduling);
        }

        public IRestResponse<Scheduling.Modification.Modification> Modification(Scheduling.Modification.Modification scheduling)
        {
            scheduling.Meta = new Scheduling.Modification.Meta
            {
                DataModel = "Scheduling",
                EventType = "Modification",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(scheduling);
        }

        public IRestResponse<Scheduling.New.New> New(Scheduling.New.New scheduling)
        {
            scheduling.Meta = new Scheduling.New.Meta
            {
                DataModel = "Scheduling",
                EventType = "New",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(scheduling);
        }

        public IRestResponse<Scheduling.Noshow.Noshow> NoShow(Scheduling.Noshow.Noshow scheduling)
        {
            scheduling.Meta = new Scheduling.Noshow.Meta
            {
                DataModel = "Scheduling",
                EventType = "NoShow",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(scheduling);
        }

        public IRestResponse<Scheduling.Reschedule.Reschedule> Reschedule(Scheduling.Reschedule.Reschedule scheduling)
        {
            scheduling.Meta = new Scheduling.Reschedule.Meta
            {
                DataModel = "Scheduling",
                EventType = "Reschedule",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(scheduling);
        }
    }
}
