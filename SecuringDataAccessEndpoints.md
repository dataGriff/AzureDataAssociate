
Virtual network lives within subscription within a region

Vnet Peering can connect Vnets

Has 1 or more IP ranges assigned to vnet

Subnets break up the IP ranges assigned above

Default end point is typically interney facing

Just because can connect to end point, doesn't mean authorised to do anything

Defense in depth - Network and Authentication



Virtual network injection
Deploy service into vnet (if single tenant)
or, Enable service to have adapter in the vnet (if multitenant)

Once in vnet, could potentially remove public access from internet
Can then do NSGs and NVAs
Network Security Groups (NSG)
Network Virtual Appliances (NVA) - can change user defined routing that go through an appliance, e.g. VM
SQL Managed instance goes into Vnet and only exposed by private IP

databricks - can go into vnet, subnet recommended but not mandatory

Using service endpoints...
Services that cannont integated with vnet but cann still have optimal routing defined
Uses most optimal route to the service on azure backbone,
making direct path from subnet to service
Create service endpoint for subnet on vnet
on service instance enable access for subnet

do we want one databricks subnet for storage, event hubs and sql?
does it need it for key vault? 
so add for key vault as well... 
dabricks subnet - storage, event hub, sql and key vault

Network Security Groups... make more granular to a region!!
NSGs enable micro-segmentation for vnets. 
Service tags? These can be region specific...
e.g. allow access for Storage.EastUS! 
Even more granular control region and service within vnet. 
Not a particular account in EastUS, but can do by region. 
So at least some limiting. 
So once set up network security group to allow only...
NorthEurope.KeyVault
NorthEurope.WestEurope Event Hub
NorthEurope.WestEurope Storage Account

Service Endpoint Policies...
This allows even more granular control to specific instances of 
that instance. Limited availability but can be done. 
Policies applied at subnet level. 
ServiceEndpointPolicy is a new type of resource in Azure.
You then add this service endpoint policy to your subnet. 
This means can only access this particular resource within that subnet, not just
all region or type of service. 

Network Virtual Appliance or Azure firewall is typicall used 
when accessing azure services from on-premises. 
- me thinks this is what we already do. 
So we have a consistent IP from on-prem that we would add
 as a fire wall rule/ 

Neatly all azure service have public endpoint
Some dedicated services have pripvate IP with option to add (e.g. managed service instance)
Need to think authentication to control who can access service
Firewalls and IP addresses can be used to restrict access
and trusted azure resources is usuall an option
cosmos has a portal access checkbox, and azure data centre one
which need to be careful with
This is done at SQL server level for sql db

Microsoft peering
ExpressRoute - private link between custommer networks and microsoft
WAN
MS peering allows for PaaS services to be using BGP via ExpressRoute
ExpressRoute is cheaper/faster than public internet
So use this private connection instead of the internet

Threat Detection...
DDoS standard offerings from Azure
AdvancedThreatProtection for Azure SQL DB and Azure Storage 
Use Vulneravility Assessment services when available
Azure Security Centre is good to use, data & storage is in there
















