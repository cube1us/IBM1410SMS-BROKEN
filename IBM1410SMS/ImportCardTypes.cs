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
    class ImportCardTypes : Importer
    {

        Table<Cardtype> cardTypeTable;
        Table<Cardeco> cardECOTable;
        Table<Eco> ecoTable;
        Table<Cardgate> cardGateTable;
        Table<Logicfamily> logicFamilyTable;
        Table<Ibmlogicfunction> ibmLogicFunctionTable;
        Table<Logicfunction> logicFunctionTable;
        Table<Logiclevels> logicLevelsTable;
        Table<Gatepin> gatePinTable;
        Table<Volume> volumeTable;
        Table<Volumeset> volumeSetTable;
        Hashtable csvColumnNames = new Hashtable();
        DateTime defaultDate = DateTime.Parse("1/1/1960");

        ImportStartupForm.Disposition disposition;


        static string[] pinNames = {
            "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M",
            "N", "P", "Q", "R"
        };

        public ImportCardTypes(string fileName, 
            ImportStartupForm.Disposition disposition, 
            bool testMode) : base(fileName) {

            this.disposition = disposition;
            
            //  List of column names.  NOTE:  THE ACTUAL COLUMNS
            //  MAY NOT BE IN THIS SAME ORDER!!!

            bool header = true;
            List<string> csvColumns;
            int volumeKey = 0;
            int parseOut;

            Cardtype currentCardType = null;
            Cardgate currentGate = null;
            Gatepin currentPin = null;

            List<Cardgate> cardGatesList = new List<Cardgate>();
            List<Gatepin> pinList;
            List < List < Gatepin >> cardGatesPinsList =
                new List<List<Gatepin>>();     //  One entry per card gate.
            List<Cardeco> cardECOList = new List<Cardeco>();

            DBSetup db = DBSetup.Instance;            
            cardTypeTable = db.getCardTypeTable();
            cardECOTable = db.getCardEcoTable();
            ecoTable = db.getEcoTable();
            cardGateTable = db.getCardGateTable();
            logicFamilyTable = db.getLogicFamilyTable();
            ibmLogicFunctionTable = db.getIbmLogicFunctionTable();
            logicFunctionTable = db.getLogicFunctionTable();
            gatePinTable = db.getGatePinTable();
            logicLevelsTable = db.getLogicLevelsTable();
            volumeTable = db.getVolumeTable();
            volumeSetTable = db.getVolumeSetTable();


            while ((csvColumns = getCSVColumns()).Count > 0) {

                //  Snag the header line so we can search for columns by name.
                //  That way, if we reorder columns, insert a column, etc.,
                //  the code will still work.

                if (header) {
                    header = false;
                    int columnIndex = 0;
                    foreach (string s in csvColumns) {
                        csvColumnNames.Add(s, columnIndex);
                        ++columnIndex;
                    }

                    //  Check for required columns...  Column order does not
                    //  matter.  (Only column Volume is optional)

                    string missingColumns = "";
                    missingColumns += checkColumn("Volume Set");
                    missingColumns += checkColumn("Volume");
                    missingColumns += checkColumn("Part Number");
                    missingColumns += checkColumn("E.C.");
                    missingColumns += checkColumn("Card Code");
                    missingColumns += checkColumn("Gate Number");
                    missingColumns += checkColumn("T##");
                    missingColumns += checkColumn("Logic Family");
                    missingColumns += checkColumn("IBM +");
                    missingColumns += checkColumn("IBM -");
                    missingColumns += checkColumn("Logic");
                    missingColumns += checkColumn("Latch");
                    missingColumns += checkColumn("O.C.");
                    missingColumns += checkColumn("Input Lev.");
                    missingColumns += checkColumn("Output Lev.");
                    missingColumns += checkColumn("Defining Pin");
                    missingColumns += checkColumn("Name");                
                    foreach (string pinName in pinNames) {
                        missingColumns += checkColumn(pinName);
                    }
                    for (int g=1; g <= 12; ++g) {
                        missingColumns += checkColumn("G" + g);
                    }

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

                //  OK, so it isn't a header.  See if there is a new volume name.

                string cardType = csvColumns[(int)csvColumnNames["Card Code"]];
                string volumeSetName = csvColumns[(int)csvColumnNames["Volume Set"]];
                string volumeName = csvColumns[(int)csvColumnNames["Volume"]];
                string partNumber = csvColumns[(int)csvColumnNames["Part Number"]];
                string ecoName = csvColumns[(int)csvColumnNames["E.C."]];
                string cardName = csvColumns[(int)csvColumnNames["Name"]];
                string logicFamilyName = csvColumns[(int)csvColumnNames["Logic Family"]];
            
                //  Process Volume Set and Volume Set Name.  If none have
                //  been specified before this, use some defaults.

                if (volumeName.Length > 0 || volumeSetName.Length > 0) {
                    volumeKey = 
                        getVolumeKeyByName(volumeName,volumeSetName,"");
                }

                if (volumeKey == 0) {
                    logMessage("Warning:  No Volume Name specified. " + 
                        "Using Defaults ");
                    volumeKey = getVolumeKeyByName("", "", "");
                }


                //  See if we have come across a new card type.

                //  The following only gets done on the FIRST line where we encounter
                //  a different card type!

                if (currentCardType == null || cardType != currentCardType.type) {

                    if (currentCardType != null) {
                        doGatesAndPins(currentCardType, cardECOList, cardGatesList,
                             cardGatesPinsList);
                    }

                    logMessage("Processing Card Type " + cardType);

                    currentCardType = new Cardtype();
                    cardECOList = new List<Cardeco>();
                    currentCardType.volume = volumeKey;
                    currentCardType.type = cardType;
                    currentCardType.part = zeroPadPartNumber(partNumber);
                    currentCardType.modelType = "SMS";
                    currentCardType.nameType = "CARD ASM TSTR";
                    currentCardType.scale = "NONE";
                    currentCardType.name = cardName;
                    currentCardType.approvedDate = defaultDate;
                    currentCardType.designDate = defaultDate;
                    currentCardType.detailDate = defaultDate;
                    currentCardType.designCheckDate = defaultDate;
                    currentCardType.approvalDate = defaultDate;
                    currentCardType.drawDate = defaultDate;
                    currentCardType.drawingCheckDate = defaultDate;

                    //  Look up the logic family.  If we find a match,
                    //  use it, otherwise add a new one.

                    List<Logicfamily> logicFamilyList = logicFamilyTable.getWhere(
                        "WHERE logicfamily.name='" + logicFamilyName + "'");
                    if(logicFamilyList.Count > 0) {
                        currentCardType.logicFamily =
                            logicFamilyList[0].idLogicFamily;
                    }
                    else {
                        Logicfamily lf = new Logicfamily();
                        lf.name = logicFamilyName;
                        lf.idLogicFamily = IdCounter.incrementCounter();
                        logicFamilyTable.insert(lf);
                        logMessage("Added Logic Family " + lf.name + 
                            ", Database ID" + lf.idLogicFamily);
                        currentCardType.logicFamily = lf.idLogicFamily;
                    }

                    //  Similar story with the ECO.  This requires a two
                    //  level look up.  First, the ECO itself, and then
                    //  the cardECO table.

                    int ecoKey = 0;
                    if (ecoName.Length > 0) {

                        //  First, check the ECO itself, and add if necessary.

                        List<Eco> ecoList = ecoTable.getWhere(
                            "WHERE eco='" + ecoName + "'");
                        if (ecoList.Count > 0) {
                            ecoKey = ecoList[0].idECO;
                        }
                        else {
                            ecoKey = IdCounter.incrementCounter();
                            Eco eco = new Eco();
                            eco.idECO = ecoKey;
                            eco.eco = ecoName;
                            eco.machine = 0;    //  No machine for now is OK
                            ecoTable.insert(eco);
                            logMessage("Added ECO " + ecoName +
                                ", Database ID=" + eco.idECO);
                        }

                        //  Remember the ECO for possible insertion later into the
                        //  cardECO table, as well.

                        Cardeco cardECO = new Cardeco();
                        cardECO.idCardTypeECO = 0;              //  Fill in keys later.
                        cardECO.eco = ecoKey;
                        cardECO.cardType = 0;                   //  Fill in keys later
                        cardECOList.Add(cardECO);
                    }

                    //  Reset the lists of remembered stuff...

                    cardGatesList = new List<Cardgate>();
                    cardGatesPinsList = new List<List<Gatepin>>();
                }


                //  Process the gate portion

                //  Note that we can't resolve the defining pin until we process the
                //  pins, and we can't resolve the latchgate until we process all of
                //  the gates on this card.

                string gateNumber = csvColumns[(int)csvColumnNames["Gate Number"]];
                string transistorNumber = csvColumns[(int)csvColumnNames["T##"]];
                string ibmPositiveLogic = csvColumns[(int)csvColumnNames["IBM +"]];
                string ibmNegativeLogic = csvColumns[(int)csvColumnNames["IBM -"]];
                string logicFunction = csvColumns[(int)csvColumnNames["Logic"]];
                string latchGate = csvColumns[(int)csvColumnNames["Latch"]];
                string openCollector = csvColumns[(int)csvColumnNames["O.C."]];
                string inputLevel = csvColumns[(int)csvColumnNames["Input Lev."]];
                string outputLevel = csvColumns[(int)csvColumnNames["Output Lev."]];
                string definingPin = csvColumns[(int)csvColumnNames["Defining Pin"]];

                if(outputLevel.Length == 0) {
                    outputLevel = inputLevel;
                }

                currentGate = new Cardgate();
                currentGate.idcardGate = IdCounter.incrementCounter();
                currentGate.cardType = 0;               //  Fill in key later...

                //  logicFunction might contain a component value...

                int multiplier = 0;

                //  Resistors (L)  

                int index = logicFunction.IndexOf(" ohm", 
                    StringComparison.CurrentCultureIgnoreCase);
                if(index >= 0) {
                        
                    //  Remove the found string (ohm, case independent)

                    string s = logicFunction.Remove(index, 4);
                    multiplier = 1;

                    //  Handle k/K and m/M multipiers

                    index = s.IndexOf("k", StringComparison.CurrentCultureIgnoreCase);
                    if(index >= 0) {
                        multiplier = 1000;
                        s = s.Remove(index, 1);
                    }
                    index = s.IndexOf("m", StringComparison.CurrentCultureIgnoreCase);
                    if (index >= 0) {
                        multiplier = 1000000;
                        s = s.Remove(index, 1);
                    }
                    
                    //  Strip any commas.
                    s = s.Replace(",", "");
                    Single v = 0;
                    if(!Single.TryParse(s, out v)) {
                        logMessage("Invalid Resistance value " + logicFunction +
                            " for Gate " + gateNumber +
                            " for Card Code " + cardType + ", Setting to Value to 0");
                        v = 0;
                    }

                    currentGate.componentValue = (int)(v * multiplier);

                    //  Reset logic function to an empty string, so the key gets 0 in
                    //  gate table.

                    logicFunction = "";
                }

                //  Capacitors (CAP)

                index = logicFunction.IndexOf(" uf", StringComparison.CurrentCultureIgnoreCase);
                if(index >= 0) {
                    string s = logicFunction.Remove(index, 3);
                    multiplier = 1000000;            //  Convert from uf to pf
                    float v;
                    if(!float.TryParse(s,out v)) {
                        logMessage("Invalid Capacitance value " + logicFunction +
                            " for Gate " + gateNumber +
                            " for Card Code " + cardType + ", Setting to Value to 0");
                        v = 0;
                    }
                    currentGate.componentValue = (int) v * multiplier;
                    logicFunction = "";
                }

                //  Delays (DLY)

                index = logicFunction.IndexOf(" ns", StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0) {
                    string s = logicFunction.Remove(index, 3);
                    int v;
                    if (!int.TryParse(s, out v)) {
                        logMessage("Invalid Delay value " + logicFunction +
                            " for Gate " + gateNumber +
                            " for Card Code " + cardType + ", Setting to Value to 0");
                        v = 0;
                    }
                    currentGate.componentValue = v;
                    logicFunction = "";
                }

                //  Oscillator
                index = logicFunction.IndexOf(" mc", StringComparison.CurrentCultureIgnoreCase);
                if (index >= 0) {
                    string s = logicFunction.Remove(index, 3);
                    float v;
                    if (!float.TryParse(s, out v)) {
                        logMessage("Invalid Oscillator frequency " + logicFunction +
                            " for Gate " + gateNumber +
                            " for Card Code " + cardType + ", Setting to Value to 0");
                        v = 0;
                    }
                    currentGate.componentValue = (int) v * 1000000;
                    logicFunction = "";
                }

                if (!int.TryParse(gateNumber, out parseOut)) {
                    logMessage("Invalid Gate Number " + gateNumber + 
                        " for Card Code " + cardType + ", Setting to Gate to 99");
                    parseOut = 99;
                }
                currentGate.number = parseOut;

                //  Hold off on defining pin until we do the pins...

                //  Strip the "T" from transistor number  (Can also
                //  be R##, C## or D##(

                parseOut = 0;
                transistorNumber = transistorNumber.Replace("T", "");
                transistorNumber = transistorNumber.Replace("R", "");
                transistorNumber = transistorNumber.Replace("C", "");
                transistorNumber = transistorNumber.Replace("D", "");
                if (transistorNumber.Length > 0 && 
                    !int.TryParse(transistorNumber, out parseOut)) {
                    logMessage("Invalid Transistor Number " + transistorNumber +
                        " for gate " + gateNumber + " of card code " + cardType +
                        " -- ignored.");
                    Console.WriteLine("Invalid Transistor Number: /" +
                        transistorNumber + "/");
                    parseOut = 0;
                }
                currentGate.transistorNumber = parseOut;

                currentGate.positiveLogicFunction = 
                    ibmLogicFunctionLookup(ibmPositiveLogic);
                currentGate.negativeLogicFunction = 
                    ibmLogicFunctionLookup(ibmNegativeLogic);
                currentGate.logicFunction =
                    logicFunctionLookup(logicFunction);
                currentGate.HDLname = "";


                //  Fill the the latch gate with the integer gate number, for now 
                //  (0 if blank or invalid).  Then when we have ALL of the gates,
                //  will fill in the number of the appropriate key value.

                parseOut = 0;
                latchGate = latchGate.Replace("G", "");
                if (!int.TryParse(latchGate, out parseOut)) {
                    if(latchGate.Length > 0) {
                        logMessage("Invalid Latch Gate " + latchGate +
                            " for gate " + gateNumber + " of card code " + cardType +
                            " -- ignored.");
                    }
                    parseOut = 0;
                }

                //  Here we remember just the gate number.  It will be smaller than
                //  any database key, and thus identifiable.  (I could also have
                //  made it negative, I suppose).

                currentGate.latchGate = parseOut;           // To be filled in later.

                currentGate.openCollector = openCollector.Length > 0 ? 1 : 0;
                currentGate.inputLevel = logicLevelLookup(inputLevel);
                currentGate.outputLevel = logicLevelLookup(outputLevel);

                //  Add it to the list of gates.

                cardGatesList.Add(currentGate);
                pinList = new List<Gatepin>();
                cardGatesPinsList.Add(pinList);

                //  Now process the pins, but issue and error message and skip if
                //  the gate number is 99.

                if (currentGate.number == 99) {
                    logMessage("Skipping Pin processing due to invalid gate number.");
                    continue;
                }

                foreach(string p in pinNames) {

                    currentPin = null;
                    string pinName = csvColumns[(int)csvColumnNames[p]];
                    int v;

                    //  If there is a specification, start with some default values.

                    if(pinName.Length > 0) {
                        currentPin = new Gatepin();
                        currentPin.pin = p;
                        currentPin.input = 0;
                        currentPin.output = 0;
                        currentPin.voltageTenths = 0;
                        currentPin.inputGate = 0;
                        currentPin.outputGate = 0;
                        currentPin.dotInput = 0;
                        currentPin.dotOutput = 0;
                    }

                    if (pinName == p) {
                        //  If they are equal, this is an input
                        currentPin.input = 1;
                    }
                    else if(pinName == "O") {
                        //  O is for output
                        currentPin.output = 1;
                    }
                    else if(int.TryParse(pinName, out v)) {
                        //  A number is voltage, in tenths.
                        currentPin.voltageTenths = v * 10;
                    }

                    //  Initialize the mapPin column, which was added after the
                    //  importers were written.

                    currentPin.mapPin = "";

                    //  Add the pin to the list of pins we know about.
                    //  Also, if this is the defining pin for the gate,
                    //  remember its index in the list of pins (+1 to avoid 0)

                    if(currentPin != null) {
                        pinList.Add(currentPin);
                        if(currentPin.pin == definingPin) {
                            currentGate.definingPin = Array.IndexOf(pinNames, p) + 1;
                        }
                    }
                }

                //  Next, process the intra card connections.  These 

                for(int intraConnection = 1; intraConnection <= 12; ++intraConnection) {

                    currentPin = null;
                    string pinName = "G" + intraConnection.ToString();
                    string pinConn = csvColumns[(int)csvColumnNames[pinName]];

                    if(pinConn.Length > 0) {
                        currentPin = new Gatepin();
                        currentPin.pin = "";     //  No name for intra gate pins.

                        //  Preset some fields, to avoid redundant code

                        currentPin.input = 0;
                        currentPin.output = 0;
                        currentPin.voltageTenths = 0;
                        currentPin.inputGate = 0;
                        currentPin.outputGate = 0;
                        currentPin.dotInput = 0;
                        currentPin.dotOutput = 0;

                        //  Simple intra card input or Input/Output

                        if(pinConn == pinName|| pinConn.Contains("I")) {
                            currentPin.input = 1;
                            currentPin.inputGate = intraConnection;     // Replaced later by key
                        }
                        
                        //  Simple output or Input/Output

                        if(pinConn.Contains("O")) {
                            currentPin.output = 1;
                            currentPin.outputGate = intraConnection;    // Replaced later by key
                        }
                        
                        //  Input from dotted outputs.

                        if(pinConn == "DIT") {
                            currentPin.input = 1;
                            currentPin.inputGate = intraConnection;     // Replace later with key
                            currentPin.dotInput = 1;
                        }

                        //  And Dotted output.

                        if(pinConn == "DOT") {
                            currentPin.output = 1;
                            currentPin.outputGate = intraConnection;    //  Replace later with key
                            currentPin.dotOutput = 1;
                        }

                        //  Add the pin to the pin list for this gate.
                        currentPin.idGatePin = IdCounter.incrementCounter();
                        pinList.Add(currentPin);
                    }
                }

            }   //End while processing input...

            //  Write out the last entry before we hit EOF, if any.

            if (currentCardType != null) {
                doGatesAndPins(currentCardType, cardECOList, cardGatesList,
                     cardGatesPinsList);
            }

            //  End the transaction...

            if (testMode) {
                db.CancelTransaction();
            }
            else {
                db.CommitTransaction();
            }

            //  Finally, display the log for the user....

            displayLog();
        }

        //  Table lookup / add methods.

        private int ibmLogicFunctionLookup(string label) {

            if(label.Length == 0) {
                return 0;
            }

            List<Ibmlogicfunction> functionList = ibmLogicFunctionTable.getWhere(
                "WHERE label='" + label + "'");
            if(functionList.Count > 0) {
                return functionList[0].idIBMLogicFunction;
            }

            //  Not found - add a new one.

            Ibmlogicfunction ilf = new Ibmlogicfunction();
            ilf.idIBMLogicFunction = IdCounter.incrementCounter();
            ilf.label = label;
            ibmLogicFunctionTable.insert(ilf);
            logMessage("Added IBM Logic Function " + ilf.label +
                ", Database ID " + ilf.idIBMLogicFunction);
            return ilf.idIBMLogicFunction;
        }

        private int logicFunctionLookup(string name) {

            if(name.Length == 0) {
                return 0;
            }

            List<Logicfunction> functionList = logicFunctionTable.getWhere(
                "WHERE logicfunction.name='" + name + "'");
            if (functionList.Count > 0) {
                return functionList[0].idLogicFunction;
            }

            //  Not found - add a new one.

            Logicfunction lf = new Logicfunction();
            lf.idLogicFunction = IdCounter.incrementCounter();
            lf.name = name;
            logicFunctionTable.insert(lf);
            logMessage("Added Logic Function " + lf.name +
                ", Database ID=" + lf.idLogicFunction);
            return lf.idLogicFunction;

        }

        private int logicLevelLookup(string level) {

            if(level.Length == 0) {
                return 0;
            }

            List<Logiclevels> levelsList = logicLevelsTable.getWhere(
                "WHERE logicLevel='" + level + "'");
            if (levelsList.Count > 0) {
                return levelsList[0].idLogicLevels;
            }

            //  Not found - add a new one.

            Logiclevels ll = new Logiclevels();
            ll.idLogicLevels = IdCounter.incrementCounter();
            ll.logicLevel = level;
            //  We don't know the logic voltage levels, so just fill in something...
            ll.logicZeroTenths = 0;
            ll.logicOneTenths = 50;
            logicLevelsTable.insert(ll);
            logMessage("Added Logic Level (0.0-5.0V) " + ll.logicLevel +
                ", Database ID=" + ll.idLogicLevels);
            return ll.idLogicLevels;
        }


        //  Code to actually do the database updates for all but the simple
        //  table types - which check for pre-existance when they run.

        private void doGatesAndPins(Cardtype cardType, List<Cardeco> cardECOList,
            List<Cardgate> cardGateList, List<List<Gatepin>> gatePinsList) {

            int oldIdCardType = 0;                 //  Remember for overwrite...

            //  See if this card type already exists in the database.  If so,
            //  handle according to the disposition specified by the user.

            List<Cardtype> cardTypeList = cardTypeTable.getWhere(
                "WHERE cardtype.type='" + cardType.type + "'");

            if (cardTypeList.Count > 0) {

                Cardtype oldCard = cardTypeList[0];

                if (disposition == ImportStartupForm.Disposition.SKIP) {
                    logMessage("Skipping import of card type " + cardType.type +
                        " due to disposition SKIP");
                    return;
                }

                else if (disposition == ImportStartupForm.Disposition.MERGE) {

                    if (oldCard.part.Length == 0) {
                        oldCard.part = cardType.part;
                    }
                    if (oldCard.nameType.Length == 0) {
                        oldCard.nameType = cardType.nameType;
                    }
                    if (oldCard.name.Length == 0) {
                        oldCard.name = cardType.name;
                    }
                    if (oldCard.logicFamily == 0) {
                        oldCard.logicFamily = cardType.logicFamily;
                    }
                    cardTypeTable.update(oldCard);

                    logMessage("Merged With Existing Card Type " + cardType.type +
                        " (Database ID " + oldCard.idCardType + ")" +
                        " due to disposition MERGE");

                    //  Replace the data with the updated data for use in the
                    //  rest of this method.

                    cardType = oldCard;
                }
                else {  //  Disposition OVERWRITE

                    //  Remember the old card database ID, so we can re-use it,
                    //  thus preserving any references!

                    oldIdCardType = oldCard.idCardType;
                    
                    //  Then go ahead and delete the old one.

                    deleteCardType(oldCard.type, oldCard.idCardType);

                    //  Delete the existing list, so the next test for
                    //  no entries in the list succeeds!

                    cardTypeList = new List<Cardtype>();
                }

            }

            //  If a new card type or overwrite was spcified, add it now.

            if(cardTypeList.Count == 0) { 
                cardType.idCardType = oldIdCardType > 0 ? oldIdCardType :
                        IdCounter.incrementCounter();
                cardTypeTable.insert(cardType);
                logMessage(
                    (oldIdCardType > 0 ? "Replaced" : "Added") +
                    " Card Type " + cardType.type +
                    ", Database ID=" + cardType.idCardType);
            }

            //  Now insert any new card type ECOs

            foreach (Cardeco cardECO in cardECOList) {
                List<Cardeco> oldECOList = cardECOTable.getWhere(
                    "WHERE eco='" + cardECO.eco + "'" +
                    " AND cardType='" + cardType.idCardType + "'");
                //  If the entry already exists, we will not have anything to
                //  add to it, so just skip it.  Otherwise, fill in the keys
                //  and insert the new row.  The key value indentifying the
                //  ECO has already been filled in.
                if (oldECOList.Count == 0) {
                    cardECO.idCardTypeECO = IdCounter.incrementCounter();
                    cardECO.cardType = cardType.idCardType;
                    cardECOTable.insert(cardECO);
                    logMessage("Added Card ECO (eco database id " +
                        cardECO.eco + ") for Card Type " + cardType.type);
                }
            }

            //  Finally, the gates and pins.  
            //  This is a TWO PASS algorithm, because we first have to get
            //  the keys of any existing entries...

            //  In the first pass, we create keys for gates and pins,
            //  We can't insert new gates yet, because they may have 
            //  a "forward" reference in the latchGate field.
            //  Similarly, we can't insert the pins in this
            //  pass, because they may have references to later gates for
            //  which we don't yet have keys.

            //  First pass...

            int gateIndex = 0;
            foreach (Cardgate gate in cardGateList) {
                //  If we find a match, leave it well enough alone.
                List<Cardgate> oldGatesList = cardGateTable.getWhere(
                    "WHERE cardType='" + cardType.idCardType + "'" +
                    " AND cardgate.number='" + gate.number + "'");
                if (oldGatesList.Count > 0) {
                    //  Grab the key for use with pins, below.
                    gate.idcardGate = oldGatesList[0].idcardGate;
                    gate.modified = false;
                    logMessage("Gate " + gate.number + " for Card Type " +
                        cardType.type + " (Database ID " + gate.idcardGate +
                        ") exists - NOT updated.");
                }
                else {
                    gate.idcardGate = IdCounter.incrementCounter();
                    gate.cardType = cardType.idCardType;
                    gate.modified = true;
                }

                //  Now get the pin list for this gate, using the index
                //  For the list of list of pins

                List<Gatepin> pins = gatePinsList[gateIndex];

                //  Again, if any of these exist, leave well enough alone.

                foreach (Gatepin pin in pins) {
                    List<Gatepin> oldPinsList = gatePinTable.getWhere(
                        "WHERE cardGate='" + gate.idcardGate + "'" +
                        " AND pin='" + pin.pin + "'");
                    if (oldPinsList.Count > 0) {
                        //  Remember the key here, too, in case another pin
                        //  is being added that depends upon this one.
                        pin.idGatePin = oldPinsList[0].idGatePin;
                        logMessage("Pin " + pin.pin + " (Database ID " + pin.idGatePin +
                            ") of Gate " + gate.number +
                            " of Card Type " + cardType.type +
                            " exists - NOT updated.");
                        pin.modified = false;
                    }
                    else {
                        //  We cannot fill in the gate key yet, because it might
                        //  be new, and we might not have its key.
                        pin.idGatePin = IdCounter.incrementCounter();
                        pin.modified = true;
                    }

                }

                //  And bump to the next entry in the list of list of pins
                //  to be ready for the next gate.

                ++gateIndex;
            }

            //  Second pass...

            //  OK.  So no all of the entries should have their keys, and we
            //  just need to do inserts (filling in latchGate and pin data 
            //  where needed)

            gateIndex = 0;
            foreach (Cardgate gate in cardGateList) {
                List<Gatepin> pins = gatePinsList[gateIndex];
                if (gate.modified) {
                    if (gate.latchGate > 0 &&
                        gate.latchGate <= cardGateList.Count) {
                        gate.latchGate = cardGateList[gate.latchGate-1].idcardGate;
                    }

                    //  NOW we can fill in the defining pin, if necessary.

                    if (gate.definingPin > 0 && gate.definingPin <= pinNames.Length) {
                        Gatepin definingPin = pins.Find(
                             x => x.pin == pinNames[gate.definingPin - 1]);
                        if (definingPin != null) {
                            gate.definingPin = definingPin.idGatePin;
                        }
                        else {
                            logMessage("Card Type " + cardType.type +
                                " Gate " + gate.number +
                                " Defining pin specifies a pin not " +
                                "actually defined.  Set to -1");
                            gate.definingPin = -1;
                        }
                    }
                    cardGateTable.insert(gate);
                    logMessage("Added Gate " + gate.number +
                        " Database ID " + gate.idcardGate +
                        " for Card Type " + cardType.type);
                }

                //  Now get the pin list for this gate, using the index
                //  For the list of list of pins
            
                //  For pins, there are more fields to fill in...

                foreach (Gatepin pin in pins) {

                    if(pin.modified) {
                        pin.cardGate = gate.idcardGate;
                        if(pin.inputGate > 0 && pin.inputGate <= cardGateList.Count) {
                            pin.inputGate = cardGateList[pin.inputGate-1].idcardGate;
                        }
                        if(pin.outputGate > 0 && pin.outputGate <= cardGateList.Count) {
                            pin.outputGate = cardGateList[pin.outputGate-1].idcardGate;
                        }
                        gatePinTable.insert(pin);
                        logMessage("Added PIN " + pin.pin +
                            " of Gate " + gate.number +
                            " of Card Type " + cardType.type +
                            " Database ID=" + pin.idGatePin);
                    }
                }

                ++gateIndex;
            }
        }

        //  Utility routine to check if a required column name is present, and
        //  return the column name follwed by a space if it is NOT present

        private string checkColumn(string column) {
            return csvColumnNames[column] == null ? column + ", " : "";
        }

        //  Method to delete an existing card type and ALL of its children.

        private void deleteCardType(string oldCardType, int oldCardTypeKey ) {

            int gateCount = 0;
            int pinCount = 0;

            List<Cardgate> cardGateList = cardGateTable.getWhere(
                "WHERE cardType='" + oldCardTypeKey + "'");
            foreach(Cardgate gate in cardGateList) {
                List<Gatepin> pinList = gatePinTable.getWhere(
                    "WHERE cardGate='" + gate.idcardGate + "'");
                foreach(Gatepin pin in pinList) {
                    gatePinTable.deleteByKey(pin.idGatePin);
                    ++pinCount;
                }
                cardGateTable.deleteByKey(gate.idcardGate);
                ++gateCount;
            }
            cardTypeTable.deleteByKey(oldCardTypeKey);

            logMessage("Deleted Existing Card Type " + oldCardType +
                " (Database ID " + oldCardTypeKey + ")" +
                ", including " + gateCount + " gates and " + pinCount +
                " pins due to disposition OVERWRITE");
        }

    }
}   
