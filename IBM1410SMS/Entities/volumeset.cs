using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Volumeset
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idVolumeSet { get; set; }
		[MyColumn] public string machineType { get; set; }
		[MyColumn] public string machineSerial { get; set; }
	}
}
