namespace IBM1410SMS
{
    partial class EditConnectionForm
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
            this.loadPinLabel = new System.Windows.Forms.Label();
            this.loadPinComboBox = new System.Windows.Forms.ComboBox();
            this.edgeSignalLabel = new System.Windows.Forms.Label();
            this.sheetEdgeComboBox = new System.Windows.Forms.ComboBox();
            this.sheetOrgDestLabel = new System.Windows.Forms.Label();
            this.sheetTextBox = new System.Windows.Forms.TextBox();
            this.edgeRefLabel = new System.Windows.Forms.Label();
            this.refComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cardTypeTextBox = new System.Windows.Forms.TextBox();
            this.gatePinIOLabel = new System.Windows.Forms.Label();
            this.pinComboBox = new System.Windows.Forms.ComboBox();
            this.typeGrouBox = new System.Windows.Forms.GroupBox();
            this.edgeRadioButton = new System.Windows.Forms.RadioButton();
            this.dotRadioButton = new System.Windows.Forms.RadioButton();
            this.gateRadioButton = new System.Windows.Forms.RadioButton();
            this.gateBlockComboBox = new System.Windows.Forms.ComboBox();
            this.dotBlockComboBox = new System.Windows.Forms.ComboBox();
            this.gateCoordinateLabel = new System.Windows.Forms.Label();
            this.dotCoordinateLabel = new System.Windows.Forms.Label();
            this.connectingPinComboBox = new System.Windows.Forms.ComboBox();
            this.gatePinLabel = new System.Windows.Forms.Label();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.polarityGroupBox = new System.Windows.Forms.GroupBox();
            this.negativeRadioButton = new System.Windows.Forms.RadioButton();
            this.positiveRadioButton = new System.Windows.Forms.RadioButton();
            this.gateRefComboBox = new System.Windows.Forms.ComboBox();
            this.gateRefLabel = new System.Windows.Forms.Label();
            this.openCollectorLabel = new System.Windows.Forms.Label();
            this.populateButton = new System.Windows.Forms.Button();
            this.ref2ComboBox = new System.Windows.Forms.ComboBox();
            this.edgeRef2Label = new System.Windows.Forms.Label();
            this.typeGrouBox.SuspendLayout();
            this.polarityGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // diagramRowTextBox
            // 
            this.diagramRowTextBox.Location = new System.Drawing.Point(417, 52);
            this.diagramRowTextBox.Name = "diagramRowTextBox";
            this.diagramRowTextBox.ReadOnly = true;
            this.diagramRowTextBox.Size = new System.Drawing.Size(33, 20);
            this.diagramRowTextBox.TabIndex = 53;
            this.diagramRowTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.diagramRowTextBox, "Diagram Row contining this Logic Block");
            // 
            // diagramColumnTextBox
            // 
            this.diagramColumnTextBox.Location = new System.Drawing.Point(265, 52);
            this.diagramColumnTextBox.Name = "diagramColumnTextBox";
            this.diagramColumnTextBox.ReadOnly = true;
            this.diagramColumnTextBox.Size = new System.Drawing.Size(35, 20);
            this.diagramColumnTextBox.TabIndex = 52;
            this.diagramColumnTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.diagramColumnTextBox, "Diagram Column contining this Logic Block");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 51;
            this.label5.Text = "Diagram Column:";
            this.toolTip1.SetToolTip(this.label5, "Diagram Column contining this Logic Block");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(341, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Diagram Row:";
            this.toolTip1.SetToolTip(this.label4, "Diagram Row contining this Logic Block");
            // 
            // pageTextBox
            // 
            this.pageTextBox.Location = new System.Drawing.Point(51, 52);
            this.pageTextBox.Name = "pageTextBox";
            this.pageTextBox.ReadOnly = true;
            this.pageTextBox.Size = new System.Drawing.Size(91, 20);
            this.pageTextBox.TabIndex = 50;
            this.pageTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.pageTextBox, "Page name for this drawing (xx.yy.zz.s)");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 48;
            this.label3.Text = "Page:";
            this.toolTip1.SetToolTip(this.label3, "Page name for this drawing (xx.yy.zz.s)");
            // 
            // machineTextBox
            // 
            this.machineTextBox.Location = new System.Drawing.Point(107, 17);
            this.machineTextBox.Name = "machineTextBox";
            this.machineTextBox.ReadOnly = true;
            this.machineTextBox.Size = new System.Drawing.Size(69, 20);
            this.machineTextBox.TabIndex = 41;
            this.machineTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.machineTextBox, "Machine for Volume that contains this ALD drawing page.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "Diagram Machine:";
            this.toolTip1.SetToolTip(this.label1, "Machine for Volume that contains this ALD drawing page.");
            // 
            // volumeTextBox
            // 
            this.volumeTextBox.Location = new System.Drawing.Point(244, 17);
            this.volumeTextBox.Name = "volumeTextBox";
            this.volumeTextBox.ReadOnly = true;
            this.volumeTextBox.Size = new System.Drawing.Size(302, 20);
            this.volumeTextBox.TabIndex = 45;
            this.volumeTextBox.TabStop = false;
            this.toolTip1.SetToolTip(this.volumeTextBox, "Volume Set and Volume containing this drawing");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(187, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Volume:";
            this.toolTip1.SetToolTip(this.label2, "Volume Set and Volume containing this drawing");
            // 
            // loadPinLabel
            // 
            this.loadPinLabel.AutoSize = true;
            this.loadPinLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadPinLabel.Location = new System.Drawing.Point(320, 120);
            this.loadPinLabel.Name = "loadPinLabel";
            this.loadPinLabel.Size = new System.Drawing.Size(61, 13);
            this.loadPinLabel.TabIndex = 105;
            this.loadPinLabel.Text = "Load Pin:";
            this.toolTip1.SetToolTip(this.loadPinLabel, "If the logic block lists two pins on the output, one of them is this load pin - a" +
        " resistor.");
            // 
            // loadPinComboBox
            // 
            this.loadPinComboBox.DisplayMember = "pin";
            this.loadPinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.loadPinComboBox.FormattingEnabled = true;
            this.loadPinComboBox.Location = new System.Drawing.Point(387, 117);
            this.loadPinComboBox.Name = "loadPinComboBox";
            this.loadPinComboBox.Size = new System.Drawing.Size(49, 21);
            this.loadPinComboBox.TabIndex = 62;
            this.toolTip1.SetToolTip(this.loadPinComboBox, "If the logic block lists two pins on the output, one of them is this load pin - a" +
        " resistor.");
            // 
            // edgeSignalLabel
            // 
            this.edgeSignalLabel.AutoSize = true;
            this.edgeSignalLabel.Location = new System.Drawing.Point(115, 318);
            this.edgeSignalLabel.Name = "edgeSignalLabel";
            this.edgeSignalLabel.Size = new System.Drawing.Size(66, 13);
            this.edgeSignalLabel.TabIndex = 68;
            this.edgeSignalLabel.Text = "Row/Signal:";
            this.toolTip1.SetToolTip(this.edgeSignalLabel, "Select the Row / Signal that is the output from this connection");
            // 
            // sheetEdgeComboBox
            // 
            this.sheetEdgeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sheetEdgeComboBox.FormattingEnabled = true;
            this.sheetEdgeComboBox.Location = new System.Drawing.Point(182, 315);
            this.sheetEdgeComboBox.Name = "sheetEdgeComboBox";
            this.sheetEdgeComboBox.Size = new System.Drawing.Size(364, 21);
            this.sheetEdgeComboBox.TabIndex = 92;
            this.toolTip1.SetToolTip(this.sheetEdgeComboBox, "Select the Row / Signal that is the output from this connection");
            this.sheetEdgeComboBox.SelectedIndexChanged += new System.EventHandler(this.sheetEdgeComboBox_SelectedIndexChanged);
            // 
            // sheetOrgDestLabel
            // 
            this.sheetOrgDestLabel.AutoSize = true;
            this.sheetOrgDestLabel.Location = new System.Drawing.Point(115, 360);
            this.sheetOrgDestLabel.Name = "sheetOrgDestLabel";
            this.sheetOrgDestLabel.Size = new System.Drawing.Size(94, 13);
            this.sheetOrgDestLabel.TabIndex = 72;
            this.sheetOrgDestLabel.Text = "Destination Sheet:";
            this.toolTip1.SetToolTip(this.sheetOrgDestLabel, "Identify the sheet that is the destination for this connection");
            // 
            // sheetTextBox
            // 
            this.sheetTextBox.Location = new System.Drawing.Point(216, 357);
            this.sheetTextBox.Name = "sheetTextBox";
            this.sheetTextBox.Size = new System.Drawing.Size(110, 20);
            this.sheetTextBox.TabIndex = 94;
            this.toolTip1.SetToolTip(this.sheetTextBox, "Identify the sheet that is the destination for this connection");
            // 
            // edgeRefLabel
            // 
            this.edgeRefLabel.AutoSize = true;
            this.edgeRefLabel.Location = new System.Drawing.Point(350, 360);
            this.edgeRefLabel.Name = "edgeRefLabel";
            this.edgeRefLabel.Size = new System.Drawing.Size(32, 13);
            this.edgeRefLabel.TabIndex = 74;
            this.edgeRefLabel.Text = "*REF";
            this.toolTip1.SetToolTip(this.edgeRefLabel, "Identify the * Edge Connection reference on this sheet edge connection, if any.");
            // 
            // refComboBox
            // 
            this.refComboBox.DisplayMember = "reference";
            this.refComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.refComboBox.FormattingEnabled = true;
            this.refComboBox.Location = new System.Drawing.Point(388, 357);
            this.refComboBox.Name = "refComboBox";
            this.refComboBox.Size = new System.Drawing.Size(47, 21);
            this.refComboBox.TabIndex = 96;
            this.toolTip1.SetToolTip(this.refComboBox, "Identify the * Edge Connection reference on this sheet edge connection, if any.");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 55;
            this.label6.Text = "Card type:";
            // 
            // cardTypeTextBox
            // 
            this.cardTypeTextBox.Location = new System.Drawing.Point(77, 87);
            this.cardTypeTextBox.Name = "cardTypeTextBox";
            this.cardTypeTextBox.ReadOnly = true;
            this.cardTypeTextBox.Size = new System.Drawing.Size(65, 20);
            this.cardTypeTextBox.TabIndex = 54;
            this.cardTypeTextBox.TabStop = false;
            // 
            // gatePinIOLabel
            // 
            this.gatePinIOLabel.AutoSize = true;
            this.gatePinIOLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gatePinIOLabel.Location = new System.Drawing.Point(17, 120);
            this.gatePinIOLabel.Name = "gatePinIOLabel";
            this.gatePinIOLabel.Size = new System.Drawing.Size(128, 13);
            this.gatePinIOLabel.TabIndex = 58;
            this.gatePinIOLabel.Text = "Edit Output From Pin:";
            // 
            // pinComboBox
            // 
            this.pinComboBox.DisplayMember = "pin";
            this.pinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pinComboBox.FormattingEnabled = true;
            this.pinComboBox.Location = new System.Drawing.Point(151, 117);
            this.pinComboBox.Name = "pinComboBox";
            this.pinComboBox.Size = new System.Drawing.Size(49, 21);
            this.pinComboBox.TabIndex = 60;
            this.pinComboBox.SelectedIndexChanged += new System.EventHandler(this.pinComboBox_SelectedIndexChanged);
            // 
            // typeGrouBox
            // 
            this.typeGrouBox.Controls.Add(this.edgeRadioButton);
            this.typeGrouBox.Controls.Add(this.dotRadioButton);
            this.typeGrouBox.Controls.Add(this.gateRadioButton);
            this.typeGrouBox.Location = new System.Drawing.Point(9, 167);
            this.typeGrouBox.Name = "typeGrouBox";
            this.typeGrouBox.Size = new System.Drawing.Size(92, 187);
            this.typeGrouBox.TabIndex = 70;
            this.typeGrouBox.TabStop = false;
            this.typeGrouBox.Text = "Type";
            // 
            // edgeRadioButton
            // 
            this.edgeRadioButton.AutoSize = true;
            this.edgeRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edgeRadioButton.Location = new System.Drawing.Point(8, 140);
            this.edgeRadioButton.Name = "edgeRadioButton";
            this.edgeRadioButton.Size = new System.Drawing.Size(83, 30);
            this.edgeRadioButton.TabIndex = 90;
            this.edgeRadioButton.TabStop = true;
            this.edgeRadioButton.Text = "Edge\r\nConnector";
            this.edgeRadioButton.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.edgeRadioButton.UseVisualStyleBackColor = true;
            this.edgeRadioButton.CheckedChanged += new System.EventHandler(this.edgeRadioButton_CheckedChanged);
            // 
            // dotRadioButton
            // 
            this.dotRadioButton.AutoSize = true;
            this.dotRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dotRadioButton.Location = new System.Drawing.Point(8, 80);
            this.dotRadioButton.Name = "dotRadioButton";
            this.dotRadioButton.Size = new System.Drawing.Size(74, 30);
            this.dotRadioButton.TabIndex = 80;
            this.dotRadioButton.TabStop = true;
            this.dotRadioButton.Text = "Dot\r\nFunction";
            this.dotRadioButton.UseVisualStyleBackColor = true;
            this.dotRadioButton.CheckedChanged += new System.EventHandler(this.dotRadioButton_CheckedChanged);
            // 
            // gateRadioButton
            // 
            this.gateRadioButton.AutoSize = true;
            this.gateRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gateRadioButton.Location = new System.Drawing.Point(8, 30);
            this.gateRadioButton.Name = "gateRadioButton";
            this.gateRadioButton.Size = new System.Drawing.Size(52, 17);
            this.gateRadioButton.TabIndex = 72;
            this.gateRadioButton.TabStop = true;
            this.gateRadioButton.Text = "Gate";
            this.gateRadioButton.UseVisualStyleBackColor = true;
            this.gateRadioButton.CheckedChanged += new System.EventHandler(this.gateRadioButton_CheckedChanged);
            // 
            // gateBlockComboBox
            // 
            this.gateBlockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gateBlockComboBox.FormattingEnabled = true;
            this.gateBlockComboBox.Location = new System.Drawing.Point(182, 196);
            this.gateBlockComboBox.Name = "gateBlockComboBox";
            this.gateBlockComboBox.Size = new System.Drawing.Size(63, 21);
            this.gateBlockComboBox.TabIndex = 74;
            this.gateBlockComboBox.SelectedIndexChanged += new System.EventHandler(this.gateBlockComboBox_SelectedIndexChanged);
            // 
            // dotBlockComboBox
            // 
            this.dotBlockComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dotBlockComboBox.FormattingEnabled = true;
            this.dotBlockComboBox.Location = new System.Drawing.Point(181, 256);
            this.dotBlockComboBox.Name = "dotBlockComboBox";
            this.dotBlockComboBox.Size = new System.Drawing.Size(63, 21);
            this.dotBlockComboBox.TabIndex = 82;
            // 
            // gateCoordinateLabel
            // 
            this.gateCoordinateLabel.AutoSize = true;
            this.gateCoordinateLabel.Location = new System.Drawing.Point(115, 202);
            this.gateCoordinateLabel.Name = "gateCoordinateLabel";
            this.gateCoordinateLabel.Size = new System.Drawing.Size(61, 13);
            this.gateCoordinateLabel.TabIndex = 63;
            this.gateCoordinateLabel.Text = "Coordinate:";
            // 
            // dotCoordinateLabel
            // 
            this.dotCoordinateLabel.AutoSize = true;
            this.dotCoordinateLabel.Location = new System.Drawing.Point(115, 259);
            this.dotCoordinateLabel.Name = "dotCoordinateLabel";
            this.dotCoordinateLabel.Size = new System.Drawing.Size(61, 13);
            this.dotCoordinateLabel.TabIndex = 64;
            this.dotCoordinateLabel.Text = "Coordinate:";
            // 
            // connectingPinComboBox
            // 
            this.connectingPinComboBox.DisplayMember = "pin";
            this.connectingPinComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.connectingPinComboBox.FormattingEnabled = true;
            this.connectingPinComboBox.Location = new System.Drawing.Point(293, 199);
            this.connectingPinComboBox.Name = "connectingPinComboBox";
            this.connectingPinComboBox.Size = new System.Drawing.Size(66, 21);
            this.connectingPinComboBox.TabIndex = 75;
            // 
            // gatePinLabel
            // 
            this.gatePinLabel.AutoSize = true;
            this.gatePinLabel.Location = new System.Drawing.Point(262, 202);
            this.gatePinLabel.Name = "gatePinLabel";
            this.gatePinLabel.Size = new System.Drawing.Size(25, 13);
            this.gatePinLabel.TabIndex = 66;
            this.gatePinLabel.Text = "Pin:";
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connectionLabel.Location = new System.Drawing.Point(196, 154);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(192, 13);
            this.connectionLabel.TabIndex = 67;
            this.connectionLabel.Text = "VVV From the Output Below VVV";
            // 
            // applyButton
            // 
            this.applyButton.Location = new System.Drawing.Point(25, 407);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 100;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.applyButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(325, 407);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 101;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(475, 407);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 102;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // polarityGroupBox
            // 
            this.polarityGroupBox.Controls.Add(this.negativeRadioButton);
            this.polarityGroupBox.Controls.Add(this.positiveRadioButton);
            this.polarityGroupBox.Location = new System.Drawing.Point(464, 104);
            this.polarityGroupBox.Name = "polarityGroupBox";
            this.polarityGroupBox.Size = new System.Drawing.Size(103, 43);
            this.polarityGroupBox.TabIndex = 999;
            this.polarityGroupBox.TabStop = false;
            this.polarityGroupBox.Text = "Polarity";
            // 
            // negativeRadioButton
            // 
            this.negativeRadioButton.AutoSize = true;
            this.negativeRadioButton.Location = new System.Drawing.Point(58, 14);
            this.negativeRadioButton.Name = "negativeRadioButton";
            this.negativeRadioButton.Size = new System.Drawing.Size(28, 17);
            this.negativeRadioButton.TabIndex = 202;
            this.negativeRadioButton.Text = "-";
            this.negativeRadioButton.UseVisualStyleBackColor = true;
            // 
            // positiveRadioButton
            // 
            this.positiveRadioButton.AutoSize = true;
            this.positiveRadioButton.Location = new System.Drawing.Point(21, 14);
            this.positiveRadioButton.Name = "positiveRadioButton";
            this.positiveRadioButton.Size = new System.Drawing.Size(31, 17);
            this.positiveRadioButton.TabIndex = 201;
            this.positiveRadioButton.Text = "+";
            this.positiveRadioButton.UseVisualStyleBackColor = true;
            // 
            // gateRefComboBox
            // 
            this.gateRefComboBox.DisplayMember = "reference";
            this.gateRefComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gateRefComboBox.FormattingEnabled = true;
            this.gateRefComboBox.Location = new System.Drawing.Point(417, 199);
            this.gateRefComboBox.Name = "gateRefComboBox";
            this.gateRefComboBox.Size = new System.Drawing.Size(47, 21);
            this.gateRefComboBox.TabIndex = 79;
            // 
            // gateRefLabel
            // 
            this.gateRefLabel.AutoSize = true;
            this.gateRefLabel.Location = new System.Drawing.Point(379, 202);
            this.gateRefLabel.Name = "gateRefLabel";
            this.gateRefLabel.Size = new System.Drawing.Size(32, 13);
            this.gateRefLabel.TabIndex = 104;
            this.gateRefLabel.Text = "*REF";
            // 
            // openCollectorLabel
            // 
            this.openCollectorLabel.AutoSize = true;
            this.openCollectorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openCollectorLabel.Location = new System.Drawing.Point(213, 120);
            this.openCollectorLabel.Name = "openCollectorLabel";
            this.openCollectorLabel.Size = new System.Drawing.Size(99, 13);
            this.openCollectorLabel.TabIndex = 107;
            this.openCollectorLabel.Text = "(Open Collector)";
            // 
            // populateButton
            // 
            this.populateButton.Enabled = false;
            this.populateButton.Location = new System.Drawing.Point(175, 407);
            this.populateButton.Name = "populateButton";
            this.populateButton.Size = new System.Drawing.Size(75, 23);
            this.populateButton.TabIndex = 1000;
            this.populateButton.Text = "Populate";
            this.populateButton.UseVisualStyleBackColor = true;
            this.populateButton.Click += new System.EventHandler(this.populateButton_Click);
            // 
            // ref2ComboBox
            // 
            this.ref2ComboBox.DisplayMember = "reference";
            this.ref2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ref2ComboBox.FormattingEnabled = true;
            this.ref2ComboBox.Location = new System.Drawing.Point(499, 356);
            this.ref2ComboBox.Name = "ref2ComboBox";
            this.ref2ComboBox.Size = new System.Drawing.Size(47, 21);
            this.ref2ComboBox.TabIndex = 97;
            this.toolTip1.SetToolTip(this.ref2ComboBox, "Identify the * Edge Connection reference on this sheet edge connection, if any.");
            // 
            // edgeRef2Label
            // 
            this.edgeRef2Label.AutoSize = true;
            this.edgeRef2Label.Location = new System.Drawing.Point(461, 359);
            this.edgeRef2Label.Name = "edgeRef2Label";
            this.edgeRef2Label.Size = new System.Drawing.Size(32, 13);
            this.edgeRef2Label.TabIndex = 1001;
            this.edgeRef2Label.Text = "*REF";
            this.toolTip1.SetToolTip(this.edgeRef2Label, "Identify the * Edge Connection reference on this sheet edge connection, if any.");
            // 
            // EditConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 442);
            this.Controls.Add(this.ref2ComboBox);
            this.Controls.Add(this.edgeRef2Label);
            this.Controls.Add(this.populateButton);
            this.Controls.Add(this.openCollectorLabel);
            this.Controls.Add(this.loadPinComboBox);
            this.Controls.Add(this.loadPinLabel);
            this.Controls.Add(this.gateRefComboBox);
            this.Controls.Add(this.gateRefLabel);
            this.Controls.Add(this.polarityGroupBox);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.refComboBox);
            this.Controls.Add(this.edgeRefLabel);
            this.Controls.Add(this.sheetTextBox);
            this.Controls.Add(this.sheetOrgDestLabel);
            this.Controls.Add(this.sheetEdgeComboBox);
            this.Controls.Add(this.edgeSignalLabel);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.gatePinLabel);
            this.Controls.Add(this.connectingPinComboBox);
            this.Controls.Add(this.dotCoordinateLabel);
            this.Controls.Add(this.gateCoordinateLabel);
            this.Controls.Add(this.dotBlockComboBox);
            this.Controls.Add(this.gateBlockComboBox);
            this.Controls.Add(this.typeGrouBox);
            this.Controls.Add(this.pinComboBox);
            this.Controls.Add(this.gatePinIOLabel);
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
            this.KeyPreview = true;
            this.Name = "EditConnectionForm";
            this.Text = "Edit Logic Block (Gate) Connection";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EditConnectionForm_KeyPress);
            this.typeGrouBox.ResumeLayout(false);
            this.typeGrouBox.PerformLayout();
            this.polarityGroupBox.ResumeLayout(false);
            this.polarityGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox cardTypeTextBox;
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
        private System.Windows.Forms.Label gatePinIOLabel;
        private System.Windows.Forms.ComboBox pinComboBox;
        private System.Windows.Forms.GroupBox typeGrouBox;
        private System.Windows.Forms.RadioButton edgeRadioButton;
        private System.Windows.Forms.RadioButton dotRadioButton;
        private System.Windows.Forms.RadioButton gateRadioButton;
        private System.Windows.Forms.ComboBox gateBlockComboBox;
        private System.Windows.Forms.ComboBox dotBlockComboBox;
        private System.Windows.Forms.Label gateCoordinateLabel;
        private System.Windows.Forms.Label dotCoordinateLabel;
        private System.Windows.Forms.ComboBox connectingPinComboBox;
        private System.Windows.Forms.Label gatePinLabel;
        private System.Windows.Forms.Label connectionLabel;
        private System.Windows.Forms.Label edgeSignalLabel;
        private System.Windows.Forms.ComboBox sheetEdgeComboBox;
        private System.Windows.Forms.Label sheetOrgDestLabel;
        private System.Windows.Forms.TextBox sheetTextBox;
        private System.Windows.Forms.Label edgeRefLabel;
        private System.Windows.Forms.ComboBox refComboBox;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox polarityGroupBox;
        private System.Windows.Forms.RadioButton negativeRadioButton;
        private System.Windows.Forms.RadioButton positiveRadioButton;
        private System.Windows.Forms.ComboBox gateRefComboBox;
        private System.Windows.Forms.Label gateRefLabel;
        private System.Windows.Forms.Label loadPinLabel;
        private System.Windows.Forms.ComboBox loadPinComboBox;
        private System.Windows.Forms.Label openCollectorLabel;
        private System.Windows.Forms.Button populateButton;
        private System.Windows.Forms.ComboBox ref2ComboBox;
        private System.Windows.Forms.Label edgeRef2Label;
    }
}