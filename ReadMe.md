
## Exam Notes

* [General](##General)
* [Azure SQL](##Azure&#32;SQL)
* [Synapse](##Synapse)
* [Cosmos](##Cosmos)
* [Storage](##Storage)
* [Data Factory](##Data&#32;Factory)
* [Databricks](##Databricks)
* [Hubs And Streams](##Hubs&#32;And&#32;Streams)
* [Azure Data Explorer](##Azure&#32;Data&#32;Explorer)

## General

* Data migration assistant can help move from on-prem to aure sql. 
* Azure database migration service for large scale. 
* Azure monitor single space for all resource monitoring and alerts. 
  * Activity log is things that have happened in your sub like deletions and deployments. 
  * Create alerts with action groups to reuse. 
* Azure monitor logs - comes from log analytics workspace. free tier deletes after 7 days. 
* DB Managed instance - no basic metric, wais, timeouts, blocks, deadlocks, auto tuning. Onlu sql insights, errors, query stats
* Azure SQL Insight - uses mahine learning to provide continuous feedback. 
* Azure SQL analytics - monitoring solution for log analytics which provided advanced monitoring for multiple databases. 

## Azure SQL
[Back to top](##Exam&#32;Notes)

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
* **Basic Metrics** - CPU and DTU. 
* **QueryStoreRunTimeStatistics** - CPU usage and query duration. 
* **SQLInsights** - performance info and provides recommendations. 
* **DatabaseWaitStatistics** - time database spent on waiting. 
* **QueryStoreWaitStatistics** - resources tha caused queries to wait such as CPU, logs or locks. 
* **Errors**
* **Timeouts**
* **Blocks**
* **Deadlocks**
* Under intelligent performance in portal...
    * Performance overview - nice overvierw of any tuning required, recommendations, top 5 queries consuming
    * Performance recommendations - if there are any, whether they can be automated
    * Query performance insight - top queries by cpu, data io, log io, long running queries, custom queries
    * Automatic tuning - force plan, create index, drop index. These can be off, on or inherit from server. Recommendations and automatic tuning both found under server level. 
* Beyond the portal - DMVs, 
        * master db -  sys.resource_stats - every 5 minutes stuff goes here
        * other db 
            - sys.dm_db_resource_stats
            - sys.dm_db_wait_stats
            - sys.dm_exec ...
            - sys.dm_tran ...
    * Query Store - collects and keeps historic info. Enabled by default. 
        * query_Store_plan, query_store_query, query_store_query_text 

        ```sql
        select ...
        from sys.query_store_plan p1 
        JOIN sys,query_store_Query as q 
        ON p1.query_id = qry.query_id
        JOIN sys.query_store_query_text as t
         ON q. query_text_id = t.query_text_id
        ```   

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

## Synapse
[Back to top](##Exam&#32;Notes)
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

### CTAS

```sql 
CREATE TABLE dbo.TEST
with
(
CLUSTERED COLUMNSTORE INDEX
,  DISTRIBUTION = ROUND_ROBIN
) as select * from ext.test
OPTION (Label='CTAS@ MyTestLoad')
```

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
[Back to top](##Exam&#32;Notes)
* A client can change consistency level at conneciton time and for each request
* TTL -1 means indefinite
* TTL null inherits TTL from above
* HTTP 429 error is RU throttling
* You'd want a fairly straight line across your physical partitions RUw-ise and storage wise. If one partition RU is really big then not evenly distributed. 
* You can lookat availability actual vs SLA
* DIagnosic setting - hub, log analuytics, storage account again
* You can use migration tool from sql to cosmos [here](https://docs.microsoft.com/en-us/azure/cosmos-db/import-data)
* You can use the command line version too dt.exe and script your migration

### Consistency Levels
* Availability and latency increases but consistency decreases... 
1. **Strong** required all commit and synch to be done before any region sees the new value. 
1. **Bounded Staleness** - specify staleness windows. See strong within window and consistentp prefix outside.
1. **Session** gurantees sees latest data within a session. 
1. **Consistent Prefix** guarantees order that is all.
1. **Eventual** guarantees eventual data will be there, not neccessarly in order.

### Permissions
* Resource token allows apps to have controlled permissions. 
* SAS tokens good to give to third parties as you can control these, remove and expire if neccessary. 

### Container Types
* Mongo and SQL - Collection for container and document is item.
* Cassandra or table API - Table for container.
* Gremlin API - Graph for container and edge for item.

### Monitoring

* Under diagnostic settings...
* (log) DataPlaneRequests, MongoRequests, QueryRunTimeStatistics, PartitionKeyStatistics, PArtitionKeyRUConsumption, ControlPlaneRequests
* (metric)Requests

* Metrics nice blade with overview of data, index size and RUs consumed on map. 
* Throughput, storage, availability, latency, consistency, system.

### gremlin (graph)...

* Vertices - discrete event, place or event etc
* Edges - relationships usually verbs
* Properties - vertices or edges, commonly adjectives

### example gremlin queries

```
 "g.addV('person').property('id', 'john').property('firstName', 'John').property('email', 'john@test.com').property('city', 'Boston')",

"g.V('john').addE('knows').to(g.V('reza'))",

"g.addV('cellphone').property('id', 'androidphone').property('os', 'Android').property('manufacturer', 'LG').property('city', 'Toronto')",

"g.V('reza').addE('uses').to(g.V('androidphone'))",
```

* below is how to query the data you have added

```
g.V()
g.V().count()
g.E().count() 
g.V('john')
g.V('john').out('knows').hasLabel('person') 
g.V('john').out('knows').hasLabel('person').values('id')
g.V('john').out('knows').hasLabel('person').count()
```

## Storage 
[Back to top](##Exam&#32;Notes)
* Premium tier storage account is to store block and append blobs. 
* Gen purpose V2 used the most. 

### Audit and Monitor

* To audit API calls just tick Audit and Requests for the log. AllMetrics contains more. 
* You can set retention periods with sliders on these too. 
* Data lake gen 2 namespaces in metrics are account, blob, table, file and queue. If only inerested in one type don't use account as that will look at all. 
* Blob capacity average metric shows avery blob size. Combine this with blobtier dimension and can understand blob size per tier. 
* Block blob and file storage need premium and premium only has these
* 1MB is the entity size limit for table storage

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
[Back to top](##Exam&#32;Notes)
* **Pipelines** are what you execute and run, your workflow. 
* Three activity types - data movement, transformation and control activities. 
* **DataFlow** mapping and wrangling.
* **Dataset** the format and a location of data. e.g. a table.
* **Linked Service** is the connection string essentially. The location and auth. 
* **Integration Runtimes** Azure, Self-hosted and Azure-SSIS
* **Triggers** schedule, interval, event driven. 
* **Templates** - lots of templates out of the box in adf. 

* To access databricks you require access token
* You can execute a pipeline in another pipeline
* You can run azure data explorer kusto commands in data factory
* You can run batch service in data factory 
* To link to key vault you need to create a linked service to key vault 

* Azure IR - public network, responsivle for flows data movements and activity. This is default IR. 
* SSIS IR - supports SSIS on public and private network.
* Self-hosted - public and private. 

### Steps to Implement SSIS IR

1. Create Azure-SSIS IR in ADF
1. Install Self-hosted IR on prem SSIS
1. Register IR with reg key
1. Setup self hosted IR as proxy for Azure SSIS IR 

### Steps to Implement IR to talk to on prem

1. New dataset - SQL server
1. New Linksed Service - connect via new integration runtime and not the default autoresolve
1. Create self hosted run time in ADF UI
1. Install self hosted IR on local network

### Monitoring

* Send logs to azure monitor and alerts. Can be done in portal or ADF UI. 
* To keep data for more than 45 days configure diagnostic logs and send to storage acocunt, event hub or log analytics workspace. 
You can send to log analytics - ActivityRuns, PipelinesRuns, TriggerRuns, AllMetrics.
* Alerts can be for failed pipelines, activities etc, cancelled, IR memory available

### Dynamic Content Examples

* Example of using outputs from activities...

 ```
select * from [Sales].[Orders]
where orderid > @{activity('LastInsertedId').output.firstRow.LastInsertedId} and orderid <= @{activity('MaxID').output.firstRow.MaxId}
 ```

 * Example to reference a parameter (called container here)

```
@pipeline().parameters.container
```
* List files from output of get metadata
```
@activity('GetFileList').output.childItems
```
* Filter transformation examples... 

* only files not folders
```
@equals(item().type, 'File')
```
* file name starts with fruit
```
@startswith(item().name, 'fruit')
```
* You can create reustable datasets with parameters, e.g. for storage account and then just put the below in relevant locaiton
```
@dataset().folder
@dataset().file
```
* Using foreach loop and getting output from previous task in a loop
```
@activity('FilterAvroOnly').output.value
```
* then in the loop you can reference the file name using this
```
@item().name
```

## Databricks
[Back to top](##Exam&#32;Notes)
* Databricks CLI and Secrets API can only be used to create databricks db back secrets. not from KV.
* Databricks can only use kv secrets by going to e.g. 
https://northeurope.azuredatabricks.net/?o=8123456789#secrets/createScope
* Dropwizard is a hava library for azure databricks that will send logs to azure monitor.  
* THerefore requires third party to send to azure monitor
* Metrics stored in azure log analytics workspace
* By default databricks metrics available in ganglia
* Spark can be used for batch and stream
* Hadoop mapreduce only for batch
* Scala, python, R, SQL, Java APIs
* Spark native MLib machine learning built in
* You install libraries on your cluster for additional functionality e.g. event hub stuff
```
com.microsoft.azure:azure-eventhubs-spark_2.11:2.3.12
```

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

## Hubs And Streams
[Back to top](##Exam&#32;Notes)

* in order to test reference data you need to upload sample! 

* **Time windowing...**
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

## Azure Data Explorer
[Back to top](##Exam&#32;Notes)
* Query event hubs!
* If streaming not in batches like every 5 minutes
* ARM template fro cluster and database
* Kusto language for creating tables etc and you can deploy this via azure devops too [see here](https://docs.microsoft.com/en-us/azure/data-explorer/devops)
* Permissions done by RBAC. 
* You can use data factory with azure data explorer too. 
* when you stop and start cluster it bring everything back in from where you paused it...! nice! 

```kql
.create table fruittab (FruitName:string,FruitColour:string,Date:datetime,StandardLog:dynamic)
```

```kql
.create table fruittab ingestion json mapping 'fruitmap' '[{"column":"FruitName","path":"$.name","datatype":"string"},{"column":"FruitColour","path":"$.colour","datatype":"string"},{"column":"Date","path":"$.standardLog.dt","datatype":"datetime"},{"column":"standardLog","path":"$.standardLog","datatype":"dynamic"}]'
```

```kql
fruittab 
| order by Date desc 
```

## Azure batch
[Back to top](##Exam&#32;Notes)

* aztk azure distributed data engineering toolkit built on top of azure batch
* low priority vms to save money maybe
* provision on demand spark clusters and run spark jobs 

## HD Insight
[Back to top](##Exam&#32;Notes)

* Apache Ambari - YARN manager, monitor and configure
* Azure monitor logs to get consolidate dview of all hd insights clusters. All the logs can be sent to central consolidated view, this can't be seen in ambari.
* Azure monitor can have alerts that do email, sms, push, ambari can only do email or SNMP.