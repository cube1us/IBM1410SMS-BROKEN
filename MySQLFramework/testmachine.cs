using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLFramework
{
    [MyTable]
    class TestMachine
    {
        [MyColumn(Key=true)] public int idMachine { get; set; }
        [MyColumn] public string name { get; set; }

        public TestMachine() {
            idMachine = 0;
            name = "";
        }

    }
}
