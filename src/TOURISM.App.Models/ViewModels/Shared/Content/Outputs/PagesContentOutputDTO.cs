using System.Collections.Generic;

namespace TOURISM.App.Models.ViewModels.Shared.Content.Outputs
{
    public class PagesContentOutputDTO
    {
        public List<PageElementOuputDTO> Pages { get; set; }

        public PagesContentOutputDTO()
        {
            Pages = new List<PageElementOuputDTO>();
        }

        public static PagesContentOutputDTO Null => new PagesContentOutputDTO();
    }

    public class PageElementOuputDTO{
        public string Key { get; set; }
        public List<ElementRenderDTO> Content { get; set; }
    }
}
