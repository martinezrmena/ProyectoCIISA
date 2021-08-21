﻿CREATE TABLE [EXONERACION](
	[NO_CIA]					[NVARCHAR](2) NOT NULL,
	[GRUPO]						[NVARCHAR](2) NOT NULL,
	[NO_CLIENTE]				[NVARCHAR](6) NOT NULL,
	[FECHA_EXONERA]				[DATETIME] NOT NULL,
	[PORC_EXONERA]				[INT] NOT NULL,
	[FECHA_FINALIZA]			[DATETIME] NOT NULL,
	[EXONERAID]					[INT] NOT NULL DEFAULT -1,
	[ARTICULO]					[NVARCHAR](30) NULL,
	[CLASI_PR]					[NVARCHAR](4) NULL,
	[SUBCLASI_PR]				[NVARCHAR](4) NULL,
	[TIPO]						[NVARCHAR](2) NOT NULL DEFAULT 'PO',
 CONSTRAINT [PK_EXONERACION] PRIMARY KEY([NO_CIA], [EXONERAID])
)