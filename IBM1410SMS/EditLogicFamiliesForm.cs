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
    public partial class EditLogicFamiliesForm : Form
    {

        DBSetup db = DBSetup.Instance;
        Table<Logicfamily> logicFamilyTable;

        List<Logicfamily> logicFamilyList;
        List<Logicfamily> deletedLogicFamilyList;
        BindingList<Logicfamily> logicFamilyBindingList;

        public EditLogicFamiliesForm() {
            InitializeComponent();

            logicFamilyTable = db.getLogicFamilyTable();

            //  This form has no pull down lists.

            populateLogicFamilyTable();

        }

        private void populateLogicFamilyTable() {

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            //  Clear out the existing grid...

            logicFamiliesDataGridView.DataSource = null;

            //  Get a list of all of the existing IBM logic functions.

            logicFamilyList = logicFamilyTable.getAll();

            //  Build a binding list from that list...

            logicFamilyBindingList =
                new BindingList<Logicfamily>(logicFamilyList);
            logicFamilyBindingList.AllowNew = true;
            logicFamilyBindingList.AllowRemove = true;
            logicFamilyBindingList.AllowEdit = true;
            logicFamiliesDataGridView.DataSource = logicFamilyBindingList;

            //  Hide columns that the user does not need to see / should not change.

            logicFamiliesDataGridView.Columns["idLogicFamily"].Visible = false;
            logicFamiliesDataGridView.Columns["modified"].Visible = false;

            //  Make the title row upper case.

            logicFamiliesDataGridView.Columns["name"].HeaderText = "Logic Family";

            //  Set the column width...

            logicFamiliesDataGridView.Columns["name"].Width = 10 * 8;

            //  Finally, throw away any deleted entries...

            deletedLogicFamilyList = new List<Logicfamily>();
        }

        private void logicFamiliesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e) {

            //  If this is the title row, ignore it...
            if (e.RowIndex < 0) {
                return;
            }

            //  Mark the edited row as changed, if it exists.

            Logicfamily changedLogicFamily =
                (Logicfamily)logicFamiliesDataGridView.Rows[e.RowIndex].DataBoundItem;
            changedLogicFamily.modified = true;
        }

        private void logicFamiliesDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e) {

            //  Clear out the error text...

            DataGridViewRow row =
                logicFamiliesDataGridView.Rows[e.RowIndex];
            row.ErrorText = "";

            //  If this is a new row, don't try to validate it...

            if (logicFamiliesDataGridView.Rows[e.RowIndex].IsNewRow) {
                return;
            }

            string columnName = logicFamiliesDataGridView.Columns[e.ColumnIndex].Name;
            if (string.Compare(columnName, "name") == 0) {
                if (e.FormattedValue == null || e.FormattedValue.ToString().Length == 0 ||
                    e.FormattedValue.ToString().Length > 16) {
                    row.ErrorText = "Logic Function is required, and has a max " +
                        "length of 16 characters.";
                    e.Cancel = true;
                }
            }
        }

        private void logicFamiliesDataGridView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e) {

            Logicfamily changedLogicFamily =
                (Logicfamily)e.Row.DataBoundItem;

            //  If the ID of this row is not filled in, then we can assume
            //  it was a enw row that the user decided not toa dd.

            if (changedLogicFamily.idLogicFamily == 0) {
                return;
            }

            //  So, the users wants to delete an existing row.  First, we need to make
            //  sure that there are not any references to it.

            List<Cardtype> cardTypeList;
            Table<Cardtype> cardTypeTable = db.getCardTypeTable();

            cardTypeList = cardTypeTable.getWhere(
                "WHERE logicFamily='" +
                changedLogicFamily.idLogicFamily + "'");

            if (cardTypeList.Count > 0) {
                MessageBox.Show("ERROR: This Logic Family is referenced " +
                    "by one or more entries in the Card Type table " +
                    "and cannot be removed.",
                    "Logic Family entry referenced by other entries",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            else {
                deletedLogicFamilyList.Add(changedLogicFamily);
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

            foreach (Logicfamily lf in deletedLogicFamilyList) {
                message += "Deleting Logic Family " + lf.name +
                    " (Database ID: " + lf.idLogicFamily + ")\n";
                areChanges = true;
            }

            //  Next run through the list looking for adds and changes...

            foreach (Logicfamily lf in logicFamilyList) {
                if (lf.idLogicFamily == 0) {
                    message += "Adding Logic Family " + lf.name + "\n";
                    areChanges = true;
                }
                else if (lf.modified) {
                    message += "Changing Logic Family w/Database ID: " +
                        lf.idLogicFamily + " To be named " + lf.name + "\n";
                    areChanges = true;
                }
            }

            //  If there are no changes, tell the user and quit.

            if (!areChanges) {
                MessageBox.Show("No Logic Family changes were completed",
                    "No IBM Logic Families updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult status = MessageBox.Show("Confirm that you wish to make " +
                "the following changes to the list of IBM Logic Families:\n\n" +
                message,
                "Confirm IBM Logic Family changes",
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

                message = "IBM Logic Families Updated:\n\n";

                db.BeginTransaction();

                //  First we do the deletes (in case they delete and then re-add
                //  the IBM Logic Function with the same label...

                foreach (Logicfamily lf in deletedLogicFamilyList) {
                    logicFamilyTable.deleteByKey(lf.idLogicFamily);
                    message += "Logic Family " + lf.name + " removed.\n";
                }

                //  Next, we do the adds and changes...

                foreach (Logicfamily lf in logicFamilyList) {

                    //  If there is no existing ID, this must be an Add
                    if (lf.idLogicFamily == 0) {
                        lf.idLogicFamily = IdCounter.incrementCounter();
                        logicFamilyTable.insert(lf);
                        message += "Logic Family " + lf.name +
                            " added.   ID=" + lf.idLogicFamily + "\n";
                    }
                    else if (lf.modified) {
                        logicFamilyTable.update(lf);
                        message += "Logic Family " + lf.name +
                            " updated.  ID=" + lf.idLogicFamily + "\n";
                    }
                }

                db.CommitTransaction();

                MessageBox.Show(message, "IBM Logic Families updated",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                //  And close the form....

                this.Close();
            }

        }
    }
}
