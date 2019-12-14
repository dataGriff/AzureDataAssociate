SELECT * FROM [dbo].[EmployeeBasic];

SELECT * FROM [dbo].[EmployeeBasic]
WHERE YEAR(dob) = 1996;

SELECT * FROM [dbo].[EmployeeRemuneration];

SELECT EmployeeID, EmployeeName
FROM [dbo].[EmployeeBasic]
ORDER BY (EmployeeName);

SELECT EmployeeID, EmployeeDesignation, Salary, DepartmentID
FROM [dbo].[EmployeeRemuneration]
ORDER BY (Salary) DESC;

SELECT SUM(Salary) AS TOTALSUM, DepartmentID
FROM [dbo].[EmployeeRemuneration] GROUP BY(DepartmentID);

SELECT AVG(Salary) AS AverageSalary, DepartmentID
FROM [dbo].[EmployeeRemuneration] GROUP BY(DepartmentID);

SELECT MIN(Salary) AS MinimumSalary, DepartmentID
FROM [dbo].[EmployeeRemuneration] GROUP BY(DepartmentID);

SELECT COUNT(EmployeeID) AS NumberOfEmployees, DepartmentID
FROM [dbo].[EmployeeRemuneration] GROUP BY(DepartmentID);

SELECT
  ER.[EmployeeID],
  [EmployeeName],
  [EmployeeDesignation],
  [Salary]
FROM [dbo].[EmployeeRemuneration] ER
JOIN [dbo].[EmployeeBasic] EB ON ER.EmployeeID = EB.EmployeeID;


SELECT
  EB.EmployeeID, EmployeeName, EmployeeDesignation, Salary,DepartmentID
FROM
  [dbo].[EmployeeBasic] EB, [dbo].[EmployeeRemuneration] ER1
WHERE
  ER1.EmployeeID = EB.EmployeeID AND
  ER1.Salary = ( SELECT MAX(Salary)
    FROM [dbo].[EmployeeRemuneration] ER2
    WHERE ER2.DepartmentID = ER1.DepartmentID );

SELECT
  MAX(Salary) as Salary, DepartmentID
FROM
  [dbo].[EmployeeRemuneration] E1
WHERE
  E1.Salary < (SELECT MAX(Salary) FROM
    [dbo].[EmployeeRemuneration] AS E2
    WHERE E2.DepartmentID = E1.DepartmentID)
OR
  E1.Salary = (SELECT MAX(Salary) FROM
    [dbo].[EmployeeRemuneration] AS E2
    WHERE E2.DepartmentID = E1.DepartmentID
    HAVING COUNT(DISTINCT Salary) = 1)
GROUP BY(DepartmentID);

SELECT TOP (1000) [SessionId]
      ,[UserId]
      ,[ProductId]
      ,[Quantity]
      ,[TransactionDateTime]
  FROM [dbo].[PurchasedProducts]
