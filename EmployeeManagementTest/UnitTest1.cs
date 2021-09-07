using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeManagement;
using EmployeeManagement.Model.SalaryModel;

namespace EmployeeManagementTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GivenSalaryData_AbleToUpdateSalaryDetails()
        {
            Salary salary = new Salary();
            SalaryUpdateModel updateModel = new SalaryUpdateModel()
            {
                SalaryId = 2,
                Month = "Jan",
                EmployeeSalary = 1300,
                EmployeeId = 2
            };

            int EmpSalary = salary.UpdateEmployeeSalary(updateModel);
            Assert.AreEqual(updateModel.EmployeeSalary, EmpSalary);
        }

        [TestMethod]
        public void GivenDateRange_ShouldReturnEmployeeName()
        {
            Salary salary = new Salary();
            var Employeename = salary.RetrieveEmployee_BetweenParticularDate();
            Assert.IsTrue(Employeename);
        }

        [TestMethod]
        public void GivenGender_ShouldReturnSalarySum()
        {
            Salary salary = new Salary();
            var Employeename = salary.FindSum();
            Assert.IsTrue(Employeename);
        }

    }
}
