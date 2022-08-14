using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOwl.Model
{
    public class IndividualDTO
    {
        public string IndividualName { get; set; }
        public string Class { get; set; }
        public string Parent { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
    }

    public class PropertyDTO
    {
        public string Class { get; set; }
        public string Parent { get; set; }
        public string Comment { get; set; }
        public string Image { get; set; }
        public List<IndividualAttributeDTO> Attributes { get; set; }
    }

    public class IndividualPropertiesDTO
    {
        public string IndividualName { get; set; }
        public List<PropertyDTO> Properties { get; set; }
    }
}
