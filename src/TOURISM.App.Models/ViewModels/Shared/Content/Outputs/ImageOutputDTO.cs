using TOURISM.App.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace TOURISM.App.Models.ViewModels.Shared.Content.Outputs
{
    public class ImageOutputDTO
    {
        public EItemPosition Position { get; set; } = EItemPosition.None;
        public string Resource { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }

        public List<ElementRenderDTO> Content { get; set; }

        public ImageOutputDTO()
        {
            Content = Enumerable.Empty<ElementRenderDTO>().ToList();
        }
    }
}
