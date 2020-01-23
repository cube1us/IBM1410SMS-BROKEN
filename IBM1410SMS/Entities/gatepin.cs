using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Gatepin
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idGatePin { get; set; }
		[MyColumn] public int cardGate { get; set; }
		[MyColumn] public string pin { get; set; }
		[MyColumn] public string mapPin { get; set; }
		[MyColumn] public int inputGate { get; set; }
		[MyColumn] public int outputGate { get; set; }
		[MyColumn] public int input { get; set; }
		[MyColumn] public int output { get; set; }
		[MyColumn] public int dotOutput { get; set; }
		[MyColumn] public int dotInput { get; set; }
		[MyColumn] public int? voltageTenths { get; set; }
	}
}
