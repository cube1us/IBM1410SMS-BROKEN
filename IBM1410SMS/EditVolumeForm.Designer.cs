namespace IBM1410SMS
{
    partial class EditVolumeForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.volumeSetComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.volumesDataGridView = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.volumesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Volume Set: ";
            this.toolTip1.SetToolTip(this.label1, "Volume set whose volume(s) you wish to add/change/delete.");
            // 
            // volumeSetComboBox
            // 
            this.volumeSetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.volumeSetComboBox.FormattingEnabled = true;
            this.volumeSetComboBox.Location = new System.Drawing.Point(145, 22);
            this.volumeSetComboBox.Name = "volumeSetComboBox";
            this.volumeSetComboBox.Size = new System.Drawing.Size(328, 21);
            this.volumeSetComboBox.TabIndex = 1;
            this.toolTip1.SetToolTip(this.volumeSetComboBox, "Volume set whose volume(s) you wish to add/change/delete.");
            this.volumeSetComboBox.SelectedIndexChanged += new System.EventHandler(this.volumeSetComboBox_SelectedIndexChanged);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cancelButton.Location = new System.Drawing.Point(159, 320);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.applyButton.Location = new System.Drawing.Point(36, 320);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // volumesDataGridView
            // 
            this.volumesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.volumesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.volumesDataGridView.Location = new System.Drawing.Point(36, 70);
            this.volumesDataGridView.Name = "volumesDataGridView";
            this.volumesDataGridView.Size = new System.Drawing.Size(595, 228);
            this.volumesDataGridView.TabIndex = 6;
            this.toolTip1.SetToolTip(this.volumesDataGridView, "Edit or Add Volumes Here.");
            this.volumesDataGridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.volumesDataGridView_CellValidating);
            this.volumesDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.volumesDataGridView_CellValueChanged);
            this.volumesDataGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.volumesDataGridView_UserDeletingRow);
            // 
            // EditVolumeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 355);
            this.Controls.Add(this.volumesDataGridView);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.volumeSetComboBox);
            this.Controls.Add(this.label1);
            this.Name = "EditVolumeForm";
            this.Text = "Edit Volumes";
            ((System.ComponentModel.ISupportInitialize)(this.volumesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox volumeSetComboBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.DataGridView volumesDataGridView;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}