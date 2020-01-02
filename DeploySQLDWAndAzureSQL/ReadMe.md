
## Deploying Modern DWH

* SQL DW concurrency query limit 32
* Can pause it, designed for query read performance
* Azure SQL 6400 connections, designed to be on and for CRUD operations
* You can create ARM template by exporting from resources or resource groups.
* Data factory you can link to your git repo when you create it
* You can also export data factory ARM templates

## Choosing Code Branching Strategy

* Azure DevOps > Org > Proj > Repos, Boards, Pipelines
* Project Admin > Team Admin > Member
* Section about how to make a repo in Azure DevOps basically
* Branch policies - found on branches of repo
* Policies protect branch
* Minimum reviewers policy. Check linked work items. CHeck all comments resolved. 

## Impleneting DW Build and Release

* CI and CD

## Manage Hybrid Azure SQL DWH

* [Azure Database Migration Guide](https://datamigration.microsoft.com/)
* Create Azure Migrate in Azure portal > Assess and Migrate Databases
* Create this migration project and pick azure migrate database assessment tool
* Then add migrate 
* You can do assessment by downloading [data migration assistant](https://www.microsoft.com/en-us/download/confirmation.aspx?id=53595)
* Azure migrate different to Azure database migration service
* Azure sql data sync with on prem - need an [sql azure data synch agent](https://www.microsoft.com/en-us/download/confirmation.aspx?id=27693)
* Name your agent once created above and generate key
* create an agent key, and submit this ket in microsoft sql data synch tool (open this on your on prem server). Enter in credentials with your on prem sql server. The credentials you enter are actually your hub azure sql credentials! Then you register your on prem sql dataabase all sql data synch tool - done!
* You can then set direction of synch - e,g, from on prem to cloud only if need be so we can have the hub in azure - but the master still on prem!
* Select database and pick which table and columns to synch. 
* Requires primary key in order to do azure synch. 