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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySQLFramework;

namespace IBM1410SMS
{
    public class ImportFeatures : Importer
    {

        ImportStartupForm.Disposition disposition;
        Hashtable csvColumnNames = new Hashtable();

        public ImportFeatures(string fileName,
            ImportStartupForm.Disposition disposition, bool testMode) : base(fileName) {

            this.disposition = disposition;

            Table<Machine> machineTable;
            Table<Feature> featureTable;

            bool continued = false;
            bool header = true;
            Feature feature = null;
            int machineKey = 0;
            string currentMachineName = "";
            string currentCode = "";
            int featureCount = 0;

            DBSetup db = DBSetup.Instance;
            machineTable = db.getMachineTable();
            featureTable = db.getFeatureTable();

            List<string> csvColumns ;

            while((csvColumns = getCSVColumns()).Count > 0) {

                //  Process the header line.

                if(header) {
                    header = false;
                    int columnIndex = 0;
                    foreach(string s in csvColumns) {
                        csvColumnNames.Add(s, columnIndex);
                        ++columnIndex;
                    }

                    //  CHeck for required columns.  Column order doesn't matter.

                    string missingColumns = "";
                    missingColumns += checkColumn("Machine");
                    missingColumns += checkColumn("MF1 Code");
                    missingColumns += checkColumn("Cont. On Next Line");
                    missingColumns += checkColumn("Explanation");

                    if (missingColumns.Length > 0) {
                        MessageBox.Show("One or more input columns are " +
                            "missing: \n" + missingColumns,
                            "Missing Column(s)",
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        closeLog();
                        return;
                    }
                    
                    //  Columns were OK, so start up the transaction.
                
                    db.BeginTransaction();
                    continue;
                }

                string machineName = csvColumns[(int)csvColumnNames["Machine"]];
                string codeName = csvColumns[(int)csvColumnNames["MF1 Code"]];
                string continuation = csvColumns[(int)csvColumnNames["Cont. On Next Line"]];
                string explanation = csvColumns[(int)csvColumnNames["Explanation"]];

                //  If there is a machine specified, capture it.

                if (machineName.Length > 0) {
                    List<Machine> machineList = machineTable.getWhere(
                        "WHERE machine.name='" + machineName + "'");
                    if(machineList.Count > 0) {
                        machineKey = machineList[0].idMachine;
                        currentMachineName = machineName;
                    }
                }

                //  If this is a continuation (noted from the previous line)
                //  add to the feature string.

                if(continued) {
                    if(feature.feature.Length > 0) {
                        feature.feature += " \n";
                    }
                    feature.feature += explanation;
                }

                //  If we are not continuing, and we have a feature, add it,
                //  skip it, or update it, depending upon the provided
                //  disposition.

                if(!continued && feature != null) {
                    List<Feature> featureList = featureTable.getWhere(
                        "WHERE machine='" + machineKey + "'" +
                        " AND feature.code='" + currentCode + "'");

                    //  Row exists, so look at disposition to decide what to do.

                    if (featureList.Count > 0) {
                        if (disposition == ImportStartupForm.Disposition.SKIP) {
                            logMessage("Feature " + codeName +
                                " for machine " + currentMachineName +
                                " already exists (Database ID " +
                                featureList[0].idFeature +
                                "). Skipped due to disposition SKIP.");
                        }
                        else if (disposition == ImportStartupForm.Disposition.MERGE) {
                            //  For merge, use the original database key, machine and 
                            //  code, but replace the description.
                            logMessage("Feature " + codeName +
                                " for machine " + currentMachineName +
                                " already exists (Database ID " +
                                featureList[0].idFeature +
                                ").  Merged due to disposition MERGE.");
                            featureList[0].feature = feature.feature;
                            featureTable.update(featureList[0]);
                        }
                        else {
                            //  Overwrite.  But again, use the original key.
                            feature.idFeature = featureList[0].idFeature;
                            featureTable.update(feature);
                            logMessage("Feature " + codeName +
                                " for machine " + currentMachineName +
                                " already exists (Database ID " +
                                featureList[0].idFeature +
                                ").  REPLACED due to disposition Overwrite.");
                        }
                    }
                    else {

                        //  New row.

                        feature.idFeature = IdCounter.incrementCounter();
                        featureTable.insert(feature);
                        logMessage("Added Feature " + feature.code +
                            " Database ID=" + feature.idFeature);
                        ++featureCount;
                    }
                    feature = null;
                }

                //  If the next line is a continuation, note that now.

                continued = (continuation.Length > 0);

                //  If there is a code, start a new feature and capture the 
                //  new feature code.

                if (codeName.Length > 0) {
                    feature = new Feature();
                    feature.machine = machineKey;
                    feature.code = codeName;
                    feature.feature = explanation;
                    currentCode = codeName;
                }

                //  If we have not seen a feature code and are waiting for one,
                //  we just ignore this line - it is an intermediary text line.

            }

            if(testMode) {
                db.CancelTransaction();
            }
            else {
                db.CommitTransaction();
            }

            displayLog();
        }

        private string checkColumn(string column) {
            return csvColumnNames[column] == null ? column + ", " : "";
        }

    }
}
