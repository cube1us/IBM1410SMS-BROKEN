using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Tiedown
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idTieDown { get; set; }
		[MyColumn] public int diagramPage { get; set; }
		[MyColumn] public int cardType { get; set; }
		[MyColumn] public int cardSlot { get; set; }
		[MyColumn] public string pin { get; set; }
		[MyColumn] public int featureWithout { get; set; }
		[MyColumn] public int featureWith { get; set; }
		[MyColumn] public int checkMark { get; set; }
		[MyColumn] public string otherPin { get; set; }
		[MyColumn] public string note { get; set; }
	}
}
