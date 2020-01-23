using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Page
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idPage { get; set; }
		[MyColumn] public int machine { get; set; }
		[MyColumn] public int volume { get; set; }
		[MyColumn] public string part { get; set; }
		[MyColumn] public string title { get; set; }
		[MyColumn] public string name { get; set; }
		[MyColumn] public string stamp { get; set; }
		[MyColumn] public string comment { get; set; }
	}
}
