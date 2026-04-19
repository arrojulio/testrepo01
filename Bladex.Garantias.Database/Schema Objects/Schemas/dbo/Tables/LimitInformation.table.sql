CREATE TABLE [dbo].[LimitInformation](
	[DefinitionID] [int] NOT NULL,
	[CustomerId] [varchar](50) NOT NULL,
	[MatrixId] [int] NOT NULL,
	[MatrixName] [varchar](200) NOT NULL,
	[LimitValue] [float] NOT NULL,
	[LastLimit] [float] NULL,
	[ExpirationDate] [datetime] NOT NULL,
	[Comments] [varchar](max) NOT NULL,
	[Active] [bit] NOT NULL,
	[MatrixStateId] [int] NOT NULL,
	[MatrixStateDescription] [varchar](255) NOT NULL,
	[MatrixTypeId] [int] NOT NULL,
	[MatrixTypeDescription] [varchar](50) NOT NULL,
 CONSTRAINT [PK_LimitInformation] PRIMARY KEY CLUSTERED 
(
	[DefinitionID] ASC,
	[CustomerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

