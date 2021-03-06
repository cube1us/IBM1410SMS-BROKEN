﻿/* 
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

using System.IO;
using System.Text.RegularExpressions;

//  This class is the super class (mostly or entirely virtual) that encapsulates
//  HDL language generation for groups of pages, to make the GenerateHDLGroup class 
//  a bit cleaner.

//  I didn't use an Interface, because I wanted to be able implement some things
//  in the superclass.

namespace IBM1410SMS
{
    public abstract class GenerateGroupHDLLogic
    {
        protected Regex replacePeriods = new Regex("\\.");
        protected Regex replaceTitle = new Regex(" |-|\\.|\\+|\\-");

        public StreamWriter outFile { get; set; }
        public StreamWriter logFile { get; set; }

        //  Method to return the proper extension to use, and clock name.

        public abstract string generateHDLExtension();
        protected string SystemClockName { get; set; } = "FPGA_CLK";

        //  Methods for specific HDL pieces...

        public abstract void generateHDLPrefix(string name, string machineName);
        public abstract void generateHDLArchitecturePrefix(string name);
        public abstract void generateHDLArchitectureSuffix();
        public abstract void generateHDLentity(
            string entityName, List<string> inputs, List<string> outputs);
        public abstract void generateHDLSignalList(
            List<string> signals, List<string> bufferSignals);
        public abstract void generatePageEntity(string pageName, string pageTitle,
            List<string> inputs, List<string> outputs, List<string> bufferSignals,
            bool needsClock);

        //  Method to produce the name to use for a signal.  
        //  It is hoped/presumed that this one will be HDL language independent.

        public string generateSignalName(string signal) {
            string outString = "";

            if (signal.Substring(0, 1) == "+") {
                outString = "P" + signal.Substring(1);
            }
            else if (signal.Substring(0, 1) == "-") {
                outString = "M" + signal.Substring(1);
            }
            outString = outString.Replace(" ", "_");
            outString = outString.Replace(".", "_DOT_");
            outString = outString.Replace("+", "_OR_");
            outString = outString.Replace("*", "_STAR_");
            outString = outString.Replace("-", "_");

            //  VHDL won't allow consecutive underscores (which can happen for
            //  inputs like ... + ... , so...

            outString = outString.Replace("__", "_");

            return (outString);
        }

        public string generateHDLEntityName(string pageName, string pageTitle) {
            return
                "ALD_" +
                replacePeriods.Replace(pageName, "_") + "_" +
                replaceTitle.Replace(pageTitle, "_");
        }

        public void logMessage(string message) {
            logFile.WriteLine(message);
            logFile.Flush();
        }



    }
}
