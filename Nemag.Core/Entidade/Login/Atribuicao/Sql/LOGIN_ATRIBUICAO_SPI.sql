CREATE PROCEDURE LOGIN_ATRIBUICAO_SPI 
    @REGISTRO_LOGIN_ID INT = NULL, 
    @LOGIN_GRUPO_ID INT = NULL, 
    @LOGIN_PERFIL_ID INT = NULL 
AS BEGIN 
    INSERT INTO LOGIN_ATRIBUICAO_TB ( 
        REGISTRO_LOGIN_ID, 
        LOGIN_GRUPO_ID, 
        LOGIN_PERFIL_ID 
    ) VALUES ( 
        @REGISTRO_LOGIN_ID, 
        @LOGIN_GRUPO_ID, 
        @LOGIN_PERFIL_ID 
    ); 

    SELECT SCOPE_IDENTITY() AS LOGIN_ATRIBUICAO_ID; 
END 