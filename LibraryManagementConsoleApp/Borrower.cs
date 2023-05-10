using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class Borrower
	{
		public string Name { get; set; }
		public int BorrowerId { get; set; }
		public string Email { get; set; }
		public int Phone { get; set; }
		public int TotalBorrowedBooks { get; set; }
	}
}
