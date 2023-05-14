using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementConsoleApp
{
	internal class MainService
	{
		public string ShowMainSelection(bool showAgainMain)
		{
			string result = "books";

			while (!showAgainMain)
			{
				Console.WriteLine("\nWelcome, in Library Management System!\nPress B to work with Books collection\nPress P to work with Borrowers collection");

				ConsoleKeyInfo userResponse = Console.ReadKey();

				if (userResponse.KeyChar == 'B' || userResponse.KeyChar == 'b')
				{
					break;
				}
				else if (userResponse.KeyChar == 'P' || userResponse.KeyChar == 'p')
				{
					result = "borrowers";
					break;
				}
				else
				{
					Console.WriteLine("\nSelection incorrect. Please, try again");
				}
			}
			return result;
		}
	}
}
