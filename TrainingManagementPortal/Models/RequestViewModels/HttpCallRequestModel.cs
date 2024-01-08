using Newtonsoft.Json.Linq;

namespace TrainingManagementPortal.Models.RequestViewModels
{
    public class HttpCallRequestModel
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public object Body { get; set; }
    }
}
