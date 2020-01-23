using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace IBM1410SMS
{
	[MyTable]
	public class Cardtype
	{

		public bool modified {get; set;} = false;

		[MyColumn(Key=true)] public int idCardType { get; set; }
		[MyColumn] public int volume { get; set; }
		[MyColumn] public string type { get; set; }
		[MyColumn] public string part { get; set; }
		[MyColumn] public string nameType { get; set; }
		[MyColumn] public string name { get; set; }
		[MyColumn] public int logicFamily { get; set; }
		[MyColumn] public int height { get; set; }
		[MyColumn] public string holePattern { get; set; }
		[MyColumn] public string developmentNumber { get; set; }
		[MyColumn] public string approvedBy { get; set; }
		[MyColumn] public DateTime approvedDate { get; set; }
		[MyColumn] public string designApprover { get; set; }
		[MyColumn] public DateTime designDate { get; set; }
		[MyColumn] public string detailer { get; set; }
		[MyColumn] public DateTime detailDate { get; set; }
		[MyColumn] public string designChecker { get; set; }
		[MyColumn] public DateTime designCheckDate { get; set; }
		[MyColumn] public string approver { get; set; }
		[MyColumn] public DateTime approvalDate { get; set; }
		[MyColumn] public string modelType { get; set; }
		[MyColumn] public string modelDevice { get; set; }
		[MyColumn] public string scale { get; set; }
		[MyColumn] public string draw { get; set; }
		[MyColumn] public DateTime drawDate { get; set; }
		[MyColumn] public string drawingChecker { get; set; }
		[MyColumn] public DateTime drawingCheckDate { get; set; }
		[MyColumn] public string develpmentNumber { get; set; }
	}
}
