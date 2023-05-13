using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class Borrower
	{
		private Guid borrowerId { get; set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public int TotalBorrowedBooks { get; set; }

		public Guid BorrowerId
		{
			get { return borrowerId; }
			set { borrowerId = value; }
		}


	}
}
