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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySQLFramework;

namespace IBM1410SMS
{
    public partial class EditIBMLogicFunctionForm : Form
    {

        DBSetup db = DBSetup.Instance;
        Table<Ibmlogicfunction> ibmLogicFunctionTable;

        List<Ibmlogicfunction> ibmLogicFunctionList;
        List<Ibmlogicfunction> deletedIbmLogicFunctionList;
        BindingList<Ibmlogicfunction> ibmLogicFunctionBindingList;

        public EditIBMLogicFunctionForm() {
            InitializeComponent();

            ibmLogicFunctionTable = db.getIbmLogicFunctionTable();

            //  This form has no pull down lists.

            populateIbmLogicFunctionTable();
        }

        //  Method to populate the Data Grid View that the user will edit.

        private void populateIbmLogicFunctionTable() {

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //  Clear out the existing grid...

            IBMLogicFunctionDataGridView.DataSource = null;

            //  Get a list of all of the existing IBM logic functions.

            ibmLogicFunctionList = ibmLogicFunctionTable.getAll();

            //  Build a binding list from that list...

            ibmLogicFunctionBindingList =
                new BindingList<Ibmlogicfunction>(ibmLogicFunctionList);
            ibmLogicFunctionBindingList.AllowNew = true;
            ibmLogicFunctionBindingList.AllowRemove = true;
            ibmLogicFunctionBindingList.AllowEdit = true;
            IBMLogicFunctionDataGridView.DataSource = ibmLogicFunctionBindingList;

            //  Hide columns that the user does not need to see / should not change.

            IBMLogicFunctionDataGridView.Columns["idIBMLogicFunction"].Visible = false;
            IBMLogicFunctionDataGridView.Columns["modified"].Visible = false;

            //  Make the title row upper case.

            IBMLogicFunctionDataGridView.Columns["label"].HeaderText = "Logic Function";

            //  Set the column width...

            IBMLogicFunctionDataGridView.Columns["label"].Width = 10 * 5;

            //  Finally, throw away any deleted entries...

            deletedIbmLogicFunctionList = new List<Ibmlogicfunction>();
        }

        private void IBMLogicFunctionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            //  If this is the title row, ignore it...
            if (e.RowIndex < 0) {
                return;
            }

            //  Mark the edited row as changed, if it exists.

            Ibmlogicfunction changedIbmLogicFunction =
                (Ibmlogicfunction)IBMLogicFunctionDataGridView.Rows[e.RowIndex].DataBoundItem;
            changedIbmLogicFunction.modified = true;
        }

        private void IBMLogicFunctionDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {

            //  Clear out the error text...

            DataGridViewRow row =
                IBMLogicFunctionDataGridView.Rows[e.RowIndex];
            row.ErrorText = "";

            //  If this is a new row, don't try to validate it...

            if (IBMLogicFunctionDataGridView.Rows[e.RowIndex].IsNewRow) {
                return;
            }

            string columnName = IBMLogicFunctionDataGridView.Columns[e.ColumnIndex].Name;
            if (string.Compare(columnName, "label") == 0) {
                if (e.FormattedValue == null || e.FormattedValue.ToString().Length == 0 ||
                    e.FormattedValue.ToString().Length > 4) {
                    row.ErrorText = "Logic Function is required, and has a max " +
                        "length of 4 characters.";
                    e.Cancel = true;
                }
            }

        }

        private void IBMLogicFunctionDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

            Ibmlogicfunction changedIbmLogicFunction =
                (Ibmlogicfunction)e.Row.DataBoundItem;

            //  If the ID of this row is not filled in, then we can assume
            //  it was a enw row that the user decided not toa dd.

            if (changedIbmLogicFunction.idIBMLogicFunction == 0) {
                return;
            }

            //  So, the users wants to delete an existing row.  First, we need to make
            //  sure that there are not any references to it.

            List<Cardgate> cardGateList;
            Table<Cardgate> cardGateTable = db.getCardGateTable();

            cardGateList = cardGateTable.getWhere(
                "WHERE positiveLogicFunction='" +
                changedIbmLogicFunction.idIBMLogicFunction +
                "' OR negativeLogicFunction='" +
                changedIbmLogicFunction.idIBMLogicFunction + "'");

            if (cardGateList.Count > 0) {
                MessageBox.Show("ERROR: This IBM Logic Function is referenced " +
                    "by one or more entries in the Card Gate table " +
                    "and cannot be removed.",
                    "IBM Logic Function entry referenced by other entries",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else {
                deletedIbmLogicFunctionList.Add(changedIbmLogicFunction);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {

            //  On cancel, just close the form.

            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e) {

            string message = "";
            bool areChanges = false;

            //  Run through the deleted IBM logic function list.

            foreach (Ibmlogicfunction lf in deletedIbmLogicFunctionList) {
                message += "Deleting IBM Logic Function " + lf.label +
                    " (Database ID: " + lf.idIBMLogicFunction + ")\n";
                areChanges = true;
            }

            //  Next run through the list looking for adds and changes...

            foreach (Ibmlogicfunction lf in ibmLogicFunctionList) {
                if (lf.idIBMLogicFunction == 0) {
                    message += "Adding IBM Logic Function " + lf.label + "\n";
                    areChanges = true;
                }
                else if (lf.modified) {
                    message += "Changing IBM Logic Function w/Database ID: " +
                        lf.idIBMLogicFunction + " To be named " + lf.label + "\n";
                    areChanges = true;
                }
            }

            //  If there are no changes, tell the user and quit.

            if (!areChanges) {
                MessageBox.Show("No IBM Logic Function changes were completed",
                    "No IBM Logic Functions updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult status = MessageBox.Show("Confirm that you wish to make " +
                "the following changes to the list of IBM Logic Functions:\n\n" +
                message,
                "Confirm IBM Logic Function changes",
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

                message = "IBM Logic Functions Updated:\n\n";

                db.BeginTransaction();

                //  First we do the deletes (in case they delete and then re-add
                //  the IBM Logic Function with the same label...

                foreach (Ibmlogicfunction lf in deletedIbmLogicFunctionList) {
                    ibmLogicFunctionTable.deleteByKey(lf.idIBMLogicFunction);
                    message += "IBM Logic Function " + lf.label + " removed.\n";
                }

                //  Next, we do the adds and changes...

                foreach (Ibmlogicfunction lf in ibmLogicFunctionList) {

                    //  If there is no existing ID, this must be an Add
                    if (lf.idIBMLogicFunction == 0) {
                        lf.idIBMLogicFunction = IdCounter.incrementCounter();
                        ibmLogicFunctionTable.insert(lf);
                        message += "IBM Logic Function " + lf.label +
                            " added.   ID=" + lf.idIBMLogicFunction + "\n";
                    }
                    else if (lf.modified) {
                        ibmLogicFunctionTable.update(lf);
                        message += "IBM Logic Function " + lf.label +
                            " updated.  ID=" + lf.idIBMLogicFunction + "\n";
                    }
                }

                db.CommitTransaction();

                MessageBox.Show(message, "IBM Logic Function(s) updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  And close the form....

                this.Close();
            }

        }
    }
}
