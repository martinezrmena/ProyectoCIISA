CREATE TABLE [COBRADOR](
--NO_CIA, TIPO_CODIGO, CODIGO, NOMBRE, CONSECUTIVO_DOC, CONSECUTIVO_REC, CONTRASENA
	[NO_CIA]				[NVARCHAR](3) NOT NULL,
	[TIPO_CODIGO]			[NVARCHAR](2) NOT NULL,
	[CODIGO]				[NVARCHAR](4) NOT NULL,
	[NOMBRE]				[NVARCHAR](25) NULL,
	[CONSECUTIVO_DOC]		[NVARCHAR](16) NULL,--documento
	[CONSECUTIVO_REC]		[NVARCHAR](16) NULL,--recibo dinero
	[CONSECUTIVO_RC]		[NVARCHAR](16) NULL,--recaudacion
	[CONTRASENA]			[NVARCHAR](25) NULL,
CONSTRAINT [PK_COBRADOR] PRIMARY KEY([CODIGO]))