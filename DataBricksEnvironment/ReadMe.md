## Implement Azure Data Bricks Environment

* Based on apache spark
* Native integration with other azure data services
* Can ingest lots of different data
* Provide to many different business users
* R, SQL, Python, Scala, Java
* Dataframe conceptually equivalent to table
* Streaming - kafka
* Maching learning
* GraphX
* Interactive analytics, data integration, machine learning, stream processing...

* Fundamental componenents....
    * Workspaces, clusters, notebooks, tables, jobs
    * Workspace - organizes all of databricks assets. Control access via workspace access control. Folders contain all assets. Can apply access control. Workspace, shared and users are special folders. 
* Premium - allows you to have RBAC within data bricks so will need this in prod!! maybe everywhere? 
* All tiers now do private vnet it seems. 
* Go to admin console and add users, admin users can create clusters
* Create AD group for data engineers, create group for data scientists, and admins!
* Then just create folders for data scientists and data engineers :)
* Permission settings on folders - run, read, edit, manage. Manage is permissions too so probably don't want data scients looking at this. 

* Azure CLI - need to generate user token first
* See [demo01 file](demo01.md)

* Spark Clusters - !!2 types!! - interactive clusters for analysis. job clusters for regularly run jobs. 
* 2 different cluster modes - standard (single user) and high concurrency (multiple users). high concurrency used to be called serverless and doesn't support scala
* Premium cluster allow ad authentication passthrough
* Autoscaling and auto terminate after x minutes
* we want standard cluster for etls, maybe 2. one for small loads during the day and big one for overnight? we want high concurency for data science https://docs.microsoft.com/en-us/azure/databricks/clusters/configure. Maybe one for production models too? 
* you can also tag clusters
* You can use JSON with azure CLI to create clusters...

* Notebooks...
   * You can import notebooks from links
   * %sql %md %scala %html to choose language per cell
   * Made up of cells you can run

 * Databricks tables
    * Collection of structured data
    * Cached, filtered, queried and more
    * Temp structure on a cluster
    * Under data in databricks you can have a look at the tables...

* Apache Spark Jobs
   * Run workflows interactively or scheduled
   * Set time to run, what cluster, use new cluster for prod jobs
   * You can use CLI with JSON to create a job

## ETL with Databricks

* Refine data to what you need...
* Use language of choice, load into data service you need
* Storage for intelligent applications
* Rerun etl from raw if need to get data again

## Scoring Apache Spark ML 

* Machine learning - use patterns and inferences to make decisions/predictions
* Apache Spark Machine LEarning LIbrary
   * ML ALgorithms
   * Featurization
   * Pipelines
* Import org.aphache.spark.ml... in your notebooks
* Batch scoring is scheduled even if data can be streamed
* Build batch scoring model pipelines in data bricks
* See [demobatchscoringml file](demobatchscoringml.md)
* When you create job json you can amend the variables in the job configuration

## Streaming Kafka

* Kafka on HDInsights
* Event > Topic (HDInsights Kafka cluster and brokers) > Event Consumer
* Event consumer can be databricks
* HDInsight > Cluster Type Kafka.. > put on vNet > use storage account > cluster size 
* Apache Ambari > Kafka configuration > restart to get kafka to pick up new config
* Create databricks > new cluster 
* vnet peering in data bricks and add another vnet. then go into the actual vnet and sort peering in there. 
* spark.readstream.format("...")
