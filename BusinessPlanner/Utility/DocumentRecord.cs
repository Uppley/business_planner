using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessPlanner.Utility
{
    class DocumentRecord
    {
        public static List<DocumentItem> DocumentList = new List<DocumentItem>()
        {
            new DocumentItem() {ItemName="Objectives", DocumentName="objectives.rtf",Seq=2 },
            new DocumentItem() {ItemName="Company Summary",DocumentName="company_summary.rtf",Seq=3 },
            new DocumentItem() {ItemName="Ownership",DocumentName="ownership.rtf",Seq=4 },
            new DocumentItem() {ItemName="Start Up Table",DocumentName="startup_table.rtf",Seq=5 },
            new DocumentItem() {ItemName="Product Summary",DocumentName="product_summary.rtf",Seq=6 },
            new DocumentItem() {ItemName="Market Summary",DocumentName="market_summary.rtf",Seq=7 },
            new DocumentItem() {ItemName="Analysis Table", DocumentName="market_analysis.rtf",Seq=8 },
            new DocumentItem() {ItemName="Market Segmentation", DocumentName="market_segmentation.rtf",Seq=9 },
            new DocumentItem() {ItemName="Business Analysis", DocumentName="business_analysis.rtf",Seq=10 },
            new DocumentItem() {ItemName="Competitive Study", DocumentName="competitive_study.rtf",Seq=11 },
            new DocumentItem() {ItemName="Sales Strategy", DocumentName="sales_strategy.rtf",Seq=12 },
            new DocumentItem() {ItemName="Sales Forecast Table", DocumentName="sales_forecast_table.rtf",Seq=13 },
            new DocumentItem() {ItemName="Explain Sales Forecast", DocumentName="explain_sales_forecast_table.rtf",Seq=14 },
            new DocumentItem() {ItemName="Strategy Summary", DocumentName="marketing_strategy_summary.rtf",Seq=15 },
            new DocumentItem() {ItemName="Competitive Edge", DocumentName="competitive_edge.rtf",Seq=16 },
            new DocumentItem() {ItemName="Financial Summary", DocumentName="financial_summary.rtf",Seq=17 },
            new DocumentItem() {ItemName="Cash Flow", DocumentName="cash_flow.rtf",Seq=18 },
            new DocumentItem() {ItemName="Balance Sheet", DocumentName="balance_sheet.rtf",Seq=19 },
            new DocumentItem() {ItemName="Executive Summary", DocumentName="executive_summary.rtf",Seq=1 },
            
        };

    }
}
