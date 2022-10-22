CREATE TABLE [dbo].[LOGIN_ATRIBUICAO_TB](
	[LOGIN_ATRIBUICAO_ID] [INT] IDENTITY(1,1) NOT NULL,
	[REGISTRO_LOGIN_ID] [INT] NOT NULL,
	[LOGIN_GRUPO_ID] [INT] NOT NULL,
	[LOGIN_PERFIL_ID] [INT] NOT NULL,
	CONSTRAINT [PK_LOGIN_ATRIBUICAO_TB] PRIMARY KEY CLUSTERED (
		[LOGIN_ATRIBUICAO_ID] ASC
	) WITH (
		PAD_INDEX = OFF, 
		STATISTICS_NORECOMPUTE = OFF, 
		IGNORE_DUP_KEY = OFF, 
		ALLOW_ROW_LOCKS = ON, 
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY],
	CONSTRAINT FK_LOGIN_ATRIBUICAO_1 FOREIGN KEY (REGISTRO_LOGIN_ID) REFERENCES LOGIN_TB (REGISTRO_LOGIN_ID),
	CONSTRAINT FK_LOGIN_ATRIBUICAO_2 FOREIGN KEY (LOGIN_GRUPO_ID) REFERENCES LOGIN_GRUPO_TB (LOGIN_GRUPO_ID),
	CONSTRAINT FK_LOGIN_ATRIBUICAO_3 FOREIGN KEY (LOGIN_PERFIL_ID) REFERENCES LOGIN_PERFIL_TB (LOGIN_PERFIL_ID)
) ON [PRIMARY]
GO