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

    public partial class EditLogicFunctionsForm : Form
    {

        DBSetup db = DBSetup.Instance;
        Table<Logicfunction> logicFunctionTable;

        List<Logicfunction> logicFunctionList;
        List<Logicfunction> deletedLogicFunctionList;
        BindingList<Logicfunction> logicFunctionBindingList;

        public EditLogicFunctionsForm() {
            InitializeComponent();

            logicFunctionTable = db.getLogicFunctionTable();

            //  This form has no pull down lists.

            populateLogicFunctionTable();

        }

        private void populateLogicFunctionTable() {

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //  Clear out the existing grid...

            logicFunctionsDataGridView.DataSource = null;

            //  Get a list of all of the existing IBM logic functions.

            logicFunctionList = logicFunctionTable.getAll();

            //  Build a binding list from that list...

            logicFunctionBindingList =
                new BindingList<Logicfunction>(logicFunctionList);
            logicFunctionBindingList.AllowNew = true;
            logicFunctionBindingList.AllowRemove = true;
            logicFunctionBindingList.AllowEdit = true;
            logicFunctionsDataGridView.DataSource = logicFunctionBindingList;

            //  Hide columns that the user does not need to see / should not change.

            logicFunctionsDataGridView.Columns["idLogicFunction"].Visible = false;
            logicFunctionsDataGridView.Columns["modified"].Visible = false;

            //  Make the title row upper case.

            logicFunctionsDataGridView.Columns["name"].HeaderText = "Logic Function";

            //  Set the column width...

            logicFunctionsDataGridView.Columns["name"].Width = 10 * 20;

            //  Finally, throw away any deleted entries...

            deletedLogicFunctionList = new List<Logicfunction>();

        }

        private void logicFunctionsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            //  If this is the title row, ignore it...
            if (e.RowIndex < 0) {
                return;
            }

            //  Mark the edited row as changed, if it exists.

            Logicfunction changedLogicFunction =
                (Logicfunction)logicFunctionsDataGridView.Rows[e.RowIndex].DataBoundItem;
            changedLogicFunction.modified = true;
        }

        private void logicFunctionsDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {
            
            //  Clear out the error text...

            DataGridViewRow row =
                logicFunctionsDataGridView.Rows[e.RowIndex];
            row.ErrorText = "";

            //  If this is a new row, don't try to validate it...

            if (logicFunctionsDataGridView.Rows[e.RowIndex].IsNewRow) {
                return;
            }

            string columnName = logicFunctionsDataGridView.Columns[e.ColumnIndex].Name;
            if (string.Compare(columnName, "name") == 0) {
                if (e.FormattedValue == null || e.FormattedValue.ToString().Length == 0 ||
                    e.FormattedValue.ToString().Length > 45) {
                    row.ErrorText = "Logic Function is required, and has a max " +
                        "length of 45 characters.";
                    e.Cancel = true;
                }
            }
        }

        private void logicFunctionsDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

            Logicfunction changedLogicFunction =
                (Logicfunction)e.Row.DataBoundItem;

            //  If the ID of this row is not filled in, then we can assume
            //  it was a enw row that the user decided not toa dd.

            if (changedLogicFunction.idLogicFunction == 0) {
                return;
            }

            //  So, the users wants to delete an existing row.  First, we need to make
            //  sure that there are not any references to it.

            List<Cardgate> cardGateList;
            Table<Cardgate> cardGateTable = db.getCardGateTable();

            cardGateList = cardGateTable.getWhere(
                "WHERE logicFunction='" +
                changedLogicFunction.idLogicFunction + "'");

            if (cardGateList.Count > 0) {
                MessageBox.Show("ERROR: This Standard Logic Function is referenced " +
                    "by one or more entries in the Card Gate table " +
                    "and cannot be removed.",
                    "Logic Function entry referenced by other entries",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else {
                deletedLogicFunctionList.Add(changedLogicFunction);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e) {

            //  On cancel, just close the form.

            this.Close();
        }

        private void applyButton_Click(object sender, EventArgs e) {

            string message = "";
            bool areChanges = false;

            //  Run through the deleted logic function list.

            foreach (Logicfunction lf in deletedLogicFunctionList) {
                message += "Deleting Logic Function " + lf.name +
                    " (Database ID: " + lf.idLogicFunction + ")\n";
                areChanges = true;
            }

            //  Next run through the list looking for adds and changes...

            foreach (Logicfunction lf in logicFunctionList) {
                if (lf.idLogicFunction == 0) {
                    message += "Adding Logic Function " + lf.name + "\n";
                    areChanges = true;
                }
                else if (lf.modified) {
                    message += "Changing IBM Function w/Database ID: " +
                        lf.idLogicFunction + " To be named " + lf.name + "\n";
                    areChanges = true;
                }
            }

            //  If there are no changes, tell the user and quit.

            if (!areChanges) {
                MessageBox.Show("No Logic Function changes were completed",
                    "No IBM Logic Functions updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult status = MessageBox.Show("Confirm that you wish to make " +
                "the following changes to the list of Standard Logic Functions:\n\n" +
                message,
                "Confirm Standard Logic Function changes",
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

                message = "Standard Logic Functions Updated:\n\n";

                db.BeginTransaction();

                //  First we do the deletes (in case they delete and then re-add
                //  the IBM Logic Function with the same label...

                foreach (Logicfunction lf in deletedLogicFunctionList) {
                    logicFunctionTable.deleteByKey(lf.idLogicFunction);
                    message += "Logic Function " + lf.name + " removed.\n";
                }

                //  Next, we do the adds and changes...

                foreach (Logicfunction lf in logicFunctionList) {

                    //  If there is no existing ID, this must be an Add
                    if (lf.idLogicFunction == 0) {
                        lf.idLogicFunction = IdCounter.incrementCounter();
                        logicFunctionTable.insert(lf);
                        message += "Logic Function " + lf.name +
                            " added.   ID=" + lf.idLogicFunction + "\n";
                    }
                    else if (lf.modified) {
                        logicFunctionTable.update(lf);
                        message += "Logic Function " + lf.name +
                            " updated.  ID=" + lf.idLogicFunction + "\n";
                    }
                }

                db.CommitTransaction();

                MessageBox.Show(message, "Standard Logic Function(s) updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  And close the form....

                this.Close();
            }

        }
    }
}
