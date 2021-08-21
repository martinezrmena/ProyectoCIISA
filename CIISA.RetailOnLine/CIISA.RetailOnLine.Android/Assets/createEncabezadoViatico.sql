CREATE TABLE [ENCABEZADOVIATICO](
	[NO_CIA]				[NVARCHAR](2) NOT NULL,
	[NO_TRANSA]				[NVARCHAR](12) NOT NULL,
	[NO_EMPLE]				[NVARCHAR](6) NULL,
	[CODIGO_SECTOR]			[NVARCHAR](3) NULL,
	[FECHA_SALE]			[DATETIME] NULL,
	[FECHA_REGRESA]			[DATETIME] NULL,
	[LUGAR]					[NVARCHAR](30) NULL,
	[MONTO_ALIMENTA]		[DECIMAL](17, 2) NULL,
	[MONTO_HOSPEDAJE]		[DECIMAL](17, 2) NULL,
	[MONTO_PEAJES_PARQ]		[DECIMAL](17, 2) NULL,
	[MONTO_TELEFONO]		[DECIMAL](17, 2) NULL,
	[FECHA_MAQUINA]			[DATETIME] NULL,
	--[FECHA_CREA]			[DATETIME] NULL CONSTRAINT [DF_ENCABEZADOVIATICO_FECHA_CREA] DEFAULT GETDATE(),
	[FECHA_CREA]			[DATETIME] NULL CONSTRAINT [DF_ENCABEZADOVIATICO_FECHA_CREA] DEFAULT (DATETIME('NOW','LOCALTIME')),
	[ENVIADO]				[NVARCHAR](1) NULL,
CONSTRAINT [PK_ENCABEZADOVIATICO] PRIMARY KEY([NO_CIA], [NO_TRANSA])
)