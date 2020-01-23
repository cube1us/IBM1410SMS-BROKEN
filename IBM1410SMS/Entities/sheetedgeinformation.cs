using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Sheetedgeinformation
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idSheetEdgeInformation { get; set; }
		[MyColumn] public int diagramPage { get; set; }
		[MyColumn] public string row { get; set; }
		[MyColumn] public string signalName { get; set; }
		[MyColumn] public int leftSide { get; set; }
		[MyColumn] public int rightSide { get; set; }
	}
}
