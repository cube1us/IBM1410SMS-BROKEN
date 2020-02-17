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

using MySql.Data.MySqlClient;
using System.Reflection;

namespace MySQLFramework
{

    //  Exceptions thrown in this class use this exception:

    public class TableException : Exception
    {

        public TableException() {
        }

        public TableException(string message) : base(message) {
        }

        public TableException(string message, Exception inner) : base(message, inner) {
        }
    }


    //  Table is a generic.  It is used for generating queries (including inserts,
    //  updates and deletes) for a given entity type T.

    public class Table<EntityType> where EntityType: new() {

        static Type myType;                         // Entity type for this table
        static PropertyInfo[] columns = null;       // Columns defined for this entity
        static PropertyInfo key = null;             // Column which is the key
        static string tableName;                    // The name of the table (== type, for now)
        static string columnNames = null;           // List of column names in columns[] order.
        static MySqlCommand getByKeyCommand = null; // Prepared statement for retrievals
        static MySqlCommand getAllCommand = null;   // Prepared statement to retrieve all rows
        static MySqlCommand insertCommand = null;   // Prepared statement for insertions
        static MySqlCommand updateCommand = null;   // Prepared statement for updates
        static MySqlCommand deleteByKeyCommand = null;  // Prepared statement for deletions

        MySqlConnection connection = null;          // Connection to use for queries with this table
                                                    // Opening connection responsibility of caller.

        //  Constructor.  Figures out what columns comprise this entity,
        //  and which column is the key.

        public Table(MySqlConnection connectionParm) {

            myType = typeof(EntityType);

            Console.WriteLine("Table<EntityType> Constructor call.");

            //  Make sure that the MyTable attribute is present.

            if (myType.GetCustomAttribute(typeof(MyTableAttribute)) == null) {
                throw new MyTableMissingException("Class " + myType.Name +
                    " does not have the MyTable Attribute present");
            }

            tableName = myType.Name;
            Console.WriteLine("Table name (type): " + tableName);

            //  Get a list of the columns in the table, using
            //  a LINQ lambda expression predicate.

            columns = myType.GetProperties().Where(
                property => property.IsDefined(typeof(MyColumnAttribute), false)).ToArray();

            //  Look through the columns to find the key column (and to make sure
            //  there is not more than one key.  While we are at it, build a list of
            //  column names for inserts, queries, updates, etc.

            for (int i = 0; i < columns.Length; ++i) {
                Console.WriteLine("Found Column: " + columns[i].Name);
                MyColumnAttribute myAttribute =
                    (MyColumnAttribute)columns[i].GetCustomAttribute(typeof(MyColumnAttribute));
                if (myAttribute.Key) {
                    Console.WriteLine(columns[i].Name + " is a key.");
                    if (key != null) {
                        throw new MyTableKeyException("More than one column marked as a key.");
                    }

                    key = columns[i];
                }

                if(columnNames != null) {
                    columnNames += ", ";
                }

                //  Escape column names with a back-tick in case they
                //  conflict with a MySQL keyword.  We don't use (")
                //  because that only works in ANSI mode.

                columnNames += "`" + columns[i].Name + "`";
            }

            //  Remember the connection to use

            connection = connectionParm;
        }


        //  Method to take a MySqlDataReader and copy out the values into the entity

        void setValues(MySqlDataReader resultSet, EntityType returnedEntity) {
            for (int i = 0; i < columns.Length; ++i) {
                //  SetValue won't accept a  DBNull type, so...
                if (!resultSet.IsDBNull(i)) {
                    columns[i].SetValue(returnedEntity, resultSet.GetValue(i));
                }
                else {
                    columns[i].SetValue(returnedEntity, null);
                }
            }
        }


        //  Method to retrieve the values for a given key, and fill in an entity

        public EntityType getByKey(int keyValue) {

            EntityType returnedEntity = new EntityType();

            //  Have we already prepared a statement for retrieval?  If not, do it now.

            if (getByKeyCommand == null) {
                getByKeyCommand = new MySqlCommand();
                getByKeyCommand.Connection = connection;

                string temp =              
                    "SELECT " + columnNames + " FROM " + tableName +
                    " WHERE " + key.Name + " = @" + key.Name;
                Console.WriteLine("Query constructed: " + temp);
                getByKeyCommand.CommandText = temp;
                getByKeyCommand.Prepare();
                getByKeyCommand.Parameters.AddWithValue("@" + key.Name,0);
            }

            //  Set the key value into the query and run the query.

            getByKeyCommand.Parameters["@" + key.Name].Value = keyValue;
            MySqlDataReader resultSet = getByKeyCommand.ExecuteReader();
            if (!resultSet.HasRows) {
                resultSet.Close();
                return returnedEntity;
            }

            //  Read the results from the first (and hopefully only) row, and fill
            //  in the values.

            resultSet.Read();
            setValues(resultSet, returnedEntity);
            
            //  See if more than one row was returned (unexpected).
            //  If so, throw an exception

            if(resultSet.Read()) {
                throw new TableException("GetByKey: More than one row returned in query " +
                    "for table " + tableName + " for key value " + keyValue);
            }

            //  Close the result set and return the constructed entity

            resultSet.Close();
            return returnedEntity;

        }


        //  Method to retrieve all of the rows of a table.

        public List<EntityType> getAll() {

            List<EntityType> returnedList = new List<EntityType>();

            //  Have we already prepared a statement for retrieval?  If not, do it now.

            if (getAllCommand == null) {
                getAllCommand = new MySqlCommand();
                getAllCommand.Connection = connection;

                string temp =
                    "SELECT " + columnNames + " FROM " + tableName;
                Console.WriteLine("Query All constructed: " + temp);
                getAllCommand.CommandText = temp;
                getAllCommand.Prepare();
            }

            //  Run the query.

            MySqlDataReader resultSet = getAllCommand.ExecuteReader();

            //  If there are no rows of data, return null.

            if (!resultSet.HasRows) {
                resultSet.Close();
                return (returnedList);
            }

            //  We have some rows, so continue:
            //  Read the results from the each row, fill in the values, and 
            //  add it to the List.

            while (resultSet.Read()) {
                EntityType tempEntity = new EntityType();
                setValues(resultSet, tempEntity);
                returnedList.Add(tempEntity);
            }

            //  Close the result set and return the constructed List

            resultSet.Close();
            return returnedList;
        }


        //  Method to retrieve rows of a table that match the provided
        //  WHERE clause.

        public List<EntityType> getWhere(string whereClause) {

            List<EntityType> returnedList = new List<EntityType>();

            //  This query does not use a prepared statement.

            MySqlCommand getWhereCommand = new MySqlCommand();
            getWhereCommand.Connection = connection;

            string temp =
                "SELECT " + columnNames + " FROM " + tableName + " " + whereClause;
            Console.WriteLine("Query Where constructed: " + temp);
            getWhereCommand.CommandText = temp;

            //  Run the query.

            MySqlDataReader resultSet = getWhereCommand.ExecuteReader();

            //  If there are no rows of data, return null.

            if (!resultSet.HasRows) {
                resultSet.Close();
                return (returnedList);
            }

            //  We have some rows, so continue...
            //  Read the results from the each row, fill in the values, and 
            //  add it to the List.

            while (resultSet.Read()) {
                EntityType tempEntity = new EntityType();
                setValues(resultSet, tempEntity);
                returnedList.Add(tempEntity);
            }

            //  Close the result set and return the constructed List

            resultSet.Close();
            return(returnedList);
        }


        //  Method to insert a new row into the table.

        public void insert(EntityType entity) {

            //  Have we already prepared a statement for insertion?  If not, do it now.

            if (insertCommand == null) {
                insertCommand = new MySqlCommand();
                insertCommand.Connection = connection;

                string temp =
                    "INSERT INTO " + tableName + "(" + columnNames +  ") VALUES(";
                for(int i=0; i < columns.Length; ++i) {
                    if(i > 0) {
                        temp += ", ";
                    }
                    temp += "@" + columns[i].Name;
                }
                temp += ")";

                Console.WriteLine("Insert constructed: " + temp);
                insertCommand.CommandText = temp;
                insertCommand.Prepare();

                //  Create the appropriate parameters.  The values do not matter,
                //  so just use those we see the first time around.

                for (int i = 0; i < columns.Length; ++i) {
                    insertCommand.Parameters.AddWithValue("@" + columns[i].Name,
                        columns[i].GetValue(entity));
                }

            }

            //  Set the column values into the query and run the query.

            for (int i=0; i < columns.Length; ++i) {
                insertCommand.Parameters["@" + columns[i].Name].Value = 
                    columns[i].GetValue(entity);
            }

            //  Run the insertion

            insertCommand.ExecuteNonQuery();
        }


        public void update(EntityType entity) {

            //  Have we already prepared a statement for updates?  If not, do it now.

            if (updateCommand == null) {
                updateCommand = new MySqlCommand();
                updateCommand.Connection = connection;

                string temp =
                    "UPDATE " + tableName + " SET ";
                for (int i = 0; i < columns.Length; ++i) {
                    if (i > 0) {
                        temp += ", ";
                    }
                    temp += "`" + columns[i].Name + "`=@" + columns[i].Name;
                }

                temp += " WHERE " + key.Name + "=@" + key.Name;

                Console.WriteLine("Update constructed: " + temp);
                updateCommand.CommandText = temp;
                updateCommand.Prepare();

                //  Create the appropriate parameters, using the values we see 
                //  the first time around.  The values do not matter at this point.

                for (int i = 0; i < columns.Length; ++i) {
                    updateCommand.Parameters.AddWithValue("@" + columns[i].Name,
                        columns[i].GetValue(entity));
                }

            }

            //  Set the column values into the query and run the query.

            for (int i = 0; i < columns.Length; ++i) {
                updateCommand.Parameters["@" + columns[i].Name].Value = 
                    columns[i].GetValue(entity);
            }

            //  Run the insertion

            updateCommand.ExecuteNonQuery();
        }

        
        //  Method to remove a row from a table.

        public void deleteByKey(int keyValue) {

            //  Have we already prepared a statement for deletion?  If not, do it now.

            if (deleteByKeyCommand == null) {
                deleteByKeyCommand = new MySqlCommand();
                deleteByKeyCommand.Connection = connection;

                string temp =
                    "DELETE FROM " + tableName +
                    " WHERE " + key.Name + " = @" + key.Name;
                Console.WriteLine("Delete Query constructed: " + temp);
                deleteByKeyCommand.CommandText = temp;
                deleteByKeyCommand.Prepare();
                deleteByKeyCommand.Parameters.AddWithValue("@" + key.Name, 0);
            }

            //  Set the key value into the delete query and run the query.

            deleteByKeyCommand.Parameters["@" + key.Name].Value = keyValue;
            deleteByKeyCommand.ExecuteNonQuery();

        }
    }
}

