CREATE PROCEDURE TotalPE
AS
BEGIN
SELECT dept.DepartmentName as DEPTNAME, 
		SUM(CASE
			WHEN txn.FKTransModeId = 1 THEN txn.ExpenseAmount
		END) as EXPENSE,
		SUM(CASE
			WHEN txn.FKTransModeId = 2 THEN txn.ExpenseAmount
		END) as PROFIT
FROM Transactions txn join 
	Departments dept
	on dept.DepartmentId = txn.FKDeptId
	group by dept.DepartmentName
END
GO

EXEC TotalPE