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
    public partial class EditSheetEdgeOutputConnectionsForm : Form
    {

        DBSetup db = DBSetup.Instance;

        Table<Connection> connectionTable;
        Table<Sheetedgeinformation> sheetEdgeInformationTable;
        Table<Edgeconnector> edgeConnectorTable;
        Table<Page> pageTable;
        Table<Diagrampage> diagramPageTable;

        Diagramblock currentDiagramBlock;
        Diagrampage currentDiagramPage;
        Page currentPage;
        Machine currentMachine;
        Volumeset currentVolumeSet;
        Volume currentVolume;
        Sheetedgeinformation currentOriginSheetEdge;
        string currentPin;
        string currentPolarity;
        string currentLoadPin;
        List<Edgeconnector> currentEdgeConnectorList;

        List<destinationEntry> destinationList;
        BindingList<destinationEntry> destinationBindingList;

        bool populatingDialog = true;

        public EditSheetEdgeOutputConnectionsForm(
            Diagramblock diagramBlock,
            Machine machine,
            Volumeset volumeSet,
            Volume volume,
            Page page,
            Diagrampage diagramPage,
            Cardtype cardType,
            string fromPin,
            string fromLoadPin,
            string fromPolarity,
            List<Edgeconnector> edgeConnectorList,
            Sheetedgeinformation originSheetEdge) {

            InitializeComponent();

            currentMachine = machine;
            currentVolumeSet = volumeSet;
            currentVolume = volume;
            currentDiagramBlock = diagramBlock;
            currentDiagramPage = diagramPage;
            currentPage = page;
            currentOriginSheetEdge = originSheetEdge;
            currentPin = fromPin;
            currentLoadPin = fromLoadPin;
            currentPolarity = fromPolarity;
            currentEdgeConnectorList = edgeConnectorList;

            connectionTable = db.getConnectionTable();
            edgeConnectorTable = db.getEdgeConnectorTable();
            sheetEdgeInformationTable = db.getSheetEdgeInformationTable();
            pageTable = db.getPageTable();
            diagramPageTable = db.getDiagramPageTable();

            machineTextBox.Text = machine.name;
            volumeTextBox.Text = volumeSet.machineType + "/" + volumeSet.machineSerial +
                " Volume: " + volume.name;
            pageTextBox.Text = page.name;
            diagramColumnTextBox.Text = diagramBlock.diagramColumn.ToString();
            diagramRowTextBox.Text = diagramBlock.diagramRow;
            cardTypeTextBox.Text = cardType.type;
            rowSignalTextBox.Text = currentOriginSheetEdge.row + " / " +
                currentOriginSheetEdge.signalName;
            outputPinTextBox.Text = fromPin;

            //  Get the list of known destinations for this signal.

            List<Sheetedgeinformation> destinationSheets =
                sheetEdgeInformationTable.getWhere("" +
                "WHERE signalName='" + currentOriginSheetEdge.signalName + "'" +
                " AND leftSide='1'");

            //  Build the list of entries for the data grid view.  Really, there should
            //  not be any, but doing in this way allows for eventually turning this into
            //  a full edit.

            destinationList = new List<destinationEntry>();

            foreach(Sheetedgeinformation edge in destinationSheets) {
                destinationEntry entry = new destinationEntry();
                entry.pageName = Helpers.getDiagramPageName(edge.diagramPage);
                entry.edgeConnectionReference = "";
                destinationList.Add(entry);                
            }

            //  If the list is empty (as we would expect), create a dummy row so we get headers

            //if(destinationList.Count == 0) {
            //    destinationEntry entry = new destinationEntry();
            //    entry.pageName = "";
            //    entry.edgeConnectionReference = "";
            //    destinationList.Add(entry);
            //}

            destinationBindingList = new BindingList<destinationEntry>(destinationList);

            //  Set up the data grid view

            sheetEdgeDataGridView.DataSource = destinationBindingList;

            sheetEdgeDataGridView.Columns["pageName"].HeaderText = "Sheet";
            sheetEdgeDataGridView.Columns["pageName"].Width = 110;
            sheetEdgeDataGridView.Columns["edgeConnectionReference"].HeaderText = "*Ref";
            sheetEdgeDataGridView.Columns["edgeConnectionReference"].Width = 40;
            sheetEdgeDataGridView.Columns["secondEdgeConnectionReference"].HeaderText = "*Ref";
            sheetEdgeDataGridView.Columns["secondEdgeConnectionReference"].Width = 40;

        }

        private void cancelButton_Click(object sender, EventArgs e) {

            //  If the user cancels, just close and return.

            this.Close();

        }

        private void sheetEdgeDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            //  The value changed, so reset the error text...

            if (e.RowIndex >= 0) {
                sheetEdgeDataGridView.Rows[e.RowIndex].ErrorText = "";
            }
        }


        private void applyButton_Click(object sender, EventArgs e) {

            string message = "";
            string tempMessage;
            bool error = false;

            db.BeginTransaction();

            //  Create a new connection for each entry in the list.

            int row = 0;
            foreach(destinationEntry entry in destinationList) {

                //  If the entered sheet does not exist, mark it and set the error flag.

                int sheetEdgeKey = EditConnectionForm.getSheetEdgeKey(
                    currentMachine, entry.pageName,
                    currentOriginSheetEdge.signalName, false, out tempMessage);

                sheetEdgeDataGridView.Rows[row].ErrorText = "";

                //  Add the message to the list.  If there was no key, set the
                //  error flag.

                message += tempMessage;
                if(sheetEdgeKey == 0) {
                    error = true;
                    sheetEdgeDataGridView.Rows[row].ErrorText = "Sheet does not exist. ";
                }

                //  Also check the supplied connector references, if any, are on the list

                if (entry.edgeConnectionReference != null) {
                    entry.edgeConnectionReference = entry.edgeConnectionReference.ToUpper();
                }
                tempMessage = checkEdgeConnectorReference(entry.edgeConnectionReference);
                if (tempMessage.Length > 0) {
                    message += tempMessage;
                    sheetEdgeDataGridView.Rows[row].ErrorText += "Invalid Reference. ";
                    error = true;
                }

                if (entry.secondEdgeConnectionReference != null) {
                    entry.secondEdgeConnectionReference =
                       entry.secondEdgeConnectionReference.ToUpper();
                }
                tempMessage = checkEdgeConnectorReference(entry.secondEdgeConnectionReference);
                if (tempMessage.Length > 0) {
                    message += tempMessage;
                    sheetEdgeDataGridView.Rows[row].ErrorText += "Invalid 2nd Reference. ";
                    error = true;
                }

                //  If we have had one or more errors, then continue but only to verify
                //  the remaining page numbers and references.  Any updates will be discarded later.

                ++row; 

                if (error) {
                    continue;
                }

                Connection connection = new Connection();
                
                //  For now, we only support connections from gates in this form.

                connection.from = "P";
                connection.fromDiagramBlock = currentDiagramBlock.idDiagramBlock;
                connection.fromPin = currentPin;
                connection.fromLoadPin = currentLoadPin;
                connection.fromPhasePolarity = currentPolarity;
                connection.fromDotFunction = 0;
                connection.fromEdgeSheet = 0;
                connection.fromEdgeOriginSheet = 0;
                connection.fromEdgeConnectorReference = "";

                connection.to = "E";
                connection.toDiagramBlock = 0;
                connection.toPin = "";
                connection.toDotFunction = 0;
                connection.toEdgeSheet = currentOriginSheetEdge.idSheetEdgeInformation;
                connection.toEdgeDestinationSheet = sheetEdgeKey;

                connection.toEdgeConnectorReference = entry.edgeConnectionReference != null ?
                    entry.edgeConnectionReference.ToUpper() : "";
                connection.toEdge2ndConnectorReference = entry.secondEdgeConnectionReference != null ?
                    entry.secondEdgeConnectionReference.ToUpper() : "";

                connection.idConnection = IdCounter.incrementCounter();
                connectionTable.insert(connection);

                message += "Connection to page " + entry.pageName +
                    (entry.edgeConnectionReference != null &&
                     entry.edgeConnectionReference.Length > 0 ?
                        "(*" + entry.edgeConnectionReference + ")" : "") +
                    " Added, Database ID=" + connection.idConnection + "\n";            
            }

            if(error) {
                message = "ERRORS:  Transaction Cancelled due to the below error(s)\n" +
                    message;
                db.CancelTransaction();
            }

            MessageBox.Show(message, "Message Log",
                MessageBoxButtons.OK,
                error ? MessageBoxIcon.Error : MessageBoxIcon.Information);

            if(error) {
                return;
            }

            db.CommitTransaction();

            this.Close();
        }

        private string checkEdgeConnectorReference(string edgeReference) {

            string message = "";

            if (edgeReference != null && edgeReference.Length > 0) {
                Edgeconnector edgeConnector = currentEdgeConnectorList.Find(
                    x => x.reference == edgeReference.ToUpper());
                if (edgeConnector == null ||
                    edgeConnector.reference != edgeReference) {
                    message += "Error: Edge connector reference " +
                        edgeReference + " is not defined on this page.\n";
                }
            }

            return message;

        }

    }

    class destinationEntry
    {
        public string pageName { get; set; }
        public string edgeConnectionReference { get; set; }
        public string secondEdgeConnectionReference { get; set; }
    }
}
