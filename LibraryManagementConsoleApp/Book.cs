﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class Book
	{
		private Guid bookId { get; set; }

		public string Title { get; set; }
		public string Author { get; set; }
		public string ISBN { get; set; }
		public bool IsAvailable { get; set; }

		public Guid BookId {
			get { return bookId; }
			set { bookId = value; }
		}

	}
}
