namespace BusinessPlanner
{
    partial class ResourceWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResourceWindow));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.openVideos = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.openDocuments = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openLinks = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(32, 103);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(730, 259);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.openVideos);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(488, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(236, 247);
            this.panel3.TabIndex = 2;
            // 
            // openVideos
            // 
            this.openVideos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openVideos.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.openVideos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openVideos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openVideos.Image = ((System.Drawing.Image)(resources.GetObject("openVideos.Image")));
            this.openVideos.Location = new System.Drawing.Point(0, 0);
            this.openVideos.Name = "openVideos";
            this.openVideos.Size = new System.Drawing.Size(236, 247);
            this.openVideos.TabIndex = 1;
            this.openVideos.Text = "VIDEOS";
            this.openVideos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openVideos.UseVisualStyleBackColor = true;
            this.openVideos.Click += new System.EventHandler(this.OpenVideos_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.openDocuments);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(247, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(235, 247);
            this.panel2.TabIndex = 1;
            // 
            // openDocuments
            // 
            this.openDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openDocuments.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.openDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openDocuments.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openDocuments.Image = ((System.Drawing.Image)(resources.GetObject("openDocuments.Image")));
            this.openDocuments.Location = new System.Drawing.Point(0, 0);
            this.openDocuments.Name = "openDocuments";
            this.openDocuments.Size = new System.Drawing.Size(235, 247);
            this.openDocuments.TabIndex = 1;
            this.openDocuments.Text = "DOCUMENTS";
            this.openDocuments.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openDocuments.UseVisualStyleBackColor = true;
            this.openDocuments.Click += new System.EventHandler(this.OpenDocuments_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.openLinks);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 247);
            this.panel1.TabIndex = 0;
            // 
            // openLinks
            // 
            this.openLinks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openLinks.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.openLinks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openLinks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openLinks.ForeColor = System.Drawing.Color.Black;
            this.openLinks.Image = ((System.Drawing.Image)(resources.GetObject("openLinks.Image")));
            this.openLinks.Location = new System.Drawing.Point(0, 0);
            this.openLinks.Name = "openLinks";
            this.openLinks.Size = new System.Drawing.Size(235, 247);
            this.openLinks.TabIndex = 0;
            this.openLinks.Text = "LINKS";
            this.openLinks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.openLinks.UseVisualStyleBackColor = true;
            this.openLinks.Click += new System.EventHandler(this.OpenLinks_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(221, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "RESOURCE TYPES";
            // 
            // ResourceWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ResourceWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BP Resources";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button openVideos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button openDocuments;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button openLinks;
    }
}