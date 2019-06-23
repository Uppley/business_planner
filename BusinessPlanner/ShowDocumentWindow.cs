using BP.Resource;
using BP.Resource.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessPlanner
{
    public partial class ShowDocumentWindow : Form
    {
        List<BookItem> bookItems;
        public ShowDocumentWindow()
        {
            InitializeComponent();
            ResourceDocument resourceDocument = new ResourceDocument();
            bookItems = resourceDocument.bookItems;
            setUpView();
        }

        private void setUpView()
        {
            int columns = 1;
            int rows = bookItems.Count() % columns == 0 ? bookItems.Count()/columns: (bookItems.Count() / columns)+1;
            Button[] buttons = new Button[bookItems.Count()];
            TableLayoutPanel tbl = new TableLayoutPanel();
            tbl.RowCount = rows;
            tbl.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddRows;
            tbl.ColumnCount = columns;
            tbl.Dock = DockStyle.Fill;
            for(int i = 0;i<tbl.RowCount;i++)
            {
                tbl.RowStyles.Add(new RowStyle(SizeType.Percent, 100/rows));
            }
            for (int i = 0; i < tbl.ColumnCount; i++)
            {
                tbl.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            }
            for (int i = 0;i<buttons.Length;i++)
            {
                buttons[i] = new Button();
                buttons[i].FlatStyle = FlatStyle.Flat;
                buttons[i].FlatAppearance.BorderColor = Color.Silver;
                buttons[i].Text = bookItems[i].Title; 
                buttons[i].Dock = DockStyle.Fill;
                buttons[i].Image = BusinessPlanner.Properties.Resources.pdf_ic1;
                buttons[i].TextImageRelation = TextImageRelation.ImageBeforeText;
                buttons[i].ImageAlign = ContentAlignment.MiddleLeft;
                buttons[i].Font = new Font("Arial", 10);
                buttons[i].Cursor = Cursors.Hand;
                buttons[i].Click += openDocument;
                tbl.Controls.Add(buttons[i], 0, 0); 
            }
            
            panel2.Controls.Add(tbl);
        }

        private void openDocument(object sender, EventArgs e)
        {
            var bt = (Button)sender;
            string doc_path = bookItems.FindAll(x => x.Title == bt.Text)[0].Path;
            try
            {
                System.Diagnostics.Process.Start(Directory.GetCurrentDirectory() + "/documents/" + doc_path);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to load the document !");
            }
            
            
        }
    }
}
