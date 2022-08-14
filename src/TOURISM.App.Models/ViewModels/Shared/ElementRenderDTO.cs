using System.Collections.Generic;

namespace TOURISM.App.Models.ViewModels.Shared
{
    public class ElementRenderDTO
    {
        public string Tag { get; set; }
        public int? Order { get; set; }
        public string Value { get; set; }
        public string Extra { get; set; } = string.Empty;
        
        public List<ElementRenderDTO> Children { get; set; }
        
    }
}
