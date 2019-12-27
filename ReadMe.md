# Stuff To Review

### Terms

* ITSMC - IT Service Management Connector 
* Apache Ambari - monitor HDINsight Hadoop Clusters
* DropWizard - send metrics from databricks to azure monitor.
* Log4J - send application logs to Azure Monitor

### Azure SQL

* Learn what Always Encrypted Is, do it
* Do some dynamic data masking
*

### Databricks
* Blog: Securely connecting to lake with vnet and key vault
   * [Data bricks key vault](https://docs.microsoft.com/en-us/azure/azure-databricks/store-secrets-azure-key-vault)
* Blog: Connect Databricks to data factory
   * [Databricks Data Factory](https://docs.microsoft.com/en-us/azure/data-factory/solution-template-databricks-notebook)    
* Go through file utilities [File Utils](https://docs.databricks.com/dev-tools/databricks-utils.html)
* Ganglia - metrics for databricks
* Databricks suppoers sending log data to azure monitor
* Metrics will be stored in Azure log analytics workspace


### Stream

* Blog: difference between the stream windows outputting to power bi
* SUs does not need configuring with edge, only in cloud. #
* IOT edge good to run directly on devices as less latency.
   * Ceate Azure blob storage container
   * Create stream anlytics job with edge hosting
   * Configure blob storage container as save location for job def
   * Set up IOT edge environment on IOT devices and add stream module
   * Confgiture routes in IoT edge


### Data Lake Gen 2:
* Blog: Retention policy applied to lake
* [Performance](https://docs.microsoft.com/en-us/azure/storage/blobs/data-lake-storage-performance-tuning-guidance)
* [Permissions](https://docs.microsoft.com/en-us/azure/storage/blobs/data-lake-storage-access-control)
   * ACLs in POSIX compaitble format
   * Read Only (4)
   * Write Only (2)
   * Execute Only (1)
   * No Access (0)
   * Read and Write: 4 + 2 (6) 
   * etc

### SQL Synapse

* DMVs - write them up e.g. sys.dm_pdw_exec_requests
sys.dm_pdw_exec_sessions
sys.dm_pdw_request_Steps
sys.dm_pdw_sql_requests
* Blog labels in PDW queries

### Azure SQL

* DMVs - e.g. sys.dm_db_resource_Stats. sys.dm_db_resource_Stats
* QUery store DMvs?? 
* Replication DMVs? 
* What is SQL data sync? Can we use this for reference data? 
* sp_wait_for_Database_copy_sync procedure...
* sys,geo_replication_links view 
* Create a security filter etc for row level security
* Need to do some always encrypted examples with random and deterministics
* Do a memory optimized table and look at how to monitor these in portal and dmv
* Do dynamic data masking examples
* Configure key vault in failover group make sure do secondary first
* Query Performance Insights?
* SQL Database Advisor - perf recommendations blade on SQL db. Review recommendations for indexes. 
* Query Performance Insight - view most resources consumed and longest run but nothing about indexes.
* Azure Advisor - recommendations for availability, security, performance and cost and indexes.
* [Look at Metrics](https://docs.microsoft.com/en-us/azure/sql-database/sql-database-metrics-diag-logging)
   * SQLInsights gathert performance information and provides recommnedations
   * QUeryStoreRunTimeStatistics provides info about CPU usage and query duraiton
   * Basic metric provides CPU and DTU usage and limits. 
   * QueryStoreWaitStatistcis provides info on waits, CPU, logs or locks
   * DatabaseWaitStatistics provides time spent waiting. 


### Cosmos

* Have a look at example queries for each API

### Data Factory

* Do course
* Set up self-hosted integration ruintime
* go through this
[Cathwilhelmsen](https://www.cathrinewilhelmsen.net/series/beginners-guide-azure-data-factory/)
* Azure data box is for offline transfers only. 40TB for normal and 1 PB for heavy.
* Install data factory integration runtime on vnet to transfer securely from things already on vnet. 
* To prepare for SSIS to integrate with the cloud...
   * Create an Azure-SSIS IR in ADF
   * Install self-hosted IT in on-prem SSIS
   * Register self-hosted IR with auth key
   * Create linked service in ADF with blob storage (Acts as stagin)
   * Setup self-hosted IR as proxy for Azure-SSIS IR

### Web Job

* Get console app in the cloud

### HDInsights

* Apache amabari provisions, monitors and manages hadoop clusters. 
* Can send metric to Azure Monitor Log Analytics. 
* Azure monitor can only monitor gateway request, active workers and scaling requests, so use Apache ambari instead. 
* HDinsights cluster rest API can be for creting new, deleting and changing confiugraiton. Not for monitoring. 

