using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class BorrowerService
	{

		public void DisplayBorrowerList()
		{
			BorrowersRepository borrowersRepository = new BorrowersRepository();
			Borrower[] borrowers = borrowersRepository.GetBorrowers();
			ShowBorrowerList(borrowers);
		}


		public void DeleteBorrower()
		{
			BorrowersRepository borrowersRepository = new BorrowersRepository();
			Borrower[] borrowers = borrowersRepository.GetBorrowers();
			ShowBorrowerList(borrowers);

			Console.WriteLine("Select borrower to delete by typing index number");

			bool showAgain = false;

			while (!showAgain)
			{
				try
				{
					int index = int.Parse(Console.ReadLine());
					if (index < 0 || index > borrowers.Length)
					{
						showAgain = false;
					}
					else
					{
						Borrower borrowerToDelete = borrowers[index - 1];
						borrowersRepository.DeleteBorrower(borrowerToDelete.BorrowerId);
					}
				}
				catch { showAgain = false; }
			}
		}

		public void EditBorrower()
		{
			BorrowersRepository borrowersRepository = new BorrowersRepository();
			Borrower[] borrowers = borrowersRepository.GetBorrowers();
			ShowBorrowerList(borrowers);

			Console.WriteLine("Select borrower to edit by typing index number");

			bool showAgain = false;

			while (!showAgain)
			{
				try
				{
					int index = int.Parse(Console.ReadLine());
					if (index < 0 || index > borrowers.Length)
					{
						showAgain = false;
					}
					else
					{
						Borrower borrowerToEdit = borrowers[index - 1];
						Borrower borrowerNewData = ReadBorrower("edit");

						borrowerToEdit.Name = borrowerNewData.Name != null ? borrowerNewData.Name : borrowerToEdit.Name;
						borrowerToEdit.Email = borrowerNewData.Email != null ? borrowerNewData.Email : borrowerToEdit.Email;
						borrowerToEdit.Phone = borrowerNewData.Phone != null ? borrowerNewData.Phone : borrowerToEdit.Phone;
						borrowerToEdit.TotalBorrowedBooks += borrowerNewData.TotalBorrowedBooks;

						borrowersRepository.EditBorrower(borrowerToEdit);
						showAgain = true;
						break;

					}
				}
				catch { showAgain = false; }
			}
		}


		public void AddBorrower()
		{
			BorrowersRepository borrowersRepository = new BorrowersRepository();
			Borrower borrower = ReadBorrower("add");
			borrowersRepository.AddBorrower(borrower);	
		}

		public bool HandleBorrowerAction()
		{
			bool showAgainMain = false;
			bool showAgainBorrowers = false;

			while (!showAgainBorrowers)
			{
				Console.WriteLine("\nYou are in borrower section. Please select action.\nPress L to see all borrowers in list\nPress E to edit borrower\nPress D to delete borrower\nPress A to add new borrower\nPress Q to quite");
				ConsoleKeyInfo selectedAction = Console.ReadKey();

				switch (selectedAction.KeyChar)
				{
					case 'd':
					case 'D':
						DeleteBorrower();
						break;

					case 'l':
					case 'L':
						DisplayBorrowerList();
						break;

					case 'e':
					case 'E':
						EditBorrower();
						break;

					case 'a':
					case 'A':
						AddBorrower();
						break;

					case 'q':
					case 'Q':
						showAgainMain = true;
						showAgainBorrowers = true;
						break;

					default:
						Console.WriteLine("Incorect selection. Please, try again");
						showAgainBorrowers = false;
						break;
				}
			}

			return showAgainMain;
		}

		private Borrower ReadBorrower(string mode)
		{
			bool showAgain = false;
			Borrower borrower = new Borrower();

			while (!showAgain)
			{
				try
				{
					Console.WriteLine("\nProvide borrower name");
					string name = Console.ReadLine();

					Console.WriteLine("Provide borrower email");
					string email = Console.ReadLine();

					Console.WriteLine("Provide borrower phone");
					string phone = Console.ReadLine();



					if (mode == "edit")
					{
						Console.WriteLine("Add borrower borrowed books number");
						int totalBorrowedBooks = Int32.Parse(Console.ReadLine());
						borrower.TotalBorrowedBooks = totalBorrowedBooks;
					}

					borrower.Name = name;
					borrower.Email = email;
					borrower.Phone = phone;

					showAgain = true;
					break;
				}
				catch
				{
					Console.WriteLine("Incorrect input. Please, try again");
					showAgain = false;
				}
			}

			return borrower;
		}

		private void ShowBorrowerList(Borrower[] borrowers)
		{

			int index = 1;
			foreach (Borrower b in borrowers)
			{
				Console.WriteLine($"\n{index}. Name: {b.Name}, Emial: {b.Email}, Phone: {b.Phone}, Total borrowed books {b.TotalBorrowedBooks}");
				index++;
			}
		}
	}
}
