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
			ShowBookList(books);
		}


		public void DeleteBook()
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

		public void EditBook()
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


		public void AddBook()
		{
			BooksRepository booksRepository = new BooksRepository();

			Book newBook = ReadBook("add");
			booksRepository.AddBook(newBook);
			
		}

		public bool HandleBookAction()
		{
			bool showAgainMain = false;
			bool showAgainBooks = false;

			while (!showAgainBooks)
			{
				Console.WriteLine("\nYou are in book section. Please select action.\nPress L to see all books in list\nPress E to edit book\nPress D to delete book\nPress A to add new book\nPress Q to quite");
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

					case 'q':
					case 'Q':
						showAgainMain = true;
						showAgainBooks = true;
						break;

					default:
						Console.WriteLine("Incorect selection. Please, try again");
						showAgainBooks = false;
						break;
				}
			}

			return showAgainMain;
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


					if (mode == "edit") { 
                    Console.WriteLine("Is book avaiable? Press Y for yes and N for no"	);
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

			int index = 1;
			foreach (Book b in books)
			{
				string available = b.IsAvailable ? "available" : "not available";

				Console.WriteLine($"\n{index}. {b.Title}, {b.Author}, {b.ISBN}, {available}");
				index++;
			}
		}
	}
}
