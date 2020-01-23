using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardslot
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardSlot { get; set; }
		[MyColumn] public int panel { get; set; }
		[MyColumn] public string cardRow { get; set; }
		[MyColumn] public int cardColumn { get; set; }
	}
}
