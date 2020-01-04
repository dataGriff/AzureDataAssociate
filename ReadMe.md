
## Exam Notes

* [General]()
* [Azure SQL]()
* [Synapse]()
* [Cosmos]()
* [Storage]()
* [ADF]()
* [Databricks]()
* [HubsAndStreams]()


## Azure SQL

* VIEW DATABASE STATE in order to see DMVs

### Monitor and Audit

* Configure at server level if want to do same for all databases

**DMVs**
```sql

--shows in memory utilisation
SELECT xtp_storage_pereent FROM dm_db_resource_stats
--
```

### Bring your own key for TDE

1. Assign an identity to SQL DB
1. Create KV and generate key
1. Grant KV permissions to SQL DB - grant the SQL db identity: get, wrapkey and unwrapkey permissions
1. Add KV key to azure SQL DB
1. Set TDE protector to use KV key
1. Enable TDE

### Dynamic Data Masking

* See example below.
* users who are db_datareader need to have  GRANT UNMASK as well to see real data

```sql
CREATE TABLE Membership  
  (MemberID int IDENTITY PRIMARY KEY,  
   FirstName varchar(100) MASKED WITH (FUNCTION = 'partial(1,"XXXXXXX",0)') NULL, --RXXXXXXX 
   LastName varchar(100) NOT NULL,  
   Phone varchar(12) MASKED WITH (FUNCTION = 'default()') NULL,  --xxxx
   Email varchar(100) MASKED WITH (FUNCTION = 'email()') NULL);  --ZXXX@XXXX.com
```

### Row Level Security

* Create function with schemabinding that applies generic filter
* Create policy on a table using funciton as filter

```sql
CREATE FUNCTION dbo.fn_securitypredicate(@LoginIsLastName AS sysname)  
RETURNS TABLE  
WITH SCHEMABINDING  
AS  
RETURN SELECT 1 AS fn_securitypredicate_result
WHERE @LoginIsLastName = USER_NAME();  
go

CREATE SECURITY POLICY [SurNameOnly]   
ADD FILTER PREDICATE [dbo].[fn_securitypredicate](LastName)   
ON [dbo].[Membership];  
```


### Encryption

* You can right click a column and choose encrypt.
* This can be deterministic or randomized.
* In connection string need to set Column Encryption Setting = ENabled to decrypt, as long as you have pemrissions to! 
* VIEW ANY COLUMN MASTER KEY DEFINITION and VIEW ANY COLUMN ENCRYPTION KEY DEFINITION to be able decrypt the data.
* To perform cryptographic operations using the wizard, you must have the VIEW ANY COLUMN MASTER KEY DEFINITION and VIEW ANY COLUMN ENCRYPTION KEY DEFINITION

**Example**
```sql
CREATE COLUMN MASTER KEY MyCMK  
WITH (  
     KEY_STORE_PROVIDER_NAME = 'MSSQL_CERTIFICATE_STORE',   
     KEY_PATH = 'Current User/Personal/f2260f28d909d21c642a3d8e0b45a830e79a1420'  
   );   --you can use key vault too 
---------------------------------------------  
CREATE COLUMN ENCRYPTION KEY MyCEK   
WITH VALUES  
(  
    COLUMN_MASTER_KEY = MyCMK,   
    ALGORITHM = 'RSA_OAEP',   
    ENCRYPTED_VALUE = 0x0170000001... --massive key 
);  
---------------------------------------------  
CREATE TABLE Customers (  
    CustName nvarchar(60)   
        COLLATE  Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyCEK,  
        ENCRYPTION_TYPE = RANDOMIZED,  
        ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'),   
    SSN varchar(11)   
        COLLATE  Latin1_General_BIN2 ENCRYPTED WITH (COLUMN_ENCRYPTION_KEY = MyCEK,  
        ENCRYPTION_TYPE = DETERMINISTIC ,  
        ALGORITHM = 'AEAD_AES_256_CBC_HMAC_SHA_256'),   
    Age int NULL  
);  
GO  
```

### Synapse

### Distribution

```sql
WITH
(   CLUSTERED COLUMNSTORE INDEX
,  DISTRIBUTION = HASH([ProductKey]),
   ,  PARTITION       ( [OrderDateKey] RANGE RIGHT FOR VALUES --...
)
;
```
```sql
WITH
(   CLUSTERED COLUMNSTORE INDEX
,  DISTRIBUTION = ROUND_ROBIN --can leave this out as default anyway
)
;
```
```sql
WITH  
  (   
    DISTRIBUTION = REPLICATE,
    CLUSTERED INDEX (thing)  
  ); 
```

### Create Statistics 

```sql 
--Create statistics on new table
CREATE STATISTICS [ProductKey] ON [FactInternetSales_CustomerKey] ([ProductKey]);
```

### Identify SKew
```sql
DBCC PDW_SHOWSPACEUSED('dbo.FactInternetSales');
```
* [Useful DMV](https://docs.microsoft.com/en-us/azure/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries)

## Cosmos 

### Consistency Levels
* Availability and latency increases but consistency decreases... 
1. **Strong** required all commit and synch to be done before any region sees the new value. 
1. **Bounded Staleness**
1. **Session** gurantees sees latest data within a session. 
1. **Consistent Prefix** guarantees order.
1. **Eventual** guarantees eventual data will be there, not neccessarly in order.

## Storage 

### Audit and Monitoir

* To audit API calls just tick Audit and Requests for the log. AllMetrics contains more. 
* You can set retention periods with sliders on these too. 

### Retention Policy

* **Portal** - Storage Account > Blob Service > Lifecylce Management > Add rule > cool storage after x days > archive storage after x days > delete blob after x days > delete snapshot after x days 
* Example of a retention policy 

```json
{
  "rules": [
    {
      "name": "ruleFoo",
      "enabled": true,
      "type": "Lifecycle",
      "definition": {
        "filters": {
          "blobTypes": [ "blockBlob" ],
          "prefixMatch": [ "container1/foo" ]
        },
        "actions": {
          "baseBlob": {
            "tierToCool": { "daysAfterModificationGreaterThan": 30 },
            "tierToArchive": { "daysAfterModificationGreaterThan": 90 },
            "delete": { "daysAfterModificationGreaterThan": 2555 }
          },
          "snapshot": {
            "delete": { "daysAfterCreationGreaterThan": 90 }
          }
        }
      }
    }
  ]
}
```

## Data Factory

* **Linked Service** is the connection string essentially. The location and auth. 

### Steps to Implement SSIS IR

1. Create Azure-SSIS IR in ADF
1. Install Self-hosted IR on prem SSIS
1. Register IR with reg key
1. Setup self hosted IR as proxy for Azure SSIS IR 


