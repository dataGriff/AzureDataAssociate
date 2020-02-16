Blob storage - each account can hold up to two PiB of data

Data lake suports data of any type and size

File sizes in data lake recommended to be 256 MB and 2 GB in size

HBase in HDInsight is a column-family database, stored in denormalized format
and data typicallly in key order

General Purpose v2 Storage - RA-GZRS provides most redundancy. RA-GZRS means 
availability zone copies in same region are available even before failing
over to secondary region

SQL DB - automated backups only up to 35 days
Long term back ups for up to 10 years

SAS only allow you to administer one of the services within account table, blob or file. 
you cannot administer the account.
you can use primary or secondary key to administer whole storage account

User delegation key is based on a user AD account and the key you provide. Can be fine-grained
security through RBAC. All based on SAS so only at service level. You can revoke
or set expiration date on SAS.

SAS does not support azure file (SMB) access.

Default firewalls on Azure SQL are only access via portal. Not over internet or 
other azure services. 

You need 1433 open on your machine to connect to Azure SQL. 
You can configure server or db level firewalls to allow access to azure sql.

APp service plan requires manual scaling for azure functions
Consumption or premium plan supports auto scaling

Livy triggers HD insights jobs, REST API that allows remote trigger of spark jobs
Beeline to monitor HD insights job, thin jdbc clin of hive server

Cosmos...
Bonded stalness - you can configure staleness of read with versions behind comitted versio
or time behind committed version. Second strongest consistency level.
Session consistency ensures same client always reads same data
Consistent prefix means never see out of order writes
Strong consistency level gurantees an RPO of 0 and an RTO of less than 1 minutes
RPO - recovery point objective
RTO - recovery time objective

event hib supports HTTPS and AMQP

Managed instance to audit logs to blob storage account
Generate SAS - T-SQL create credential - T-SQL server audit

HDINsight pig activity provides on demand HDINsight cluster and removes resources when no longer needed
Databricks notebooks do not support on-demand processing environment

dmanager - sql role in azure to create new databases
db_accessadmin - add or remoove access to a database

From databricks you can not use SQL, R or scala to access blob storage!!!

Default backup retention polict for Azure SQL is 35 days!!

archive file storage must be there for at least 180 days to avoid early deletion charge
can take up to 15 hours to get out of archive

cool storage must be kept for at least 30 days to avoid early deletion charge

blob file access only offering that support anonymouse public access

standard SSD supoprts up to 6000 IOPS across multiple drivers
HDD is limited to 2000 IOPS

ULTRA SSD should be at least 128GB and 38,400 IOPS

Scala 0 OO langugage that supoprt static type checking
Python is OO langauge athat uses dynamic tyoe checking

Only Azure files support SMB protocol access!


Azure Database migration service - on prem to azure MySQL migration

MySQLWorkbench to migrate a schema, then use mysqldump to restore the data.
This process best suited to small databases. 

Data Migration Assistant (DMA) only supports SQL Server to Azure SQL.

distcp allows copuing between two regional locations and can copy deltas

sqoop is used to copu from Azure SQL to Data Lake Gen 1

Pinning a cluster in databricks ensures it is retained for 30 days after termination

GZRS - does not suport blob storage

when you regenerate an access key the followng are affected
Shared Key, Service SAS, Account SAS
User delegated SAS is not affected!












