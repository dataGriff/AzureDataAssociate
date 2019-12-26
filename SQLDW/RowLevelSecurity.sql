CREATE SCHEMA Security
GO 

CREATE FUNCTION Security.fn_rowlevel_predicate(@EmployeeLoginId AS sysname)  
    RETURNS TABLE  
WITH SCHEMABINDING  
AS  
    RETURN SELECT 1 AS fn_rowlevel_predicate_result
WHERE @EmployeeLoginId = 'demodw_user'
GO

CREATE SECURITY POLICY EmployeeFilter
ADD FILTER PREDICATE Security.fn_rowlevel_predicate(LoginId)
ON dbo.DimEmployee
WITH (STATE = ON)
GO