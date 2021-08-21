CREATE TABLE [DETALLEDOCUMENTOBK](
	[CODCIA]				[NVARCHAR](2) NOT NULL,
	[CODDOCUMENTO]			[NVARCHAR](16) NOT NULL,
	[CODTIPODOCUMENTO]		[NVARCHAR](2) NOT NULL,
	[NUMLINEA]				[INT] NOT NULL,
	[CODPRODUCTO]			[NVARCHAR](30) NOT NULL,
	[CANTIDAD]				[DECIMAL](18, 2) NOT NULL,
	[COMENTARIO]			[NVARCHAR](256) NULL,
	[TOTALLINEA]			[DECIMAL](18, 2) NOT NULL,
	[TOTAL_IMP_LIN]			[DECIMAL](17, 2) NULL,
	[NO_AGENTE]				[NVARCHAR](10) NULL,
	[ENVIADO]				[NVARCHAR](1) NULL,
	[PRECIO_UNI]			[DECIMAL](17, 2) NULL,
	[PORC_DEC]				[DECIMAL](10, 2) NULL,
	[MONTO_DESCUENTO]		[DECIMAL](17, 2) NULL,
	--[FECHA_CREA]			[DATETIME] NULL CONSTRAINT [DF_DETALLEDOCUMENTO_FECHA_CREA] DEFAULT GETDATE(),
	[FECHA_CREA]			[DATETIME] NULL CONSTRAINT [DF_DETALLEDOCUMENTO_FECHA_CREA] DEFAULT (DATETIME('NOW','LOCALTIME')),
	[COD_MOTIVO]			[NVARCHAR](4) NULL,
	[ANULADO]				[NVARCHAR](1) NULL,	
	[ESTADODEVOLUCION]		[NVARCHAR](5) NULL,	
	[EMBALAJE]				[DECIMAL](17, 2) NULL,
	[ES_VISCERA]			[NVARCHAR](1) NULL DEFAULT('N'),
	[TIPO_PORCION]			[NVARCHAR](1) NULL,
	[CONSECUTIVODR]			[NVARCHAR] NULL,
	[PORCENTAJE_IMP]		[DECIMAL](10, 2) NULL,
	[PORCENTAJE_IMP_EXO]	[DECIMAL](10, 2) NULL,
	[EXONERAID]				[INT] NOT NULL,
	[TIPO_EXO]				[NVARCHAR](2) NULL,
	[ES_FACTURA]			[NVARCHAR](1) NULL DEFAULT('N'),
 CONSTRAINT [PK_DETALLEDOCUMENTOBK] PRIMARY KEY([CODCIA], [CODDOCUMENTO], [CODTIPODOCUMENTO], [NUMLINEA])
)