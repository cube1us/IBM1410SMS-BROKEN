using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Frame
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idFrame { get; set; }
		[MyColumn] public string name { get; set; }
		[MyColumn] public int machine { get; set; }
	}
}
