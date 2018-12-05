using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Media = RedoxApi.Models.Media;

namespace RedoxApi.ModelActions
{
    public class MediaActions : ModelActionsBase
    {
        internal MediaActions(RedoxClient client) : base(client) { }

        public IRestResponse<Media.Delete.Delete> Delete(Media.Delete.Delete media)
        {
            media.Meta = new Media.Delete.Meta
            {
                DataModel = "Media",
                EventType = "Delete",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(media);
        }

        public IRestResponse<Media.New.New> New(Media.New.New media)
        {
            media.Meta = new Media.New.Meta
            {
                DataModel = "Media",
                EventType = "New",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(media);
        }

        public IRestResponse<Media.Replace.Replace> Replace(Media.Replace.Replace media)
        {
            media.Meta = new Media.Replace.Meta
            {
                DataModel = "Media",
                EventType = "Replace",
                EventDateTime = DateTime.UtcNow.ToString("s", System.Globalization.CultureInfo.InvariantCulture),
            };

            return redoxClient.Post(media);
        }
    }
}
