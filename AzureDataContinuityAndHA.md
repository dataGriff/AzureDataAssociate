
RTO - Recovery Time Objective. How long does it take be back up 
and running.
RPO - Recover Point Objective. How much data can be lost.

Backup - go back to a point in time
HA - make service available within a location
DR - recover from a site level event

Azure Backup
BackupVault
Security PIN, MFA, PIN lasts 5 minutes, to access backups
Daily, weekly, annual backups of VM for example.
stores the deltas
Can restore entier VM or restore files of VM
Special support for SQL VMs
You can have a file server from VM and mount on your windows,
then just restore to previous version in the GUI

Synapse snaphsots taken every 8 hours and kept for 7 days
You can create UDF snapshots
Replicated to paired region once a day

Cosmos
Backup taken every 4 hours
Ticket raised to restore data
You have 8 hours to do something about it as thats how
long retained!
May need something custom with ADF

Change data capture only in managed instance which tracks
old and new values
Change tracking is just "this row has changed"
CosmosDB change feed acts on any change, so could
log what has changed

Azure Storage 
LRS - 3 copies in one cluster (rack)
ZRS - 3 copies over 3 availability zones (3 data centres in region)
GRS and RA-GRS - 3 copies on paired region as well
Can't change to zone redundant after created
Customer initiatied failover in preview
Azure storage geo secondaries are always paired region

SQL PaaS - business critical is basically an always on
availability group
You can create your own geo-replication for SQL PaaS
You can create replicas of replicas if you want more than four...
Cost and latency a consideration
Failover manually initiated
Only one write region in SQL server, can have multiple secondaries
Listeners available so can point things at read-write 
or read-only listeners
You can have automatic failover in this setup

Cosmos DB Availabililty
Single-write or multi-write account
Single-write can have auto failover configured
Consistency models in cosmos
Strong, bounded staleness, session, consistent prefix, eventual
strong is not available in multi-writes, only in single-write
you can specify preferred write location in connection policy
you can do same for read location too where replicated

For maintenance tasks in SQL Azure...
Elastics Jobs
Automation Account





























