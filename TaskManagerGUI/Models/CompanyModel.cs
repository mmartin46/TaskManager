﻿namespace TaskManagerGUI.Models
{
    public class CompanyModel
    {
        public string Currency { get; set; }
        public string Name { get; set; }
        public double Estimate { get; set; }
        public DateTime FiscalDateEnding { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
