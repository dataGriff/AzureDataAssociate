
## Stream Analytics Overview

* Options
    * HDINsight with spark
    * HDInsight with Storm
    * Apache spark databricks
    * Azure functions
    * Webjobs
    * Azure stream analytics...
* Stream event hub inputs - event hub, IoT hub, blob store, reference data
* Outputs - blobs, data lake, SQL DB or DW, event hub, power bi
* Time windowing...
    * Tumbling - no overlap, fixed size. TumblingWindow(second,5)
    * Hopping - overlap, fixed size. HoppingWindow(second,10,5). (unit,size,hop)
    * Sliding - only get window when event occurs .SlidingWindow(second,10)
    * Session - only get window when events occur and window keeps growing if events happening. You can give limit to size of window too. SessionWindow(second,5,10). (unit, session timeout, window size limit). Stream analytics only checks window size at multiple of the window size limit, so windows can be longer than expected if don't check until that multiple... 
 * Output config...
    * Minimum rows - not output will be created until minimum rows reached
    * Max time - length of time will wait until makes output

## Configure with Event Hub and Blob Storage Inputs

* See event sending app. Like the million you have done before. 

## Stream Analytics Query Language

* Stream format input can be json, csv or apache avro. 
* Avro - json schema included, the data is seralized into binary format
* You can create your own seralizier and add it as an output if wish too
* Data types
    * Float, bigint, bit, nvarchar(max), datetime, record (key value pair), array
    * CAST(), TRY_CAST(), GetType()
    * Type cast errors - error policy can be drop or retry in job
* Language elements
    * select, from , over, union, into, group by, having, case, coalesce, into, apply, create table (strongly typed inputs)
* Built-in functions
    * Aggregate, Analytic, Array, GeoSpatial, Input Metadata, Record, Windowing , conversion, date and time, mathematical, string
* Timing
    * All streams have timestamp. Arrival (when received) and event time. Stream uses arrival time as timestamp as default. TIMESTAMP BY clause means can apply custom datetime values to timestamp by.
* Ordering
    * Out of order and late arriving policies apply only if TIMESTAMP BY used
    * Can use a drop policy, which means lose the message, or adjust policy. Adjust policy just changes datetime to now. 

    