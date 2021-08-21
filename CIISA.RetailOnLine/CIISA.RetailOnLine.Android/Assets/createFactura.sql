CREATE TABLE [FACTURA](
	[NO_FISICO]				[NVARCHAR](12) NOT NULL,
	[NO_CLIENTE]			[NVARCHAR](6) NOT NULL,
	[SALDO]					[DECIMAL](17, 2) NULL,
	[MONTOORIGINAL]			[DECIMAL](17, 2) NULL,
	[FECHA_DOCUMENTO]		[DATETIME] NULL,
	[FECHA_VENCE]			[DATETIME] NULL,
	[ENVIADO]				[NVARCHAR](1) NULL,
	[ANULADO]				[NVARCHAR](1) NULL,
	[TRAMITE]				[NVARCHAR](1) NULL,
	[CREADA]				[NVARCHAR](1) NULL,
	[TIPO_DOC]				[NVARCHAR](2) NULL,
	[NUM_ESTABLECIMIENTO]	[INT] NOT NULL,
	[FECHA_INSERT]			[DATETIME] NULL,
 CONSTRAINT [PK_FACTURA] PRIMARY KEY([NO_FISICO], [NO_CLIENTE], [NUM_ESTABLECIMIENTO])
)