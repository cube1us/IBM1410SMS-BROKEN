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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
    class GenerateHDLLogicVHDL : GenerateHDLLogic
    {
        public GenerateHDLLogicVHDL(
            Page page) : 
            base (page) {
        }

        public override string generateHDLExtension() {
            return ("vhdl");
        }

        public override void generateHDLNand(List<string> inputs, string output) {
            outFile.WriteLine("\t" + output + " <= NOT(" +
                string.Join(" AND ", inputs) + " );");
        }

        public override void generateHDLNor(List<string> inputs, string output) {
            outFile.WriteLine("\t" + output + " <= NOT(" +
                string.Join(" OR ", inputs) + " );");
        }

        public override void generateHDLOr(List<string> inputs, string output) {
            outFile.WriteLine("\t" + output + " <= " +
                string.Join(" OR ", inputs) + ";");
        }

        public override void generateHDLNot(List<string> inputs, string output) {
            outFile.WriteLine("\t" + output + " <= NOT " + inputs[0] + ";");
        }

        public override void generateHDLEqual(List<string> inputs, string output) {
            outFile.WriteLine("\t" + output + " <= " + inputs[0] + ";");
        }

        public override void generateHDLSignalAssignment(string blockOutput, string outputSignal) {
            outFile.WriteLine("\t" + outputSignal + " <= " + blockOutput + ";");
        }

        //  Handle generation of VHDL for Special blocks (Logic functions Special and
        //  Trigger, and perhaps more).

        //  NOTE:  Quite a lot of this code would be common with, say Verilog, and
        //  could prehaps be refactored/hoisted into GenerateHDLLogic.

        public override int generateHDLSpecial(LogicBlock block, List<string> inputs,
            List<string> outputs) {

            int temp_errors = 0;
            int inputIndex = 0;
            string mapPin;
            List<string> mapPins = new List<string>();      //  All of the map pins

            if(block.HDLname == null || block.HDLname.Length == 0) {
                logMessage("ERROR: generateHDLSpecial (VHDL): HDLname for block " +
                    "using Special logic function at " +
                    block.getCoordinate() + " is null or zero length.");
                ++temp_errors;
                return (temp_errors);
            }

            //  Build a list of all of the pins in the port map

            foreach(Gatepin pin in block.pins) {
                if(pin.mapPin != null && pin.mapPin.Length > 0 &&
                    !mapPins.Contains(pin.mapPin)) {
                    mapPins.Add(pin.mapPin);
                }
            }

            outFile.WriteLine();
            outFile.WriteLine("\t" + block.HDLname + "_" + block.getCoordinate() + ": " +
                "entity " + block.HDLname + " port map (");
            
            //  If this is a trigger, give it a clock

            if(block.logicFunction == "Trigger") {
                outFile.WriteLine("\t\tFPGA_CLK => FPGA_CLK,");
            }

            //  First, map the inputs...

            foreach(Connection connection in block.inputConnections) {

                if (connection.toPin == null || connection.toPin.Length == 0) {
                    logMessage("ERROR: Special gate for block at " +
                        block.getCoordinate() + "input from " +
                        inputs[inputIndex] + " has an unnamed input pin -- skipped.");
                    ++temp_errors;
                }
                else {

                    mapPin = block.pins.Find(x => x.pin == connection.toPin).mapPin;

                    if (mapPin == null || mapPin.Length == 0) {
                        logMessage("ERROR:  Special gate for block at " +
                            block.getCoordinate() + " no mapPin found for input pin "
                            + connection.toPin);
                        ++temp_errors;
                    }
                    else {
                        outFile.WriteLine("\t\t" + mapPin + " => " + 
                            inputs[inputIndex] + "," + "\t" +
                            "-- Pin " + connection.toPin);
                        if(mapPins.Contains(mapPin)) {
                            mapPins.Remove(mapPin);
                        }
                    }
                }

                ++inputIndex;
            }

            //  Then map the outputs.  There will always be at least ONE output.
            //  But, unlike the inputs, we won't know if there will be more
            //  (either outuputs, or open pins), so the comma gets written out
            //  when we have one (other than the first).

            //  Also, unlike the inputs, there can be more than one output from
            //  the same pin.

            bool firstOutputMap = true;
            int outputIndex = 0;
            foreach (Connection connection in block.outputConnections) {

                if (connection.fromPin == null || connection.fromPin.Length == 0) {
                    logMessage("ERROR: Special gate for block at " +
                        block.getCoordinate() + " output from " +
                        outputs[outputIndex] + " has an unnamed output pin -- skipped.");
                    ++temp_errors;
                }
                else {

                    Gatepin mapGatePin = block.pins.Find(x => x.pin == connection.fromPin);
                    if (mapGatePin == null || mapGatePin.idGatePin == 0) {
                        logMessage("ERROR: Special gate for block at " +
                            block.getCoordinate() + " output pin " + connection.fromPin +
                            " not found in gate definition.");
                        mapPin = "";
                    }
                    else {
                        mapPin = mapGatePin.mapPin;
                    }

                    if (mapPin == null || mapPin.Length == 0) {
                        logMessage("ERROR: Special gate for block at " +
                            block.getCoordinate() + " no mapPin found for output pin "
                            + connection.fromPin);
                        ++temp_errors;
                    }
                    else {

                        //  This output pin may have already been mapped, in which case
                        //  we ignore it.

                        if (mapPins.Contains(mapPin)) {
                            if (!firstOutputMap) {
                                outFile.WriteLine(",");
                            }
                            firstOutputMap = false;
                            outFile.Write("\t\t" + mapPin + " => " +
                                outputs[outputIndex]);
                            if (mapPins.Contains(mapPin)) {
                                mapPins.Remove(mapPin);
                            }
                        }
                    }
                }

                ++outputIndex;
            }

            //  If we have any unmapped port map pins, declare them as open.

            foreach(string pin in mapPins) {
                if(!firstOutputMap) {
                    outFile.WriteLine(",");
                }
                firstOutputMap = false;
                outFile.Write("\t\t" + pin + " => OPEN");
            }

            outFile.WriteLine(" );");

            //mapPin = block.pins.Find(x => x.pin == block.outputConnections[0].fromPin).mapPin;

            //if (mapPin == null || mapPin.Length == 0) {
            //    logMessage("ERROR:  Special gate for block at " +
            //        block.getCoordinate() + " no mapPin found for output pin "
            //        + block.outputConnections[0].fromPin);
            //    ++temp_errors;
            //}
            //else {
            //    outFile.WriteLine("\t\t" + mapPin + " => " + output + " );");
            //}

            outFile.WriteLine();
            return (temp_errors);
        }

        public override int generateHDLDFlipFlop(LogicBlock block) {

            int temp_errors = 0;
            string outputPinName =
                generateOutputPinName(block, block.outputConnections[0], out temp_errors);

            outFile.WriteLine("\t" + LatchPrefix + "_" + block.getCoordinate() + ": " +
                "entity DFlipFlop port map (");
            outFile.WriteLine("\t\tC => " + SystemClockName + ",");
            outFile.WriteLine("\t\tD => " + outputPinName + "_" + LatchPrefix + ",");
            outFile.WriteLine("\t\tQ => " + outputPinName + ",");
            outFile.WriteLine("\t\tQBar => OPEN );");
            outFile.WriteLine();
            return (temp_errors);
        }

        public override void generateHDLArchitectureSuffix() {
            outFile.WriteLine();
            outFile.WriteLine("end;");
        }


        //  Class to generate HDL prefix information - standard intro comments,
        //  library declarations, etc.

        public override void generateHDLPrefix() {
            outFile.WriteLine("-- VHDL for IBM SMS ALD page " + page.name);
            outFile.WriteLine("-- Title: " + page.title);
            outFile.WriteLine("-- IBM Machine Name " + Helpers.getMachineFromPage(page));
            outFile.WriteLine("-- Generated by GenerateHDL");
            outFile.WriteLine();
            outFile.WriteLine("library IEEE;");
            outFile.WriteLine("use IEEE.STD_LOGIC_1164.ALL;");
            outFile.WriteLine();
            outFile.WriteLine("library xil_defaultlib;");
            outFile.WriteLine("use xil_defaultlib.all;");
            outFile.WriteLine();

            //if (needsDFlipFlop) {
            //    outFile.WriteLine("use work.DflipFlop.all;");
            //    outFile.WriteLine();
            //}
        }

        public override void generateHDLEntity(
            bool needsClock,
            List<Sheetedgeinformation> sheetInputsList,
            List<Sheetedgeinformation> sheetOutputsList) {

            outFile.WriteLine("entity " + getHDLEntityName() + " is");
            outFile.WriteLine("\tPort (");
            if (needsClock) {
                outFile.WriteLine("\t\t" +
                    SystemClockName + ":\t\t in STD_LOGIC;");
            }
            foreach (Sheetedgeinformation signal in sheetInputsList) {
                outFile.WriteLine("\t\t" + generateSignalName(signal.signalName) +
                    ":\t in STD_LOGIC;");
            }

            bool firstOutput = true;
            foreach (Sheetedgeinformation signal in sheetOutputsList) {
                if (!firstOutput) {
                    outFile.WriteLine(";");
                }
                firstOutput = false;
                //  Write out signal WITHOUT the ";" so that the LAST one doesn't have one
                outFile.Write("\t\t" + generateSignalName(signal.signalName) +
                    ":\t out STD_LOGIC");
            }

            //  Write out the trailing );
            outFile.WriteLine(");");

            outFile.WriteLine("end " + getHDLEntityName() + ";");
            outFile.WriteLine();
        }

        public override int generateHDLArchitecturePrefix() {

            int errors = 0;
            int temp_errors = 0;

            outFile.WriteLine("architecture Behavioral of " + getHDLEntityName() +
                " is ");
            outFile.WriteLine();
            foreach (LogicBlock block in logicBlocks) {

                if (block.ignore) {
                    continue;
                }

                List<string> processedPins = new List<string>();
                List<string> processedNoPins = new List<string>();

                foreach (Connection connection in block.outputConnections) {

                    //  A DOT function can only have one output "pin"

                    if (block.isDotFunction()) {
                        outFile.WriteLine("\tsignal " +
                            generateOutputPinName(block, connection, out temp_errors) +
                            ": STD_LOGIC;");
                        errors += temp_errors;
                        break;
                    }


                    //  Only gates from here on...

                    //  First handle un-pinned, intra card connections.  
                    //  They are remembered by the diagram block key.

                    if (connection.fromPin == "--") {
                        if(processedNoPins.Contains(connection.fromDiagramBlock.ToString())) {
                            continue;
                        }
                        processedNoPins.Add(connection.fromDiagramBlock.ToString());
                        outFile.WriteLine("\tsignal " +
                            generateOutputPinName(block, connection, out temp_errors) +
                            ": STD_LOGIC;");
                        errors += temp_errors;
                        //  Even these can be latched...
                        if (block.latchOutputs) {
                            outFile.WriteLine("\tsignal " +
                                generateOutputPinName(block, connection, out temp_errors) +
                                "_Latch" + ": STD_LOGIC;");
                            errors += temp_errors;
                        }

                    }
                    else {
                        //  Connection is from an ordinary pin.
                        //  Signal is delcared only once for each gate output pin

                        if (processedPins.Contains(connection.fromPin)) {
                            continue;
                        }

                        processedPins.Add(connection.fromPin);
                        outFile.WriteLine("\tsignal " +
                            generateOutputPinName(block, connection, out temp_errors) +
                            ": STD_LOGIC;");
                        errors += temp_errors;

                        //  If it is latched, we also have to declare that signal as well.

                        if(block.latchOutputs) {
                            outFile.WriteLine("\tsignal " +
                                generateOutputPinName(block, connection, out temp_errors) +
                                "_Latch" + ": STD_LOGIC;");
                            errors += temp_errors;
                        }
                    }

                }
            }

            outFile.WriteLine();
            outFile.WriteLine("begin");
            outFile.WriteLine();

            return (errors);
        }

        private void generateVHDLArchitectureSuffix() {
            outFile.WriteLine();
            outFile.WriteLine("end;");
        }


    }
}
