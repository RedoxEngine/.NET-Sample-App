using Newtonsoft.Json.Linq;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Controllers;

namespace SampleSite
{
    public class RedoxActionSelector : ApiControllerActionSelector
    {
        public override HttpActionDescriptor SelectAction(HttpControllerContext controllerContext)
        {
            if (!controllerContext.ControllerDescriptor.ControllerName.Contains("Redox"))
            {
                return base.SelectAction(controllerContext);
            }

            var requestContent = new HttpMessageContent(controllerContext.Request);
            var json = requestContent.HttpRequestMessage.Content.ReadAsStringAsync().Result;
            var body = JObject.Parse(json);

            // If we're using the redox controller due to a route like /api/redox, let's check out what's in the body
            // 1. This could be a dang ol' verification request
            var actions = controllerContext.ControllerDescriptor.ControllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            var challenge = (string)body.SelectToken("challenge");
            if (!string.IsNullOrEmpty(challenge))
            {
                var action = actions.Single(x => x.Name == "Verify");
                return new ReflectedHttpActionDescriptor(controllerContext.ControllerDescriptor, action);
            }

            // 2. This could be a datamodel then, just call the method named datamodel_eventtype
            var datamodel = (string)body.SelectToken("Meta.DataModel");
            var eventType = (string)body.SelectToken("Meta.EventType");
            if (!string.IsNullOrEmpty(datamodel) && !string.IsNullOrEmpty(eventType))
            {
                // Expect there to be an action with the right name
                var action = actions.SingleOrDefault(x => x.Name == datamodel + "_" + eventType);
                if (action != null)
                {
                    return new ReflectedHttpActionDescriptor(controllerContext.ControllerDescriptor, action);
                }
            }

            // Idunno what this is, just let MVC figure it out.
            return base.SelectAction(controllerContext);

        }
    }
}