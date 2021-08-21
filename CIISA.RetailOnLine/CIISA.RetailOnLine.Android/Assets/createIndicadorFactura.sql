CREATE TABLE [INDICADORFACTURA](
	[NO_CIA]				[NVARCHAR](2) NOT NULL,
	[NO_CLIENTE]			[NVARCHAR](6) NOT NULL,
	[IND_PED]				[NVARCHAR](1) NULL,
	[IND_FACCONT]			[NVARCHAR](1) NULL,
	[IND_FACCRED]			[NVARCHAR](1) NULL,
	[IND_RESPETA_LIMITE]	[NVARCHAR](1) NULL,
	[IND_CHEQUE]			[NVARCHAR](1) NULL,
	[MONTO_LIMITE]			[DECIMAL](17, 2) NULL,
	[IND_VENCIMIENTO]		[NVARCHAR](1) NULL,
	[IND_ESTADO]			[NVARCHAR](1) NULL,
	[NO_AGENTE]				[NVARCHAR](6) NOT NULL,
	[COBRADOR]				[NVARCHAR](6) NOT NULL,
	[IND_COBRO]				[NVARCHAR](1) NOT NULL,
	--[DIA]					[INT] NOT NULL,
	[NO_ESTABLECIMIENTO]	[INT] NOT NULL,
	[IND_GEO]				[NVARCHAR](1) NOT NULL,
	[IND_NUM_ORDEN]			[NVARCHAR](1) NOT NULL,
    [IND_FECHA_ORDEN]		[NVARCHAR](1) NOT NULL, 
    [IND_NUM_RECEPCION]		[NVARCHAR](1) NOT NULL,
	[IND_FECHA_RECEPCION]	[NVARCHAR](1) NOT NULL,
    [IND_NUM_RECLAMO]		[NVARCHAR](1) NOT NULL, 
    [IND_FECHA_RECLAMO]		[NVARCHAR](1) NOT NULL, 
    [IND_COD_PROVEEDOR]		[NVARCHAR](1) NOT NULL,
	[TRAMITE_FACT]			[NVARCHAR](1) NULL,
 CONSTRAINT [PK_INDICADORFACTURA] PRIMARY KEY([NO_CIA], [NO_CLIENTE], [NO_AGENTE], [COBRADOR], [IND_COBRO], [NO_ESTABLECIMIENTO])
)