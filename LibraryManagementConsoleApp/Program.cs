using LibraryManagementConsoleApp;


MainService mainService = new MainService();
BookService bookService = new BookService();
BorrowerService borrowerService = new BorrowerService();

bool showAgainMain = false;

string selectedCollection = mainService.ShowMainSelection(showAgainMain);

//work with books
if (selectedCollection == "books")
{
	bookService.HandleBookAction(); 
}
// work with borrowers
else if (selectedCollection == "borrowers")
{
	borrowerService.HandleBorrowerAction();
}

selectedCollection = mainService.ShowMainSelection(showAgainMain);

