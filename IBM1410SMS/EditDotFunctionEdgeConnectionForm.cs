/* 
 *  COPYRIGHT 2018, 2019, 2020 Jay R. Jaeger
 *  
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  (file COPYING.txt) along with this program.  
 *  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySQLFramework;

namespace IBM1410SMS
{

    public partial class EditDotFunctionEdgeConnectionForm : Form
    {
        DBSetup db = DBSetup.Instance;

        Table<Connection> connectionTable;
        Table<Dotfunction> dotFunctionTable;
        Table<Sheetedgeinformation> sheetEdgeInformationTable;
        Table<Page> pageTable;
        Table<Diagrampage> diagramPageTable;
        Table<Edgeconnector> edgeConnectorTable;

        Connection currentConnection;
        Diagrampage currentDiagramPage;
        Page currentPage;
        Machine currentMachine;
        Volumeset currentVolumeSet;
        Volume currentVolume;
        Dotfunction currentDotFunction;

        List<Sheetedgeinformation> sheetEdgeInformationList;
        List<Edgeconnector> edgeConnectorList;

        bool populatingDialog = true;

        public EditDotFunctionEdgeConnectionForm(
            Dotfunction dotFunction,
            Machine machine,
            Volumeset volumeSet,
            Volume volume,
            Diagrampage diagramPage,
            int connectionKey) {

            InitializeComponent();

            Console.WriteLine("Passed connection key is " + connectionKey);

            currentMachine = machine;
            currentVolumeSet = volumeSet;
            currentVolume = volume;
            currentDiagramPage = diagramPage;
            currentDotFunction = dotFunction;

            connectionTable = db.getConnectionTable();
            dotFunctionTable = db.getDotFunctionTable();
            sheetEdgeInformationTable = db.getSheetEdgeInformationTable();
            pageTable = db.getPageTable();
            diagramPageTable = db.getDiagramPageTable();
            edgeConnectorTable = db.getEdgeConnectorTable();

            currentPage = pageTable.getByKey(diagramPage.page);

            //  Fill in the constant data

            machineTextBox.Text = machine.name;
            volumeTextBox.Text = volumeSet.machineType + "/" + volumeSet.machineSerial +
                " Volume: " + volume.name;
            pageTextBox.Text = currentPage.name;
            diagramColumnTextBox.Text = dotFunction.diagramColumnToLeft.ToString(); ;
            diagramRowTextBox.Text = dotFunction.diagramRowTop;

            //  TODO: The following should probably show "Source sheet" for inputs?

            sheetOrgDestLabel.Text = "Destination Sheet";

            currentConnection = null;
            if(connectionKey != 0) {
                currentConnection = connectionTable.getByKey(connectionKey);
            }

            //  Set the buttons and labels with the appropriate text and visibility.

            if(currentConnection == null) {
                applyButton.Text = "Add";
                deleteButton.Visible = false;
            }
            else {
                applyButton.Text = "Apply";
                deleteButton.Visible = true;
            }

            //  Populate the edge connector reference list (which is independent of
            //  whether this is input or output).

            edgeConnectorList = edgeConnectorTable.getWhere(
                "WHERE diagramPage='" + currentDiagramPage.idDiagramPage + "'" +
                " AND edgeconnector.order='0'");
            Edgeconnector dummyEdge = new Edgeconnector();
            dummyEdge.diagramPage = 0;
            dummyEdge.reference = "";
            edgeConnectorList.Insert(0, dummyEdge);
            refComboBox.DataSource = edgeConnectorList;

            //  If there is no existing connection, create a new one, and assume output
            //  from this dot function for the combo box (since most will be).

            if (currentConnection == null || currentConnection.idConnection == 0) {

                Console.WriteLine("Processing new connection...");
                currentConnection = new Connection();
                currentConnection.idConnection = 0;
                currentConnection.from = "";
                currentConnection.fromDiagramBlock = 0;
                currentConnection.fromPin = "";
                currentConnection.fromLoadPin = "";
                currentConnection.fromPhasePolarity = "";
                currentConnection.fromDotFunction = 0;
                currentConnection.fromEdgeSheet = 0;
                currentConnection.fromEdgeOriginSheet = 0;
                currentConnection.fromEdgeConnectorReference = "";
                currentConnection.to = "";
                currentConnection.toDiagramBlock = 0;
                currentConnection.toPin = "";
                currentConnection.toDotFunction = 0;
                currentConnection.toEdgeSheet = 0;
                currentConnection.toEdgeDestinationSheet = 0;
                currentConnection.toEdgeConnectorReference = "";
                currentConnection.toEdge2ndConnectorReference = "";
                sheetEdgeComboBox.SelectedIndex = -1;
                sheetTextBox.Text = "";
                refComboBox.SelectedIndex = -1;
                if (edgeConnectorList.Count > 0) {
                    refComboBox.SelectedItem = edgeConnectorList[0];
                }

                //  Enable the radio buttons, so the user can tell us what they want to do.

                inputRadioButton.Enabled = true;
                outputRadioButton.Enabled = true;
                populateEdgeComboBox(false);
                outputRadioButton.Checked = true;

            }
            else {
                Console.WriteLine("Processing existing connection " + currentConnection.idConnection);

                Sheetedgeinformation sheetEdge;
                Sheetedgeinformation otherSheet;
                String edgeReference;

                //  Determine if this is input or output, popoulate the combo box
                //  appropriately, and get the appropriate data.

                if (currentConnection.toEdgeSheet != 0) {
                    populateEdgeComboBox(false);
                    sheetEdge = sheetEdgeInformationTable.getByKey(
                        currentConnection.toEdgeSheet);
                    otherSheet = sheetEdgeInformationTable.getByKey(
                            currentConnection.toEdgeDestinationSheet);
                    edgeReference = currentConnection.toEdgeConnectorReference;
                    outputRadioButton.Checked = true;
                }
                else {
                    if(currentConnection.fromEdgeSheet == 0) {
                        MessageBox.Show("ERROR:  Existing connection has neither to or from " +
                            "edge sheet.", "ERROR: Connection not to or from edge",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                    populateEdgeComboBox(true);
                    sheetEdge = sheetEdgeInformationTable.getByKey(
                        currentConnection.fromEdgeSheet);
                    otherSheet = sheetEdgeInformationTable.getByKey(
                        currentConnection.fromEdgeOriginSheet);
                    edgeReference = currentConnection.fromEdgeConnectorReference;
                    inputRadioButton.Checked = true;
                }

                sheetEdgeComboBox.SelectedItem =
                    sheetEdge.row + " / " + sheetEdge.signalName;
                sheetTextBox.Text = Helpers.getDiagramPageName(otherSheet.diagramPage);
                refComboBox.SelectedItem = edgeConnectorList.Find(
                    x => x.reference == edgeReference);

                //  Since we already have an input or output, disable the radio button.

                inputRadioButton.Enabled = false;
                outputRadioButton.Enabled = false;
            }

            populatingDialog = false;

        }

        //  Method to populate the Edge Combo Box with the approriate information for 
        //  input or output.

        private void populateEdgeComboBox(bool isInput) {

            sheetEdgeInformationList = sheetEdgeInformationTable.getWhere(
                "WHERE diagramPage='" + currentDiagramPage.idDiagramPage + "'" +
                (isInput ? " AND leftSide='1'" : " AND rightSide='1'") +
                " ORDER BY sheetedgeinformation.row, sheetedgeinformation.signalName");

            sheetEdgeComboBox.Items.Clear();
            foreach (Sheetedgeinformation edgeInfo in sheetEdgeInformationList) {
                sheetEdgeComboBox.Items.Add(edgeInfo.row + " / " + edgeInfo.signalName);
            }
        }

        //  If the in/out radio buttons are changed, repoplate the pull down list.

        private void inputRadioButton_CheckedChanged(object sender, EventArgs e) {
            populateEdgeComboBox(inputRadioButton.Checked);
        }

        private void outputRadioButton_CheckedChanged(object sender, EventArgs e) {
            populateEdgeComboBox(inputRadioButton.Checked);
        }

        private void sheetEdgeComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            //  If populating dialog, or this is an ouput, ignore...

            if (populatingDialog || outputRadioButton.Checked) {
                return;
            }

            //  Find the sheet edge information whose RIGHT side matches the selected
            //  signal name.  We expect there to only be none or 1.

            List<Sheetedgeinformation> originSheetList = sheetEdgeInformationTable.getWhere(
                "WHERE signalName='" +
                sheetEdgeInformationList[sheetEdgeComboBox.SelectedIndex].signalName + "'" +
                " AND rightSide='1'");

            if (originSheetList.Count == 1) {
                sheetTextBox.Text = Helpers.getDiagramPageName(originSheetList[0].diagramPage);
            }
            else {
                sheetTextBox.Text = "";
            }

        }


        private void cancelButton_Click(object sender, EventArgs e) {

            //  Your canceled wish is my command...

            this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e) {

            //  If the current dot function connection is not to a sheet edge, 
            //  or has a 0 key, just close the form - there is nothing to do.

            if((currentConnection.from != "D" && currentConnection.to != "D")  ||
                (currentConnection.from != "E" && currentConnection.to != "E") ||
                currentConnection.idConnection == 0) {
                MessageBox.Show("Current Connection is either new or not a DOT function " +
                    "to/from an Edge.  Delete request ignored.", "Delete Ignored",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            //  Otherwise, go ahead and delete the connection.

            connectionTable.deleteByKey(currentConnection.idConnection);

            MessageBox.Show("Connection (Database ID " + currentConnection.idConnection +
                ") Deleted.", "Connection Deleted",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e) {

            string message = "";
            bool isInput = inputRadioButton.Checked;

            db.BeginTransaction();

            //  Update the connection from the entered data.

            if (sheetEdgeComboBox.SelectedIndex < 0) {
                MessageBox.Show("Sheet Edge Signal not selected", "Sheet Edge Signal Required");
                db.CancelTransaction();
                return;
            }
            if (sheetTextBox.Text.Length <= 0) {
                MessageBox.Show("Sheet Edge Sheet not identified", "Sheet Edge Sheet Required");
                db.CancelTransaction();
                return;
            }

            //  Different handling depending on whether this is a connection from and edge TO
            //  a DOT function (input), or FROM a DOT function to an edge...

            if (isInput) {
                currentConnection.from = "E";
                currentConnection.to = "D";
                currentConnection.toDotFunction = currentDotFunction.idDotFunction;
                currentConnection.fromEdgeSheet =
                    sheetEdgeInformationList[sheetEdgeComboBox.SelectedIndex].idSheetEdgeInformation;

                //  Use the method in EditConnectionForm to find the appropriate sheet...

                currentConnection.fromEdgeOriginSheet = EditConnectionForm.getSheetEdgeKey(
                    currentMachine,
                    sheetTextBox.Text,
                    sheetEdgeInformationList[sheetEdgeComboBox.SelectedIndex].signalName,
                    isInput,
                    out message);

                currentConnection.fromEdgeConnectorReference =
                    ((Edgeconnector)refComboBox.SelectedItem).reference;

                if (currentConnection.fromEdgeOriginSheet == 0) {
                    MessageBox.Show(message, "Page/Diagram Page not defined",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    db.CancelTransaction();
                    return;
                }
            }
            else {
                currentConnection.to = "E";
                currentConnection.from = "D";
                currentConnection.fromDotFunction = currentDotFunction.idDotFunction;
                currentConnection.toEdgeSheet =
                    sheetEdgeInformationList[sheetEdgeComboBox.SelectedIndex].idSheetEdgeInformation;

                //  Use the method in EditConnectionForm to find the appropriate sheet...

                currentConnection.toEdgeDestinationSheet = EditConnectionForm.getSheetEdgeKey(
                    currentMachine,
                    sheetTextBox.Text,
                    sheetEdgeInformationList[sheetEdgeComboBox.SelectedIndex].signalName,
                    isInput,
                    out message);

                currentConnection.toEdgeConnectorReference =
                    ((Edgeconnector)refComboBox.SelectedItem).reference;

                if (currentConnection.toEdgeDestinationSheet == 0) {
                    MessageBox.Show(message, "Page/Diagram Page not defined",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    db.CancelTransaction();
                    return;
                }
            }

            if (currentConnection.idConnection == 0) {
                currentConnection.idConnection = IdCounter.incrementCounter();
                connectionTable.insert(currentConnection);
                message = "Added";
            }
            else {
                connectionTable.update(currentConnection);
                message = "Updated";
            }

            db.CommitTransaction();

            MessageBox.Show(message + " connection, Database ID=" +
                currentConnection.idConnection);

            this.Close();
        }

    }
}
