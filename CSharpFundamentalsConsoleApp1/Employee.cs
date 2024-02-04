using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentalsConsoleApp1
{
	internal class Employee
	{

		public string firstName;
		public string lastName;
		public string email;

		public int numberOfHoursWorked;
		public double wage;
		public double hourlyRate;

		public DateTime birthDay;

		public void PerformWork()
		{
			numberOfHoursWorked++;
			Console.WriteLine($"{firstName} {lastName} has worked for {numberOfHoursWorked} hour(s)");

		}

		public void PerformWork(int hours)
		{
			numberOfHoursWorked += hours;
			Console.WriteLine($"{firstName} {lastName} has worked for {numberOfHoursWorked} hour(s)");
		}


		public double ReceiveWage(bool resetHours = true)
		{
			wage = numberOfHoursWorked * hourlyRate;
			Console.WriteLine($"{firstName} {lastName} has received a wage of {wage} for {numberOfHoursWorked} hour(s) of work.");
			
			if (resetHours)
			{
				numberOfHoursWorked = 0;
			}
			
			return wage;
		}

		public void DisplayEmployeeDetails()
		{
			Console.WriteLine($"\nFirst name: {firstName}\nLast name: {lastName}\nEmail: {email}\nBirthday: {birthDay.ToShortDateString()}\n");
		}



	}
}
