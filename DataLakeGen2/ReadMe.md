
## Get Started ADLS Gen 2

* Encrypted at rest and doesn't affect performance
* POSIX based security, hierarchical namespace
* Scaled for huge amounts of capacity
* HDFS - hadoop distributed file system
* BLob is cost efficient
* Gen2 = HDFS + Blob
* Images and backups no good for adls gen 2

## Managing Data ADLS Gen 2

* ACL, POSIC, RBAC
* Ways to move data...
    * distcp
    * ADF
    * AzCopy
* Hadoop and Rest API Commands
* When copying with ADF choose binary copy for it to be exact copy of file. 
* Running paralle will us more compute resources
* To use Rest API use OAuth access token which uses client id, client secret and tenant id - this is just like databricks! 
* e.g. 
* GET https://{accountName}.{dnsSuffix}/?resource=account
* Scheme is this bit ://...
* WASB was old one - Windows Azure Storage Blob Driver
* ABFS[S] for data lake gen 2 - Azure Blob Filesystem Driver. [S] for secure. 
* abfss://<container>@accountname.dfs.core.windows.net/...
* hadoop distcp sourcelakename destlakename example of hadoop copy commmand
* AzCopy - V10 and above. Use this in commandl line 
* e.g. azcopy cp "files/*" "sourcelake" "destlake"