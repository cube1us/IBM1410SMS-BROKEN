using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLFramework
{

    //  Exception thrown if a member of the mysqlEntity class does not have
    //  the MyTable attribute

    public class MyTableMissingException : Exception
    {

        public MyTableMissingException() {
        }

        public MyTableMissingException(string message) : base(message) {
        }

        public MyTableMissingException(string message, Exception inner) : base(message, inner) {
        }
    }


    //  Exception thrown if a member of the mysqlEntity class has more than one
    //  column with the MyColumn attribute with the Key value set to true.

    public class MyTableKeyException: Exception { 

        public MyTableKeyException() {
        }

        public MyTableKeyException(string message) : base(message) {
        }

        public MyTableKeyException(string message, Exception inner) : base(message,inner) {
        }
    }
}
