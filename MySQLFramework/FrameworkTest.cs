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

using System.Reflection;
using MySql.Data.MySqlClient;


namespace MySQLFramework
{
    class reflectionTest
    {
        static void Main(string[] args) {

            MySqlConnection connection = null;
            string connectionString = @"server=localhost;userid=collection;
            password=twiddle;database=test";

            TestMachine machine = null;
            List<TestMachine> machineList = null;

            //  Open the connection.

            try {
                connection = new MySqlConnection(connectionString);
                connection.Open();

            }
            catch (MySqlException ex) {
                Console.WriteLine("Error: {0}", ex.ToString());
                return;
            }
            
            //  Test the various queries

            Table<TestMachine> machineTable = new Table<TestMachine>(connection);

            machine = machineTable.getByKey(1410);
            Console.WriteLine("Key is " + machine.idMachine + ", Name: " +
                machine.name);

            machine = machineTable.getByKey(1108);
            if(machine.name == "Univac 1108" || machine.name == "univac1108") {                 Console.WriteLine("Key is " + machine.idMachine + ", Name: " +
                    machine.name);
                Console.WriteLine("Deleting Record 1108");
                machineTable.deleteByKey(1108);
            }

            machine = machineTable.getByKey(1130);
            if (machine.name == "IBM1130") {
                Console.WriteLine("Key is " + machine.idMachine + ", Name: " +
                    machine.name);
                Console.WriteLine("Deleting Record 1130");
                machineTable.deleteByKey(1130);
            }

            Console.WriteLine("Inserting Record 1130");
            machine.idMachine = 1130;
            machine.name = "IBM1130";
            machineTable.insert(machine);

            Console.WriteLine("Inserting Record 1108");
            machine.idMachine = 1108;
            machine.name = "Univac 1108";
            machineTable.insert(machine);

            machine = machineTable.getByKey(1108);
            Console.WriteLine("Key is " + machine.idMachine + ", Name: " +
                machine.name);

            Console.WriteLine("Updating Previous record...");
            machine.name = "univac1108";
            machineTable.update(machine);

            machine = machineTable.getByKey(1108);
            Console.WriteLine("Key is " + machine.idMachine + ", Name: " +
                machine.name);

            machineList = machineTable.getAll();
            Console.WriteLine("All Machines:");
            foreach (TestMachine tempMachine in machineList) {
                Console.WriteLine("Key: " + tempMachine.idMachine + ", Name: " +
                    tempMachine.name);
            }

            string whereclause = "WHERE dummy IS NOT NULL";
            machineList = machineTable.getWhere(whereclause);
            Console.WriteLine(whereclause);
            foreach (TestMachine tempMachine in machineList) {
                Console.WriteLine("Key: " + tempMachine.idMachine + ", Name: " +
                    tempMachine.name);
            }

            Console.WriteLine("Done.");

        }
    }
}
