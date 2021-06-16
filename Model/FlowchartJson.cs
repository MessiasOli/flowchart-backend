using System.ComponentModel.DataAnnotations.Schema;

namespace Agricultural_Plan.Model
{
    public class FlowchartJson
    {
        public int id { get; set; }
        public string lastEdition { get; set; }

        [Column(TypeName = "json")]
        public string flowchartStructure { get; set; }
    }
}
