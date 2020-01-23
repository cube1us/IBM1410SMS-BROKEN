using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardlocationbottomnote
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardLocationBottomNote { get; set; }
		[MyColumn] public int cardLocation { get; set; }
		[MyColumn] public string note { get; set; }
	}
}
