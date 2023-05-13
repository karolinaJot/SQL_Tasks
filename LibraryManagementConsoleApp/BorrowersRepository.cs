
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
							borrower.BorrowerId = Guid.Parse(reader.GetString(reader.GetOrdinal("BorrowerID")));
							borrower.Name = reader.GetString(reader.GetOrdinal("Name"));
							borrower.Email = reader.GetString(reader.GetOrdinal("Email"));
							borrower.Phone = reader.GetString(reader.GetOrdinal("Phone"));
							borrower.TotalBorrowedBooks = reader.GetInt32(reader.GetOrdinal("TotalBorrowedBooks"));

							borrowerList.Add(borrower);
						}
					}
				}

				return borrowerList.ToArray();
			}
		}

		public void AddBorrower(Borrower borrower)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{

					connection.Open();

					using (SqlCommand command =
						new SqlCommand("INSERT INTO Borrowers (BorrowerID, Name, Email, Phone, TotalBorrowedBooks ) VALUES(@BorrowerID, @Name, @Email, @Phone, @TotalBorrowedBooks)", connection))
					{
						Guid borrowerId = Guid.NewGuid();

						command.Parameters.AddWithValue("@BorrowerID", borrowerId);
						command.Parameters.AddWithValue("@Name", borrower.Name);
						command.Parameters.AddWithValue("@Email", borrower.Email);
						command.Parameters.AddWithValue("@Phone", borrower.Phone);
						command.Parameters.AddWithValue("@TotalBorrowedBooks", 0);
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine("New Borrower was added");
						}
						else
						{
							Console.WriteLine("Adding new Borrower faild. Please, try again");
						}

					}
				}
				catch (SqlException exception)
				{
					Console.WriteLine("An error occurred while connecting to the database or executing the command: " + exception.Message);
				}
				catch (Exception exception)
				{
					Console.WriteLine("An unexpected error occurred: " + exception.Message);
				}
			}
		}

		public void DeleteBorrower(Guid id)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{

					connection.Open();

					using (SqlCommand command = new SqlCommand("DELETE FROM Borrowers WHERE BorrowerID = @BorrowerID", connection))
					{
						command.Parameters.AddWithValue("@BorrowerID", id);
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine("Borrower deleted");
						}
						else
						{
							Console.WriteLine("Deleting the Borrower faild. Please, try again");
						}
					}
				}
				catch (SqlException exception)
				{
					Console.WriteLine("An error occurred while connecting to the database or executing the command: " + exception.Message);
				}
				catch (Exception exception)
				{
					Console.WriteLine("An unexpected error occurred: " + exception.Message);
				}
			}
		}

		public void EditBorrower(Borrower editedBorrower)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{

					connection.Open();

					using (SqlCommand command =
						new SqlCommand("UPDATE Borrowers SET Name=@Name, Email=@Email, Phone=@Phone, TotalBorrowedBooks=@TotalBorrowedBooks WHERE BorrowerID = @BorrowerID", connection))
					{
						command.Parameters.AddWithValue("@Name", editedBorrower.Name);
						command.Parameters.AddWithValue("@Email", editedBorrower.Email);
						command.Parameters.AddWithValue("@Phone", editedBorrower.Phone);
						command.Parameters.AddWithValue("@TotalBorrowedBooks", editedBorrower.TotalBorrowedBooks);
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine($"Borrower{editedBorrower.Name} edited successfully.");
						}
						else
						{
							Console.WriteLine("Editing Borrower faild. Please, try again");
						}

					}
				}
				catch (SqlException exception)
				{
					Console.WriteLine("An error occurred while connecting to the database or executing the command: " + exception.Message);
				}
				catch (Exception exception)
				{
					Console.WriteLine("An unexpected error occurred: " + exception.Message);
				}
			}

		}

	}
}
