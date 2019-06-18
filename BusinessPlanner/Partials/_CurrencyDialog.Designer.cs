namespace BusinessPlanner.Partials
{
    partial class _CurrencyDialog
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
            this.modifyBt = new System.Windows.Forms.Button();
            this.cancelBt = new System.Windows.Forms.Button();
            this.currencyName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // modifyBt
            // 
            this.modifyBt.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.modifyBt.Location = new System.Drawing.Point(232, 94);
            this.modifyBt.Name = "modifyBt";
            this.modifyBt.Size = new System.Drawing.Size(121, 34);
            this.modifyBt.TabIndex = 1;
            this.modifyBt.Text = "Modify";
            this.modifyBt.UseVisualStyleBackColor = true;
            this.modifyBt.Click += new System.EventHandler(this.ModifyBt_Click);
            // 
            // cancelBt
            // 
            this.cancelBt.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBt.Location = new System.Drawing.Point(91, 94);
            this.cancelBt.Name = "cancelBt";
            this.cancelBt.Size = new System.Drawing.Size(121, 34);
            this.cancelBt.TabIndex = 2;
            this.cancelBt.Text = "Cancel";
            this.cancelBt.UseVisualStyleBackColor = true;
            this.cancelBt.Click += new System.EventHandler(this.CancelBt_Click);
            // 
            // currencyName
            // 
            this.currencyName.DropDownHeight = 150;
            this.currencyName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currencyName.FormattingEnabled = true;
            this.currencyName.IntegralHeight = false;
            this.currencyName.ItemHeight = 20;
            this.currencyName.Items.AddRange(new object[] {
            "AED",
            "USD",
            "EUR"});
            this.currencyName.Location = new System.Drawing.Point(37, 31);
            this.currencyName.Name = "currencyName";
            this.currencyName.Size = new System.Drawing.Size(316, 28);
            this.currencyName.TabIndex = 13;
            // 
            // _CurrencyDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 156);
            this.Controls.Add(this.currencyName);
            this.Controls.Add(this.cancelBt);
            this.Controls.Add(this.modifyBt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_CurrencyDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Modify Project Name";
            this.Load += new System.EventHandler(this._ProjectNameDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button modifyBt;
        private System.Windows.Forms.Button cancelBt;
        private System.Windows.Forms.ComboBox currencyName;
    }
}