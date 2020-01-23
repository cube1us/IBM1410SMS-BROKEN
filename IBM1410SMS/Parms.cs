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

/*
 * This class contains methods to assist with retrieving and storing
 * parameters - information that persisists across executions of the
 * user interface (or other classes).
 */


namespace IBM1410SMS
{
    public class Parms
    {
        private static readonly Parms instance = new Parms();
        private static DBSetup db = DBSetup.Instance;
        private static Table<Parameters> parametersTable = db.getParametersTable();


        //  The constructor CANNOT do ANYTHING, because it is
        //  statically initialized.

        private Parms() {
        }

        //  Everything was set up in the class, so Init() does not do anything.

        public Boolean Init() {
            return true;
        }

        public static string getParmValue(string name) {

            List<Parameters> parms = parametersTable.getWhere(
                "WHERE parameters.name='" + name + "'");
            if(parms.Count > 0) {
                return parms[0].value;
            }
            else {
                return ("");
            }
        }

        public static void setParmValue(string name, string value) {
            List<Parameters> parms = parametersTable.getWhere(
                "WHERE parameters.name='" + name + "'");
            if (parms.Count > 0) {
                parms[0].value = value;
                parametersTable.update(parms[0]);
            }
            else {
                Parameters parm = new Parameters();
                parm.idParameter = IdCounter.incrementCounter();
                parm.name = name;
                parm.value = value;
                parametersTable.insert(parm);
            }
        }


    }
}
