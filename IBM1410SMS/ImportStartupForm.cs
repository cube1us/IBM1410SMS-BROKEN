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

namespace IBM1410SMS
{
    public partial class ImportStartupForm : Form {

        public enum Disposition {
            OVERWRITE, MERGE, SKIP
        };

        public Disposition disposition { get;  set; }
        public bool testMode { get; set; } = false;

        public string fileName { get; set; }

        public ImportStartupForm() {

            InitializeComponent();
        }

        private void importButton_Click(object sender, EventArgs e) {


            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) {
                return;
            }

            //  Copy the interesting data from the form before we close it

            if (overwriteRadioButton.Checked) {
                disposition = Disposition.OVERWRITE;
            }
            else if (mergeRadioButton.Checked) {
                disposition = Disposition.MERGE;
            }
            else {
                disposition = Disposition.SKIP;
            }

            testMode = testModeCheckBox.Checked;
            fileName = openFileDialog1.FileName;

            DialogResult = DialogResult.OK;

            this.Close();                    
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
