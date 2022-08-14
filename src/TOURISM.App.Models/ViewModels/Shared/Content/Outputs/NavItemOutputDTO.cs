using System.Collections.Generic;
using TOURISM.App.Models.Enums;

namespace TOURISM.App.Models.ViewModels.Shared.Content.Outputs
{
    public class NavItemOutputDTO
    {
        public string Key { get; set; }
        public int Order { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string Resource { get; set; }

        public ENavItemType Type { get; set; } = ENavItemType.None;
        public EItemPosition Position { get; set; } = EItemPosition.None;

        public List<NavItemOutputDTO> NavItems { get; set; } = new List<NavItemOutputDTO>();
    }
}
