
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

* raw, stream, stage, curated in storage account. 
* In S3 get access key which is what you need. 
* Create copy data task in pipeline. 
* Create source and sink dataset. 
* Remember to go to schema and import schema on dataset. 
* On source you can check recursively so wil do all subfolders and max concrrent connections will mean how many threads will do in parallel. 
* In sink choose container of of storage account. 
* Pipeline debugging - breakpoints, debug complete pipeline or subset of activities, breakpoint - debuguntil feature.
* Debug only want to do a subset of data. 
* When debug can click on output tab of pipeline and see input and output of individual tasks in json  

## Azure Data Factory Copy Data Tool

* To do copy data manually you create copy data activity, create datasets and create linked services. 
* Copy data tool speeds this process up and when want to a once only copy. 
* Copy data tool use to quickly create copy pipeline then edit this yourself to make better. 
* Use copy data wizard on homepage of ADF. 
* Run once or on a schedule.
* Configure data stores with a linked service. 
* Data Interation Units (DIU) in settings. This is what data factory uses. 
* The copy data pipeline then looks like a regular pipeline that you can edit. 
* Tick binary copy to deal with avro... 
* Trigger didn't seem to work until did one manually then copy did work.. for avro from ehub. 

## Pipelines with ADF

* You can add parameters to your pipeline
* to reference parameter

```
@pipeline().parameters.container
```

* Get metadata task allows you to get the list of files when you choose argument "Child Items". 
* Get metadata can get files or folders. 
* You can pass the list into a filter transformation for example and only pick files, no folders, or specific files

```
@activity('GetFileList').output.childItems
```
* only files not folders
```
@equals(item().type, 'File')
```
* file name starts with fruit
```
@startswith(item().name, 'fruit')
```

* Create reusble dataset by parameterising. So you can create a reusable data lake gen 2 dataset and just parameterise file and folder... CHoose binary as file format when setting up generic dataset. 
* Create linked service to something already setup, then change file path and file name to be dynamic content. These need to be supplied every time dataset created then. 
* Remember this linked service is stil the one storage account
* So would need raw generic, curated generic and partner data generic

```
@dataset().folder
@dataset().file
```

* Get snapshot of files using get metadata activity
* Move files once
* Use staging container in this demo
* There is a prebuilt template in ADF to do this
* Use foreach 
* Not native way to move files. Move is copy and delete (we wouldn't want to do this neccessarily - that is delete from source.)
* when you use foreach you double click into it and has its own mini activity pipeline

* so going into foreach loop with file list...

```
@activity('FilterAvroOnly').output.value
```

* then in the loop you can reference the file name using this

```
@item().name
```

### Databricks with ADF

* Have note book in data bricks
* Add note book activity
* connect to your data bricks, generate a token in data bricks and add in data bricks connection in ADF. Choose cluster. 
* databricks cluster will start automatically when you kick off your ADF pipeline :) 
* also you get a lovely url that takes you to notebook and you can see the errors :) 
* you can click on the little glasses to go see details of notebook running

* web activity can be used to call logic app. 

* you can deploy data factories with .net, powershell, arm template, python, rest api

## Realtime Pipelines with Hub and Stream

* in order to test reference data you need to upload sample! 

## Real Time Monitor Power BI