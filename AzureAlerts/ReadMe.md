
## Alerting Capabilities

* Performance, health and availability, cost/budget, security
* Alert actions = dashboard, email, sms, automation (runbook, logic app, azure function, webhook)
* Action groups - alert mechanisms shared, certain e-mail group, certain actions
* Metrics, Activity Logs (thing that happen to resources, create, delete etc) Azure monitor is where these go. 
* Diagnostic logs - 3 locations - Azure Monitor Logs is Log Analyutics. Storage Account, hub.
* Metrics and activity logs can go to azure monitor logs so can then use kusto. 
* JSON ARM can be used to create alerts. 
* Smart Groups - AI groups like alerts together so less noise. 
* Alert rule may not have action goup. you can do action rule instead of action group. 
* Service health alerts can be created and action groups used too. 
* Alert off azure monitor logs, but can then use kusto to make custom alerts. * Log analytics alerts also appear as alerts in azure monitor, they are just sourced from log analytics instead.  

## Alerts on Azure Data Services

* Monitoring > Alerts. Create condition. Just like alerts from azure monitor.
* ADF alerting - azure monitor and diagnostic logs. Just like everything else. 
* Even in the ADF environment the alert tab goes to azure monitor. 
* HDInsight - apache ambari. You can trigger email and custom alerts in here. 

