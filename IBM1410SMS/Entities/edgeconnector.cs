using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Edgeconnector
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idEdgeConnector { get; set; }
		[MyColumn] public int diagramPage { get; set; }
		[MyColumn] public string reference { get; set; }
		[MyColumn] public int cardSlot { get; set; }
		[MyColumn] public string pin { get; set; }
		[MyColumn] public int order { get; set; }
	}
}
