using CSharpFundamentalsConsoleApp1.HR;


namespace CSharpFundamentalsConsoleApp1.Test
{
	public class EmployeeTests
	{
		[Fact]
		public void PerformWork_Adds_NumberOfHours()
		{
			//ARRANGE
			Employee employee = new Employee("FName", "LName", "e@mail.com", new DateTime(1985, 1, 16), 25);
			int numberOfHours = 3;

			//ACT
			employee.PerformWork(numberOfHours);

			//ASSERT
			Assert.Equal(numberOfHours, employee.NumberOfHoursWorked);
		}


		[Fact]
		public void PerformWork_Adds_DefaultNumberOfHours_IfNoValueSpecified()
		{
			//ARRANGE
			Employee employee = new Employee("FName", "LName", "e@mail.com", new DateTime(1985, 1, 16), 25);

			//ACT
			employee.PerformWork();

			//ASSERT
			Assert.Equal(Employee.minimalHoursWorkedUnit, employee.NumberOfHoursWorked);


		}


	}
}