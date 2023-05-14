using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class BookService
	{
		public bool HandleBookAction()
		{
			bool showAgainMain = false;
			bool showAgainBooks = false;

			while (!showAgainBooks)
			{
				Console.WriteLine("\nYou are in book section. Please select action.\nPress L to see all books in list\nPress E to edit book\nPress D to delete book\nPress A to add new book\nPress S to search book\nPress Q to quite");
				ConsoleKeyInfo selectedAction = Console.ReadKey();

				switch (selectedAction.KeyChar)
				{
					case 'd':
					case 'D':
						DeleteBook();
						break;

					case 'l':
					case 'L':
						DisplayBookList();
						break;

					case 'e':
					case 'E':
						EditBook();
						break;

					case 'a':
					case 'A':
						AddBook();
						break;

					case 's':
					case 'S':
						showAgainBooks = SelectSearchType();
						break;

					case 'q':
					case 'Q':
						showAgainMain = true;
						showAgainBooks = true;
						break;

					default:
						Console.WriteLine("\nIncorect selection. Please, try again");
						showAgainBooks = false;
						break;
				}
			}

			return showAgainMain;
		}

		private bool SelectSearchType()
		{
			bool showAgain = false;
			bool showAgainBookActions = true;

			while (!showAgain)
			{
				Console.WriteLine("\nThis is search book panel");
				Console.WriteLine("Press T to serach by title\nPress A to search by author\nPress B to serach by ISBN\nPress Q to quite");

				ConsoleKeyInfo selectedSearch = Console.ReadKey();
				switch (selectedSearch.KeyChar)
				{
					case 't':
					case 'T':
						SearchByTitle();
						break;

					case 'a':
					case 'A':
						SearchByAuthor();
						break;

					case 'b':
					case 'B':
						SearchByISBN();
						break;

					case 'q':
					case 'Q':
						showAgain = true;
						showAgainBookActions = false;
						break;

					default:
						Console.WriteLine("\nIncorect selection. Please, try again");
						showAgain = false;
						break;
				}
			}

			return showAgainBookActions;
		}

		private void SearchByISBN()
		{
			BooksRepository booksRepository = new BooksRepository();

			try
			{
				Console.WriteLine("\nProvide book ISBN");
				string ISBNvalue = Console.ReadLine();

				Book[] books = booksRepository.GetSelectedBooks("ISBN", ISBNvalue);
				ShowBookList(books);
			}
			catch { Console.WriteLine("Somthing went wrong. Try again."); }

		}

		private void SearchByAuthor()
		{
			BooksRepository booksRepository= new BooksRepository();

			try {
				Console.WriteLine("\nProvide author");
				string author = Console.ReadLine();

				Book[] books = booksRepository.GetSelectedBooks("Author", author);
				ShowBookList(books);
					}
			catch { Console.WriteLine("Somthing went wrong. Try again."); }
		}

		private void SearchByTitle()
		{
			BooksRepository booksRepository = new BooksRepository();

			try
			{
				Console.WriteLine("\nProvide title");
				string title = Console.ReadLine();

				Book[] books = booksRepository.GetSelectedBooks("Title", title);
				ShowBookList(books);
			}
			catch { Console.WriteLine("Somthing went wrong. Try again."); }
		}


		private void DisplayBookList()
		{
			BooksRepository booksRepository = new BooksRepository();
			Book[] books = booksRepository.GetBooks();
			ShowBookList(books);
		}


		private void DeleteBook()
		{
			BooksRepository booksRepository = new BooksRepository();
			Book[] books = booksRepository.GetBooks();
			ShowBookList(books);

			Console.WriteLine("Select book to delete by typing index number");

			bool showAgain = false;

			while (!showAgain)
			{
				try
				{
					int index = int.Parse(Console.ReadLine());
					if (index < 0 || index > books.Length)
					{
						showAgain = false;
					}
					else
					{
						Book bookToDelete = books[index - 1];
						booksRepository.DeleteBook(bookToDelete.BookId);
					}
				}
				catch { showAgain = false; }
			}
		}

		private void EditBook()
		{
			BooksRepository booksRepository = new BooksRepository();
			Book[] books = booksRepository.GetBooks();
			ShowBookList(books);

			Console.WriteLine("Select book to edit by typing index number");

			bool showAgain = false;

			while (!showAgain)
			{
				try
				{
					int index = int.Parse(Console.ReadLine());
					if (index < 0 || index > books.Length)
					{
						showAgain = false;
					}
					else
					{
						Book bookToEdit = books[index - 1];
						Book bookNewData = ReadBook("edit");

						bookToEdit.Title = bookNewData.Title != null ? bookNewData.Title : bookToEdit.Title;
						bookToEdit.Author = bookNewData.Author != null ? bookNewData.Author : bookToEdit.Author;
						bookToEdit.ISBN = bookNewData.ISBN != null ? bookNewData.ISBN : bookToEdit.ISBN;
						bookToEdit.IsAvailable = bookNewData.IsAvailable;

						booksRepository.EditBook(bookToEdit);
						showAgain = true;
						break;

					}
				}
				catch { showAgain = false; }
			}
		}


		private void AddBook()
		{
			BooksRepository booksRepository = new BooksRepository();

			Book newBook = ReadBook("add");
			booksRepository.AddBook(newBook);

		}


		private Book ReadBook(string mode)
		{
			bool showAgain = false;
			Book book = new Book();

			while (!showAgain)
			{
				try
				{
					Console.WriteLine("\nProvide book title");
					string title = Console.ReadLine();

					Console.WriteLine("Provide book author");
					string autor = Console.ReadLine();

					Console.WriteLine("Provide book ISBN");
					string ISBN = Console.ReadLine();


					if (mode == "edit")
					{
						Console.WriteLine("Is book avaiable? Press Y for yes and N for no");
						bool isBookAvaiable = CheckAvailability();
						book.IsAvailable = isBookAvaiable;
					}

					book.Title = title;
					book.Author = autor;
					book.ISBN = ISBN;

					showAgain = true;
					break;
				}
				catch
				{
					Console.WriteLine("Incorrect input. Please, try again");
					showAgain = false;
				}
			}

			return book;
		}

		private bool CheckAvailability()
		{
			bool showAgain = false;

			while (!showAgain)
			{
				Console.WriteLine("Is book avaiable? Press Y for yes and N for no");
				ConsoleKeyInfo response = Console.ReadKey();

				switch (response.KeyChar)
				{
					case 'y':
					case 'Y':
						return true;

					case 'n':
					case 'N':
						return false;

					default:
						Console.WriteLine("Inncorect input. Please, try again");
						showAgain = false;
						break;
				}
			}

			return false;
		}

		private void ShowBookList(Book[] books)
		{
			if (books.Length < 1)
			{
				Console.WriteLine("The list of books is empty.");
			}

			int index = 1;
			foreach (Book b in books)
			{
				string available = b.IsAvailable ? "yes" : "no";

				Console.WriteLine($"\n{index}. Title: {b.Title}, Author: {b.Author}, ISBN: {b.ISBN}, Is available: {available}");
				index++;
			}
		}
	}
}
