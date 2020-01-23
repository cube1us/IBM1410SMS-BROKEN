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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySQLFramework;

namespace IBM1410SMS
{
    class ImportCardLocationPages : Importer
    {

        Table<Page> pageTable;
        Table<Cardlocationpage> cardLocationPageTable;
        Table<Machine> machineTable;
        Table<Volumeset> volumeSetTable;
        Table<Volume> volumeTable;
        Table<Eco> ecoTable;

        Hashtable csvColumnNames = new Hashtable();
        ImportStartupForm.Disposition disposition;

        public ImportCardLocationPages(string fileName,
            ImportStartupForm.Disposition disposition,
            bool testMode) : base(fileName) {

            this.disposition = disposition;
            bool header = true;
            List<string> csvColumns;
            int volumeKey;
            int machineKey;
            int parseOut;

            DBSetup db = DBSetup.Instance;
            pageTable = db.getPageTable();
            cardLocationPageTable = db.getCardLocationPageTable();
            machineTable = db.getMachineTable();
            volumeSetTable = db.getVolumeSetTable();
            volumeTable = db.getVolumeTable();
            ecoTable = db.getEcoTable();

            Page currentPage;

            while ((csvColumns = getCSVColumns()).Count > 0) {

                //  Snag the header line so we can search for columns by name.
                //  That way, if we reorder columns, insert a column, etc.,
                //  the code will still work.

                if (header) {
                    header = false;
                    int columnIndex = 0;
                    foreach (string s in csvColumns) {
                        if (s.Length > 0) {
                            csvColumnNames.Add(s, columnIndex);
                        }
                        ++columnIndex;
                    }

                    //  Check for required columns...  Column order does not
                    //  matter.  (Only column Volume is optional)

                    string missingColumns = "";
                    missingColumns += checkColumn("Machine Serial");
                    missingColumns += checkColumn("Volume");
                    missingColumns += checkColumn("Drawing P/N");
                    missingColumns += checkColumn("Machine");
                    missingColumns += checkColumn("Page");
                    missingColumns += checkColumn("ECO");
                    missingColumns += checkColumn("Run");
                    missingColumns += checkColumn("Previous ECO");
                    missingColumns += checkColumn("Title");

                    if (missingColumns.Length > 0) {
                        MessageBox.Show("One or more input columns are " +
                            "missing: \n" + missingColumns,
                            "Missing Column(s)",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        closeLog();
                        return;
                    }

                    //  Start a single transaction for all of this...

                    db.BeginTransaction();

                    continue;
                }

                //  We are past the headers, to get the column data...

                string machineSerial = csvColumns[(int)csvColumnNames["Machine Serial"]];
                string volumeName = csvColumns[(int)csvColumnNames["Volume"]];
                string drawingPart = csvColumns[(int)csvColumnNames["Drawing P/N"]];
                string machineName = csvColumns[(int)csvColumnNames["Machine"]];
                string pageName = csvColumns[(int)csvColumnNames["Page"]];
                string ecoName = csvColumns[(int)csvColumnNames["ECO"]];
                string run = csvColumns[(int)csvColumnNames["Run"]];
                string previousECOName = csvColumns[(int)csvColumnNames["Previous ECO"]];
                string title = csvColumns[(int)csvColumnNames["Title"]];

                //  Title maxes out at 80.

                if(title.Length > 80) {
                    logMessage("Warning:  Title for page named " + pageName +
                        " truncated to 80 characters.");
                    title = title.Substring(0, 80);
                }

                //  Get the key for the specified machine.  Add the machine if
                //  it is not already there.

                if((machineKey = getMachineKeyByName(machineName)) == 0) {
                    Machine machine = new Machine();
                    machine.idMachine = IdCounter.incrementCounter();
                    machine.name = machineName;
                    machineTable.insert(machine);
                    machineKey = machine.idMachine;
                    logMessage("Added Machine " + machineName +
                        ", Database ID=" + machineKey);
                }

                //  Process Volume Set and Volume Set Name.  Add them if
                //  they don't already exist.  The volume machine Type 
                //  is the machine name.

                volumeKey = getVolumeKeyByName(volumeName, machineName, 
                    machineSerial);

                //  See if this page already exists.  If so, then we
                //  need to process according to disposition.  If not,
                //  we just add it.

                //  Expand part number out to 7 digits...

                drawingPart = zeroPadPartNumber(drawingPart);

                List<Page> pageList = pageTable.getWhere(
                    "WHERE machine='" + machineKey + "'" +
                    " AND volume='" + volumeKey + "'" +
                    " AND part='" + drawingPart + "'" +
                    " AND page.name='" + pageName + "'");

                Console.WriteLine("Padded part number is " + drawingPart);

                if(pageList.Count > 0) {

                    Page oldPage = pageList[0];

                    if (disposition == ImportStartupForm.Disposition.SKIP) {
                        logMessage("Skipping import of duplicate page " + pageName +
                            " due to disposition SKIP");
                    }
                    else if (disposition == ImportStartupForm.Disposition.MERGE) {
                        if (oldPage.title.Length == 0) {
                            oldPage.title = title;
                        }
                        pageTable.update(oldPage);
                        logMessage("Page " + pageName + " merged with existing " +
                            "page (Database ID " + oldPage.idPage +
                            ") due to disposition MERGE");
                    }
                    else {
                        //  Overwrite

                        oldPage.title = title;
                        oldPage.stamp = "";
                        pageTable.update(oldPage);
                        logMessage("Existing Page " + pageName + 
                            " (Database ID " + oldPage.idPage +
                            ") overwritten due to disposition OVERWRITE");                    
                    }
                    currentPage = oldPage;
                }
                else {
                    //  New page
                    currentPage = new Page();
                    currentPage.idPage = IdCounter.incrementCounter();
                    currentPage.volume = volumeKey;
                    currentPage.machine = machineKey;
                    currentPage.part = drawingPart;
                    currentPage.title = title;
                    currentPage.name = pageName;
                    currentPage.stamp = "";
                    pageTable.insert(currentPage);
                    logMessage("Added page " + pageName + 
                        ", Database ID=" + currentPage.idPage);
                }

                //  Same story with the Card Location Page

                int ecoKey = 0;
                if (ecoName.Length > 0) {
                    ecoKey = getECOByName(machineKey, ecoName);
                }

                List<Cardlocationpage> cardLocationPageList =
                    cardLocationPageTable.getWhere("" +
                    "WHERE cardlocationpage.page='" + currentPage.idPage + "'" +
                    " AND eco='" + ecoKey + "'");

                if(cardLocationPageList.Count > 0) {

                    Cardlocationpage oldCardLocationPage =
                        cardLocationPageList[0];

                    if(disposition == ImportStartupForm.Disposition.SKIP) {
                        logMessage("Skipping import of duplicate Card Location Page " +
                            " for page " + pageName +
                            "(Database ID" + oldCardLocationPage.idCardLocationPage +
                            " due to disposition SKIP");                           
                    }
                    else if(disposition == ImportStartupForm.Disposition.MERGE) {
                        if(oldCardLocationPage.page == 0) {
                            oldCardLocationPage.page = currentPage.idPage;
                        }
                        if(oldCardLocationPage.run == 0) {
                            parseOut = 0;
                            if(int.TryParse(run, out parseOut)) {
                                oldCardLocationPage.run = parseOut;
                            }
                        }
                        if(oldCardLocationPage.previousECO == 0 &&
                            previousECOName.Length > 0) {
                            oldCardLocationPage.previousECO =
                                getECOByName(machineKey, previousECOName);
                        }
                        cardLocationPageTable.update(oldCardLocationPage);
                        logMessage("Merged Card Location Page with existing page " +
                            " for page " + pageName +
                            "(Database ID" + oldCardLocationPage.idCardLocationPage +
                            ") due to disposition MERGE");
                    }
                    else {
                        //  Overwrite
                        oldCardLocationPage.page = currentPage.idPage;
                        parseOut = 0;
                        int.TryParse(run, out parseOut);
                        oldCardLocationPage.run = parseOut;
                        //   Leave panel alone, if it has one.
                        oldCardLocationPage.previousECO =
                            getECOByName(machineKey, previousECOName);
                        cardLocationPageTable.update(oldCardLocationPage);
                        logMessage("Existing Card Location Page " +
                            "for page " + pageName +
                            "(Database ID " + oldCardLocationPage.idCardLocationPage +
                            ") overwritten due to disposition OVERWRITE");

                    }
                }

                else {
                    //  New card location page...
                    Cardlocationpage newCardLocationPage = new Cardlocationpage();
                    newCardLocationPage.idCardLocationPage = IdCounter.incrementCounter();
                    newCardLocationPage.page =
                        currentPage.idPage;
                    newCardLocationPage.eco = ecoKey;
                    newCardLocationPage.panel = 0;      // We don't know the panel...
                    parseOut = 0;
                    int.TryParse(run, out parseOut);
                    newCardLocationPage.run = parseOut;
                    newCardLocationPage.previousECO =
                        getECOByName(machineKey, previousECOName);
                    newCardLocationPage.sheets = 1;
                    cardLocationPageTable.insert(newCardLocationPage);
                    logMessage("Added new Card Location Page for page " +
                        pageName + " Database ID=" +
                        newCardLocationPage.idCardLocationPage);
                }
            }

            //  End the transaction to apply the updates, and display the log.

            if (testMode) {
                db.CancelTransaction();
            }
            else {
                db.CommitTransaction();
            }            

            displayLog();
        }

        //  Utility routine to check if a required column name is present, and
        //  return the column name follwed by a space if it is NOT present

        private string checkColumn(string column) {
            return csvColumnNames[column] == null ? column + ", " : "";
        }
    }
}
