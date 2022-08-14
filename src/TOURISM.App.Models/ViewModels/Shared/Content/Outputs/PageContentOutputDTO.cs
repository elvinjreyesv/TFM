using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using TOURISM.App.Models.ViewModels.Shared.Content.Outputs;
using TOURISM.App.Models.ViewModels.Shared;

namespace TOURISM.App.Models.ViewModels.Shared.Content.Outputs
{
    public class PageContentOutputDTO
    {
        public List<ElementRenderDTO> Content { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public string ObjType { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        public string SearchValues { get; set; }
        public PageContentOutputDTO()
        {
            Content = Enumerable.Empty<ElementRenderDTO>().ToList();
        }

        public static PageContentOutputDTO Null => new PageContentOutputDTO();
    }

    public class PageContentOutputDTO<T> : PageContentOutputDTO
    {
        public T Arguments { get; set; }
        public PageContentOutputDTO() { }

        public PageContentOutputDTO(PageContentOutputDTO dto)
        {
            Content = dto.Content;
            Arguments = default(T);
        }

        public PageContentOutputDTO(PageContentOutputDTO dto, T arguments)
        {
            Content = dto.Content;
            Arguments = arguments;
        }

        public static PageContentOutputDTO<T> Null => new PageContentOutputDTO<T>()
        {
            Content = Enumerable.Empty<ElementRenderDTO>().ToList(),
            ObjType = string.Empty,
            Arguments = default(T),
            SearchValues = string.Empty,
        };
    }
}
