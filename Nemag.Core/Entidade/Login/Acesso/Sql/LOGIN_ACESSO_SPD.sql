CREATE PROCEDURE LOGIN_ACESSO_SPD 
    @LOGIN_ACESSO_ID INT = NULL 
AS BEGIN 
    DELETE A FROM 
        LOGIN_ACESSO_TB A 
    WHERE 
        LOGIN_ACESSO_ID = ISNULL(@LOGIN_ACESSO_ID, A.LOGIN_ACESSO_ID) 
END 
