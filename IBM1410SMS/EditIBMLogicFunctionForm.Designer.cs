namespace IBM1410SMS
{
    partial class EditIBMLogicFunctionForm
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
            this.IBMLogicFunctionDataGridView = new System.Windows.Forms.DataGridView();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.IBMLogicFunctionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // IBMLogicFunctionDataGridView
            // 
            this.IBMLogicFunctionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IBMLogicFunctionDataGridView.Location = new System.Drawing.Point(12, 12);
            this.IBMLogicFunctionDataGridView.Name = "IBMLogicFunctionDataGridView";
            this.IBMLogicFunctionDataGridView.Size = new System.Drawing.Size(174, 279);
            this.IBMLogicFunctionDataGridView.TabIndex = 0;
            this.toolTip1.SetToolTip(this.IBMLogicFunctionDataGridView, "Enter IBM Logic Function here.  (e.g. T, A, O, I, IP, L, CAP)");
            this.IBMLogicFunctionDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.IBMLogicFunctionDataGridView_CellValidating);
            this.IBMLogicFunctionDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.IBMLogicFunctionDataGridView_CellValueChanged);
            this.IBMLogicFunctionDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.IBMLogicFunctionDataGridView_UserDeletingRow);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(12, 326);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 1;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(111, 326);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditIBMLogicFunctionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(204, 373);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.IBMLogicFunctionDataGridView);
            this.Name = "EditIBMLogicFunctionForm";
            this.Text = " IBM Logic Function Form";
            ((System.ComponentModel.ISupportInitialize)(this.IBMLogicFunctionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView IBMLogicFunctionDataGridView;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}