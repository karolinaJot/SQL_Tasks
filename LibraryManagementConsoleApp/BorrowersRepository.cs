
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class BorrowersRepository
	{

		private string connectionString =
		"Server=(localdb)\\mssqllocaldb;Database=LibraryDatabase;Integrated Security=True;";

		public Borrower[] GetBorrowers()
		{
			List<Borrower> borrowerList = new List<Borrower>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{

				connection.Open();
				using (SqlCommand command =
					new SqlCommand("SELECT BorrowerID, Name, Email, Phone, TotalBorrowedBooks FROM Borrowers", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Borrower borrower = new Borrower();
							borrower.BorrowerId = reader.GetInt32(0);
							borrower.Name = reader.GetString(1);
							borrower.Email = reader.IsDBNull(2) ? "no data" : reader.GetString(2);
							borrower.Phone = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
							borrower.TotalBorrowedBooks = reader.GetInt32(4);

							borrowerList.Add(borrower);
						}
					}
				}

				return borrowerList.ToArray();
			}
		}
	}
}
