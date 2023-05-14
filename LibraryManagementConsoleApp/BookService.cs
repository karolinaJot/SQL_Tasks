using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class BookService
	{
		public void DisplayBookList()
		{
			BooksRepository booksRepository = new BooksRepository();
			Book[] books = booksRepository.GetBooks();

			int index = 1;
			foreach (Book b in books)
			{
				string available = b.IsAvailable ? "available" : "not available";

				Console.WriteLine($"\n{index}. {b.Title}, {b.Author}, {b.ISBN}, {available}");
				index++;
			}
		}

		public void DeleteBook()
		{
			throw new NotImplementedException();
		}

		public void EditBook()
		{
			throw new NotImplementedException();
		}

		internal void AddBook()
		{
			throw new NotImplementedException();
		}
	}
}
