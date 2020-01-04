
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
* Use Azure SQL Data sync when want bi-direction sync and good for starting up
* Data migration service one way trip for data to azure 
* ReplicationState should be 2 when synch has occured between failover group when queriying Get-AzSQLDatabseFailoverGroup
    * Status of 1 means synching
* sp_Wait_for_database_copy_sync procedure on primary  tells the applicaiton to wait for synch with secondary 
* New-AzSqlDatabaseInstanceFailoverGroup

### Monitor and Audit

* Configure at server level if want to do same for all databases
* Query performance insight shows queries taking most resource but not index recommendations
* SQL DB Advisor recommends for indexes, fixing schemas and parameterize queries.
* Azure advisor integrates with above to show same index recommendations along with security, performance and cost.  
* **Logs**
* Basic Metrics - CPU and DTU. 
* QueryStoreRunTimeStatistics - CPU usage and query duration. 
* SQLInsights - performance info and provides recommendations. 
* DatabaseWaitStatistics - time database spent on waiting. 
* QueryStoreWaitStatistics - resources tha caused queries to wait such as CPU, logs or locks. 


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

* when you encrpt TDE with KV in two regions you must do secondary first

### Dynamic Data Masking

* See example below.
* users who are db_datareader need to have  GRANT UNMASK as well to see real data
* Default is XXXX for chars
* Random masks numeric values with specified range
* See below for other examples 

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
* In connection string need to set **Column Encryption Setting = ENabled** to decrypt, as long as you have pemrissions to! 
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

### DMVs
* sys.dm_pdw_exec_requests - current or recent queries, e.g. long running ones
* sys.dm_pdw_exec_sessions - current or recent logins e.g. ip address, number of queries

### Import Data
1. Create master key
1. Create credential
1. Create external data source
  * HADOOP for accessing blob
1. Create external file format
  * First row =2 to skip header
1. Create external table

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

### Query Label

```sql 
select * from fact OPTION (Label='MyQueryTHing')
```

### Identify SKew
```sql
DBCC PDW_SHOWSPACEUSED('dbo.FactInternetSales');
```
* [Useful DMV](https://docs.microsoft.com/en-us/azure/sql-data-warehouse/sql-data-warehouse-tables-overview#table-size-queries)

## Cosmos 

* A client can change consistency level at conneciton time and for each request
* TTL -1 means indefinite
* TTL null inherits TTL from above
* HTTP 429 error is RU throttling

### Consistency Levels
* Availability and latency increases but consistency decreases... 
1. **Strong** required all commit and synch to be done before any region sees the new value. 
1. **Bounded Staleness** - When a client performs read operations within a region that accepts writes, the guarantees provided by bounded staleness consistency are identical to those guarantees by the strong consistency.
1. **Session** gurantees sees latest data within a session. 
1. **Consistent Prefix** guarantees order.
1. **Eventual** guarantees eventual data will be there, not neccessarly in order.

### Permissions
* Resource token allows apps to have controlled permissions. 
* SAS tokens good to give to third parties as you can control these, remove and expire if neccessary. 

### Container Types
* Mongo and SQL - Collection for container and document is item.
* Cassandra or table API - Table for container.
* Gremlin API - Graph for container and edge for item.

## Storage 

* Premium tier storage account is to store block and append blobs. 
* Gen purpose V2 used the most. 

### Audit and Monitor

* To audit API calls just tick Audit and Requests for the log. AllMetrics contains more. 
* You can set retention periods with sliders on these too. 
* Data lake gen 2 namespaces in metrics are account, blob, table, file and queue. If only inerested in one type don't use account as that will look at all. 
* Blob capacity average metric shows avery blob size. Combine this with blobtier dimension and can understand blob size per tier. 

### Retention Policy

* Rehydration of blobs from archive can take up to 15 hours. Larger files are better. 

### Posix Permissions
* Read only: 4
* Write only: 2
* Execute only: 1
* No access: 0
* Read and write: 4+2 = 6
* Read and execute: 4+1=5
* 640 permissions on a file...
1. Owner (6 - read and write)
1. Owner Group (4 - read only)
1. Everyone else (0 - no access)

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

* To access databricks you require access token

### Steps to Implement SSIS IR

1. Create Azure-SSIS IR in ADF
1. Install Self-hosted IR on prem SSIS
1. Register IR with reg key
1. Setup self hosted IR as proxy for Azure SSIS IR 

### Steps to Implement IR to talk to on prem

1. CReate self hosted run time in ADF UI
1. Install self hosted IR on local network

### Monitoring

* Send logs to azure monitor and alerts. Can be done in portal or ADF UI. 
* To keep data for more than 45 days configure diagnostic logs and send to storage acocunt, event hub or log analytics workspace. 


## Databricks

* Databricks CLI and Secrets API can only be used to create databricks db back secrets. not from KV.
* Databricks can only use kv secrets by going to e.g. 
https://northeurope.azuredatabricks.net/?o=8123456789#secrets/createScope
* Dropwizard is a hava library for azure databricks that will send logs to azure monitor.  
* THerefore requires third party to send to azure monitor
* Metrics stored in azure log analytics workspace
* By default databricks metrics available in ganglia

```
--lists content of path
dbutils.fs.ls("path")

--reads json file 
spark.read.json("path")

--copies files from one path to 2
dbutils.fs.cp("path1","path2")

--sets configuration property
spark.conf.set("path")
```

```py
dbutils.help()
dbutils.fs.help()
dbutils.notebook.help()
dbutils.widgets.help()
dbutils.secrets.help()
dbutils.library.help()
```

### Databricks read for ADLS Gen2

* Configure with spark.conf.set()
* Mount the file system

## Hub and Stream

* Time windowing...
* **Tumbling** - no overlap, fixed size. TumblingWindow(second,5)
* **Hopping** - overlap, fixed size. HoppingWindow(second,10,5). (unit,size,hop)
* **Sliding** - only get window when event occurs .SlidingWindow(second,10)
* [**Session**](https://docs.microsoft.com/en-us/stream-analytics-query/session-window-azure-stream-analytics) - only get window when events occur and window keeps growing if events happening. You can give limit to size of window too. SessionWindow(second,5,10). (unit, session timeout, window size limit). Stream analytics only checks window size at multiple of the window size limit, so windows can be longer than expected if don't check until that multiple... Session filters out times when no events occuring!! 

### Setup IOT Edge Steam on Devices to Minimise Latency

1. Create blob container
1. Create stream job with edge hosting
1. Configure blob as save lovation for job
1. Setup IoT edge environment on devices and add stream analytics module
1. Confifure routes in IoT edge

### Monitoring

* All admin operations signal logic to look for failed status.
* Runtime errors would show errors, but data would still be coming in so would not show unexpeced stoppages. 

## HD Insight

* Apache Ambari - YARN manager, monitor and configure
* Azure monitor logs to get consolidate dview of all hd insights clusters. All the logs can be sent to central consolidated view, this can't be seen in ambari.
* Azure monitor can have alerts that do email, sms, push, ambari can only do email or SNMP.
