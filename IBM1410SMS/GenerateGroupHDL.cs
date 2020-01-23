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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;
using System.IO;

namespace IBM1410SMS
{
    class GenerateGroupHDL
    {
        Machine machine;
        List<Diagrampage> diagramPageList;
        List<string> pageNames;
        string directory;
        string outFileName;
        string logFileName;

        Table<Diagrampage> diagramPageTable;
        Table<Sheetedgeinformation> sheetEdgeInformationTable;
        Table<Page> pageTable;

        DBSetup db = DBSetup.Instance;

        StreamWriter outFile;
        StreamWriter logFile;

        public GenerateGroupHDL(
            Machine machine,
            List<Diagrampage> diagramPageList,
            List<string> pageNames, 
            string outFileName, 
            string directory) {

            this.machine = machine;
            this.diagramPageList = diagramPageList;
            this.pageNames = pageNames;
            this.directory = directory;
            this.outFileName = outFileName;

            diagramPageTable = db.getDiagramPageTable();
            sheetEdgeInformationTable = db.getSheetEdgeInformationTable();
            pageTable = db.getPageTable();
        }

        public int generateGroupHDL() {

            GenerateGroupHDLLogicVHDL generator = new GenerateGroupHDLLogicVHDL();

            List<string> inputList = new List<string>();
            List<string> outputList = new List<string>();

            List<string> removedInputSignals = new List<string>();
            List<string> removedOutputSignals = new List<string>();

            List<string> internalSignals = new List<string>();
            List<string> allSignals = new List<string>();
            List<string> bufferSignals = new List<string>();

            int errors = 0;

            logFileName = Path.Combine(directory, outFileName + ".log");

            outFile = new StreamWriter(Path.Combine(directory, outFileName + 
                "." + generator.generateHDLExtension()),false);
            logFile = new StreamWriter(Path.Combine(logFileName), false);

            generator.outFile = outFile;
            generator.logFile = logFile;

            generator.logMessage("Generating HDL for group named " + outFileName +
                " containing pages: ");
            string temp = "";
            foreach(string pageName in pageNames) {
                if(temp.Length + pageName.Length > 60) {
                    generator.logMessage(temp);
                    temp = "";
                }
                temp = temp + (temp.Length == 0 ? "\t" : ", ") + pageName;
            }
            if(temp.Length > 0) {
                generator.logMessage(temp);
            }


            //  Get lists of all of the input and output signals for all of the pages
            //  together.

            generator.logMessage("Building lists of signals on " +
                diagramPageList.Count + " pages...");

            foreach(Diagrampage page in diagramPageList) {
                List<Sheetedgeinformation> signals =
                    sheetEdgeInformationTable.getWhere(
                        "WHERE diagramPage='" + page.idDiagramPage + "'");

                generator.logMessage("Found " + signals.Count +
                    " signals on page " + Helpers.getDiagramPageName(page.idDiagramPage));

                foreach(Sheetedgeinformation signal in signals) {
                    if(signal.leftSide == 1 &&
                        !inputList.Contains(signal.signalName)) {
                        inputList.Add(signal.signalName);
                    }
                    if(signal.rightSide == 1 &&
                        !outputList.Contains(signal.signalName)) {
                        outputList.Add(signal.signalName);
                    }
                    if(!allSignals.Contains(signal.signalName)) {
                        allSignals.Add(signal.signalName);
                    }
                }                
            }

            generator.logMessage("Found " + inputList.Count +
                " unique input signals and " + outputList.Count +
                " unique output signals, (" + allSignals.Count +
                " total unique signals)");

            //  For each input signal used somewhere in the group, check to see
            //  if it originates outside the group.  If so, leave it alone.  If 
            //  not, then move it to the remove list.

            generator.logMessage("Determining sources for all input signals...");

            foreach(string signal in inputList) {
                bool external = false;
                List<Sheetedgeinformation> edgeList =
                    sheetEdgeInformationTable.getWhere(
                        "WHERE signalName='" + signal + "'" +
                        " AND rightSide='1'");
                if(edgeList.Count == 0) {
                    generator.logMessage("ERROR:  Cannot find source of signal named " +
                        signal);
                    ++errors;
                }
                else if(edgeList.Count > 1) {
                    generator.logMessage("ERROR:  More than one source found for signal " +
                        "named " + signal);
                    ++errors;
                }
                foreach(Sheetedgeinformation edge in edgeList) {
                    string pageName = Helpers.getDiagramPageName(edge.diagramPage);
                    if(!pageNames.Contains(pageName)) {
                        external = true;
                        break;
                    }
                }

                generator.logMessage("INFO:  Signal " + signal + " originates " +
                    (external ? "outside" : "inside") + " the group.");

                if(!external) {
                    removedInputSignals.Add(signal);
                }
            }

            //  Now, something similar for output signals, to see if they are used by
            //  any pages outside the group.

            generator.logMessage("Determining destinations for all output signals...");

            foreach (string signal in outputList) {
                bool external = false;

                List<Sheetedgeinformation> edgeList =
                    sheetEdgeInformationTable.getWhere(
                        "WHERE signalName='" + signal + "'" +
                        " AND leftSide='1'");

                if (edgeList.Count == 0) {
                    generator.logMessage("WARNING:  Cannot find any uses of signal named " +
                        signal);
                }

                foreach (Sheetedgeinformation edge in edgeList) {
                    string pageName = Helpers.getDiagramPageName(edge.diagramPage);
                    if (!pageNames.Contains(pageName)) {
                        external = true;
                        break;
                    }
                }

                generator.logMessage("INFO:  Signal " + signal + " is used " +
                    (external ? "outside" : "only inside") + " the group.");

                if (!external) {
                    removedOutputSignals.Add(signal);
                }
            }

            //  Remove any output signals that do not travel outside the group...

            generator.logMessage("Removing " + removedOutputSignals.Count +
                " output signals that do not have destinations outside the group...");

            foreach (string signal in removedOutputSignals) {
                outputList.Remove(signal);
            }

            //  Now, remove the input signals that originate from inside the group...
            //  But, while we are at it, if a signal is also an output signal that
            //  travels outside the group, then remember that 
            //  (older VHDL does not allow reading an output signal)

            //  (This issue cannot crop up with input signals that originate outside
            //  the group, as such a signal would necessarily not originate from any
            //  entity inside the grou).

            generator.logMessage("Removing " + removedInputSignals.Count +
                " input signals that originate inside the group...");

            foreach(string signal in removedInputSignals) {
                inputList.Remove(signal);
                if(outputList.Contains(signal)) {
                    generator.logMessage("Signal " + signal +
                        " is output from the group, but also used as an input " +
                        "inside the group.");
                    bufferSignals.Add(signal);
                }
            }

            //  The signals (wires for Verilog fans) will be any signal in the
            //  orignal full list that is not in what remains of either the input
            //  or the output list.

            generator.logMessage("Generating list of internal signals/wires ...");

            foreach(string signal in allSignals) {
                if(!inputList.Contains(signal) && !outputList.Contains(signal)) {
                    internalSignals.Add(signal);
                }
            }

            generator.logMessage(internalSignals.Count + " internal signals/wires found.");

            generator.logMessage("Generating HDL prefixes...");

            //  Generate the stuff at the beginning of the file

            generator.generateHDLPrefix(outFileName, machine.name);

            //  Generate the entity / module definition

            generator.generateHDLentity(outFileName, inputList, outputList);

            //  Generate the beginning of the actual HDL

            generator.generateHDLArchitecturePrefix(outFileName);

            //  Generate the list of (internal) signals / wires

            generator.generateHDLSignalList(internalSignals, bufferSignals);

            //  Generate the individual page instantiations...

            foreach(Diagrampage page in diagramPageList) {
                List<string> pageInputNames = new List<string>();
                List<string> pageOutputNames = new List<string>();
                List<Sheetedgeinformation> pageInputs =
                    sheetEdgeInformationTable.getWhere(
                        "WHERE diagramPage='" + page.idDiagramPage + "'" +
                        " AND leftSide='1'" +
                        " ORDER BY sheetedgeinformation.row");
                List<Sheetedgeinformation> pageOutputs = 
                    sheetEdgeInformationTable.getWhere(
                        "WHERE diagramPage='" + page.idDiagramPage + "'" +
                        " AND rightSide='1'" +
                        " ORDER BY sheetedgeinformation.row");
                foreach(Sheetedgeinformation edge in pageInputs) {
                    pageInputNames.Add(edge.signalName);
                }
                foreach(Sheetedgeinformation edge in pageOutputs) {
                    pageOutputNames.Add(edge.signalName);
                }
                Page thisPage = pageTable.getByKey(page.page);
                if(thisPage == null || thisPage.idPage == 0) {
                    generator.logMessage(
                        "GenerateGroupHDL: Cannot find page for diagram page with " +
                        "database key of " + page.idDiagramPage + "referring to page " +
                        "with key of " + page.page);
                    continue;
                }

                //  Generate the page entity.  For now, just force needsClock to true.

                generator.logMessage("Generating HDL for page " + thisPage.name +
                    " (" + thisPage.title + ")");

                generator.generatePageEntity(thisPage.name, thisPage.title,
                    pageInputNames, pageOutputNames, bufferSignals, true);


            }

            //  Generate anything needed at the end

            generator.generateHDLArchitectureSuffix();

            //  Close out the file and logs, and return.

            outFile.Close();
            logFile.Close();

            return (errors);
        }

        public string getLogfileName() {
            return logFileName;
        }

    }
}
