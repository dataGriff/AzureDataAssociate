

## General
* Singkle, pool, managed instance
* DTU or VCore
* MS recommends VCore model
* If use over 200 DTUs then vCore might be better than DTU model
* Managed instance can ony use vCore not DTU model
* Basic, Standard and Premium DTU models
* IO gets better >>>
* Columstore only S3 and above
* General Purpose, Hyperscale, business critical for vCore Model
* No hyperscale in DTU model

## Singleton and Elastic Pool

* FOr VCore model compute tier can be provisioned (based on what you configure) or serverless whyich autoscales based on vcores used...
* Compute generation is Gen4 (24 cores, 168 GB mem) or Gen5( 80 cores, 408 memory).
* Pool share resources on single server, prevents over provisioning
* Can move singleton into pool
* Good for generally low with spikes
* When use serverless compute tier the number of cores available is less and only gen5. 

## Managed Instance

* Easy from on-prem or IaaS to managed instance
* Nearly 100% compatible with IaaS
* HA build in to managed instance
* Specify physical file paths not supported
* No windows auth onlu azure AD
* ADF for SSIS (no SSIS)
* Backup and restore to migrate
* Or data migration service
* General purpose or business critical tiers
* Management operations
    * Instance creation, update or deletion
    * Provision of managed instance initially can take a long time
    * Takes up to 6 hours to provision so not going to do it...(will just go up to point of click ok)
* Security - TDE, threat detection, row-level security, auditing, dynamic data masking, Azure AD integration
* Specifc to managemenet instance - native virtual network wih express route or vpn,  default only through private endpoint.
* Have to put it on Vnet when create.
* Connection type can be redirect or proxy.
* Redirect improves latency but can only be via private endpoint, public will always default to proxy. 
* Create jumpbox virtual machine on same vnet as managed instance and then can connect to managed instance. 

## Database Backups

* Full, dif and tran are taken.
* Azure SQL backups between 7 and 35 days
* Full every week, diff every 12 hours, tran every 5-10 mins
* RA-GRS storage is locaton of backups and have TDE on them.
* Long-term retention can be put on for up to 10 years. (LTR feature). Not available in managed instance. Use SQL agent in managed instance for this. 
* Deleted DB will be at least available for 7 days.
* If you delete SQL logical server then all point inn time backups go and no longer available. 
* Compute size, size of db etc can all affect restore time. 
* You can pick manage backups and change Point in Time Restore (POITR) times (7 days is default, can go up to 35).
* Can change pricing model of restored database if you want. 
* You can configure LTR in manage backups too for weekly, yearly and monthly backups. 
* Can pick last week of the year and choose that as the yearly backup to keep for up to 10 years. 
* Backup configuration is at the server level blade. 

## Elastic Database Jobs

* Scheduling technology that can be used for Azure SQL
* Jobs can be created with T-SQL, ARM, Portal, Pshell
* Can run scripts in parallel across T-SQL server
* 1 or more dbs
* Not available on managed instance
* Servers and pools enumerated at runtime so runs across all
* Require a job database on logical server
* Singleton, pool or SQL DW can be target of elastic job
* Job database should be empty, S0 or higher, and wil contiann logs and config etc
* You can have a job output database too if want to log any results
* You create a resource of "Elastic Job Agent" and set your server and database. 
* This is a preview feature at the moment...

## SQL Agent Jobs

* Use in managed instances
* Not available for singletons, pools or synapse
* T-SQL, Pshell, SSIS or OS Command
* Properties configuration not supported in managed instance, can't disable the service either

## SQL Data Sync

* Hub and member databases. 
*  Sync schema defines data to be synched, synch can be one or both directions, conflict resolution and syn interval need setting. 
* Create synch database as brand new one when option comes up.
* So hub, synch and members. 
* For on-prem sync you need to install synch agent on prem.
* Hub and sync dbs must be in azure sql
* Member database can be azure sql, iaas, on -prem etc. 
* Data sync is trigger based. 
* Each table must have primary key. 
* Does not support managed instance. 
* User defined data types not supported in synch. 
* Data sync good for ref data - not etl, migrations, DR or read only copies. 
