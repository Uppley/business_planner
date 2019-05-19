namespace BusinessPlanner
{
    partial class ForecastWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.month6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sales,
            this.vat,
            this.month1,
            this.month2,
            this.month3,
            this.month4,
            this.month5,
            this.month6});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 35;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(1024, 537);
            this.dataGridView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(825, 543);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(199, 59);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1024, 768);
            this.panel1.TabIndex = 2;
            // 
            // sales
            // 
            this.sales.HeaderText = "Sales";
            this.sales.Name = "sales";
            this.sales.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // vat
            // 
            this.vat.HeaderText = "Vat Rate";
            this.vat.Name = "vat";
            this.vat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // month1
            // 
            this.month1.HeaderText = "Month1";
            this.month1.Name = "month1";
            this.month1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // month2
            // 
            this.month2.HeaderText = "Month2";
            this.month2.Name = "month2";
            this.month2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // month3
            // 
            this.month3.HeaderText = "Month3";
            this.month3.Name = "month3";
            this.month3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // month4
            // 
            this.month4.HeaderText = "Month4";
            this.month4.Name = "month4";
            this.month4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // month5
            // 
            this.month5.HeaderText = "Month5";
            this.month5.Name = "month5";
            this.month5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // month6
            // 
            this.month6.HeaderText = "Month6";
            this.month6.Name = "month6";
            this.month6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ForecastWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ForecastWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ForecastWindow";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn sales;
        private System.Windows.Forms.DataGridViewTextBoxColumn vat;
        private System.Windows.Forms.DataGridViewTextBoxColumn month1;
        private System.Windows.Forms.DataGridViewTextBoxColumn month2;
        private System.Windows.Forms.DataGridViewTextBoxColumn month3;
        private System.Windows.Forms.DataGridViewTextBoxColumn month4;
        private System.Windows.Forms.DataGridViewTextBoxColumn month5;
        private System.Windows.Forms.DataGridViewTextBoxColumn month6;
    }
}