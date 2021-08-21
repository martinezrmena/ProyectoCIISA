CREATE TABLE [ENCABEZADORECIBOBK](
	[NO_CIA]				[NVARCHAR](2) NOT NULL,
	[NO_TRANSA]				[NVARCHAR](16) NOT NULL,
	[NO_CLIENTE]			[NVARCHAR](6) NULL,
	[NO_ESTABLECIMIENTO]	[INT] NULL,
	[NO_AGENTE]				[NVARCHAR](10) NULL,
	[OBSERVACION]			[NVARCHAR](50) NULL,
	[MONTO]					[DECIMAL](17,2) NULL,
	--[FECHA_CREA]			[DATETIME] NULL CONSTRAINT [DF_ENCABEZADORECIBOBK_FECHA_CREA] DEFAULT GETDATE(),
	[FECHA_CREA]			[DATETIME] NULL CONSTRAINT [DF_ENCABEZADORECIBOBK_FECHA_CREA] DEFAULT (DATETIME('NOW','LOCALTIME')),
	[SALDO]					[DECIMAL](18, 2) NULL,
	[TIPO_DOC]				[NVARCHAR](2) NOT NULL,
	[NO_LINEA]				[INT] NULL,
	[MONTO_DEVOL]			[DECIMAL](17, 2) NULL,
	[NUMERO_DEVOL]			[NVARCHAR](12) NULL,
	[ENVIADO]				[NVARCHAR](1) NULL,
	[ANULADO]				[NVARCHAR](1) NULL,
	[CLIENTE_NUEVO]			[NVARCHAR](6) NULL,
	[FECHA_TOMA]			[DATETIME] NULL,
	[LATITUD]				[DECIMAL](20, 10) NULL,
	[LONGITUD]				[DECIMAL](20, 10) NULL,
 CONSTRAINT [PK_ENCABEZADORECIBOBK] PRIMARY KEY([NO_CIA], [NO_TRANSA], [TIPO_DOC])
)