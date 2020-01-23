using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardlocation
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardLocation { get; set; }
		[MyColumn] public int page { get; set; }
		[MyColumn] public int cardSlot { get; set; }
		[MyColumn] public int type { get; set; }
		[MyColumn] public int feature { get; set; }
		[MyColumn] public int crossedOut { get; set; }
	}
}
