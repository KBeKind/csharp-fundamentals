using CSharpFundamentalsConsoleApp1.HR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentalsConsoleApp1
{
	internal class Utilities
	{
		private static string directory = @"C:\data\BethanysPieShopHRM\";
		private static string fileName = "employees.txt";

		internal static void RegisterEmployee(List<Employee> employees)
		{
			Console.WriteLine("Creating an employee");

			Console.WriteLine("What type of employee do you want to register?");
			Console.WriteLine("1. Employee\n2. Manager\n3. Store manager\n4. Researcher\n5. Junior researcher");
			Console.Write("Your selection: ");
			string employeeType = Console.ReadLine();

			if (employeeType != "1" && employeeType != "2" && employeeType != "3"
				&& employeeType != "4" && employeeType != "5")
			{
				Console.WriteLine("Invalid selection!");
				return;
			}

			Console.Write("Enter the first name: ");
			string firstName = Console.ReadLine();

			Console.Write("Enter the last name: ");
			string lastName = Console.ReadLine();

			Console.Write("Enter the email: ");
			string email = Console.ReadLine();

			Console.Write("Enter the birth day: ");
			DateTime birthDay = DateTime.Parse(Console.ReadLine());

			Console.Write("Enter the hourly rate: ");
			string hourlyRate = Console.ReadLine();
			double rate = double.Parse(hourlyRate);

			Employee employee = null;

			switch (employeeType)
			{
				case "1":
					employee = new Employee(firstName, lastName, email, birthDay, rate);
					break;
				case "2":
					employee = new Manager(firstName, lastName, email, birthDay, rate);
					break;
				case "3":
					employee = new StoreManager(firstName, lastName, email, birthDay, rate);
					break;
				case "4":
					employee = new Researcher(firstName, lastName, email, birthDay, rate);
					break;
				case "5":
					employee = new JuniorResearcher(firstName, lastName, email, birthDay, rate);
					break;
			}

			employees.Add(employee);

			Console.WriteLine("Employee created!\n\n");
		}

		internal static void CheckForExistingEmployeeFile()
		{
			Console.ForegroundColor = ConsoleColor.Yellow;

			string path = $"{directory}{fileName}";

			bool existingFileFound = File.Exists(path);
			if (existingFileFound)
			{
				Console.WriteLine("An existing file with Employee data is found.");
			}
			else
			{
				// CREATE DIRECTORY
				if (!Directory.Exists(directory))
					Directory.CreateDirectory(directory);
				Console.WriteLine("Directory is ready for saving files.");
			}
			Console.ForegroundColor = ConsoleColor.Blue;

		}

		internal static void ViewAllEmployees(List<Employee> employees)
		{
			for (int i = 0; i < employees.Count; i++)
			{
				employees[i].DisplayEmployeeDetails();
			}
		}

		internal static void LoadEmployees(List<Employee> employees)
		{
			string path = $"{directory}{fileName}";

			try
			{
				if (File.Exists(path))
				{
					// CLEAR EMPLOYEES CURRENTLY IN MEMORY
					employees.Clear();

					// READ THE FILE
					string[] employeesAsString = File.ReadAllLines(path);
					for (int i = 0; i < employeesAsString.Length; i++)
					{
						// PARSING THE STRING INTO THE PARTS
						// SPLITTING ON THE ; AND GRABBING THE TEXT AFTER THE :
						string[] employeeSplits = employeesAsString[i].Split(';');
						string firstName = employeeSplits[0].Substring(employeeSplits[0].IndexOf(':') + 1);
						string lastName = employeeSplits[1].Substring(employeeSplits[1].IndexOf(':') + 1);
						string email = employeeSplits[2].Substring(employeeSplits[2].IndexOf(':') + 1);
						DateTime birthDay = DateTime.Parse(employeeSplits[3].Substring(employeeSplits[3].IndexOf(':') + 1));
						double hourlyRate = double.Parse(employeeSplits[4].Substring(employeeSplits[4].IndexOf(':') + 1));
						string employeeType = employeeSplits[5].Substring(employeeSplits[5].IndexOf(':') + 1);

						Employee employee = null;

						switch (employeeType)
						{
							case "1":
								employee = new Employee(firstName, lastName, email, birthDay, hourlyRate);
								break;
							case "2":
								employee = new Manager(firstName, lastName, email, birthDay, hourlyRate);
								break;
							case "3":
								employee = new StoreManager(firstName, lastName, email, birthDay, hourlyRate);
								break;
							case "4":
								employee = new Researcher(firstName, lastName, email, birthDay, hourlyRate);
								break;
							case "5":
								employee = new JuniorResearcher(firstName, lastName, email, birthDay, hourlyRate);
								break;
						}


						employees.Add(employee);

					}
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine($"Loaded {employees.Count} employees!\n\n");
			//		Console.ResetColor();

				}
			}
			catch (FileNotFoundException fnfex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("The file can not be found");
				Console.WriteLine(fnfex.Message);
				Console.WriteLine(fnfex.StackTrace);
			//	Console.ResetColor();
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Something went wrong");
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);
			//	Console.ResetColor();
			}
			finally
			{
				Console.ResetColor();
			}

		}

		internal static void SaveEmployees(List<Employee> employees)
		{
			// CONVERT EMPLOYEE TO A STRING
			// ALSO WE COULD USE JSON.NET AND STORE IT AS JSON

			string path = $"{directory}{fileName}";
			StringBuilder sb = new StringBuilder();

			foreach (var employee in employees)
			{
				string type = GetEmployeeType(employee);

				sb.Append($"firstName:{employee.FirstName};");
				sb.Append($"lastName:{employee.LastName};");
				sb.Append($"email:{employee.Email};");
				sb.Append($"birthDay:{employee.BirthDay.ToShortDateString()};");
				sb.Append($"hourlyRate:{employee.HourlyRate};");
				sb.Append($"type:{type};");
				sb.Append(Environment.NewLine);
			}

			File.WriteAllText(path, sb.ToString());

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Saved employees successfully");
			Console.ResetColor();
		}

		private static string GetEmployeeType(Employee employee)
		{
			if (employee is Manager)
				return "2";
			else if (employee is StoreManager)
				return "3";
			else if (employee is JuniorResearcher)
				return "5";
			else if (employee is Researcher)
				return "4";
			else if (employee is Employee)
				return "1";
			return "0";
		}

		internal static void LoadEmployeeById(List<Employee> employees)
		{

			Console.WriteLine("Enter the employee ID: ");

			try
			{
				int selectedId = int.Parse(Console.ReadLine());
				Employee selectedEmployee = employees[selectedId];
				selectedEmployee.DisplayEmployeeDetails();
			}
			catch (FormatException ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Invalid ID format");
				Console.ResetColor();
			}
		



		}
	}
}
