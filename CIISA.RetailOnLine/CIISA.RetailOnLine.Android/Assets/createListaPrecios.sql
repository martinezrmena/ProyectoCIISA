CREATE TABLE [LISTAPRECIOS](
	[NO_CIA]		[NVARCHAR](2) NOT NULL,
	[GRUPO_PRE]		[NVARCHAR](4) NOT NULL,
	[DESCRIPCION]	[NVARCHAR](40) NULL,
 CONSTRAINT [PK_LISTASDEPRECIOS] PRIMARY KEY ([NO_CIA], [GRUPO_PRE])
)