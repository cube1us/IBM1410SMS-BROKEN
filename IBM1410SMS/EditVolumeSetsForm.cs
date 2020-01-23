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
    public partial class EditVolumeSetsForm : Form
    {

        DBSetup db = DBSetup.Instance;
        Table<Volumeset> volumeSetTable;
        List<Volumeset> volumeSetList;
        BindingList<Volumeset> volumeSetBindingList;
        List<Volumeset> deletedVolumeSetList = null;

        public EditVolumeSetsForm() {
            InitializeComponent();

            populateDialog();
        }

        //  Method to  populate the rest of the dialog, which for this form,
        //  is simply the data grid!

        private void populateDialog() {

            //  Clear out the existing grid...

            volumeSetsDataGridView.DataSource = null;

            //  Initialize with the list of volumeSets from the database

            volumeSetTable = db.getVolumeSetTable();
            volumeSetList = volumeSetTable.getWhere(
                "ORDER BY machineType, machineSerial");


            //  Build a new binding list for the DataGridView control,
            //  and adjust the control size and the parent window size
            //  appropriately.

            volumeSetBindingList = new BindingList<Volumeset>(volumeSetList);
            volumeSetBindingList.AllowNew = true;
            volumeSetBindingList.AllowRemove = true;
            volumeSetBindingList.AllowEdit = true;

            volumeSetsDataGridView.DataSource = volumeSetBindingList;

            //  Hide the id  and modified columns.

            volumeSetsDataGridView.Columns["idvolumeSet"].Visible = false;
            volumeSetsDataGridView.Columns["modified"].Visible = false;

            //  Set the titles and widths.

            volumeSetsDataGridView.Columns["machineType"].HeaderText = "volumeSet";
            volumeSetsDataGridView.Columns["machineType"].Width = 10 * 8;
            volumeSetsDataGridView.Columns["machineSerial"].HeaderText = "Serial / Comment";
            volumeSetsDataGridView.Columns["machineSerial"].Width = 40 * 8;

            //  Start a new list of possible deletions.

            deletedVolumeSetList = new List<Volumeset>();
        }

        private void volumeSetsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            DataGridViewRow row = volumeSetsDataGridView.Rows[e.RowIndex];
            row.ErrorText = "";

            //  If this is on a new row, ignore it.  Otherwise the user can end
            //  up in a "Catch-22" in cases where they didn't really mean to add
            //  a row, such as by hitting tab after editing the last existing row.

            if (volumeSetsDataGridView.Rows[e.RowIndex].IsNewRow) {
                return;
            }

            string columnName = volumeSetsDataGridView.Columns[e.ColumnIndex].Name;
            if (String.Compare(columnName, "machineType") == 0) {
                if (e.FormattedValue == null || e.FormattedValue.ToString().Length == 0 ||
                    e.FormattedValue.ToString().Length > 32) {
                    row.ErrorText = columnName + " is required, and " +
                        "has a max length of 32 characters.";
                    e.Cancel = true;
                }
            }
            else if (String.Compare(columnName, "machineSerial") == 0) {
                if (e.FormattedValue != null && e.FormattedValue.ToString().Length > 32) {
                    row.ErrorText = columnName + "has a max length of 32 characters.";
                    e.Cancel = true;
                }
            }
        }

        private void volumeSetsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            //  If this is the title row, ignore it
            if (e.RowIndex < 0) {
                return;
            }

            //  Mark the edited row as changed.

            Volumeset changedVolumeSet =
                (Volumeset)volumeSetsDataGridView.Rows[e.RowIndex].DataBoundItem;
            changedVolumeSet.modified = true;
        }

        private void volumeSetsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

            Table<Volume> volumeTable = db.getVolumeTable();
            List<Volume> volumeList;

            Volumeset changedVolumeSet = (Volumeset)e.Row.DataBoundItem;

            //  If the ID of this row is 0, it is a new row, so it is safe to delete.

            if(changedVolumeSet.idVolumeSet == 0) {
                return;
            }

            //  Otherwise, see if there is anything (a volume) referencing this volume set.

            volumeList = volumeTable.getWhere(
                "WHERE volume.set='" + changedVolumeSet.idVolumeSet + "'");

            if(volumeList.Count > 0) {
                MessageBox.Show("ERROR: Volume Set for machine type " + 
                    changedVolumeSet.machineType + 
                    " (Database ID: " + changedVolumeSet.idVolumeSet + " is referenced" +
                    " by entries in the volume table, and cannot be removed",
                    "Volume Set entry referenced by other entries",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else {
                deletedVolumeSetList.Add(changedVolumeSet);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            //  Just close it, already....
            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e) {

            //  Apply adds, deletes, updates

            string message = "";
            bool areChanges = false;

            //  Run through the deleted feature list.

            foreach (Volumeset volumeSet in deletedVolumeSetList) {
                message += "Deleting Volume Set for machine type " + 
                    volumeSet.machineType +
                    " (Database ID: " + volumeSet.idVolumeSet + ")\n";
                areChanges = true;
            }

            //  Run through the list looking for adds and changes.

            foreach (Volumeset volumeSet in volumeSetList) {
                if (volumeSet.idVolumeSet == 0) {
                    message += "Adding Volume Set for machine type " + 
                        volumeSet.machineType;
                    areChanges = true;
                }
                else if (volumeSet.modified) {
                    message += "Changing Volume Set for machine type " + 
                        volumeSet.machineType +
                        " (Database ID: " + volumeSet.idVolumeSet + ")\n";
                    areChanges = true;
                }
            }

            //  If there are not any changes, tell user and quit.

            if (!areChanges) {
                MessageBox.Show("No Volume Set changes were completed",
                    "No Volume Sets updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult status = MessageBox.Show("Confirm that you wish to make " +
                "the following changes to these Volume Set(s): \n\n" + message,
                "Confirm Volume Set changes",
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

                message = "Volume Set(s) Updated:\n\n";

                //  First, we do the deletes (in case they delete and then
                //  re-add a feature with the same name.)

                foreach (Volumeset volumeSet in deletedVolumeSetList) {
                    volumeSetTable.deleteByKey(volumeSet.idVolumeSet);
                    message += "Volume Set for machine type " + 
                        volumeSet.machineType + 
                        " (Database ID " + volumeSet.idVolumeSet + ") removed.\n";
                }

                //  Next we do the adds and changes...

                foreach (Volumeset volumeSet in volumeSetList) {
                    if (volumeSet.idVolumeSet == 0) {
                        volumeSet.idVolumeSet = IdCounter.incrementCounter();
                        volumeSetTable.insert(volumeSet);
                        message += "VolumeSet for machine type " + 
                            volumeSet.machineType + " added.  Database ID=" +
                            volumeSet.idVolumeSet + "\n";
                    }
                    else if (volumeSet.modified) {
                        volumeSetTable.update(volumeSet);
                        message += "VolumeSet for machine type " + volumeSet.machineType +
                            " (Database ID=" + volumeSet.idVolumeSet + ")" +
                            " updated.\n";
                    }
                }

                db.CommitTransaction();

                MessageBox.Show(message, "VolumeSet(s) updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
