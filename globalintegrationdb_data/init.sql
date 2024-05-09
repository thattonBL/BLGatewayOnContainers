IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Global_Integration')
  BEGIN
    CREATE DATABASE Global_Integration
    ON PRIMARY(
        NAME = N'Global_Integration',
        FILENAME = N'/var/opt/mssql/data/Global_Integration.mdf',
        SIZE = 10240KB,
        MAXSIZE = UNLIMITED,
        FILEGROWTH = 1024KB
    ) 
  END
GO

USE Global_Integration
GO

-- CREATE THE TABLES 

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Global_Integration].[dbo].[IntegrationEventLog]') AND type in (N'U'))
BEGIN
  CREATE TABLE Global_Integration.dbo.IntegrationEventLog (
    EventId uniqueidentifier NOT NULL,
    Content nvarchar(max) NOT NULL,
    CreationTime datetime2 NOT NULL,
    EventTypeName nvarchar(max) NOT NULL,
    State int NOT NULL,
    TimesSent int NOT NULL,
    TransactionId nvarchar(max) NULL,
    CONSTRAINT PK_IntegrationEventLog PRIMARY KEY CLUSTERED (EventId)
  )
END

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO