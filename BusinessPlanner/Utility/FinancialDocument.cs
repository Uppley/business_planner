﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class FinancialDocument
    {
        public static List<DocumentItem> DocumentList = new List<DocumentItem>()
        {
            new DocumentItem() {ItemName="Executive Summary", DocumentName="executive_summary.rtf",Ftype="rtf",Seq=1 },
            new DocumentItem() {ItemName="Company Summary",DocumentName="company_summary.rtf",Ftype="rtf",Seq=2 },
            new DocumentItem() {ItemName="Start Up Investment",DocumentName="startup_table.rtf",Ftype="rtf",Seq=3 },
            new DocumentItem() {ItemName="Company Expenditure",DocumentName="expenditure.rtf",Ftype="rtf",Seq=4 },
            new DocumentItem() {ItemName="Product Summary",DocumentName="product_summary.rtf",Ftype="rtf",Seq=5 },
            new DocumentItem() {ItemName="Service Summary",DocumentName="service_summary.rtf",Ftype="rtf",Seq=6 },
            new DocumentItem() {ItemName="Product & Service Summary",DocumentName="product_service_summary.rtf",Ftype="rtf",Seq=7 },
            new DocumentItem() {ItemName="Sales Strategy", DocumentName="sales_strategy.rtf",Ftype="rtf",Seq=8 },
            new DocumentItem() {ItemName="Sales Forecast Table", DocumentName="sales_forecast_table.rtf",Ftype="rtf",Seq=9 },
            new DocumentItem() {ItemName="Explain Sales Forecast", DocumentName="explain_sales_forecast_table.rtf",Ftype="rtf",Seq=10 },
            new DocumentItem() {ItemName="Financial Summary", DocumentName="financial_summary.rtf",Ftype="rtf",Seq=11 },
            new DocumentItem() {ItemName="Financial Statement", DocumentName="financial_statement.rtf",Ftype="rtf",Seq=12 },
            new DocumentItem() {ItemName="MainData", DocumentName="data.xls",Ftype="xls",Seq=13 },
            
        };
    }
}
