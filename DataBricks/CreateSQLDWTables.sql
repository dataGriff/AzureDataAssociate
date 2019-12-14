CREATE TABLE [dbo].[EmployeeBasic]
(
  [EmployeeID] int NOT NULL,
  [EmployeeName] varchar(30) NOT NULL,
  [DOB] date NOT NULL,
  [Address] varchar(50) NOT NULL,
  [BloodGroup] nvarchar(2) NOT NULL
)
WITH
(
  CLUSTERED COLUMNSTORE INDEX,
  DISTRIBUTION = HASH([EmployeeID])
);

CREATE TABLE [dbo].[Sales]
 (
   [ProductKey] int NOT NULL,
   [OrderDateKey] int NOT NULL,
   [CustomerKey] int NOT NULL,
   [PromotionKey] int NOT NULL,
   [SalesOrderNumber] nvarchar(20) NOT NULL,
   [OrderQuantity] smallint NOT NULL,
   [UnitPrice] money NOT NULL,
   [SalesAmount] money NOT NULL
 )
 WITH
 (
   CLUSTERED COLUMNSTORE INDEX,
   DISTRIBUTION = ROUND_ROBIN
 );

 CREATE TABLE [dbo].[States]
  (
    [StateKey] int NOT NULL,
    [State] nvarchar(20) NOT NULL
  )
  WITH
  (
    CLUSTERED COLUMNSTORE INDEX,
    DISTRIBUTION = REPLICATE
  );

  CREATE TABLE [dbo].[EmployeeRemuneration]
(
  [EmployeeID] int NOT NULL,
  [EmployeeDesignation] varchar(30) NOT NULL,
  [Salary] money NOT NULL,
  [DepartmentID] int NOT NULL
)
WITH
(
  CLUSTERED COLUMNSTORE INDEX,
  DISTRIBUTION = HASH([EmployeeID])
);