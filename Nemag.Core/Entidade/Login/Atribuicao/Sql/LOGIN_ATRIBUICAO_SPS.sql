CREATE PROCEDURE LOGIN_ATRIBUICAO_SPS 
    @LOGIN_ATRIBUICAO_ID INT = NULL, 
    @REGISTRO_LOGIN_ID INT = NULL, 
    @LOGIN_GRUPO_ID INT = NULL, 
    @LOGIN_PERFIL_ID INT = NULL 
AS BEGIN 
    SELECT 
        A.LOGIN_ATRIBUICAO_ID, 
        A.REGISTRO_LOGIN_ID, 
        A.LOGIN_GRUPO_ID, 
        A.LOGIN_PERFIL_ID 
    FROM 
        LOGIN_ATRIBUICAO_TB A 
    WHERE 
        A.LOGIN_ATRIBUICAO_ID = ISNULL(@LOGIN_ATRIBUICAO_ID, A.LOGIN_ATRIBUICAO_ID) 
        AND A.REGISTRO_LOGIN_ID = ISNULL(@REGISTRO_LOGIN_ID, A.REGISTRO_LOGIN_ID) 
        AND A.LOGIN_GRUPO_ID = ISNULL(@LOGIN_GRUPO_ID, A.LOGIN_GRUPO_ID) 
        AND A.LOGIN_PERFIL_ID = ISNULL(@LOGIN_PERFIL_ID, A.LOGIN_PERFIL_ID) 
END 