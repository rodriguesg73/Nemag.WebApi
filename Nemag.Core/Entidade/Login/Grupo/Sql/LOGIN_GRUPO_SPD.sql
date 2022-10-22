CREATE PROCEDURE LOGIN_GRUPO_SPD 
    @LOGIN_GRUPO_ID INT = NULL 
AS BEGIN 
    DELETE A FROM 
        LOGIN_GRUPO_TB A 
    WHERE 
        LOGIN_GRUPO_ID = ISNULL(@LOGIN_GRUPO_ID, A.LOGIN_GRUPO_ID) 
END 
