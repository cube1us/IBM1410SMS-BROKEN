using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Diagramecotag
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idDiagramECOTag { get; set; }
		[MyColumn] public int diagramPage { get; set; }
		[MyColumn] public string name { get; set; }
		[MyColumn] public int eco { get; set; }
		[MyColumn] public DateTime date { get; set; }
	}
}
