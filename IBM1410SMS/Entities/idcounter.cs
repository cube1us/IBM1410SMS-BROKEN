using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Idcounter
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCounter { get; set; }
		[MyColumn] public int counter { get; set; }
	}
}
