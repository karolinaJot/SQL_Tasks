
using LibraryManagementConsoleApp;

BorrowersRepository borrowersRepository = new BorrowersRepository();

//Borrower borrower = new Borrower { Name = "Karolina", Email = "tojest@mail", Phone = "666777888" };

//borrowersRepository.AddBorrower(borrower);

Borrower[] borrowers = borrowersRepository.GetBorrowers();

foreach (Borrower b in borrowers)
{

	Console.WriteLine($"{b.BorrowerId} {b.Name} {b.Email} {b.Phone} {b.TotalBorrowedBooks}");
}

BooksRepository booksRepository = new BooksRepository();

//Book book = new Book
//{
//	Title = "New Life 2",
//	Author = "Joe Doe",
//	ISBN = "GG-dfd-e3r"
//};

//booksRepository.AddBook(book);

Book[] books = booksRepository.GetBooks();

foreach (Book b in books)
{
	int index = 1;
	Console.WriteLine($"List of books: {index}. {b.Title}, {b.Author}, {b.ISBN}, {b.IsAvailable} ");
	index++;
}

borrowersRepository.DeleteBorrower(borrowers[0].BorrowerId);