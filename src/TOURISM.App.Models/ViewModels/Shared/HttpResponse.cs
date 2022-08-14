
namespace TOURISM.App.Models.ViewModels.Shared
{
    public class HttpResponse 
    {
        public string StatusCode { get; set; }
        public string RequestMessage { get; set; }

        public dynamic Model { get; set; }
    }
}
