namespace IBM1410SMS
{
    partial class ImportStartupForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.skipRadioButton = new System.Windows.Forms.RadioButton();
            this.mergeRadioButton = new System.Windows.Forms.RadioButton();
            this.overwriteRadioButton = new System.Windows.Forms.RadioButton();
            this.importButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cancelButton = new System.Windows.Forms.Button();
            this.testModeCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.skipRadioButton);
            this.groupBox1.Controls.Add(this.mergeRadioButton);
            this.groupBox1.Controls.Add(this.overwriteRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(34, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 116);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Overwrite/Merge/Skip";
            // 
            // skipRadioButton
            // 
            this.skipRadioButton.AutoSize = true;
            this.skipRadioButton.Checked = true;
            this.skipRadioButton.Location = new System.Drawing.Point(34, 77);
            this.skipRadioButton.Name = "skipRadioButton";
            this.skipRadioButton.Size = new System.Drawing.Size(46, 17);
            this.skipRadioButton.TabIndex = 2;
            this.skipRadioButton.TabStop = true;
            this.skipRadioButton.Text = "Skip";
            this.toolTip1.SetToolTip(this.skipRadioButton, "Select Skip to skip any records that match existing records.");
            this.skipRadioButton.UseVisualStyleBackColor = true;
            // 
            // mergeRadioButton
            // 
            this.mergeRadioButton.AutoSize = true;
            this.mergeRadioButton.Location = new System.Drawing.Point(34, 54);
            this.mergeRadioButton.Name = "mergeRadioButton";
            this.mergeRadioButton.Size = new System.Drawing.Size(55, 17);
            this.mergeRadioButton.TabIndex = 1;
            this.mergeRadioButton.Text = "Merge";
            this.toolTip1.SetToolTip(this.mergeRadioButton, "Select Merge to have imported records merge with any existing records that match " +
        "(old data takes precedence)");
            this.mergeRadioButton.UseVisualStyleBackColor = true;
            // 
            // overwriteRadioButton
            // 
            this.overwriteRadioButton.AutoSize = true;
            this.overwriteRadioButton.Location = new System.Drawing.Point(34, 31);
            this.overwriteRadioButton.Name = "overwriteRadioButton";
            this.overwriteRadioButton.Size = new System.Drawing.Size(70, 17);
            this.overwriteRadioButton.TabIndex = 0;
            this.overwriteRadioButton.Text = "Overwrite";
            this.toolTip1.SetToolTip(this.overwriteRadioButton, "Select Overwrite to have imported records overwrite any existing records that mat" +
        "ch");
            this.overwriteRadioButton.UseVisualStyleBackColor = true;
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(34, 215);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Import";
            this.toolTip1.SetToolTip(this.importButton, "Begin the import process");
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.csv";
            this.openFileDialog1.Filter = "CSV Files|*.csv";
            this.openFileDialog1.InitialDirectory = "D:\\Users\\Jay\\Schematics\\IBM1410";
            this.openFileDialog1.Title = "Specify File To Import";
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(223, 215);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // testModeCheckBox
            // 
            this.testModeCheckBox.AutoSize = true;
            this.testModeCheckBox.Location = new System.Drawing.Point(68, 168);
            this.testModeCheckBox.Name = "testModeCheckBox";
            this.testModeCheckBox.Size = new System.Drawing.Size(146, 17);
            this.testModeCheckBox.TabIndex = 4;
            this.testModeCheckBox.Text = "TEST MODE (no commit)";
            this.toolTip1.SetToolTip(this.testModeCheckBox, "Select Test Mode for testing/editing");
            this.testModeCheckBox.UseVisualStyleBackColor = true;
            // 
            // ImportStartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 262);
            this.Controls.Add(this.testModeCheckBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "ImportStartupForm";
            this.Text = "Specify Import Parameters";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton skipRadioButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RadioButton mergeRadioButton;
        private System.Windows.Forms.RadioButton overwriteRadioButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.CheckBox testModeCheckBox;
    }
}