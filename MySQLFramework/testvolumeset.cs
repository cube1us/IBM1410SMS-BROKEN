using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//	This is a test entity type that was created and edited
//	outside of Visual Studio.

namespace MySQLFramework
{
    [MyTable]
    class TestVolumeSet
    {
        [MyColumn(Key=true)] public int idVolumeSet { get; set; }
        [MyColumn] public string machineType { get; set; }
	    [MyColumn] public string machineSerial { get; set; }

        public TestVolumeSet() {
            idVolumeSet = 0;
            machineType = "";
            machineSerial = "";
        }

    }
}
