using Agricultural_Plan.Model;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Agricultural_Plan.Service
{
    public class GenerateReportView
    {
        private List<MatirialView> report = new List<MatirialView>();
        private double totalValue = 0;
        private Area area;

        public GenerateReportView(Area area) {
            this.area = area;
        }

        public void AddMaterial(MatirialInput mat)
        {
            MatirialView view = new MatirialView();

            view.id = mat.idmaterial;
            view.description = mat.description;
            view.amount = toFixed(this.area.value * mat.specificvalue, 2);
            view.specificvalue = toFixed(mat.specificvalue, 4);
            double price = view.amount * mat.price;
            view.value = price.ToString("C", CultureInfo.CurrentCulture);
            view.specificunit = $"{mat.unitmensurement}/{area.unitmensurement}";

            this.totalValue += price;
            report.Add(view);
        }

        public Object GetReport()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            if(this.report.Count > 0)
            {
                MatirialView view = new MatirialView();
                view.description = "TOTAL";
                view.value = this.totalValue.ToString("C", CultureInfo.CurrentCulture); ;
                this.report.Add(view);
            }

            return new 
            {
                date,
                this.area,
                this.report,
                this.totalValue
            };
        }

        private double toFixed(double num, int inFixed)
        {

            return Convert.ToDouble(num.ToString($"N{inFixed}"));
        }
    }
}
