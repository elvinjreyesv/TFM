
namespace TOURISM.App.Models.ViewModels.Shared.Content.Inputs
{
    public class PageContentInputDTO : InternationalizationSharedInputDTO
    {
        public bool IncludeSocialNetwork { get; set; } = true;
        public bool IncludeNavItems { get; set; } = true;
        public bool IncludeGallery { get; set; } = false;
        public bool IncludePageInfo{ get; set; } = true;
        public bool IncludeFooter { get; set; } = true;
        public string Code { get; set; }
        public string KeyContent { get; set; }
        public string ImageCode { get; set; }
        public string ImageType { get; set; }
    }
}
