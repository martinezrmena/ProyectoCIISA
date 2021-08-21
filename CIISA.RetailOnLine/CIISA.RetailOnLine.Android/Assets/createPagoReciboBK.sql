--PAGORECIBO
CREATE TABLE [PAGORECIBOBK](
	[NO_CIA]		[NVARCHAR](2) NOT NULL,
	[NO_TRANSA]		[NVARCHAR](16) NOT NULL,
	[NO_LINEA]		[INT] NOT NULL,
	[MONTO]			[DECIMAL](17,2) NULL,
	--EJEMPLO: EFECTIVO, TRANSFERENCIA
	[TIPO]			[NVARCHAR](2) NULL,
	[NO_FISICO]		[NVARCHAR](12) NULL,
	[SERIE]			[NVARCHAR](15) NULL,
	[FECHA_CREA]	[DATETIME] NULL,
	[BANCO]			[NVARCHAR](2) NULL,
	--EJEMPLO: RECIBO DINERO, FACTURA CONTADO
	[TIPO_DOC]		[NVARCHAR](2) NOT NULL,
	[ENVIADO]		[NVARCHAR](1) NULL,
	[ANULADO]		[NVARCHAR](1) NULL,
CONSTRAINT [PK_PAGOSRECIBOSBK] PRIMARY KEY([NO_CIA], [NO_TRANSA], [NO_LINEA], [TIPO_DOC])
)