# 
#  COPYRIGHT 1998, 1999, 2000, 2019, 2020 Jay R. Jaeger
#  
#  This program is free software: you can redistribute it and/or modify
#  it under the terms of the GNU General Public License as published by
#  the Free Software Foundation, either version 3 of the License, or
#  (at your option) any later version.
#
#  This program is distributed in the hope that it will be useful,
#  but WITHOUT ANY WARRANTY; without even the implied warranty of
#  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#  GNU General Public License for more details.
#
#  You should have received a copy of the GNU General Public License
#  (file COPYING.txt) along with this program.  
#  If not, see <https://www.gnu.org/licenses/>.
#

#	This perl script reads a MySQL database and creates an entity object
#	for each table or for a specified table.  

use Data::Dumper;
use DBI;
use Getopt::Std;

sub usage();
sub emitsuffix($$);										# Emits common end of class lines
sub emitprefix($$);										# Emits commend start of class lines

$database = 'ibm1410sms';								# Name of database to process
$dbuser = 'collection';									# User ID to use
$dbpw = 'twiddle';										# Password to use

$outputDirectory = 'D:/users/jay/VisualStudioProjects/IBM1410SMS/IBM1410SMS/Entities';
$namespace = 'MySQLFramework';
$tableAttribute = 'MyTable';
$columnAttribute = 'MyColumn';
$columnKey = 'Key';

$processTable = '';										# Single table to process
$allTables = 1;											# Process all tables if true
$overwrite = 0;											# Overwrite existing output files
$verbose = 0;

#
#	Hash of sql to C# data types
#

%typeTable = (
	'int'		=> 'int',
	'date'		=> 'DateTime',
	'tinyint'	=> 'int',
	'varchar'	=> 'string',
	'char'		=> 'char',
);

#
#	Process command line options
#

getopts('ab:d:t:u:p:n:ov');

$verbose = $opt_v;
$overwrite = $opt_o;

if($opt_a && $opt_t) {
	usage();
	exit(4);
}

if($opt_t) {
	$allTables = 0;
	$processTable = $opt_t;
	print "Processing only table $processTable\n";
}

if($opt_b) {
	$database = $opt_b;
}

if($opt_u) {
	$dbuser = $opt_u;
}

if($opt_p) {
	$dbpw = $opt_p;
}

if($opt_d) {
	$outputDirectory = $opt_d;
}

if($opt_n) {
	$namespace = $opt_n;
}

#
#	Connect to the database
#
$dbh = DBI -> connect("DBI:mysql:$database",$dbuser,$dbpw);
if(!$dbh) {
	print "Could not open database $database as user #dbuser\n";
	exit(8);
}

#
#	Prepare the query for use...
#

$retrievalStmt = 'SELECT * FROM information_schema.columns ' .
	"WHERE table_schema = '$database' " .
	'ORDER BY table_name, ordinal_position';

$retrievalSth = $dbh -> prepare($retrievalStmt);

if(!$retrievalSth) {
	print 'Could not prepare information_schema query: ' . $dbh -> errstr . "\n";
	$dbh -> disconnect;
	exit(8);
}

#
#	And execute that query.
#

if(!$retrievalSth -> execute()) {
	print 'information_schema query failed: ' . $dbh -> errstr . "\n";
	exit(8);
}

#
#	Process the rows, each containing a column.
#

$previousTable = '';

while($row = $retrievalSth -> fetchrow_hashref) {

	$colTable = $row->{TABLE_NAME};
	$colName = $row->{COLUMN_NAME};
	$colType = $row->{DATA_TYPE};
	$colLen = $row->{CHARACTER_MAXIMUM_LENGTH};
	$colKey = $row->{COLUMN_KEY};
	
	if($verbose) {
		print "$colTable, $colName, $colType, $colLen, $colKey\n";
	}

	if($colTable ne $previousTable) {

		# If a class was being written, then close it out.
		# And, if appropriate, start up a new class file.

		if($allTables || $processTable eq $previousTable) {

			emitsuffix(OUTPUTFILE,$previousTable);
			close(OUTPUTFILE);
		}
	
		#	If this table should be processed, start up a new file.

		if($allTables || $processTable eq $colTable) {
				
			$outFileName = $outputDirectory . '/' . $colTable . '.cs';

			print "Output file name: $outFileName\n";
				
			# If not overwrite, check if file exists.  Error if so.

			if(!$overwrite && -f $outFileName) {
				print "Output file $outFileName already exists and overwrite NOT specified.\n";
				print "Terminating.\n";
				exit(8);
			}

			#	Open the output file
			
			if(!open(OUTPUTFILE,'>',$outFileName)) {
				print "Can''t open $outFileName for output: $! . Terminating.\n";
				exit(8);
			}
			
			#	Write out the C# member prefix.
			
			emitprefix(OUTPUTFILE,$colTable);

		}

		#	Remember the last table we saw.

		$previousTable = $colTable;

	}

	#	If we are processing this table, emit the column.

	if($allTables || $previousTable eq $processTable) {
	
		#	Figure out the approriate C# data type

		$ctype = $typeTable{$colType};

		if(!$ctype) {
			$ctype = 'UNKNOWN';
			print "No type conversion entry for database type $colType\n";
		}

		#	A couple of "int" columns are special cases, because they need
		#	to be C# nullable - but not ALL int columns.   (An alternative
		#	migtht be to do this for every non-foreign key that does not
		#	have the NN flag, but this will do for now.  The ones here are
		#	the cases where 0 means something other than null - 0 is a
		#	valid non-null value.

		if($ctype eq "int" && 
			($colName eq "voltageTenths" || $colName eq "componentValue")) {
		   	   $ctype = "int?";
		}

		$keyString = $colKey eq "PRI" ? "($columnKey=true)" : '';
		print OUTPUTFILE
			"\t\t[$columnAttribute$keyString] public $ctype $colName { get; set; }\n";
	}
}		

#	If we are processing this table, write out the suffix for this table.

if($allTables || $processTable eq $previousTable) {
	emitsuffix(OUTPUTFILE,$previousTable);
	close(OUTPUTFILE);
}


#	Clean up database handles, and be done.

$retrievalSth -> finish();
$dbh -> disconnect;
exit(0);

#
#	Output lines common to the start of each class.
#

sub emitprefix($$) {

	($OUTFILE,$table) = @_;

	# First letter of class shoud be uppercase.

	$table = ucfirst $table;

($prefix = <<PREFIX);
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySQLFramework;

namespace $namespace
{
	[$tableAttribute]
	public class $table
	{

		public bool modified {get; set;} = false;

PREFIX

	print $OUTFILE $prefix;

}

#
#	Output lines common to the end of each class.

sub emitsuffix($$) {
	
	($OUTFILE,$table) = @_;

	#	print the end of class and end of namespace braces

	print $OUTFILE "\t}\n";
	print $OUTFILE "}\n";
}


sub usage() {
	print "Usage: per $0 [-b database] [-u user] [-p pw] [-d outputdir] [-a | -t table] [-o] [-v]\n";
	print "\t-b database:  database to use as input.\n";
	print "\t-d outputdir: directory in which to place generated C# entity classes\n";
	print "-a:             process all tables [default]\n";
	print "-t table:       process just one table\n";
	print "-o              overwrite any existing output files.\n";
	print "-v              verbose / debug output\n";
	print "-u username     user to connect to database with\n";
	print "-p password     password to use.\n";
}

