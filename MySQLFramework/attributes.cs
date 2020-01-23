using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//   Usage:  [MyTable]
//   Usage:  [MyColumn(Key=true|false)]

namespace MySQLFramework

{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class MyColumnAttribute : Attribute {
        public bool Key;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MyTableAttribute : Attribute {
    }
}
