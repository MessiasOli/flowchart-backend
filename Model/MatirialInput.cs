using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agricultural_Plan.Model
{
    public class MatirialInput
    {
        public string idarea { get; set; }
        public string idmaterial { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public string unitmensurement { get; set; }
        public double specificvalue { get; set; }
    }
}
