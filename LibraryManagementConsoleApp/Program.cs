
using LibraryManagementConsoleApp;

BorrowersRepository borrowersRepository = new BorrowersRepository();
Borrower[] borrowers = borrowersRepository.GetBorrowers();

foreach (Borrower b in borrowers) {

    Console.WriteLine( $"{b.BorrowerId} {b.Name} {b.Email} {b.Phone} {b.TotalBorrowedBooks}"  );
}