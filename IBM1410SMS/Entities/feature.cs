using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Feature
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idFeature { get; set; }
		[MyColumn] public int machine { get; set; }
		[MyColumn] public string code { get; set; }
		[MyColumn] public string feature { get; set; }
	}
}
