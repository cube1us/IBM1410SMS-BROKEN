using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardnote
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardNote { get; set; }
		[MyColumn] public int cardType { get; set; }
		[MyColumn] public string noteName { get; set; }
		[MyColumn] public string note { get; set; }
	}
}
