using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentalsConsoleApp1.HR
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

        public EmployeeType employeeType;

        const int minimalHoursWorkedUnit = 1;

        public static double taxRate = 0.15;

        public Employee(string firstName, string lastName, string email, DateTime birthDay) : this(firstName, lastName, email, birthDay, 0, EmployeeType.StoreManager) { }


        public Employee(string firstName, string lastName, string email, DateTime birthDay, double hourlyRate, EmployeeType employeeType)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.birthDay = birthDay;
            this.hourlyRate = hourlyRate;
            this.employeeType = employeeType;
        }

        public void PerformWork()
        {
            PerformWork(minimalHoursWorkedUnit);
        }

        public void PerformWork(int hours)
        {
            numberOfHoursWorked += hours;
            Console.WriteLine($"{firstName} {lastName} has worked for {numberOfHoursWorked} hour(s)");
        }

        public double ReceiveWage(bool resetHours = true)
        {
            double wage;

            if (employeeType == EmployeeType.Manager)
            {
                wage = numberOfHoursWorked * hourlyRate * 1.5;
            }
            else
            {
                wage = numberOfHoursWorked * hourlyRate;
            }

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

        public void CalculateBonus(ref int bonus, out int bonus2)
        {
            bonus2 = 0;
            if (numberOfHoursWorked > 10)
            {
                bonus += 1000;
                bonus2 = bonus + 10000;
            }
        }


        public static void DisplayTaxRate()
        {
            Console.WriteLine($"The tax rate is {taxRate}");
        }

    }
}
