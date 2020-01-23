using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Eco
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idECO { get; set; }
		[MyColumn] public int machine { get; set; }
		[MyColumn] public string eco { get; set; }
		[MyColumn] public string description { get; set; }
	}
}
