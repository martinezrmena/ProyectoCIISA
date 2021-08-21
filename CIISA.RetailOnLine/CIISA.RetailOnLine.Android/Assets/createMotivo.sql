CREATE TABLE [MOTIVO](
	[NO_CIA]		[NVARCHAR](2) NOT NULL,
	[TIPO_DOC]		[NVARCHAR](2) NOT NULL,
	[CODIGO]		[NVARCHAR](4) NOT NULL,
	[DESCRIPCION]	[NVARCHAR](60) NULL,
 CONSTRAINT [PK_MOTIVO] PRIMARY KEY([NO_CIA], [TIPO_DOC], [CODIGO])
)