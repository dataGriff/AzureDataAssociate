CREATE MASTER KEY ENCRYPTION BY PASSWORD = '5up3r53cr3t!';

/*
--didn't do it this way in the end, used managed service identity below instead!!!
--https://docs.microsoft.com/en-us/azure/sql-data-warehouse/load-data-from-azure-blob-storage-using-polybase -- to use managed id
drop DATABASE SCOPED CREDENTIAL ADL_User
CREATE DATABASE SCOPED CREDENTIAL ADL_User WITH 
IDENTITY = '15fd08dc-8e60-4eb0-9aec-141bddebfbe6@https://login.microsoftonline.com/cafe5856-f1cc-43b5-b041-4cfc98c266e7/oauth2/token', 
SECRET = 'x5MqGB]5mX54d/+NvozKVq_0O[K.K-Fg'; --generated under certificates and secrets
*/

--this created with ManagedIDDWAccess.ps1 in same repo
--https://docs.microsoft.com/en-us/azure/sql-data-warehouse/load-data-from-azure-blob-storage-using-polybase#authenticate-using-managed-identities-to-load-optional
CREATE DATABASE SCOPED CREDENTIAL msi_cred WITH IDENTITY = 'Managed Service Identity';


--execute permissions given at container level? using  storage explorer
--drop EXTERNAL DATA SOURCE AzureDataLakeStore
CREATE EXTERNAL DATA SOURCE AzureDataLakeStore
WITH (
    TYPE = HADOOP,
    LOCATION = 'abfss://curated@grifffruitvegsa.dfs.core.windows.net',
    CREDENTIAL = msi_cred
);

CREATE EXTERNAL FILE FORMAT parquetfile1  
WITH (  
    FORMAT_TYPE = PARQUET,  
    DATA_COMPRESSION = 'org.apache.hadoop.io.compress.SnappyCodec'  
);  

drop external table [dbo].[Fruit]
CREATE EXTERNAL TABLE [dbo].[Fruit] ( 
      [EventType] nvarchar(max)
) 
WITH (LOCATION='/', 
      DATA_SOURCE = AzureDataLakeStore, 
      FILE_FORMAT = parquetfile1
);

select * from [dbo].[Fruit]


/*
CREATE EXTERNAL FILE FORMAT TextFileFormat WITH ( 
       FORMAT_TYPE = DELIMITEDTEXT, 
       FORMAT_OPTIONS (FIELD_TERMINATOR =',',
                       STRING_DELIMITER = '"', 
                       USE_TYPE_DEFAULT = TRUE)
);

--got ambulance data from here
--https://github.com/Azure/usql/tree/master/Examples/Samples/Data/AmbulanceData
--read permissions given at folder levelm, using storage explorer
CREATE EXTERNAL TABLE [dbo].[Ambulance_Data2] ( 
      [vehicle_id] int,
      [entry_id] bigint,
      [event_date] DateTime,
      [latitude] float,
      [longitude] float,
      [speed] int,
      [direction] char(5),
      [trip_id] int 
) 
WITH (LOCATION='AmbulanceData/Vehicle1/20140914', 
      DATA_SOURCE = AzureDataLakeStore, 
      FILE_FORMAT = TextFileFormat
);

select  * from [dbo].[Ambulance_Data2] --28799

select * from [dbo].[Ambulance_Data] 
where vehicle_id = 1 
and [event_date] >= '2014-09-14' and [event_date] < '2014-09-15' --2 seconds 28799
--460784
--5 seconds - 115,196
*/