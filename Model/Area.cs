using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agricultural_Plan.Model
{
    public class Area
    {
        public string id { get; set; }
        public string nameOfArea { get; set; }
        public double value { get; set; }
        public string unitmensurement { get; set; }

        public void CopyFrom(Area a)
        {
            this.nameOfArea = a.nameOfArea;
            this.value = a.value;
            this.unitmensurement = a.unitmensurement;
        }
    }
}
