CREATE TABLE [BANCO](
	[BANCO]				[NVARCHAR](50)	NOT NULL,
	[DESCRIPCION]		[NVARCHAR](60)	NULL,
	[DESCRIP_RUTERO]	[NVARCHAR](30)	NULL,
	[SIGLA]				[NVARCHAR](6)	NULL,
 CONSTRAINT [PK_BANCO] PRIMARY KEY([BANCO])
)