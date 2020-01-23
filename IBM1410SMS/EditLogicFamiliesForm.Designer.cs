namespace IBM1410SMS
{
    partial class EditLogicFamiliesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.logicFamiliesDataGridView = new System.Windows.Forms.DataGridView();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.logicFamiliesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // logicFamiliesDataGridView
            // 
            this.logicFamiliesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logicFamiliesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logicFamiliesDataGridView.Location = new System.Drawing.Point(12, 12);
            this.logicFamiliesDataGridView.Name = "logicFamiliesDataGridView";
            this.logicFamiliesDataGridView.Size = new System.Drawing.Size(212, 320);
            this.logicFamiliesDataGridView.TabIndex = 0;
            this.toolTip1.SetToolTip(this.logicFamiliesDataGridView, "Enter IBM Logic Families here (e.g. SDTRL, SDTDL, RELAY, DDTL)");
            this.logicFamiliesDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.logicFamiliesDataGridView_CellValidating);
            this.logicFamiliesDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.logicFamiliesDataGridView_CellValueChanged);
            this.logicFamiliesDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.logicFamiliesDataGridView_UserDeletingRow);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyButton.Location = new System.Drawing.Point(12, 348);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.Location = new System.Drawing.Point(149, 348);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditLogicFamiliesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(236, 383);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.logicFamiliesDataGridView);
            this.Name = "EditLogicFamiliesForm";
            this.Text = "Edit IBM Logic Families";
            ((System.ComponentModel.ISupportInitialize)(this.logicFamiliesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridView logicFamiliesDataGridView;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
    }
}