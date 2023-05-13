using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class BooksRepository
	{
		private string connectionString =
		"Server=(localdb)\\mssqllocaldb;Database=LibraryDatabase;Integrated Security=True;";

		public Book[] GetBooks()
		{
			List<Book> bookList = new List<Book>();

			using (SqlConnection connection = new SqlConnection(connectionString))
			{

				connection.Open();

				using (SqlCommand command =
					new SqlCommand("SELECT BookId, Title, Author, ISBN, Availability FROM Books", connection))
				{
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Book book = new Book();
							book.BookId = reader.GetGuid(reader.GetOrdinal("BookId"));
							book.Title = reader.GetString(reader.GetOrdinal("Title"));
							book.Author = reader.GetString(reader.GetOrdinal("Author"));
							book.ISBN = reader.GetString(reader.GetOrdinal("ISBN"));
							book.IsAvailable = reader.GetBoolean(reader.GetOrdinal("Availability"));

							bookList.Add(book);
						}
					}
				}

				return bookList.ToArray();
			}
		}

		public void AddBook(Book book)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{

					connection.Open();

					using (SqlCommand command =
						new SqlCommand("INSERT INTO Books (BookId, Title, Author, ISBN, Availability ) VALUES(@BookId, @Title, @Author, @ISBN, @Availability)", connection))
					{
						Guid bookId = Guid.NewGuid();

						command.Parameters.AddWithValue("@BookId", bookId);
						command.Parameters.AddWithValue("@Title", book.Title);
						command.Parameters.AddWithValue("@Author", book.Author);
						command.Parameters.AddWithValue("@ISBN", book.ISBN);
						command.Parameters.AddWithValue("@Availability", 1);
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine("New book was added");
						}
						else
						{
							Console.WriteLine("Adding new book faild. Please, try again");
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

		public void DeleteBook(Guid id)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{

					connection.Open();

					using (SqlCommand command = new SqlCommand("DELETE FROM Books WHERE BookId = @BookId", connection))
					{
						command.Parameters.AddWithValue("@BookId", id);
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine("Book deleted");
						}
						else
						{
							Console.WriteLine("Deleting the book faild. Please, try again");
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

		public void EditBook(Book editedBook)
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				try
				{

					connection.Open();

					using (SqlCommand command =
						new SqlCommand("UPDATE Books SET Title =@Title, Author =@Author, ISBN =@ISBN, Availability =@Availability WHERE BookId = @BookId", connection))
					{
						command.Parameters.AddWithValue("@Title", editedBook.Title);
						command.Parameters.AddWithValue("@Author", editedBook.Author);
						command.Parameters.AddWithValue("@ISBN", editedBook.ISBN);
						command.Parameters.AddWithValue("@Availability", editedBook.IsAvailable);
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine($"Book {editedBook.Title} edited successfully.");
						}
						else
						{
							Console.WriteLine("Editing Book faild. Please, try again");
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
