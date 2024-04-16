IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Gateway')
  BEGIN
    CREATE DATABASE Gateway
    ON PRIMARY(
        NAME = N'Gateway',
        FILENAME = N'/var/opt/mssql/data/Gateway.mdf',
        SIZE = 10240KB,
        MAXSIZE = UNLIMITED,
        FILEGROWTH = 1024KB
    ) 
  END
GO

USE Gateway
GO

-- CREATE THE TABLES 

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[Common]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.Common (
    id int IDENTITY,
    msg_status varchar(50) NULL,
    msg_source varchar(50) NULL,
    msg_target int NOT NULL,
    prty varchar(50) NULL,
    type int NOT NULL,
    ref_source varchar(50) NULL,
    ref_request_id varchar(50) NULL,
    ref_seq_no int NULL,
    dt_created datetime NOT NULL,
    CONSTRAINT PK_Common_id PRIMARY KEY CLUSTERED (id)
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[RSI]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.RSI (
    id int IDENTITY,
    collection_code varchar(50) NULL,
    shelfmark varchar(50) NULL,
    volume_number varchar(50) NULL,
    storage_location_code varchar(50) NULL,
    author varchar(50) NULL,
    title varchar(50) NULL,
    publication_date datetime NULL,
    periodical_date datetime NULL,
    article_line1 varchar(50) NULL,
    article_line2 varchar(50) NULL,
    catalogue_record_url varchar(50) NULL,
    further_details_url varchar(50) NULL,
    dt_required varchar(50) NULL,
    route varchar(50) NULL,
    reading_room_staff_area varchar(50) NULL,
    seat_number varchar(50) NULL,
    reading_category varchar(50) NULL,
    identifier varchar(50) NULL,
    reader_name varchar(50) NULL,
    reader_type int NULL,
    operator_information varchar(50) NULL,
    item_identity varchar(50) NULL,
    CONSTRAINT PK_RSI_id PRIMARY KEY CLUSTERED (id)
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[messageTypeLookup]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.messageTypeLookup (
    id int IDENTITY,
    type varchar(50) NULL,
    CONSTRAINT PK_messageType_id PRIMARY KEY CLUSTERED (id)
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[Queue]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.Queue (
    id int IDENTITY,
    msg_target int NULL,
    prty int NULL,
    type int NULL,
    dt_created datetime NULL,
    is_acknowledged bit NOT NULL
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[REA]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.REA (
    id int IDENTITY,
    dt_of_action varchar(50) NULL,
    request_response_flag varchar(50) NULL,
    failure_code varchar(50) NULL,
    container_id int NULL,
    text_message varchar(50) NULL,
    stack_identity varchar(50) NULL,
    tray_identity varchar(50) NULL,
    CONSTRAINT PK_REA_id PRIMARY KEY CLUSTERED (id)
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[REC]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.REC (
    id int IDENTITY,
    dt_of_action varchar(50) NULL,
    request_response_flag varchar(50) NULL,
    failure_code varchar(50) NULL,
    text_message varchar(50) NULL,
    stack_identity varchar(50) NULL,
    tray_identity varchar(50) NULL,
    CONSTRAINT PK_REC_id PRIMARY KEY CLUSTERED (id)
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[RIR]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.RIR (
    id int IDENTITY,
    outcome varchar(50) NULL,
    reason varchar(50) NULL,
  )
END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Gateway].[dbo].[RSI]') AND type in (N'U'))
BEGIN
  CREATE TABLE Gateway.dbo.RSI (
    id int IDENTITY,
    collection_code varchar(50) NULL,
    shelfmark varchar(50) NULL,
    volume_number varchar(50) NULL,
    storage_location_code varchar(50) NULL,
    author varchar(50) NULL,
    title varchar(50) NULL,
    publication_date datetime NULL,
    periodical_date datetime NULL,
    article_line1 varchar(50) NULL,
    article_line2 varchar(50) NULL,
    catalogue_record_url varchar(50) NULL,
    further_details_url varchar(50) NULL,
    dt_required varchar(50) NULL,
    route varchar(50) NULL,
    reading_room_staff_area varchar(50) NULL,
    seat_number varchar(50) NULL,
    reading_category varchar(50) NULL,
    identifier varchar(50) NULL,
    reader_name varchar(50) NULL,
    reader_type int NULL,
    operator_information varchar(50) NULL,
    item_identity varchar(50) NULL,
    CONSTRAINT PK_RSI_id PRIMARY KEY CLUSTERED (id)
  )
END

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

-- ADD DATA --
BEGIN
  IF NOT EXISTS(SELECT top 1 1 FROM Gateway.dbo.messageTypeLookup)
  BEGIN
    
    INSERT Gateway.dbo.messageTypeLookup(type) VALUES ('RSI')
    INSERT Gateway.dbo.messageTypeLookup(type) VALUES ('RIR')
    INSERT Gateway.dbo.messageTypeLookup(type) VALUES ('REA')
    INSERT Gateway.dbo.messageTypeLookup(type) VALUES ('REC')

    INSERT INTO Gateway.dbo.RSI(collection_code, shelfmark, volume_number, storage_location_code, author, title, publication_date, periodical_date, article_line1, article_line2, catalogue_record_url, further_details_url, dt_required, route, reading_room_staff_area, seat_number, reading_category, identifier, reader_name, reader_type, operator_information, item_identity) VALUES ('CODE', 'SHLMK1234', '1', '33', 'T HATTON', 'My Life', '1990-03-22 00:00:00.000', '2024-03-13 00:00:00.000', 'My Life In Summary', 'Is Great and Wonderful', 'http://comecatalogue/url', 'http://some/further/details', NULL, '66', 'Yes', '15', 'Biographies', 'GB1234', 'Martin Boswell', 4, 'Charlie', '123456789');
      
  END
END

BEGIN
  IF NOT EXISTS(SELECT top 1 1 FROM Gateway.dbo.Queue)
  BEGIN   
    INSERT INTO Gateway.dbo.Queue(msg_target, prty, type, dt_created, is_acknowledged) VALUES (1, 1, 1, '2024-03-12 00:00:00.000', CONVERT(bit, 'False'));
  END
END

BEGIN
  IF NOT EXISTS(SELECT top 1 1 FROM Gateway.dbo.Common)
  BEGIN
    INSERT INTO Gateway.dbo.Common(msg_status, msg_source, msg_target, prty, type, ref_source, ref_request_id, ref_seq_no, dt_created) VALUES ('Processing', 'B33', 1, NULL, 1, NULL, NULL, NULL, '2024-03-13 00:00:00.000');
  END
END

-- ADD FOREIGN KEYS --

ALTER TABLE Gateway.dbo.Queue
  ADD CONSTRAINT FK_Queue_Common_id FOREIGN KEY (msg_target) REFERENCES dbo.Common (id)
GO

ALTER TABLE Gateway.dbo.Queue
  ADD CONSTRAINT FK_Queue_messageTypeLookup_id FOREIGN KEY (type) REFERENCES dbo.messageTypeLookup (id)
GO

ALTER TABLE Gateway.dbo.Common
  ADD CONSTRAINT FK_Common_messageTypeLookup_id FOREIGN KEY (type) REFERENCES dbo.messageTypeLookup (id)
GO

-- ADD STORED PROCEDURE --

IF EXISTS (
        SELECT type_desc, type
        FROM sys.procedures WITH(NOLOCK)
        WHERE NAME = 'dbo.spGetRsiMessage'
            AND type = 'P'
      )
     DROP PROCEDURE dbo.myProc
GO

CREATE PROCEDURE dbo.spGetRsiMessages
AS 
  SET NOCOUNT ON;

  SELECT rsi.* 
  FROM dbo.RSI AS rsi 
  JOIN dbo.Common AS com ON com.msg_target = rsi.id 
  JOIN dbo.Queue AS q ON q.msg_target = com.id
  WHERE q.is_acknowledged = 0;

  RETURN;
GO