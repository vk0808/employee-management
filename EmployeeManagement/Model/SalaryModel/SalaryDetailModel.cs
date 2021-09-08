using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeManagement.Model.SalaryModel
{
    public class SalaryDetailModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string JobDescription { get; set; }
        public string Month { get; set; }
        public decimal EmployeeSalary { get; set; }
        public int SalaryId { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public int DeptNo { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public string ProfileImage { get; set; }
        
    }
}
