using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TOURISM.App.Models.ViewModels.Public.Shared.Inputs
{
    public class ClientTCInputDTO
    {
        [Required, JsonProperty("IpAddressClient")]
        public string IpAddressClient { get; set; }

        [Required, JsonProperty("Token")]
        public string Token { get; set; }

        [JsonIgnore]
        public string IpAddressService { get; set; }

        [Required, JsonProperty("SiteId")]
        public string SiteId { get; set; }

        public virtual void SetDefaults()
        {
        }
    }

    public class ClientTCInternationalizationInputDTO : ClientTCInputDTO
    {
        public string Lang { get; set; }

        public override void SetDefaults()
        {
            base.SetDefaults();
            Lang = Lang ?? "ENG";
        }
    }
}
