﻿CREATE TABLE [TIPOIVA](
	[CLAVE_IVA]		[NVARCHAR](2) NOT NULL,
	[SIMBOLO]		[NVARCHAR](2) NULL,
	[PORCENTAJE]	[DECIMAL](7,4) NULL,
 CONSTRAINT [PK_TIPOIVA] PRIMARY KEY([CLAVE_IVA])
)