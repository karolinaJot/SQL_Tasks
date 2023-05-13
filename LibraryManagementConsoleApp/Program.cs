
using LibraryManagementConsoleApp;

BorrowersRepository borrowersRepository = new BorrowersRepository();

//Borrower borrower = new Borrower { Name = "Karolina", Email = "tojest@mail", Phone = "666777888" };

//borrowersRepository.AddBorrower(borrower);

Borrower[] borrowers = borrowersRepository.GetBorrowers();

foreach (Borrower b in borrowers)
{

    Console.WriteLine($"{b.BorrowerId} {b.Name} {b.Email} {b.Phone} {b.TotalBorrowedBooks}");
}

//borrowersRepository.DeleteBorrower(borrowers[0].BorrowerId);