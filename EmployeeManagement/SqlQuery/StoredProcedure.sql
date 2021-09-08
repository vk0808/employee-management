﻿CREATE PROCEDURE [dbo].[spUpdateEmployeeSalary]
	@ID INT,
	@Month VARCHAR(20),
	@Salary INT,
	@EmpId INT
AS
BEGIN
SET XACT_ABORT ON;
BEGIN TRY
BEGIN TRANSACTION;
	UPDATE SALARY
	SET EmpSalary=@Salary
	WHERE SALARYId = @ID AND SALARYMONTH = @Month AND EmpId = @EmpId;
	SELECT e.EmpId, e.EmpName, s.EmpSalary, s.SALARYMONTH, s.SALARYId
	FROM EmployeeTable e INNER JOIN SALARY s
	ON e.EmpId = s.EmpId WHERE s.SALARYId = @ID;
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
SELECT ERROR_NUMBER() AS ErrorNumber, ERROR_MESSAGE() AS ErrorMessage;
IF(XACT_STATE()) = -1
	BEGIN
		PRINT N'The transaction is in an uncommitable state.'+'Rolling back transaction.'
		ROLLBACK TRANSACTION;
	END;

IF(XACT_STATE()) = 1
	BEGIN
		PRINT N'The transaction is committable. '+'Committing transaction.'
		COMMIT TRANSACTION;
	END;
END CATCH
END
GO


CREATE PROCEDURE [dbo].[SpAddingRecord]
(
	@EmpName VARCHAR(20),
	@Gender CHAR(1),
	@HireDay DATE,
	@DeptNo INT,
	@Email VARCHAR(20),
	@BirthDay DATE,
	@JobDiscription VARCHAR(20),
	@ProfileImage VARCHAR(20)
)
AS
BEGIN
	INSERT INTO [EmployeeTable] VALUES
	(@EmpName,@Gender,@HireDay,@DeptNo,@Email,@BirthDay,@JobDiscription,@ProfileImage)
END
GO