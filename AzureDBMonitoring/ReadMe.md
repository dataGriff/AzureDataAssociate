
## Key Concepts

* Monitoring critical to database services
* Undetected db problems can mean whole organisation can suffer
* Performance compared to baseline
* SLA adherence
* Spikes and unexpected errors
* Capacity - storage and quotas
* Types of monitoring - metrics, logs, alerts
* Sources - sub/tenant, resources, guest OS and gues apps
* Azure monitor - single space for all resource monitoring, pick a specific resource want to look at, multiples in one
* Azure monitor logs, log analytics in azure monitor. Use kusto. 
* Diagnostic log settings, one or mote types of log, can be sent to three possible targets
    * storage account - long term cheap retention
    * log analytics - retention and rich analysis, can move to blob later
    * event hub - SIEM could subscribe to this then
    * pick these in diagnostic settings
* SQL Pool Pool only has basic metrics available
* SQL DB has lots of different diagnostics availabl

## Implement Monitoring SQL DB and DW

* Start in the portal... then move to more advanced tools. 
* SQL DB - sql isnights, auto tuning, waits, errors, basic, deadlocks, blocks, timeouts
* DB Managed instance - no basic metric, wais, timeouts, blocks, deadlocks, auto tuning. Onlu sql insights, errors, query stats
* DWH - Dmsworkers, execrequests, requeststeps, sqlrequests, waits, basic.
* Azure SQL Insight - uses mahine learning to provide continuous feedback. 
* Azure SQL analytics - monitoring solution for log analytics which provided advanced monitoring for multiple databases. 
* Log analytics intelligent solutions doesnt work for SQL DW
* Query Performance Insight (query store enabled and not available on DWH) and SQL Database Advisor to aid with query performance
* Under intelligent performance...
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

        ```
        select ...
        from sys.query_store_plan p1 
        JOIN sys,query_store_Query as q 
        ON p1.query_id = qry.query_id
        JOIN sys.query_store_query_text as t
         ON q. query_text_id = t.query_text_id
        ```     
    * Extended events (XEvents) - lightweight profiler.   Go to extended events on db, new wizard.. or can use sql script.  Write out to file in storage account.    


## Cosmos Monitoring

* Optimise partitioning and identift RU exhaustion.
* Containers > Items. Items logically partitioned by partitioned key. 
* Go to azure monitor and look for cosmos db instances. 
* Better view in comsos db metrics itself. 
* Tabs of overview, throughput, latency, storage, availability, consistency, system
* http 429 is error need to look for - throttling!!!
* You can look at specific container for partition key range specific metrics in same area. 
* You'd want a fairly straight line across your physical partitions RUw-ise and storage wise. If one partition RU is really big then not evenly distributed. 
* You can lookat availability actual vs SLA
* DIagnosic setting - hub, log analuytics, storage account again
* Logs - dataplanrequests and query run time stats for all APIs. then other logs are specific to APIs. 
* Pretty much metrics in portal is all you need for cosmos db. 


