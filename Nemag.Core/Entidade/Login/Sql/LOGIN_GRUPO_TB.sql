CREATE TABLE [dbo].[LOGIN_GRUPO_TB](
	[LOGIN_GRUPO_ID] [INT] IDENTITY(1,1) NOT NULL,
	[NOME] [VARCHAR](50) NOT NULL,
	[DESCRICAO] [VARCHAR](500) NULL,
	 CONSTRAINT [PK_LOGIN_GRUPO_TB] PRIMARY KEY CLUSTERED (
		[LOGIN_GRUPO_ID] ASC
	) WITH (
		PAD_INDEX = OFF, 
		STATISTICS_NORECOMPUTE = OFF, 
		IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS = ON, 
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO