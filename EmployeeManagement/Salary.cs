using EmployeeManagement.Model.SalaryModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeeManagement
{
    public class Salary
    {
        private static SqlConnection ConnectionSetup()
        {
            return new SqlConnection(@"Data Source=(LocalDb)\localDb;Initial Catalog=payroll_service;Integrated Security=True");
        }
        SqlConnection SalaryConnection = ConnectionSetup();


        public int UpdateEmployeeSalary(SalaryUpdateModel salaryUpdateModel)
        {
            SqlConnection SalaryConnection = ConnectionSetup();
            int salary = 0;
            try
            {
                using (SalaryConnection)
                {
                    string id = "2";

                    string query = @"SELECT * FROM EmployeeTable WHERE EmpId =" + Convert.ToInt32(id);
                    SalaryDetailModel displayModel = new SalaryDetailModel();
                    SqlCommand command = new SqlCommand("spUpdateEmployeeSalary", SalaryConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", salaryUpdateModel.SalaryId);
                    command.Parameters.AddWithValue("@Month", salaryUpdateModel.Month);
                    command.Parameters.AddWithValue("@Salary", salaryUpdateModel.EmployeeSalary);
                    command.Parameters.AddWithValue("@EmpId", salaryUpdateModel.EmployeeId);
                    SalaryConnection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeId = Convert.ToInt32(dr["EmpId"]);
                            displayModel.EmployeeName = dr["EmpName"].ToString();
                            displayModel.EmployeeSalary = Convert.ToInt32(dr["EmpSalary"]);
                            displayModel.Month = dr["SalaryMonth"].ToString();
                            displayModel.SalaryId = Convert.ToInt32(dr["SalaryId"]);

                            Console.WriteLine(displayModel.EmployeeName + " " + displayModel.EmployeeSalary + " " + displayModel.Month + "\n");
                            salary = Convert.ToInt32(displayModel.EmployeeSalary);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return salary;
        }

        public bool RetrieveEmployee_BetweenParticularDate()
        {
            try
            {
                SalaryDetailModel displayModel = new SalaryDetailModel();


                using (SalaryConnection)
                {
                    SqlCommand command = new SqlCommand("SELECT EmpName FROM EmployeeTable WHERE HireDay BETWEEN '2007-07-12' and GETDATE();", SalaryConnection);

                    SalaryConnection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            displayModel.EmployeeName = dr.GetString(0);
                            Console.WriteLine(displayModel.EmployeeName);

                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return false;
        }

        public bool FindSum()
        {
            try
            {
                SalaryDetailModel displayModel = new SalaryDetailModel();


                using (SalaryConnection)
                {
                    SqlCommand command = new SqlCommand("SELECT SUM(s.EmpSalary), e.Gender FROM Salary s INNER JOIN EmployeeTable e ON s.Empid = e.Empid  GROUP BY Gender;", SalaryConnection);
                    SalaryUpdateModel updateModel = new SalaryUpdateModel();

                    SalaryConnection.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            updateModel.EmployeeSalary = dr.GetDecimal(0);
                            displayModel.Gender = dr.GetString(1);
                            Console.WriteLine(displayModel.Gender + " " + updateModel.EmployeeSalary);

                        }
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return false;
        }

        public bool AddNewRecord(SalaryDetailModel displayModel)
        {
            SqlConnection SalaryConnection = ConnectionSetup();

            try
            {
                using (SalaryConnection)
                {
                    SqlCommand command = new SqlCommand("SpAddingRecord", SalaryConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpName", displayModel.EmployeeName);
                    command.Parameters.AddWithValue("@Gender", displayModel.Gender);
                    command.Parameters.AddWithValue("@HireDay", displayModel.HireDate);
                    command.Parameters.AddWithValue("@DeptNo", displayModel.DeptNo);
                    command.Parameters.AddWithValue("@Email", displayModel.Email);
                    command.Parameters.AddWithValue("@BirthDay", displayModel.BirthDay);
                    command.Parameters.AddWithValue("@JobDiscription", displayModel.JobDescription);
                    command.Parameters.AddWithValue("@ProfileImage", displayModel.ProfileImage);
                    SalaryConnection.Open();

                    var result = command.ExecuteNonQuery();
                    SalaryConnection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                SalaryConnection.Close();
            }
            return false;
        }
    }
}
