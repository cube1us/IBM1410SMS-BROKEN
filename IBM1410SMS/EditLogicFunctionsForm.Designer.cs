namespace IBM1410SMS
{
    partial class EditLogicFunctionsForm
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
            this.logicFunctionsDataGridView = new System.Windows.Forms.DataGridView();
            this.applyButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.logicFunctionsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // logicFunctionsDataGridView
            // 
            this.logicFunctionsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logicFunctionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logicFunctionsDataGridView.Location = new System.Drawing.Point(12, 12);
            this.logicFunctionsDataGridView.Name = "logicFunctionsDataGridView";
            this.logicFunctionsDataGridView.Size = new System.Drawing.Size(262, 306);
            this.logicFunctionsDataGridView.TabIndex = 0;
            this.toolTip1.SetToolTip(this.logicFunctionsDataGridView, "Edit Standard Logic Functions Here (e.g. NAND, NOR, NOT, EQUAL, DELAY, Resistor, " +
        "Capacitor)");
            this.logicFunctionsDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.logicFunctionsDataGridView_CellValidating);
            this.logicFunctionsDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.logicFunctionsDataGridView_CellValueChanged);
            this.logicFunctionsDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.logicFunctionsDataGridView_UserDeletingRow);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyButton.Location = new System.Drawing.Point(12, 352);
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
            this.cancelButton.Location = new System.Drawing.Point(153, 352);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // EditLogicFunctionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 387);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.logicFunctionsDataGridView);
            this.Name = "EditLogicFunctionsForm";
            this.Text = "Edit Standard Logic Functions";
            ((System.ComponentModel.ISupportInitialize)(this.logicFunctionsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView logicFunctionsDataGridView;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}