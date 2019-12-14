CREATE MASTER KEY ENCRYPTION BY PASSWORD = '980AbctotheCloud427leet';

CREATE EXTERNAL DATA SOURCE LabAzureStorage
 WITH
 (
   TYPE = Hadoop,
   LOCATION = 'wasbs://labdata@fruitvegrgstore141219.blob.core.windows.net/'
 );

 CREATE EXTERNAL FILE FORMAT TextFileFormat
WITH
(
  FORMAT_TYPE = DELIMITEDTEXT,
  FORMAT_OPTIONS (
    FIELD_TERMINATOR = ',',
    STRING_DELIMITER = '',
    DATE_FORMAT = 'yyyy-MM-dd HH:mm:ss.fff',
    USE_TYPE_DEFAULT = FALSE
  )
);

CREATE SCHEMA [asb];

CREATE EXTERNAL TABLE [asb].[Transaction]
(
  [TransactionId] [int] NOT NULL,
  [ProductId] [int] NOT NULL,
  [DateApsId] [int] NULL,
  [StoreId] [int] NOT NULL,
  [StaffId] [int] NOT NULL,
  [RetailChannelId] [int] NOT NULL,
  [CustomerSegmentId] [int] NOT NULL,
  [PaymentMethodId] [int] NOT NULL,
  [COGS] [money] NULL,
  [SaleQuantity] [int] NOT NULL,
  [UnitPrice] [money] NULL,
  [SaleAmount] [money] NULL,
  [TaxRate] [money] NULL,
  [TaxAmount] [money] NULL,
  [GrossAmount] [money] NULL,
  [TenderedAmount] [money] NULL,
  [ProfitMargin] [money] NULL,
  [PaymentCount] [int] NULL,
  [ReturnQuantity] [int] NULL,
  [ReturnAmount] [money] NULL,
  [TransactionDate] [datetime] NULL,
  [CogsPerUnit] [money] NULL
)
WITH 
(
  LOCATION='Transaction.txt', DATA_SOURCE = LabAzureStorage, FILE_FORMAT = TextFileFormat, REJECT_TYPE = VALUE, REJECT_VALUE = 1
);

SELECT * FROM [asb].[Transaction]

CREATE SCHEMA [cso];

drop table [cso].[Transaction]

CREATE TABLE [cso].[Transaction]
WITH
(
  DISTRIBUTION = HASH([TransactionId])
)
AS
SELECT * FROM [asb].[Transaction]
OPTION (LABEL = 'CTAS : Load [cso].[Transaction]');

select * from [cso].[Transaction]

SELECT * FROM [dbo].[AnalyzeCampaigns]