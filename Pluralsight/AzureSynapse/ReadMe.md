
## Backup / Restore

* You can make your own restore points in the portal
* They are taken regularly
* Backups kept for 7 days, 42 rstore points
* Not available when paused
* Backups charged premiun storage rates to nearest TB
* GEO redundant
* Restore in the portal too, which can be auto restore points or user defined ones
* Restore is a full restore so can take a while. 
* Comes back with a new version with a datestamp like Azure SQL. 

## Managing Costs

* Go to azure pricing page [here.](https://azure.microsoft.com/en-gb/pricing/details/synapse-analytics/gen2/)
* Split by compute and storage the pricing. 
* Storage charged even when paused (but a lot cheaper than compute). This is all that is charged when paused though.
* Threat detection costs too. 

## Controlling Workloads

* Classification, importance, isolation
* Classification based on database user will take precedence over role membership
* Create WORKLOAD CLASSIFER for this
* WORKLOAD CLASSIFIER has the workload group defined for isolation and importance (See both below)
* 5 levels of importance:
   * low
   * below_normal
   * normal
   * above_normal
   * high
* Importance can prevent contention as higher importance takes precedence
* Workload groups define isolation.
* MIN_PERCENTAGE_ISOLATION, CAP_PERCENTAGE_ISOLATION used to define workload group.
* You can then add this work load group to a classifer

## Connection Security

* Firewall rules... Only available at server level.
* Auth security - SQL or Azure AD. 
* Authorisation by roles. 
* TDE - encrypt and decrypt at rest. AES-256. 
* Advanced data security in blae of portal, enable, can audit to log anaytics. 

## Monitoring

* Query activity in portal blade to see queries.
* You can configure alerts in portal too. 
* Create ActionGroup for alerts which can be e-mail address, text etc. 
* Diagnostic settings is where configure logs to send to log analytics workspace. 


