
CREATE USER LabUser FOR LOGIN LabUserLogin;

EXEC sp_addrolemember 'db_datareader', 'LabUser';

EXEC sp_addrolemember 'largerc', 'LabUser';

CREATE LOGIN LabUserLogin WITH PASSWORD = 'Str0ng_password';

CREATE USER LabUser FOR LOGIN LabUserLogin;