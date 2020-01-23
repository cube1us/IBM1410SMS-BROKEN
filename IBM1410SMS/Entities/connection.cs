using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Connection
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idConnection { get; set; }
		[MyColumn] public string from { get; set; }
		[MyColumn] public int fromDiagramBlock { get; set; }
		[MyColumn] public string fromPin { get; set; }
		[MyColumn] public string fromLoadPin { get; set; }
		[MyColumn] public string fromPhasePolarity { get; set; }
		[MyColumn] public int fromDotFunction { get; set; }
		[MyColumn] public int fromEdgeSheet { get; set; }
		[MyColumn] public int fromEdgeOriginSheet { get; set; }
		[MyColumn] public string fromEdgeConnectorReference { get; set; }
		[MyColumn] public string to { get; set; }
		[MyColumn] public int toDiagramBlock { get; set; }
		[MyColumn] public string toPin { get; set; }
		[MyColumn] public int toDotFunction { get; set; }
		[MyColumn] public int toEdgeSheet { get; set; }
		[MyColumn] public int toEdgeDestinationSheet { get; set; }
		[MyColumn] public string toEdgeConnectorReference { get; set; }
		[MyColumn] public string toEdge2ndConnectorReference { get; set; }
	}
}
