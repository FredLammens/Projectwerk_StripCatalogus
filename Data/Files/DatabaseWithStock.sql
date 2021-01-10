-- ****************** SqlDBM: Microsoft SQL Server ******************
-- ******************************************************************

-- ************************************** [dbo].[Authors]

CREATE TABLE [dbo].[Authors]
(
 [ID]   int IDENTITY (1, 1) NOT NULL ,
 [Name] varchar(100) NOT NULL ,


 CONSTRAINT [PK_author] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

-- ************************************** [dbo].[Publishers]

CREATE TABLE [dbo].[Publishers]
(
 [ID]   int IDENTITY (1, 1) NOT NULL ,
 [Name] varchar(100) NOT NULL ,


 CONSTRAINT [PK_publisher] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

-- ************************************** [dbo].[Series]

CREATE TABLE [dbo].[Series]
(
 [ID]   int IDENTITY (1, 1) NOT NULL ,
 [Name] varchar(100) NOT NULL ,


 CONSTRAINT [PK_series] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

-- ************************************** [dbo].[Comics]

CREATE TABLE [dbo].[Comics]
(
 [ID]            int IDENTITY (1, 1) NOT NULL ,
 [Title]         varchar(100) NOT NULL ,
 [SeriesNr]      int NULL ,
 [IsInCatalogue] bit NOT NULL CONSTRAINT [DF_Comics_IsInCatalogue] DEFAULT ((1)) ,
 [Publisher_ID]  int NOT NULL ,
 [Series_ID]     int NOT NULL ,


 CONSTRAINT [PK_comic] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_85] FOREIGN KEY ([Publisher_ID])  REFERENCES [dbo].[Publishers]([ID]),
 CONSTRAINT [FK_91] FOREIGN KEY ([Series_ID])  REFERENCES [dbo].[Series]([ID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_85] ON [dbo].[Comics] 
 (
  [Publisher_ID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_91] ON [dbo].[Comics] 
 (
  [Series_ID] ASC
 )

GO

-- ************************************** [dbo].[Comics_Authors]

CREATE TABLE [dbo].[Comics_Authors]
(
 [Comics_ID]  int NOT NULL ,
 [Authors_ID] int NOT NULL ,


 CONSTRAINT [FK_67] FOREIGN KEY ([Comics_ID])  REFERENCES [dbo].[Comics]([ID]),
 CONSTRAINT [FK_76] FOREIGN KEY ([Authors_ID])  REFERENCES [dbo].[Authors]([ID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_67] ON [dbo].[Comics_Authors] 
 (
  [Comics_ID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_76] ON [dbo].[Comics_Authors] 
 (
  [Authors_ID] ASC
 )

GO

-- ************************************** [Stock]

CREATE TABLE [Stock]
(
 [ID]      int IDENTITY (0, 1) NOT NULL ,
 [ComicID] int NOT NULL ,
 [Stock]   int NOT NULL ,


 CONSTRAINT [PK_stock] PRIMARY KEY CLUSTERED ([ID] ASC),
 CONSTRAINT [FK_95] FOREIGN KEY ([ComicID])  REFERENCES [dbo].[Comics]([ID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_96] ON [Stock] 
 (
  [ComicID] ASC
 )

GO

-- ************************************** [Deliveries]

CREATE TABLE [Deliveries]
(
 [ID]           int  IDENTITY (1, 1) NOT NULL ,
 [Date]         datetime NOT NULL ,
 [DeliveryDate] datetime NOT NULL ,


 CONSTRAINT [PK_deliveries] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

CREATE TABLE [DeliveriesComics]
(
 [AmountDelivered] int NOT NULL ,
 [DeliveryID]     int NOT NULL ,
 [StockID]         int NOT NULL ,


 CONSTRAINT [FK_107] FOREIGN KEY ([StockID])  REFERENCES [Stock]([ID]),
 CONSTRAINT [FK_72] FOREIGN KEY ([DeliveryID])  REFERENCES [Deliveries]([ID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_108] ON [DeliveriesComics] 
 (
  [StockID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_72] ON [DeliveriesComics] 
 (
  [DeliveryID] ASC
 )

GO

-- ************************************** [Orders]

CREATE TABLE [Orders]
(
 [ID]        int  IDENTITY (1, 1) NOT NULL ,
 [OrderDate] datetime NOT NULL ,


 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

-- ************************************** [OrdersComics]

CREATE TABLE [OrdersComics]
(
 [AmountOrdered] int NOT NULL ,
 [OrderID]       int NOT NULL ,
 [StockID]       int NOT NULL ,


 CONSTRAINT [FK_101] FOREIGN KEY ([StockID])  REFERENCES [Stock]([ID]),
 CONSTRAINT [FK_55] FOREIGN KEY ([OrderID])  REFERENCES [Orders]([ID])
);
GO


CREATE NONCLUSTERED INDEX [fkIdx_102] ON [OrdersComics] 
 (
  [StockID] ASC
 )

GO

CREATE NONCLUSTERED INDEX [fkIdx_55] ON [OrdersComics] 
 (
  [OrderID] ASC
 )

GO