using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardeco
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardTypeECO { get; set; }
		[MyColumn] public int eco { get; set; }
		[MyColumn] public int cardType { get; set; }
		[MyColumn] public DateTime date { get; set; }
		[MyColumn] public string approver { get; set; }
		[MyColumn] public int note { get; set; }
	}
}
