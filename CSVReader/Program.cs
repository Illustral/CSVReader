using System;
using System.IO;
using System.Collections.Generic;

namespace CSVReader
{
	class Program
	{
		static void Main(string[] args)
		{
			string inputDate;
			List<string> names = new List<string>();
			List<string> dates = new List<string>();
			int i = 0;
			try
			{
				Console.WriteLine("Reading csv file...");
				using (var reader = new StreamReader(@"../../nimet.csv"))
				{
					while (!reader.EndOfStream)
					{
						var line = reader.ReadLine();
						var values = line.Split(';');

						dates.Add(values[0]);
						names.Add(values[1]);
					}
				}
				try
				{
					if (dates.Count == 0 || names.Count == 0)
					{
						throw (new CsvException("CSV file does not have enough data"));
					}
				}
				catch (CsvException ex)
				{
					Console.WriteLine(ex);
				}
				Console.WriteLine("CSV file loaded");
				restart:
				Console.WriteLine("Enter date: ");
				{
					inputDate = Console.ReadLine();
					foreach (var date in dates)
					{
						if (date == inputDate)
						{
							Console.WriteLine("People with name(s) " + names[i] + " have a nameday on " + inputDate);
							return;
						}
						i++;
						if (!dates.Contains(inputDate))
						{
							Console.WriteLine(inputDate + " is not in the csv.");
							i = 0;
							goto restart;
						}
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine(ex);
			}
			catch (CsvException ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
	public class CsvException : Exception
	{
		public CsvException(string message) : base(message)
		{
		}
	}
}
