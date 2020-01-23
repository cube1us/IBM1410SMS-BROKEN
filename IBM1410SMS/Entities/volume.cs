using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Volume
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idVolume { get; set; }
		[MyColumn] public int set { get; set; }
		[MyColumn] public string name { get; set; }
		[MyColumn] public int order { get; set; }
		[MyColumn] public string machineSerial { get; set; }
		[MyColumn] public string firstPage { get; set; }
		[MyColumn] public string lastPage { get; set; }
	}
}
