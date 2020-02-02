

Authentication - you are who you say you are
Authorisation - what an identity can do 

Control plane - refers to management (Azure AD)
Data plane -  refers to actual usage of data services 
and data object storage (Azure AD and keys)

RBAC - Role Based Access Control
You can create custom roles
RBAC permissions usually set at resource group level
As that of resources provide a service and have a lifecylce together

Object in Azure AD
User 
Group
Service Principal in Azure - made from app registration
-need secret then and store this in key vault
Managed Identity

Azure AD for RBAC
Management Group > Subscription > Resource Group > Resource
BLOB and Queues suppport RBAC as low granule level
Focus on least privlilege
Grant access to groups not users
Privileged Identity Management (PIM)
Active or eligible for a role using PIM so they can give
themslevess acces for a period. 
GRANT RBAC overrides deny athe ACL level

Azure AD with ADLS Gen 2
RBAC and POSIX-like ACLs
RBAC is evaluated first, ACLS only evaluated if not obtained via RBAC
Needs to use Azure Storage Explorer or REST API
Unlike NTFS, POSIX does not inherit permissions
Use default ACL so new files will use that
If change ACLs on a folder, old files keep same permissions and
only on new ones does it change
Three types of permissions available - Read, Write and execute (execute doesn't apply to files)
Read (4), Write (2), Execute (1)

Robocopy from from NTFS to data lake gen 2 can keep ACL

ACL - Access Control List 

Azure Storage Access Keys and Shared Access Signatures
You can regenerate keys, 2 to allow for swapping around
Storage Account Key Operator Role specifically for managing keys
You can create alert for key regeneration action.
You could use managed identity, grant managed identity
access to key vault secret, then can use that
Storage Account Key is god. They could also write data to your account
and not just read. e.g. large data to make it cost, or 
illegal data and get you in trouble. 

shared access signature is time based token for access to 
storage account / container
again can do this via secrets in key vault
You can scope shared access signatures to IP address too
Types of SAS - 
Service SAS, access to specific resource - container
,blob or file
Accoune SAS - access to one more more storage services
Both types are signed by account key
If you regenerate keys then will invalidate SAS
Forms of SAS
Ad Hoc SAS
SAS using stored acess policy. Policy has all constraints etc 
defined and you just add a SAS token to that policy. 
Create Access Policy on a container. 
Revoking a SAS... If change stored acces policy which has SAS
all wil lbe impacted. If delete a stored access policy 
then impacts all SAS that used it
Only way to revoke ad hoc is to regenerate master key, so 

Best practices for keys
Always use HTTPS
Use policy based SAS if possible
Be as specfic as possible with the scope

make sure have small date duration on it










