using System;
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
            
            new DocumentItem() {ItemName="Company Summary",DocumentName="company_summary.rtf",Ftype="rtf",Seq=4 },
            new DocumentItem() {ItemName="Start Up Table",DocumentName="startup_table.rtf",Ftype="rtf",Seq=6 },
            new DocumentItem() {ItemName="Product Summary",DocumentName="product_summary.rtf",Ftype="rtf",Seq=7 },
            new DocumentItem() {ItemName="Sales Strategy", DocumentName="sales_strategy.rtf",Ftype="rtf",Seq=13 },
            new DocumentItem() {ItemName="Sales Forecast Table", DocumentName="sales_forecast_table.rtf",Ftype="rtf",Seq=14 },
            new DocumentItem() {ItemName="Explain Sales Forecast", DocumentName="explain_sales_forecast_table.rtf",Ftype="rtf",Seq=15 },
            new DocumentItem() {ItemName="Financial Summary", DocumentName="financial_summary.rtf",Ftype="rtf",Seq=18 },
            new DocumentItem() {ItemName="Cash Flow", DocumentName="cash_flow.rtf",Ftype="rtf",Seq=19 },
            new DocumentItem() {ItemName="Balance Sheet", DocumentName="balance_sheet.rtf",Ftype="rtf",Seq=20 },
            new DocumentItem() {ItemName="Executive Summary", DocumentName="executive_summary.rtf",Ftype="rtf",Seq=1 },
            new DocumentItem() {ItemName="Financial Data", DocumentName="data.xls",Ftype="xls",Seq=21 },
        };
    }
}
