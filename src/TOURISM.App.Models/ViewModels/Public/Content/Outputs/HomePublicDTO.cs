using TOURISM.App.Models.ViewModels.Shared;

namespace TOURISM.App.Models.ViewModels.Public.Content.Outputs
{
    public class HomePublicDTO
    {
        public bool IsAlertAction { get; set; }
        public AlertDTO AlertResponse { get; set; }
    }
}
