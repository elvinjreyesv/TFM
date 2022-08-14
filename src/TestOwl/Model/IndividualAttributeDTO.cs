using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestOwl.Model
{
    public class IndividualAttributeDTO
    {
        public string Domain { get; set; }
        public string Property { get; set; }
        public string PropertyType { get; set; }
        public string Range { get; set; }
        public List<AttributeValueDTO> Value { get; set; }
    }

    public class AttributeValueDTO
    {
        public string Value { get; set; }
    }

    public class AttributeQueryDTO
    {
        public string Property { get; set; }
        public string PropertyType { get; set; }
        public string Individual { get; set; }
        public string QueyItems { get; set; }
        public string Condition { get; set; }
        public string Domain { get; set; }
    }
}
