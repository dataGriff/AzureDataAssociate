
## Data Integrate Services on Azure

* Combine data into meaningul and relvant data
* ETL or ELT
* Migrate transform store
* Azure Data Factory. Over 80 connectors. 
* Data migration assistant can help move from on-prem to aure sql. 
* Azure database migration service for large scale. 
* Copy data tool part of ADF. 
* Hubs - real time ingestion. 
* Azure stream analytics - serverless real time analysis platform. 
* Databaricks - collaborative apach spark analytics service. 

## Migrate on prem to Azure

* Data migration assistant. 
* Assessment project type.
* Migration project type. Can migrate schema and data. save for later or do immediately etc.

* ADF...
* One or more pipelines
* Pipeline logical group of activities
* Activities managed as a set. they are action to perform on data. Ingest transform store.
* Pipeleines
* Three activity types - data movement, transformation and control activities. 
* Data movement included copy data activity. 
* Many data movement connectors.
* Transform - hive, pig, mark reduce, spark, data bricks
* Control - e.g. foreach. web.
* Activities can be serial or parallel. 
* Dataset - named view that references data to be used in activity. table, file... 
* Linked service first. these are connection strings. 
* Dataset used linked service. 
* Activities, datasets, linked services. 
*Integration runtime bridge between activities and linked service. Azure, self-hosted and azure-ssis. 
* Azure IR - public network, responsivle for flows data movements and activity. This is default IR. 
* SSIS IR - supports SSIS on public and private network.
* Self-hosted - public and private. 

* source table, watermark table, destination table

* Install integration runtime...
   * New dataset - SQL server
   * New Linksed Service - connect via new integration runtime and not the default autoresolve
   * Choose self-hosted integration runtime
   * Create, Choose express setup (1) and will download IR on your machine, you then run this. 
   Manual setup option (2) involves authentication keys and registering them. 
   * Once installed your IR you choose this in your linked sevice, the set server name and database and authentication like normal.

   * In your dataset you can add dynamic content based on variables, there are a lot of system variables built in. So you can make the query dynamic if use one. 

   * Copy data task. copy between two data sources. source and sink datasets. 
   * You can create queries that access pipeline query proerties. 
   * You can refer to the pipelines activity in the text below and then firstRow if you have chosen that, e.g. for id ranges. 
   * You can edit your sink and import schema from your source by choosing that in sink.
   * Have a stored procedure that updates the incremental watermark. 
   * Need to add parameters for latest id updated and also the table name. 

   ```
select * from [Sales].[Orders]
where orderid > @{activity('LastInsertedId').output.firstRow.LastInsertedId} and orderid <= @{activity('MaxID').output.firstRow.MaxId}
   ```

## Amaxzon S3 to Blob

## Pipelines in ADF Copy Tool

## Pipelines with ADF

## Realtime Pipelines with Hub and Stream

## Real Time Monitor Power BI