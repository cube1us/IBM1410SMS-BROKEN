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
using System.Collections;
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
    public partial class EditConnectionsForm : Form
    {

        DBSetup db = DBSetup.Instance;
        Table<Cardgate> cardGateTable;
        Table<Gatepin> gatePinTable;
        Table<Connection> connectionTable;
        Table<Diagramblock> diagramBlockTable;
        Table<Dotfunction> dotFunctionTable;
        Table<Sheetedgeinformation> sheetEdgeInformationTable;

        Diagramblock currentDiagramBlock;
        Machine currentMachine;
        Volumeset currentVolumeSet;
        Volume currentVolume;
        Page currentPage;
        Diagrampage currentDiagramPage;
        Cardtype currentCardType;

        // List<Cardgate> cardGateList;
        List<Gatepin> cardInputPinsList;
        List<Gatepin> cardOutputPinsList;
        List<ConnectionInfo> inputConnectionsList;
        List<ConnectionInfo> outputConnectionsList;

        // int currentGateKey;

        // SortedDictionary<string, int> definingPins = new SortedDictionary<string, int>();

        public EditConnectionsForm(
            Diagramblock diagramBlock,
            Machine machine,
            Volumeset volumeSet,
            Volume volume,
            Page page,
            Diagrampage diagramPage,
            Cardtype cardType
            ) {

            InitializeComponent();

            currentDiagramBlock = diagramBlock;
            currentMachine = machine;
            currentVolumeSet = volumeSet;
            currentVolume = volume;
            currentPage = page;
            currentDiagramPage = diagramPage;
            currentCardType = cardType;

            cardGateTable = db.getCardGateTable();
            gatePinTable = db.getGatePinTable();
            connectionTable = db.getConnectionTable();
            diagramBlockTable = db.getDiagramBlockTable();
            dotFunctionTable = db.getDotFunctionTable();
            sheetEdgeInformationTable = db.getSheetEdgeInformationTable();

            machineTextBox.Text = machine.name;
            volumeTextBox.Text = volumeSet.machineType + "/" + volumeSet.machineSerial +
                " Volume: " + volume.name;
            pageTextBox.Text = page.name;
            diagramColumnTextBox.Text = diagramBlock.diagramColumn.ToString();
            diagramRowTextBox.Text = diagramBlock.diagramRow;
            cardTypeTextBox.Text = cardType.type;

            //  Get the list of input pins and output pins for this card.

            cardInputPinsList = Helpers.getGatePins(cardType.idCardType, true);
            cardOutputPinsList = Helpers.getGatePins(cardType.idCardType, false);

            //  Set up the Edit button click handlers.

            inputsDataGridView.CellClick += new DataGridViewCellEventHandler(
                    inputEditButton_Click);
            outputsDataGridView.CellClick += new DataGridViewCellEventHandler(
                    outputEditButton_Click);

            //  Initial popoulation of connections

            populateConnections();

            //  Set focus on the input button.

            addInputButton.Select();

        }

        private void populateConnections() {

            DataGridView dgv;
            string definingPin = ((string)definingPinComboBox.SelectedItem);
            // currentGateKey = definingPins[definingPin];
            List<Connection> connectionsList;
            List<ConnectionInfo> connectionInfoList;

            foreach (bool isInput in new bool[] { true, false }) {

                dgv = isInput ? inputsDataGridView : outputsDataGridView;

                connectionsList = new List<Connection>();

                string whichWay = (isInput ? "to" : "from");
                List<Connection> cardConnectionsList = connectionTable.getWhere(
                    "WHERE " + whichWay + "DiagramBlock='" +
                        currentDiagramBlock.idDiagramBlock + "'" +
                    " AND connection." + whichWay + "='P'" +
                    " ORDER BY connection." + whichWay + "Pin");

                //  Identify the connections which are for pins on this diagram block 
                //  (i.e. Gate)

                foreach (Connection connection in cardConnectionsList) {
                    string connectionPin = (isInput ? connection.toPin :
                        connection.fromPin);
                    if(connectionPin != "--") {
                        Gatepin matchingPin = 
                        (isInput ? cardInputPinsList : cardOutputPinsList)
                            .Find(x => x.pin == connectionPin);
                        if (matchingPin != null && matchingPin.idGatePin != 0) {
                            connectionsList.Add(connection);
                        }
                    }
                    else {
                        connectionsList.Add(connection);
                    }
                }

                //  Build the list of connections info for the data grid view

                connectionInfoList = new List<ConnectionInfo>();
                foreach (Connection connection in connectionsList) {

                    ConnectionInfo info = new ConnectionInfo();
                    info.inputOutput = "";      //  Not used in this form
                    info.info3 = "";
                    info.info4 = "";
                    info.idConnection = connection.idConnection;

                    if (isInput) {
                        info.pin = connection.toPin;                // Pin on THIS gate
                        info.connectionType = connection.from;
                        switch (connection.from) {
                            case "P":
                                Diagramblock block = diagramBlockTable.getByKey(
                                    connection.fromDiagramBlock);
                                info.info1 = block.diagramColumn.ToString() + block.diagramRow;
                                info.info2 = connection.fromPin;
                                break;
                            case "D":
                                Dotfunction dot = dotFunctionTable.getByKey(
                                    connection.fromDotFunction);
                                info.info1 = dot.diagramColumnToLeft.ToString() +
                                    dot.diagramRowTop;
                                info.info2 = dot.logicFunction;
                                break;
                            case "E":
                                Sheetedgeinformation edge = sheetEdgeInformationTable.getByKey(
                                    connection.fromEdgeOriginSheet);
                                info.info1 = Helpers.getDiagramPageName(edge.diagramPage);
                                edge = sheetEdgeInformationTable.getByKey(
                                    connection.fromEdgeSheet);
                                if (connection.fromEdgeConnectorReference != null &&
                                    connection.fromEdgeConnectorReference.Length > 0) {
                                    info.info2 = "*" + connection.fromEdgeConnectorReference;
                                }
                                info.info4 = edge.row + " / " + edge.signalName;
                                break;
                            default:
                                info.info1 = "???";
                                info.info2 = "???";
                                break;
                        }
                    }
                    else {
                        info.pin = connection.fromPin + connection.fromLoadPin;
                        info.connectionType = connection.to;
                        info.info3 = connection.fromPhasePolarity;
                        switch (connection.to) {
                            case "P":
                                Diagramblock block = diagramBlockTable.getByKey(
                                    connection.toDiagramBlock);
                                info.info1 = block.diagramColumn.ToString() + block.diagramRow;
                                info.info2 = connection.toPin;
                                if (connection.toEdgeConnectorReference != null &&
                                    connection.toEdgeConnectorReference.Length > 0) {
                                    info.info4 = "*" + connection.toEdgeConnectorReference;
                                }
                                if (connection.toEdge2ndConnectorReference != null &&
                                    connection.toEdge2ndConnectorReference.Length > 0) {
                                    info.info4 += ", *" + connection.toEdge2ndConnectorReference;
                                }
                                break;
                            case "D":
                                Dotfunction dot = dotFunctionTable.getByKey(
                                    connection.toDotFunction);
                                info.info1 = dot.diagramColumnToLeft.ToString() +
                                    dot.diagramRowTop;
                                info.info2 = dot.logicFunction;
                                break;
                            case "E":
                                Sheetedgeinformation edge = sheetEdgeInformationTable.getByKey(
                                    connection.toEdgeDestinationSheet);
                                info.info1 = Helpers.getDiagramPageName(edge.diagramPage);
                                edge = sheetEdgeInformationTable.getByKey(
                                    connection.toEdgeSheet);
                                if (connection.toEdgeConnectorReference != null &&
                                    connection.toEdgeConnectorReference.Length > 0) {
                                    info.info2 = "*" + connection.toEdgeConnectorReference;
                                }
                                if (connection.toEdge2ndConnectorReference != null &&
                                    connection.toEdge2ndConnectorReference.Length > 0) {
                                    info.info2 += ", *" + connection.toEdge2ndConnectorReference;
                                }
                                info.info4 = edge.row + " / " + edge.signalName;
                                break;
                            default:
                                info.info1 = "???";
                                info.info2 = "???";
                                break;
                        }
                    }

                    connectionInfoList.Add(info);
                }

                //  Remember the list for later (not sure if I will need this...)

                if (isInput) {
                    inputConnectionsList = connectionInfoList;
                }
                else {
                    outputConnectionsList = connectionInfoList;
                }

                //  Now build the datagridview itself!

                //   Point wand, "obliviate"
                dgv.DataSource = null;
                dgv.Rows.Clear();
                dgv.Columns.Clear();
                dgv.DataSource = connectionInfoList;

                DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                buttonColumn.Name = "EditButton";
                buttonColumn.HeaderText = "Edit";
                buttonColumn.Text = "Edit";
                buttonColumn.UseColumnTextForButtonValue = true;
                dgv.Columns.Insert(0, buttonColumn);

                dgv.Columns["idConnection"].Visible = false;
                dgv.Columns["inputOutput"].Visible = false;
                dgv.Columns["edit"].Visible = false;
                dgv.Columns[0].Width = 40;
                dgv.Columns["pin"].HeaderText = "Pin";
                dgv.Columns["pin"].Width = 30;
                dgv.Columns["connectionType"].HeaderText = "Type";
                dgv.Columns["connectionType"].Width = 40;
                dgv.Columns["info1"].HeaderText = " ";
                dgv.Columns["info1"].Width = 60;
                dgv.Columns["info2"].HeaderText = " ";
                dgv.Columns["info2"].Width = 40;
                dgv.Columns["info3"].HeaderText = " ";
                dgv.Columns["info3"].Width = 40;
                dgv.Columns["info4"].HeaderText = "*Ref -or- Row / Signal Name";
                dgv.Columns["info4"].Width = 200;

            }
        }


        private void definingPinComboBox_SelectedIndexChanged(object sender, EventArgs e) {

            //  repopulate the data grid views for the selected pin...

            populateConnections();
        }

        private void inputEditButton_Click(object sender, DataGridViewCellEventArgs e) {

            //  Ignore clicks not on a button cell.

            if (e.RowIndex < 0 ||
                e.ColumnIndex != inputsDataGridView.Columns["EditButton"].Index) {
                return;
            }

            int connectionKey = (int)inputsDataGridView.Rows[e.RowIndex].Cells["idConnection"].Value;

            EditConnectionForm EditConnectionForm = new EditConnectionForm(
                currentDiagramBlock, currentMachine, currentVolumeSet, currentVolume,
                currentPage, currentDiagramPage, currentCardType, connectionKey, true);

            EditConnectionForm.ShowDialog();

            populateConnections();
        }

        private void outputEditButton_Click(object sender, DataGridViewCellEventArgs e) {

            if (e.RowIndex < 0 ||
                e.ColumnIndex != inputsDataGridView.Columns["EditButton"].Index) {
                return;
            }

            int connectionKey = (int)outputsDataGridView.Rows[e.RowIndex].Cells["idConnection"].Value;

            EditConnectionForm EditConnectionForm = new EditConnectionForm(
                currentDiagramBlock, currentMachine, currentVolumeSet, currentVolume,
                currentPage, currentDiagramPage, currentCardType, connectionKey,false);

            EditConnectionForm.ShowDialog();


            populateConnections();
        }

        private void addInputButton_Click(object sender, EventArgs e) {

            EditConnectionForm EditConnectionForm = new EditConnectionForm(
                currentDiagramBlock, currentMachine, currentVolumeSet, currentVolume,
                currentPage, currentDiagramPage, currentCardType, 0, true);

            EditConnectionForm.ShowDialog();
            populateConnections();
        }

        private void addOutputButton_Click(object sender, EventArgs e) {

            EditConnectionForm EditConnectionForm = new EditConnectionForm(
                currentDiagramBlock, currentMachine, currentVolumeSet, currentVolume,
                currentPage, currentDiagramPage, currentCardType, 0,false);

            EditConnectionForm.ShowDialog();
            populateConnections();
        }

    }

    public class ConnectionInfo {
        public int idConnection { get; set; }
        public string inputOutput { get; set; }
        public string pin { get; set; }
        public string connectionType { get; set; }
        public string info1 { get; set; }
        public string info2 { get; set; }
        public string info3 { get; set; }
        public string info4 { get; set; }
        public string edit { get; set; }
    }
}
