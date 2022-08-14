using TOURISM.App.Models.ViewModels.Public.Shared.Inputs;
using System.ComponentModel.DataAnnotations;

namespace TOURISM.App.Models.ViewModels.Public.Content.Inputs
{
    public class ContentInputDTO : ClientTCInternationalizationInputDTO
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string KeyContent { get; set; }
    }
}
