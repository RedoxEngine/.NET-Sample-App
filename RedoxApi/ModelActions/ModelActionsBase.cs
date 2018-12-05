using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedoxApi.ModelActions
{
    public class ModelActionsBase
    {
        protected RedoxClient redoxClient { get; set; }

        protected ModelActionsBase(RedoxClient client)
        {
            redoxClient = client;
        }
    }
}
