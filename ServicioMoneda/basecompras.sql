CREATE DATABASE [basecompras];

CREATE TABLE [dbo].[USUARIOS](
	[ID] [int] NOT NULL,
	[Nombre] [varchar](80) COLLATE Modern_Spanish_CI_AS NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY];

CREATE TABLE [dbo].[COMPRAS](
	[ID_COMPRA] [uniqueidentifier] NOT NULL,
	[ID_USER] [int] NOT NULL,
	[VALOR] [decimal](18, 2) NULL,
	[MONEDA] [varchar](60) COLLATE Modern_Spanish_CI_AS NULL,
	[FECHA] [varchar](20) COLLATE Modern_Spanish_CI_AS NULL,
	[DIA] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[MES] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
	[ANO] [varchar](50) COLLATE Modern_Spanish_CI_AS NULL,
 CONSTRAINT [PK_COMPRAS] PRIMARY KEY CLUSTERED 
(
	[ID_COMPRA] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[COMPRAS]  WITH CHECK ADD  CONSTRAINT [FK_COMPRAS_USUARIOS] FOREIGN KEY([ID_USER])
REFERENCES [dbo].[USUARIOS] ([ID]);

CREATE TABLE [dbo].[LOG](
	[ID_LOG] [uniqueidentifier] NULL,
	[MENSAJE] [varchar](1000) COLLATE Modern_Spanish_CI_AS NULL,
	[FECHA] [varchar](40) COLLATE Modern_Spanish_CI_AS NULL
) ON [PRIMARY];

INSERT INTO [dbo].[USUARIOS] VALUES(1 , 'Virtual Mind User 1');
INSERT INTO [dbo].[USUARIOS] VALUES(2 , 'Virtual Mind User 2');

