
using Azure.Identity;
using LibraryManagementConsoleApp;


MainService mainService = new MainService();
BookService bookService = new BookService();

bool showAgainMain = false;

string selectedCollection = mainService.ShowMainSelection(showAgainMain);

//work with books
if (selectedCollection == "books")
{
	bool showAgainBooks = false;
	while (!showAgainBooks)
	{
		Console.WriteLine("\nYou are in book section. Please select action.\nPress L to see all books in list\nPress E to edit book\nPress D to delete book\nPress A to add new book\nPress Q to quite");
		ConsoleKeyInfo selectedAction = Console.ReadKey();

		switch (selectedAction.KeyChar)
		{
			case 'd':
			case 'D':
				bookService.DeleteBook();
				break;

			case 'l':
			case 'L':
				bookService.DisplayBookList();
				break;

			case 'e':
			case 'E':
				bookService.EditBook();
				break;

			case 'a':
			case 'A':
				bookService.AddBook();
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
}
// work with borrowers
else if (selectedCollection == "borrowers")
{

}

//selectedCollection = mainService.ShowMainSelection(showAgainMain);



//BorrowersRepository borrowersRepository = new BorrowersRepository();

//Borrower borrower = new Borrower { Name = "Karolina", Email = "tojest@mail", Phone = "666777888" };

//borrowersRepository.AddBorrower(borrower);

//Borrower[] borrowers = borrowersRepository.GetBorrowers();

//foreach (Borrower b in borrowers)
//{

//	Console.WriteLine($"{b.BorrowerId} {b.Name} {b.Email} {b.Phone} {b.TotalBorrowedBooks}");
//}



//Book book = new Book
//{
//	Title = "New Life 2",
//	Author = "Joe Doe",
//	ISBN = "GG-dfd-e3r"
//};

//booksRepository.AddBook(book);



//borrowersRepository.DeleteBorrower(borrowers[0].BorrowerId);