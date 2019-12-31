## Intro to NoSQL

* Scaling horizontal easier for NoSQL
* So cheaper to scale out across cheap machines
* No need to define schema first
* Availability is easier with NoSQL
* IaaS - and install own NoSQL
* PaaS - managed NoSQL. Azure Storage, cheap. e.g. Data Lake gen 2. CosmosDB.
* Storage - blob, table, queue and file. 
* Cosmos - SQL, Table, Mongo, Cassandra and Graph APIs. 
* JSON docs - SQL or Mongo API
* Key-value - table API
* Wide-columns - cassandra API
* Graph - gremlin API

## Azure Storage Accounts -Table, Blob, Queue and File

* Use Gen Purpose V2 now for storage accounts
* STandard (most cases) or premium
* Block blob and file storage need premium and premium only has these
* Azure Table Storage is focus here. 
* Good for TB of webscale applications, not good for relational stucture
* Fast on clustered index retrieval, OData protocol and LINQ via SDK. 
* Storage Account > Tale > Entity > Properties (name-value pairs)
* 252 properties allowed
* Mandatory properties are partition key, row key and timestamp
* PartitionKey and RowKey - developer responsible for inserting these
* 1MB is the entity size limit. 
* Comsos DB Table API is the premium offering for Table Storage
* Blob storage - docuument url direct to browser, audio and video, store backups, analysis raw data can be kept here, log files, files for distributed access. 
* Queue storage - backlog for asynch work, pass messages from web role to worker role.
* Azure file storage - accessible bia Server Message block (SMB) protocol. Lift and shift from on-prem.
* Secure storage account... 
   * RBAC roles - owner, contributor, reader etc. 
   * Storage Account Keys for access (but don't want to give these out), these can rotate
   * or SAS (sas means can give for limited time)
   * Restrict to HTTPS
   * SAS tokens have option to enforce HTTPS too
   * ENcryptio at rest - SSE on and can't turn off. This is at rest storage service encryption.
   * Client-side encryption using storage account SDK
   * Audit/Monitor 
   * CORS for browser-based clients. Allow origins, methods and headers so your web app can work. You can whitelist allow origins of request. 
   * SAS token can have specific permissions on blob, file, table queue, read, exec etc, can whitelist specific IPs on that SAS, can specify key want to initially validate the SAS token. 
   * SAS token gives URL for all the types - table, blob, file

## Azure Comsos DB Overview

* Globally distributed, can add any region with click in portal
* Means highly available
* Conn string always stays the same even when regions added or failover etc
* Traffic manager can redirect to appropriate app service and then multi-homing states which cosmos db to go to
* Time to live (TTL) - can set it on data items and at account level
* Data consistency - consistency vs availability & latency on the other
* Strong > bounded staleness > session > consistent prefix > Eventual Consistency
* Consistent prefix - reads never see out of order writes!
* Bounded staleness - specify staleness windows. See strong within window and consistentp prefix outside.
* Session - within same session strong. Other sessions see consistent prefix.
* Eventual COnsistency - will see latest data at some point, could be out of order. 
* Partitioning - choose key that has wide range of values so spread evenly. Partition key should be included in filtering. 10GB is max size of partition. 
* SQL API recommend for new APIs. 
* Mongo, Cassandara, Table, Gremlin APIs. 
* Security - RBAC, firewalls, VNet and NSGs. CORS can be set. Read only and read-write keys. 

# #SQL API

* Container - unique scalability unit throughput and storage
* SQL API - container is called collection
* You can query nested properties in the JSON too with SQL syntax
* Azure Csomsos DB Client Lib 3.x for .Net
* Unique constraint is only per partition
* You can use migration tool from sql to cosmos [here](https://docs.microsoft.com/en-us/azure/cosmos-db/import-data)
* You can use the command line version too dt.exe and script your migration

## Mongo API

* Stored in JSON format
* Mongo API - container is called collection
* Can migrate to Mongo while preserving most of its logic
* Pre-migration steps - create account, estimate workload, pick optimal partition key, understand index policy
* RUs affected by size, complexity of queries
* Writes are more RUs than reads
* Can use [capacity calcualtor](https://cosmos.azure.com/capacitycalculator/) for comsos db
* Can change container specific RUs in seconds
* Turn of indexing is recommended when migrating
*  Can't create unique index when data already in there
* Use cosmos data migration tool
* Post-mig steps - connect app, optimize index, consistency and globally distribute

## Data Migration Service

* Before can use data migration service you need to register it on your subscription, you can see this under Resource Providers of your subscription. 
* Azure Database Migration Service is then available in marketplace. 
* Azure database migration service need a vnet
* You can amend the number of cores if lots of data
* Lowest standard tier is free
* Create new data migration project then within your service
* You can choose online or offliine migration (offline means data unavailable during migration)
* Each migration run is called an activity
* Data migration service goes from non-azure to Azure 

## Table API

* Table is the container
* Comsos has lower latency, throughput and global distribution than table storage. 
* Cosmos db can have indexes on all properties
* Different consistency levels in cosmos
* Better SLAs in comsos
* Query by using LINQ, OData, partition key and row key. 
* Need recent azure.cosmos.table sdk
* Can use data migration tool to move data from table storage into cosmos
* Table storage reader can just change conn string to point at cosmos

# Gremlin (Graph) API

* Gremlin container is a graph
* Property graph - this is the vertices and edges model.
* Graph dbs model relationships and properties
* Vertices - discrete event, place or event etc
* Edges - relationships usually verbs
* Properties - vertices or edges, commonly adjectives
* Social networks, recommendation engines, IoT
* Gremlin is graph traversal language
* Can interact with gremlin SDK, gremlin console, or azure portal, can use sql to query gremlin API
* In data exlorper can see in json or graph
* Some example gremlin below, can see vertices with properties and the edge between two vertices

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

## Cassandra API

* DB is key space... collection is table
* Wide-column db
* Names and format of columns can vary from row to row in same table
* CQLv4 compliant drivers
* CQL = Cassandra Query Langugage
* CQLShell, data explorer, SDK CassandraCsharpDriver

## Data Lake Gen 2

* Hyper scale for big data analytics
* Can be accessed from Hadoop Rest APIs
* Gen 1 and Gen 2 (combines blob with gen 1)
* ADLS gen 2 has hierarchical namespace
* Gen 2 POSIX permissions are supported
* Gen 2 Lower costs
* you can use azcopy.exe to copy files up to data lakie gen 2. 
* or just use azure storage explorer