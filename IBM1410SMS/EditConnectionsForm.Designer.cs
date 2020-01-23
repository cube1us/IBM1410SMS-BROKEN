namespace IBM1410SMS
{
    partial class EditConnectionsForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.label7 = new System.Windows.Forms.Label();
            this.definingPinComboBox = new System.Windows.Forms.ComboBox();
            this.cardTypeTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addInputButton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.inputsDataGridView = new System.Windows.Forms.DataGridView();
            this.outputsDataGridView = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.addOutputButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.inputsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // diagramRowTextBox
            // 
            this.diagramRowTextBox.Location = new System.Drawing.Point(412, 47);
            this.diagramRowTextBox.Name = "diagramRowTextBox";
            this.diagramRowTextBox.ReadOnly = true;
            this.diagramRowTextBox.Size = new System.Drawing.Size(33, 20);
            this.diagramRowTextBox.TabIndex = 40;
            this.diagramRowTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.diagramRowTextBox, "Diagram Row contining this Logic Block");
            // 
            // diagramColumnTextBox
            // 
            this.diagramColumnTextBox.Location = new System.Drawing.Point(260, 47);
            this.diagramColumnTextBox.Name = "diagramColumnTextBox";
            this.diagramColumnTextBox.ReadOnly = true;
            this.diagramColumnTextBox.Size = new System.Drawing.Size(35, 20);
            this.diagramColumnTextBox.TabIndex = 39;
            this.diagramColumnTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.diagramColumnTextBox, "Diagram Column contining this Logic Block");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 50);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 38;
            this.label5.Text = "Diagram Column:";
            this.toolTip1.SetToolTip(this.label5, "Diagram Column contining this Logic Block");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(336, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 37;
            this.label4.Text = "Diagram Row:";
            this.toolTip1.SetToolTip(this.label4, "Diagram Row contining this Logic Block");
            // 
            // pageTextBox
            // 
            this.pageTextBox.Location = new System.Drawing.Point(46, 47);
            this.pageTextBox.Name = "pageTextBox";
            this.pageTextBox.ReadOnly = true;
            this.pageTextBox.Size = new System.Drawing.Size(91, 20);
            this.pageTextBox.TabIndex = 36;
            this.pageTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.pageTextBox, "Page name for this drawing (xx.yy.zz.s)");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "Page:";
            this.toolTip1.SetToolTip(this.label3, "Page name for this drawing (xx.yy.zz.s)");
            // 
            // machineTextBox
            // 
            this.machineTextBox.Location = new System.Drawing.Point(102, 12);
            this.machineTextBox.Name = "machineTextBox";
            this.machineTextBox.ReadOnly = true;
            this.machineTextBox.Size = new System.Drawing.Size(69, 20);
            this.machineTextBox.TabIndex = 34;
            this.machineTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.machineTextBox, "Machine for Volume that contains this ALD drawing page.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Diagram Machine:";
            this.toolTip1.SetToolTip(this.label1, "Machine for Volume that contains this ALD drawing page.");
            // 
            // volumeTextBox
            // 
            this.volumeTextBox.Location = new System.Drawing.Point(239, 12);
            this.volumeTextBox.Name = "volumeTextBox";
            this.volumeTextBox.ReadOnly = true;
            this.volumeTextBox.Size = new System.Drawing.Size(302, 20);
            this.volumeTextBox.TabIndex = 32;
            this.volumeTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.volumeTextBox, "Volume Set and Volume containing this drawing");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 31;
            this.label2.Text = "Volume:";
            this.toolTip1.SetToolTip(this.label2, "Volume Set and Volume containing this drawing");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(171, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "Defining Pin:";
            this.toolTip1.SetToolTip(this.label7, "The pin that defines the gate used by this logic block");
            this.label7.Visible = false;
            // 
            // definingPinComboBox
            // 
            this.definingPinComboBox.DisplayMember = "pin";
            this.definingPinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.definingPinComboBox.FormattingEnabled = true;
            this.definingPinComboBox.Location = new System.Drawing.Point(244, 82);
            this.definingPinComboBox.Name = "definingPinComboBox";
            this.definingPinComboBox.Size = new System.Drawing.Size(51, 21);
            this.definingPinComboBox.TabIndex = 44;
            this.toolTip1.SetToolTip(this.definingPinComboBox, "The pin that defines the gate used by this logic block");
            this.definingPinComboBox.Visible = false;
            this.definingPinComboBox.SelectedIndexChanged += new System.EventHandler(this.definingPinComboBox_SelectedIndexChanged);
            // 
            // cardTypeTextBox
            // 
            this.cardTypeTextBox.Location = new System.Drawing.Point(72, 82);
            this.cardTypeTextBox.Name = "cardTypeTextBox";
            this.cardTypeTextBox.ReadOnly = true;
            this.cardTypeTextBox.Size = new System.Drawing.Size(65, 20);
            this.cardTypeTextBox.TabIndex = 41;
            this.cardTypeTextBox.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 42;
            this.label6.Text = "Card type:";
            // 
            // addInputButton
            // 
            this.addInputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addInputButton.Location = new System.Drawing.Point(339, 114);
            this.addInputButton.Name = "addInputButton";
            this.addInputButton.Size = new System.Drawing.Size(88, 23);
            this.addInputButton.TabIndex = 45;
            this.addInputButton.Text = "Add Input";
            this.addInputButton.UseVisualStyleBackColor = true;
            this.addInputButton.Click += new System.EventHandler(this.addInputButton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(65, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(193, 13);
            this.label8.TabIndex = 46;
            this.label8.Text = "Inputs to this Logic Block / Gate";
            // 
            // inputsDataGridView
            // 
            this.inputsDataGridView.AllowUserToAddRows = false;
            this.inputsDataGridView.AllowUserToDeleteRows = false;
            this.inputsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.inputsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.inputsDataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.inputsDataGridView.Location = new System.Drawing.Point(12, 143);
            this.inputsDataGridView.Name = "inputsDataGridView";
            this.inputsDataGridView.ReadOnly = true;
            this.inputsDataGridView.Size = new System.Drawing.Size(529, 120);
            this.inputsDataGridView.TabIndex = 47;
            this.inputsDataGridView.TabStop = false;
            // 
            // outputsDataGridView
            // 
            this.outputsDataGridView.AllowUserToAddRows = false;
            this.outputsDataGridView.AllowUserToDeleteRows = false;
            this.outputsDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputsDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.outputsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputsDataGridView.Location = new System.Drawing.Point(12, 306);
            this.outputsDataGridView.Name = "outputsDataGridView";
            this.outputsDataGridView.ReadOnly = true;
            this.outputsDataGridView.Size = new System.Drawing.Size(529, 120);
            this.outputsDataGridView.TabIndex = 48;
            this.outputsDataGridView.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(65, 279);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(215, 13);
            this.label9.TabIndex = 49;
            this.label9.Text = "Outputs from this Logic Block / Gate";
            // 
            // addOutputButton
            // 
            this.addOutputButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addOutputButton.Location = new System.Drawing.Point(339, 274);
            this.addOutputButton.Name = "addOutputButton";
            this.addOutputButton.Size = new System.Drawing.Size(88, 23);
            this.addOutputButton.TabIndex = 50;
            this.addOutputButton.Text = "Add Output";
            this.addOutputButton.UseVisualStyleBackColor = true;
            this.addOutputButton.Click += new System.EventHandler(this.addOutputButton_Click);
            // 
            // EditConnectionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 448);
            this.Controls.Add(this.addOutputButton);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.outputsDataGridView);
            this.Controls.Add(this.inputsDataGridView);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.addInputButton);
            this.Controls.Add(this.definingPinComboBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cardTypeTextBox);
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
            this.Name = "EditConnectionsForm";
            this.Text = "Edit Connections";
            ((System.ComponentModel.ISupportInitialize)(this.inputsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.outputsDataGridView)).EndInit();
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
        private System.Windows.Forms.TextBox cardTypeTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox definingPinComboBox;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView inputsDataGridView;
        private System.Windows.Forms.DataGridView outputsDataGridView;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button addOutputButton;
    }
}