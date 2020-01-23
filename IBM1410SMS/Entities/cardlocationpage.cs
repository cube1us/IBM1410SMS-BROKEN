using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardlocationpage
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardLocationPage { get; set; }
		[MyColumn] public int page { get; set; }
		[MyColumn] public int eco { get; set; }
		[MyColumn] public int panel { get; set; }
		[MyColumn] public int run { get; set; }
		[MyColumn] public int previousECO { get; set; }
		[MyColumn] public int sheets { get; set; }
	}
}
