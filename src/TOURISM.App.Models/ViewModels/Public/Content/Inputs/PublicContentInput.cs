using TOURISM.App.Models.ViewModels.Public.Shared.Inputs;

namespace TOURISM.App.Models.ViewModels.Public.Content.Inputs
{
    public class PublicContentInputDTO : ClientTCInternationalizationInputDTO
    {
        public string ItemClass { get; set; }
        public string ParentClass { get; set; }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Lang = Lang ?? "ENG";
        }
    }
}
