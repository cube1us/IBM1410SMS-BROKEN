﻿/* 
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySQLFramework;

//  This class edites the panel table, but ALSO edits the card slot table

namespace IBM1410SMS
{
    public partial class EditPanelsForm : Form {
        DBSetup db = DBSetup.Instance;
        Table<Machine> machineTable;
        Table<Frame> frameTable;
        Table<Machinegate> machineGateTable;
        Table<Panel> panelTable;
        Table<Cardslot> cardSlotTable;
        Table<Cardlocation> cardLocationTable;
        Table<Cardlocationpage> cardLocationPageTable;
        Table<Tiedown> tieDownTable;
        Table<Diagramblock> diagramBlockTable;
        Table<Edgeconnector> edgeConnectorTable;
        List<Machine> machineList;
        List<Frame> frameList;
        List<Machinegate> machineGateList;
        List<Panel> panelList;
        List<PanelRowColumn> panelRowColumnList;
        List<PanelRowColumn> deletedPanelRowColumnList;
        BindingList<PanelRowColumn> panelRowColumnBindingList;
        Machine currentMachine = null;
        Frame currentFrame = null;
        Machinegate currentMachineGate = null;

        string[] validColumnList = Enumerable.Range(1, 50).ToList().ToArray().
            Select(i => i.ToString()).ToArray();

//         bool settingDataGridValues = false;

        public EditPanelsForm() {
            InitializeComponent();

            machineTable = db.getMachineTable();
            frameTable = db.getFrameTable();
            machineGateTable = db.getMachineGateTable();
            panelTable = db.getPanelTable();
            cardSlotTable = db.getCardSlotTable();
            cardLocationTable = db.getCardLocationTable();
            tieDownTable = db.getTieDownTable();
            diagramBlockTable = db.getDiagramBlockTable();
            edgeConnectorTable = db.getEdgeConnectorTable();
            cardLocationPageTable = db.getCardLocationPageTable();

            machineList = machineTable.getAll();
            currentMachine = machineList[0];

            //  Fill in the machine combo box, and remember which machine
            //  we started out with.

            machineComboBox.DataSource = machineList;

            //  Then do the same for the frame combo box, but just for the
            //  frames in this machine.  This also populates the machine
            //  gate combo box and teh data grid.

            populateFrameComboBox(currentMachine);

        }


        //  Shared method to populate (or not) the Frame combo box (a given
        //  machine might not have any).

        private void populateFrameComboBox(Machine machine) {

            frameList = frameTable.getWhere("WHERE machine='" +
                currentMachine.idMachine + "' ORDER BY frame.name");
            frameComboBox.DataSource = frameList;       //  Might be empty!

            //  Then if the machine has frames, set the current frame as
            //  the first one.

            currentFrame = frameList.Count > 0 ? frameList[0] : null;
            populateMachineGateComboBox(currentFrame);
        }


        //  Shared method to populate (or not) the Gate combo box (a given
        //  Gate might not have any), along with the data grid view.

        private void populateMachineGateComboBox(Frame frame) {

            //  If the frame is null, then set the current gate to null, too

            if (frame == null) {
                machineGateList = new List<Machinegate>();
                currentMachineGate = null;
            }
            else {
                machineGateList = machineGateTable.getWhere("WHERE frame='" +
                    frame.idFrame + "' ORDER BY machinegate.name");
                machineGateComboBox.DataSource = machineGateList;  //  Might be empty!
            }

            //  Then if the frame has gates, set the current gate as
            //  the first one.

            currentMachineGate = machineGateList.Count > 0 ? machineGateList[0] : null;

            //  Finally, populate the data grid table appropriately (or not)

            populatePanelTable(currentMachineGate);
        }


        //  Shared method to populate the DataGridView table that the user will
        //  edit.  This one is more interesting, becaust it is not just displaying
        //  a list.  It also has pull down lists within to identify the range
        //  of slots in the panel.

        private void populatePanelTable(Machinegate machinegate) {

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //  Clear out the existing grid, and clear out any memory of
            //  deleted entries.

            panelsDataGridView.DataSource = null;
            deletedPanelRowColumnList = new List<PanelRowColumn>();

            //  If the parameter is null, we are done -- if there is no
            //  gate, we cannot add any panels.  Otherwise, continue on.

            if (machinegate == null) {
                panelList = new List<Panel>();
                panelRowColumnList = new List<PanelRowColumn>();
                return;
            }

            //  Find the panels, if any, for the sepcified machine gate (which
            //  in turn is bound to a given frame, which is bound to a given
            //  machine.

            panelList = panelTable.getWhere("WHERE gate='" +
                machinegate.idGate + "' ORDER BY panel");
            panelRowColumnList = new List<PanelRowColumn>();
            foreach(Panel p in panelList) {
                PanelRowColumn prc = new PanelRowColumn(p);
                panelRowColumnList.Add(prc);
            }

            //  Build a new binding list for the DataGridView control.

            panelRowColumnBindingList = new BindingList<PanelRowColumn>(panelRowColumnList);
            panelRowColumnBindingList.AllowNew = true;
            panelRowColumnBindingList.AllowRemove = true;
            panelRowColumnBindingList.AllowEdit = true;

            //  Add in the actual data source.

            panelsDataGridView.DataSource = panelRowColumnBindingList;

            //  Hide columns that the user does not need to see / should not change

            panelsDataGridView.Columns["idPanel"].Visible = false;
            panelsDataGridView.Columns["gate"].Visible = false;
            panelsDataGridView.Columns["modified"].Visible = false;
            panelsDataGridView.Columns["validCoordinate"].Visible = false;

            //  Set the width of the various columns

            panelsDataGridView.Columns["panel"].Width = 10 * 8;
            panelsDataGridView.Columns["minRow"].Width = 10 * 6;
            panelsDataGridView.Columns["maxRow"].Width = 10 * 6;
            panelsDataGridView.Columns["minCol"].Width = 10 * 6;
            panelsDataGridView.Columns["maxCol"].Width = 10 * 6;
        }


        private void machineComboBox_SelectedIndexChanged(object sender, EventArgs e) {

            //  First, change the current machine.

            currentMachine = machineList[machineComboBox.SelectedIndex];

            //  Next, reset the Frames combo box and the Machine Gate Combo Box
            //  and the data grid.

            populateFrameComboBox(currentMachine);
        }

        private void frameComboBox_SelectedIndexChanged(object sender, EventArgs e) {

            //  First, change the current Frame.

            currentFrame = frameList[frameComboBox.SelectedIndex];

            //  Next, reset the Gates combo box, and repopulate the data grid.

            populateMachineGateComboBox(currentFrame);

        }

        private void machineGateComboBox_SelectedIndexChanged(object sender, EventArgs e) {

            //  Change the current gate...

            currentMachineGate = machineGateList[machineGateComboBox.SelectedIndex];
            populatePanelTable(currentMachineGate);
        }



        //  Notice if anything is changed for a given cell.  If so, check it
        //  and warn the user about any potential issues.

        private void panelsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            //  If this is the title row, ignore.

            if (e.RowIndex < 0) {
                return;
            }

            DataGridViewCell cell =
                panelsDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string columnName = panelsDataGridView.Columns[e.ColumnIndex].Name;

            //  Mark the edited row as changed.

            PanelRowColumn changedPanel =
                (PanelRowColumn)panelsDataGridView.Rows[e.RowIndex].DataBoundItem;
            changedPanel.modified = true;

            //  Check min < max for rows and columns, and issue a warning
            //  (not an outright error) if things are amiss.   When the apply 
            //  button is used, then if it is stil wrong it is an error.

            if (String.Compare(columnName, "minRow") == 0 ||
                String.Compare(columnName, "maxRow") == 0) {
                if (changedPanel.maxRow != null && changedPanel.minRow != null &&
                    String.Compare(changedPanel.minRow, changedPanel.maxRow) > 0) {
                    MessageBox.Show("Warning:  First row of panel must be <= last row.",
                        "Panel Rows Invalid",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            if (String.Compare(columnName, "minCol") == 0 ||
                String.Compare(columnName, "maxCol") == 0) {
                if (changedPanel.maxCol > 0 && 
                    changedPanel.minCol > changedPanel.maxCol) {
                    MessageBox.Show(
                        "Warning:  First column of panel must be <= last column.",
                        "Panel Rows Invalid",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }


        //  Edit the data grid min/max rows/columns to prevent errors...

        private void panelsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {

            //  Clear out the error text.

            DataGridViewRow row =
                panelsDataGridView.Rows[e.RowIndex];
            row.ErrorText = "";

            //  If this is a new row, don't complain about anything yet...

            if(panelsDataGridView.Rows[e.RowIndex].IsNewRow) {
                return;
            }

            //  Use the column name to decide what to check.

            string columnName = panelsDataGridView.Columns[e.ColumnIndex].Name;

            if (String.Compare(columnName, "minRow") == 0 ||
                String.Compare(columnName, "maxRow") == 0) {
                if (!PanelRowColumn.ROWPATTERN.IsMatch(
                    e.FormattedValue.ToString())) {
                    row.ErrorText =
                        columnName + " is required, and must be A-ZZ, not I or O";
                    e.Cancel = true;
                }
            }

            if (String.Compare(columnName, "minCol") == 0 ||
                String.Compare(columnName, "maxCol") == 0) {
                int v = -1;
                if (!Int32.TryParse(e.FormattedValue.ToString(), out v) ||
                    v < 0 || v > 99) {
                    row.ErrorText =
                        columnName + " is required, and must be an integer, 1-99";
                    e.Cancel = true;
                }
            }
            
            if(String.Compare(columnName,"panel") == 0) {
                if(e.FormattedValue == null || e.FormattedValue.ToString().Length == 0 ||
                    e.FormattedValue.ToString().Length > 8) {
                    row.ErrorText = columnName + 
                        " is required, and has a max length of 8.";
                    e.Cancel = true;
                }
            }

        }

        //  On cancel, just close the form without committing chnages...

        private void cancelButton_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e) {

            string message = "";

            //  First, make sure that the rows and columns in the DisplayGridView
            //  are valid, before we proceed.

            foreach (PanelRowColumn panelRowColumn in panelRowColumnList) {

                if (panelRowColumn.panel == null || panelRowColumn.panel.Length == 0) {
                    message += "No panel id specified.\n";
                }

                if (panelRowColumn.minRow == null || panelRowColumn.maxRow == null ||
                    panelRowColumn.minRow.Length == 0 || panelRowColumn.maxRow.Length == 0 ||
                    String.Compare(panelRowColumn.minRow, panelRowColumn.maxRow) > 0) {
                    message += "Panel " + panelRowColumn.panel + " " +
                        (panelRowColumn.idPanel > 0 ?
                            "(Database ID " + panelRowColumn.idPanel + ") " : "") +
                        "First Row \"" + panelRowColumn.minRow +
                            "\" is empty or greater than " +
                        "Last Row \"" + panelRowColumn.maxRow + "\"\n";
                }

                if (panelRowColumn.minCol < 1 || panelRowColumn.minCol > 99 ||
                    panelRowColumn.minCol > panelRowColumn.maxCol) {
                    message += "Panel " + panelRowColumn.panel + " " +
                        (panelRowColumn.idPanel > 0 ?
                            "(Database ID " + panelRowColumn.idPanel + ") " : "") +
                        "First Column " + panelRowColumn.minCol +
                            " is <= 0 or is greater than " +
                        "Last Column " + panelRowColumn.maxCol + "\n";
                }
            }

            //  Then run through looking at the changes...
            //  Note that changes may result in removing slots, so we need
            //  to check references on those.

            foreach (PanelRowColumn prc in panelRowColumnList) {
                if (prc.modified) {
                    
                    //  Get a list of all of the card slots
                    List<Cardslot> cardSlotList = cardSlotTable.getWhere(
                        "WHERE panel='" + prc.idPanel + "'");

                    //  If any of those are now out of range, check to see if it has anything 
                    //  referring to it

                    foreach (Cardslot cs in cardSlotList) {
                        if (String.Compare(cs.cardRow, prc.minRow) < 0 ||
                           String.Compare(cs.cardRow, prc.maxRow) > 0 ||
                           cs.cardColumn < prc.minCol ||
                           cs.cardColumn > prc.maxCol) {
                            string slotWhereClause = "WHERE cardSlot='" +
                                cs.idCardSlot + "'";
                            if (cardLocationTable.getWhere(slotWhereClause).Count > 0 ||
                               tieDownTable.getWhere(slotWhereClause).Count > 0 ||
                               diagramBlockTable.getWhere(slotWhereClause).Count > 0 ||
                               edgeConnectorTable.getWhere(slotWhereClause).Count > 0) {
                                message += "Panel " + prc.panel +
                                    (prc.idPanel > 0 ? " (Database ID " + prc.idPanel + ") " : " ") +
                                    "has Card Slot(s) now out of the specified range which are still " +
                                    "referred to in another table.\n";
                                break;
                            }
                        }
                    }
                }
            }

            //  If there are problems with the first/last row or columns anywhere,
            //  issue a message...

            if (message.Length > 0) {
                MessageBox.Show("Errors in first/last row/columns for one or more panels:\n\n" +
                    message, "Panel Row Column Errors",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                //  And then just return so that they can fix the error(s)

                return;
            }

            //  Run through the deleted Panel list and add them to the confirmation
            //  message

            foreach (PanelRowColumn prc in deletedPanelRowColumnList) {
                message += "Deleting Panel " + prc.panel +
                    " (Database ID: " + prc.idPanel + ") (and any empty rows/columns)\n";
            }

            //  Then run through looking for adds and changes...
            //  Note that changes may result in removing slots, so we need
            //  to check references on those.

            foreach (PanelRowColumn prc in panelRowColumnList) {
                if (prc.idPanel == 0) {
                    message += "Adding Panel " + prc.panel + "\n";
                }
                else if (prc.modified) {
                    message += "Changing Panel with Database ID " + prc.idPanel +
                        " to Panel " + prc.panel + " (and/or rows/columns) \n";
                }
            }

            //  If there were no changes, tell the user and quit.

            if (message.Length == 0) {
                MessageBox.Show("No Panel changes were completed.",
                    "No Panels Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //  There were changes, so get confirmation...

            DialogResult status = MessageBox.Show("Confirm that you wish to make " +
                "the following changes to Panel(s) for machine " +
                currentMachine.name + ", Frame " + currentFrame.name +
                ", Machine Gate " + currentMachineGate.name + ":\n\n" + message,
                "Confirm Panel(s) changes",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            //  If the user hits cancel button, just return.  Do NOT close
            //  the dialog, in case they just want to fix something and
            //  try again.

            if (status == System.Windows.Forms.DialogResult.Cancel) {
                return;
            }

            //  If they hit OK, proceed with the adds, removes and updates, and
            //  then close the form.

            else if (status == DialogResult.OK) {

                db.BeginTransaction();

                message = "Panel(s) Updated: \n\n";

                //  First, do the deletes, starting with any (unreferenced)
                //  cardslots that refer to the panel to be deleted.  Note that
                //  we already checked to make sure that the cardslots did not
                //  have any references to them when the user makred the panel
                //  for removal.

                foreach(PanelRowColumn prc in deletedPanelRowColumnList) {
                    List<Cardslot> cardSlotToDeleteList = cardSlotTable.getWhere(
                        "WHERE panel='" + prc.idPanel + "'");

                    int deletedSlotsCount = 0;

                    foreach (Cardslot cs in cardSlotToDeleteList) {
                        ++deletedSlotsCount;
                        cardSlotTable.deleteByKey(cs.idCardSlot);
                    }

                    panelTable.deleteByKey(prc.idPanel);
                    message += "Panel " + prc.panel + 
                        " (Database ID " + prc.idPanel + ") removed.";
                    if(deletedSlotsCount > 0) {
                        message += " (" + deletedSlotsCount + " slots)";
                    }
                    message += "\n";
                }

                //  Next look through the entries that remain, checking to see if
                //  anything was changed.  [NOTE:  We do NOT check for adding/removing
                //  columns if there were no changes to the panel entry.]

                foreach (PanelRowColumn prc in panelRowColumnList) {

                    //  If this is an add, now is the time to add it.
                    //  If this is a change, time to update it.

                    if (prc.idPanel == 0 || prc.modified) {

                        Panel panel = new Panel();
                        panel.panel = prc.panel;
                        if (prc.idPanel == 0) {
                            panel.idPanel = IdCounter.incrementCounter();
                            panel.gate = currentMachineGate.idGate;
                            panelTable.insert(panel);
                            message += "Panel " + panel.panel + " added, " +
                                "Database ID = " + panel.idPanel;
                            //  Remember the new database ID.
                            prc.idPanel = panel.idPanel;
                            prc.gate = panel.gate;
                        }
                        else { // modified
                            panel.idPanel = prc.idPanel;
                            panel.gate = prc.gate;
                            panelTable.update(panel);
                            message += "Panel " + panel.panel +
                                " (Database ID = " + panel.idPanel + ")";
                        }

                        //  Next, proceed to delete any card slots which are now
                        //  out of range.  (We have already checked for references...)

                        List<Cardslot> cardSlotList = cardSlotTable.getWhere(
                            "WHERE panel='" + prc.idPanel + "'");
                        int deletedSlotsCount = 0;

                        foreach (Cardslot cs in cardSlotList) {
                            if (String.Compare(cs.cardRow, prc.minRow) < 0 ||
                               String.Compare(cs.cardRow, prc.maxRow) > 0 ||
                               cs.cardColumn < prc.minCol ||
                               cs.cardColumn > prc.maxCol) {
                                cardSlotTable.deleteByKey(cs.idCardSlot);
                                ++deletedSlotsCount;
                            }
                        }
                        if (deletedSlotsCount > 0) {
                            message += " (" + deletedSlotsCount + " card slots removed.)";
                        }

                        //  Then add any missing card slots.  We iterate through the valid
                        //  row/column combinations, and look for a matching card slot entry,
                        //  and insert a new card slot if none is found.

                        int addedSlotsCount = 0;
                        for (int cardSlotRowIndex = 
                                Array.FindIndex(Helpers.validRows, s => s.Equals(prc.minRow));
                            cardSlotRowIndex <= 
                                Array.FindIndex(Helpers.validRows, s => s.Equals(prc.maxRow));
                            ++cardSlotRowIndex) {
                            for (int cardSlotColumn = prc.minCol; cardSlotColumn <= prc.maxCol; ++cardSlotColumn) {

                                //  Check to see if there is an entry for this row/column...

                                bool missingCardSlot = true;
                                foreach (Cardslot cs in cardSlotList) {
                                    if (cs.cardRow == Helpers.validRows[cardSlotRowIndex] && cs.cardColumn == cardSlotColumn) {
                                        missingCardSlot = false;
                                        break;
                                    }
                                }

                                //  Add the card slot if it was missing.

                                if (missingCardSlot) {
                                    Cardslot cs = new Cardslot();
                                    cs.idCardSlot = IdCounter.incrementCounter();
                                    cs.cardRow = Helpers.validRows[cardSlotRowIndex];
                                    cs.cardColumn = cardSlotColumn;
                                    cs.panel = prc.idPanel;
                                    ++addedSlotsCount;
                                    cardSlotTable.insert(cs);
                                }
                            }   // Column iteration
                        }   //  Row iteration

                        if (addedSlotsCount > 0) {
                            message += " (" + addedSlotsCount + " card slots added.)";
                        }

                        //  And finalize the message...

                        message += "\n";

                    }   //  End if new/modified

                }   //  End looking through panel row/column entries

                db.CommitTransaction();

                MessageBox.Show(message, "Panel(s) / Slot(s) Updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  For this one we don't close the form, so that the user can
                //  select another gate, but we do need to clear the deleted list,
                //  and rest the modified flags.

                deletedPanelRowColumnList = new List<PanelRowColumn>();
                foreach(Panel p in panelList) {
                    p.modified = false;
                }
                foreach(PanelRowColumn prc in panelRowColumnList) {
                    prc.modified = false;
                }

            }   //  End OK to update.

        }   //  End Apply Button Click event handler.


        //  If the user is trying to delete a panel, make sure that there are not
        //  references to it or to cardslots that refer to it.

        private void panelsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

            PanelRowColumn changedPanelRowColumn = (PanelRowColumn)e.Row.DataBoundItem;
            string referringTables = "";

            //  If the ID on this row is not filled in, then we can assume that
            //  it was a new row that the user decided not to add.

            if(changedPanelRowColumn.idPanel == 0) {
                return;
            }

            //  So, the user wants to delete an existing row.  We need to make
            //  sure that there are no references to it, or, if there are card slot
            //  references to it, those have no references to them in turn (so we
            //  can freely delete them when the time comes)

            string whereClause = "WHERE panel='" + changedPanelRowColumn.idPanel + "'";

            if(cardLocationPageTable.getWhere(whereClause).Count > 0) {
                referringTables += "CardLocationPage, ";
            }

            //  Method to check to see if there are any card slots referring to a
            //  given panel, and if there are, if any of those are referenced from
            //  the other relavent tables.

            foreach (Cardslot cardSlot in cardSlotTable.getWhere(whereClause)) {
                string slotWhereClause = "WHERE cardSlot='" + cardSlot.idCardSlot + "'";
                if (cardLocationTable.getWhere(slotWhereClause).Count > 0 ||
                   tieDownTable.getWhere(slotWhereClause).Count > 0 ||
                   diagramBlockTable.getWhere(slotWhereClause).Count > 0 ||
                   edgeConnectorTable.getWhere(slotWhereClause).Count > 0) {

                    referringTables += "[referenced]CardSlot, ";
                    break;
                }
            }

            if(referringTables.Length > 0) {
                MessageBox.Show("ERROR: Panel " + changedPanelRowColumn.panel +
                    ", database ID: " + changedPanelRowColumn.idPanel + " is referenced" +
                    " by one or more entries in table(s) " + referringTables + 
                    " and cannot be removed.",
                    "Panel or Card Slot entry referenced by other entries",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else {
                deletedPanelRowColumnList.Add(changedPanelRowColumn);
            }
        }

    }
}
