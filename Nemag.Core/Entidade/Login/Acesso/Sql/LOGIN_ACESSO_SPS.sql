ALTER PROCEDURE LOGIN_ACESSO_SPS 
    @LOGIN_ACESSO_ID INT = NULL, 
    @REGISTRO_LOGIN_ID INT = NULL,
	@IP VARCHAR(40) = NULL,
	@DATA_INCLUSAO SMALLDATETIME = NULL,
	@DATA_VALIDADE SMALLDATETIME = NULL
AS BEGIN 
    SELECT 
        A.LOGIN_ACESSO_ID, 
        A.REGISTRO_LOGIN_ID, 
        A.TOKEN, 
        A.IP, 
        A.DATA_INCLUSAO, 
        A.DATA_VALIDADE 
    FROM 
        LOGIN_ACESSO_TB A 
    WHERE 
        A.LOGIN_ACESSO_ID = ISNULL(@LOGIN_ACESSO_ID, A.LOGIN_ACESSO_ID) 
        AND A.REGISTRO_LOGIN_ID = ISNULL(@REGISTRO_LOGIN_ID, A.REGISTRO_LOGIN_ID) 
		AND A.IP = ISNULL(@IP, A.IP)
		AND A.DATA_INCLUSAO >= ISNULL(@DATA_INCLUSAO, A.DATA_INCLUSAO)
		AND A.DATA_VALIDADE >= ISNULL(@DATA_VALIDADE, A.DATA_VALIDADE)
END