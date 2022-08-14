using System;
using System.Collections.Generic;
using System.Text;

namespace TOURISM.App.Models.ViewModels.Public.Content.Outputs
{
    public class TopHeaderDTO
    {
        public string AdditionalText { get; set; }
        public string Image { get; set; }
        public List<BreadCrumbDTO> BreadCrumb { get; set; }
    }

    public class BreadCrumbDTO
    {
        public string Action { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
    }
}
