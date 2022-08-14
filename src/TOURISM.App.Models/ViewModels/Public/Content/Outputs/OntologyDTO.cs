using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOURISM.App.Models.ViewModels.Public.Content.Outputs
{
    public class OntologyDTO
    {
        public string Class { get; set; }
        public string Parent { get; set; }
        public string Comment { get; set; }
        public List<OntologyDTO> Children { get; set; }
        public List<IndividualPropertiesDTO> Individuals { get; set; }
    }
}
