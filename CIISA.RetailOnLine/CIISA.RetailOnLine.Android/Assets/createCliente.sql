﻿CREATE TABLE [CLIENTE](
	[NO_CIA]				[NVARCHAR](2) NOT NULL,
	[NO_CLIENTE]			[NVARCHAR](6) NOT NULL,
	[NOMBRE]				[NVARCHAR](80) NULL,
	[NOMBRE_LARGO]			[NVARCHAR](80) NULL,
	[CEDULA]				[NVARCHAR](15) NULL,
	[EXCENTO_IMP]			[NVARCHAR](1) NULL,
	[PLAZO]					[INT] NULL,
	[F_CIERRE]				[DATETIME] NULL,
	[LISTA_PRECIOS]			[NVARCHAR](4) NULL,
	[PAIS]					[NVARCHAR](6) NULL,
	[PROVINCIA]				[NVARCHAR](6) NULL,
	[CANTON]				[NVARCHAR](6) NULL,
	[DISTRITO]				[NVARCHAR](2) NULL,
	[DIRECCION]				[NVARCHAR](200) NULL,
	[TELEFONO]				[NVARCHAR](15) NULL,
	--DUEÑO
	[NOMBRE_ENC]			[NVARCHAR](80) NULL,
	[NO_AGENTE]				[NVARCHAR](4) NULL,
	--SE UTILIZA CON LOS CLIENTES NUEVOS
	[CLIENTE_NUEVO]	[NVARCHAR](6) NULL,
	--DETERMINA SI EL CLIENTE SE ENVIO, FUE CARGADO A LA MAQUINA
	[ENVIADO]				[NVARCHAR](1) NULL,
	[COPIAS_FAC]			[INT] NULL,
	[TIPO_ID_TRIBUTARIO]	[NVARCHAR](1) NULL,
	[NUEVO_CLIENTE]			[NVARCHAR](1) NULL,
	[CLASIFICACION]			[NVARCHAR](2) NULL,
	[RUTA_COBRO]			[NVARCHAR](4) NULL,
	[EMAIL]					[NVARCHAR](100) NULL,
	[LATITUD]				[NUMERIC](17,8) NULL,
	[LONGITUD]				[NUMERIC](17,8) NULL,
	[NOMBRE_APO]			[NVARCHAR](80) NULL,
	[CEDULA_APO]			[NVARCHAR](15) NULL,
	[PROVINCIA_APO]			[NVARCHAR](6) NULL,
	[CANTON_APO]			[NVARCHAR](6) NULL,
	[DISTRITO_APO]			[NVARCHAR](2) NULL,
	[DIRECCION_APO]			[NVARCHAR](200) NULL,
	[OBSERVACIONES]         [NVARCHAR](800) NULL,
	[DIAS_ATENCION]         [NVARCHAR](100) NULL,
 CONSTRAINT [PK_CLIENTE] PRIMARY KEY([NO_CIA], [NO_CLIENTE])
)