using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOURISM.App.Models.ViewModels.Public.Content.Outputs
{
    public class IndividualAxiomDTO
    {
        public string Domain { get; set; }
        public string Property { get; set; }
        public string PropertyType { get; set; }
        public string Range { get; set; }
        public List<AxiomValueDTO> Values { get; set; }
    }
    public class AxiomValueDTO
    {
        public string Value { get; set; }
    }
    public class ValueDTO
    {
        public string Property { get; set; }
        public string Range { get; set; }
        public List<AxiomValueDTO> Values { get; set; }
    }
    public class AxiomQueryDTO
    {
        public string Property { get; set; }
        public string PropertyType { get; set; }
        public string Individual { get; set; }
        public string QueyItems { get; set; }
        public string Condition { get; set; }
        public string Domain { get; set; }
    }
}
