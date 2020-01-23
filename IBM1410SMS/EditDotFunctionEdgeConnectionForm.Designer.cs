namespace IBM1410SMS
{
    partial class EditDotFunctionEdgeConnectionForm
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
            this.diagramRowTextBox = new System.Windows.Forms.TextBox();
            this.diagramColumnTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pageTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.machineTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.volumeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.refComboBox = new System.Windows.Forms.ComboBox();
            this.edgeRefLabel = new System.Windows.Forms.Label();
            this.sheetTextBox = new System.Windows.Forms.TextBox();
            this.sheetOrgDestLabel = new System.Windows.Forms.Label();
            this.sheetEdgeComboBox = new System.Windows.Forms.ComboBox();
            this.edgeSignalLabel = new System.Windows.Forms.Label();
            this.cancelButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.applyButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.outputRadioButton = new System.Windows.Forms.RadioButton();
            this.inputRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // diagramRowTextBox
            // 
            this.diagramRowTextBox.Location = new System.Drawing.Point(274, 98);
            this.diagramRowTextBox.Name = "diagramRowTextBox";
            this.diagramRowTextBox.ReadOnly = true;
            this.diagramRowTextBox.Size = new System.Drawing.Size(69, 20);
            this.diagramRowTextBox.TabIndex = 30;
            this.diagramRowTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.diagramRowTextBox, "TOP row of Sheet Edge affected by this DOT function.");
            // 
            // diagramColumnTextBox
            // 
            this.diagramColumnTextBox.Location = new System.Drawing.Point(102, 98);
            this.diagramColumnTextBox.Name = "diagramColumnTextBox";
            this.diagramColumnTextBox.ReadOnly = true;
            this.diagramColumnTextBox.Size = new System.Drawing.Size(69, 20);
            this.diagramColumnTextBox.TabIndex = 29;
            this.diagramColumnTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.diagramColumnTextBox, "Diagram Column to the LEFT of this DOT function");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Diagram Column:";
            this.toolTip1.SetToolTip(this.label5, "Diagram Column to the LEFT of this DOT function");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(194, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Diagram Row:";
            this.toolTip1.SetToolTip(this.label4, "TOP row of logic gates affected by this DOT function.");
            // 
            // pageTextBox
            // 
            this.pageTextBox.Location = new System.Drawing.Point(250, 12);
            this.pageTextBox.Name = "pageTextBox";
            this.pageTextBox.ReadOnly = true;
            this.pageTextBox.Size = new System.Drawing.Size(118, 20);
            this.pageTextBox.TabIndex = 26;
            this.pageTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.pageTextBox, "Page name for this drawing (xx.yy.zz.s)");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(209, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Page:";
            this.toolTip1.SetToolTip(this.label3, "Page name for this drawing (xx.yy.zz.s)");
            // 
            // machineTextBox
            // 
            this.machineTextBox.Location = new System.Drawing.Point(66, 12);
            this.machineTextBox.Name = "machineTextBox";
            this.machineTextBox.ReadOnly = true;
            this.machineTextBox.Size = new System.Drawing.Size(69, 20);
            this.machineTextBox.TabIndex = 24;
            this.machineTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.machineTextBox, "Machine for Volume that contains this ALD drawing page.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Machine:";
            this.toolTip1.SetToolTip(this.label1, "Machine for Volume that contains this ALD drawing page.");
            // 
            // volumeTextBox
            // 
            this.volumeTextBox.Location = new System.Drawing.Point(66, 55);
            this.volumeTextBox.Name = "volumeTextBox";
            this.volumeTextBox.ReadOnly = true;
            this.volumeTextBox.Size = new System.Drawing.Size(302, 20);
            this.volumeTextBox.TabIndex = 22;
            this.volumeTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.volumeTextBox, "Volume Set and Volume containing this drawing");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Volume:";
            this.toolTip1.SetToolTip(this.label2, "Volume Set and Volume containing this drawing");
            // 
            // refComboBox
            // 
            this.refComboBox.DisplayMember = "reference";
            this.refComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refComboBox.FormattingEnabled = true;
            this.refComboBox.Location = new System.Drawing.Point(305, 182);
            this.refComboBox.Name = "refComboBox";
            this.refComboBox.Size = new System.Drawing.Size(47, 21);
            this.refComboBox.TabIndex = 102;
            this.toolTip1.SetToolTip(this.refComboBox, "Identify the * Edge Connection reference on this sheet edge connection, if any.");
            // 
            // edgeRefLabel
            // 
            this.edgeRefLabel.AutoSize = true;
            this.edgeRefLabel.Location = new System.Drawing.Point(267, 185);
            this.edgeRefLabel.Name = "edgeRefLabel";
            this.edgeRefLabel.Size = new System.Drawing.Size(32, 13);
            this.edgeRefLabel.TabIndex = 99;
            this.edgeRefLabel.Text = "*REF";
            this.toolTip1.SetToolTip(this.edgeRefLabel, "Identify the * Edge Connection reference on this sheet edge connection, if any.");
            // 
            // sheetTextBox
            // 
            this.sheetTextBox.Location = new System.Drawing.Point(109, 182);
            this.sheetTextBox.Name = "sheetTextBox";
            this.sheetTextBox.Size = new System.Drawing.Size(110, 20);
            this.sheetTextBox.TabIndex = 101;
            this.toolTip1.SetToolTip(this.sheetTextBox, "Identify the sheet that is the destination for this DOT function");
            // 
            // sheetOrgDestLabel
            // 
            this.sheetOrgDestLabel.AutoSize = true;
            this.sheetOrgDestLabel.Location = new System.Drawing.Point(8, 185);
            this.sheetOrgDestLabel.Name = "sheetOrgDestLabel";
            this.sheetOrgDestLabel.Size = new System.Drawing.Size(94, 13);
            this.sheetOrgDestLabel.TabIndex = 98;
            this.sheetOrgDestLabel.Text = "Destination Sheet:";
            this.toolTip1.SetToolTip(this.sheetOrgDestLabel, "Identify the sheet that is the destination for this DOT function");
            // 
            // sheetEdgeComboBox
            // 
            this.sheetEdgeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sheetEdgeComboBox.FormattingEnabled = true;
            this.sheetEdgeComboBox.Location = new System.Drawing.Point(75, 140);
            this.sheetEdgeComboBox.Name = "sheetEdgeComboBox";
            this.sheetEdgeComboBox.Size = new System.Drawing.Size(364, 21);
            this.sheetEdgeComboBox.TabIndex = 100;
            this.toolTip1.SetToolTip(this.sheetEdgeComboBox, "Select the Row / Signal that is the output from this DOT function");
            this.sheetEdgeComboBox.SelectedIndexChanged += new System.EventHandler(this.sheetEdgeComboBox_SelectedIndexChanged);
            // 
            // edgeSignalLabel
            // 
            this.edgeSignalLabel.AutoSize = true;
            this.edgeSignalLabel.Location = new System.Drawing.Point(8, 143);
            this.edgeSignalLabel.Name = "edgeSignalLabel";
            this.edgeSignalLabel.Size = new System.Drawing.Size(66, 13);
            this.edgeSignalLabel.TabIndex = 97;
            this.edgeSignalLabel.Text = "Row/Signal:";
            this.toolTip1.SetToolTip(this.edgeSignalLabel, "Select the Row / Signal that is the output from this DOT function");
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(347, 224);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 105;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(179, 224);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 104;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(11, 224);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 103;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outputRadioButton);
            this.groupBox1.Controls.Add(this.inputRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(368, 85);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(92, 37);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // outputRadioButton
            // 
            this.outputRadioButton.AutoSize = true;
            this.outputRadioButton.Location = new System.Drawing.Point(46, 14);
            this.outputRadioButton.Name = "outputRadioButton";
            this.outputRadioButton.Size = new System.Drawing.Size(42, 17);
            this.outputRadioButton.TabIndex = 1;
            this.outputRadioButton.TabStop = true;
            this.outputRadioButton.Text = "Out";
            this.outputRadioButton.UseVisualStyleBackColor = true;
            this.outputRadioButton.CheckedChanged += new System.EventHandler(this.outputRadioButton_CheckedChanged);
            // 
            // inputRadioButton
            // 
            this.inputRadioButton.AutoSize = true;
            this.inputRadioButton.Location = new System.Drawing.Point(6, 14);
            this.inputRadioButton.Name = "inputRadioButton";
            this.inputRadioButton.Size = new System.Drawing.Size(34, 17);
            this.inputRadioButton.TabIndex = 0;
            this.inputRadioButton.TabStop = true;
            this.inputRadioButton.Text = "In";
            this.inputRadioButton.UseVisualStyleBackColor = true;
            this.inputRadioButton.CheckedChanged += new System.EventHandler(this.inputRadioButton_CheckedChanged);
            // 
            // EditDotFunctionEdgeConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 279);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.refComboBox);
            this.Controls.Add(this.edgeRefLabel);
            this.Controls.Add(this.sheetTextBox);
            this.Controls.Add(this.sheetOrgDestLabel);
            this.Controls.Add(this.sheetEdgeComboBox);
            this.Controls.Add(this.edgeSignalLabel);
            this.Controls.Add(this.diagramRowTextBox);
            this.Controls.Add(this.diagramColumnTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pageTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.machineTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.volumeTextBox);
            this.Controls.Add(this.label2);
            this.Name = "EditDotFunctionEdgeConnectionForm";
            this.Text = "Edit DOT Function Edge Connection";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox diagramRowTextBox;
        private System.Windows.Forms.TextBox diagramColumnTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox pageTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox machineTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox volumeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox refComboBox;
        private System.Windows.Forms.Label edgeRefLabel;
        private System.Windows.Forms.TextBox sheetTextBox;
        private System.Windows.Forms.Label sheetOrgDestLabel;
        private System.Windows.Forms.ComboBox sheetEdgeComboBox;
        private System.Windows.Forms.Label edgeSignalLabel;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton outputRadioButton;
        private System.Windows.Forms.RadioButton inputRadioButton;
    }
}