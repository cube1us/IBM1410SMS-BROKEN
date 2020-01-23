using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Ibmlogicfunction
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idIBMLogicFunction { get; set; }
		[MyColumn] public string label { get; set; }
	}
}
