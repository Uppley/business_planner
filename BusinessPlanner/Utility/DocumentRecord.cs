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
            new DocumentItem() {ItemName="Objectives", DocumentName="objectives.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Company Summary",DocumentName="company_summary.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Ownership",DocumentName="ownership.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Start Up Table",DocumentName="startup_table.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Product Summary",DocumentName="product_summary.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Market Summary",DocumentName="market_summary.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Analysis Table", DocumentName="market_analysis.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Market Segmentation", DocumentName="market_segmentation.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Business Analysis", DocumentName="business_analysis.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Competitive Study", DocumentName="competitive_study.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Sales Strategy", DocumentName="sales_strategy.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Sales Forecast Table", DocumentName="sales_forecast_table.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Explain Sales Forecast", DocumentName="explain_sales_forecast_table.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Strategy Summary", DocumentName="marketing_strategy_summary.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Competitive Edge", DocumentName="competitive_edge.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Financial Summary", DocumentName="financial_summary.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Cash Flow", DocumentName="cash_flow.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Balance Sheet", DocumentName="balance_sheet.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Executive Summary", DocumentName="executive_summary.rtf",IsActive=1 },
            new DocumentItem() {ItemName="Mission", DocumentName="mission.rtf",IsActive=0 }
        };

    }
}
