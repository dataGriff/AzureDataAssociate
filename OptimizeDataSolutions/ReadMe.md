
## Core Optimzation Concepts

* Consumption - storage (Capacity, throughput, distribution), compute (CPU, nodes, scale), transactions (quotas, number, work), resiliency (replicas)
* SKUs related to features as well as performance
* Make things bigger - just spending more money!
* Optimize - reduce cost without sacrifce performance, increase performance without spending more
* DBs - execution plans, indexes, reduce data being operated on, partition data, optimize query, reduce unneccessary data.
* Can resources be shared?
* Can we use in-memory technologies? 
* Partition to distribte over multiple compute and storage
* Could even partition to more secure areas
* Parallel writing to partitions

## Optimize Azure SQL and DW

* SQL Db - DTU and vCore models. 
* DTU compute and storage increase linearly together. 
* Managed instance can only use vcore. 
* General purpose (standard) vs business critical (premium)
    * General purpose - compute and storage separated. blob storage. one active, anyting happens a spare will take over. data and logs in blob. 
    * busines critical - SSD storage, primary read write and optional read only endpoint. 4 nodes in an availabilty group. backup files go to blob. 
 * Serverless and hyperscale. 
    * Serverless - can be paused or even auto paused. Can only be used for single databases. Serverless asks for max and minumum number of vcores as will scale automatically. will then pause during inactivity that you set. 
    * Hyperscale - very large DB workloads. Supports multiple read-scale replicas. 
 * Database sharding - horizontal scaling. Data structures identical across all dbs.    
 * Indexing and tuning - you can use automatic tuning in azure. you can tel it to create or drop during specific times. validation is performend after does auto tune. This is under intelligent performance. on sql server or per db. Still need to maintain indexes in azure. 
 * Estimated and actual execution plans. 
 * Query store dmvs are good to look for regression, performance, all sorts. Query performance insights under intelligent performance lecerages query store and gives good metrics. 
 * In-memory tables, increase number of transactions and latency with OLTP. Only available in business critical/premium tiers only. Hight premium SKU get more memory in in-memory tables. Right click table and you can get memory otimisationa advice. 
 * Compression can be looked at table > storage > compression advisor. or index. Disk performance improved as more cpu is used with compression. You can click calculate to see how much compression would save. 

 * Azure SQL DWH. Compure and storage separated/
 * Compute memory and IO are DWUnits
 * DW can be paused 
 * Scales horizontally. compute nodes ranges from 1 to 60.
 * Data is always distributed over 60 distributions, always, even with 1 node. 
 * Hash, round robin and replicated. 
 * Partition poorly may only use one of the 60 distributions if all one the same partition hash.
 * Optimize data load - polybase import. 
 * DMS - data movement service. 
 * BCP will be bottle necked by control node, so polybase best.
 * Round robin for staging, unless hash then subsequenly improves load into proper dim/fact. 
 * Use high resource class when importing. Want minimum million rows to compress really. Ensure good partition key to distribute over nodes. 

## Optimize Cosmos DB

* Requests unit - provisioned at account or collection. 
* RUs provisioned per second. 1KB read, 4KB write. 
* RUs provisioned go to each region when replicate. 
* Number of RUs are returned in the reponse header. 
* Settings of container includes RUs, indexes, conflict resolution.
* Cosmos db parition key, hash value generated for partition key, make a lot of logical paritions is what you want. Good distribution is what you need. 
* Picking partition key very important. 
* Create compiste partition key if need to. 
* Don't be afraid of too many partition keys.
* CosmosDB is schemaless!
* Cross-partition queries are not good. 
* Group things together with similar access pattens.   
* Normalisation not a priority in cosmos.
* May need to duplicate data to satisfy different query patterns. Balance extra writes vs benefit over reads. 
* Change feed can be used to duplicate data based on update/insert. Different read heavy pattern scenarios so duplicate data to save RUs (cross partition queries)
* Use TTL to remove unrequired data. 

## Optimize Data Services

* Data lake gen 2 tuning. 
* Want parallel reads and writes... 
* Data size (may want to create bigger files from small ones) and naming of files. 
* Ensure lake is in same region as service utilizing the lake. 
* Stream analytics...
    * Default limit on SUs per region
    * 80% signifies need to do something
    * Most optimal scenario is embarasslingly parallel
    * Time policies can also be used to handle time skews

