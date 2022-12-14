CREATE PROCEDURE LOGIN_ACESSO_SPI 
    @REGISTRO_LOGIN_ID INT = NULL, 
    @TOKEN VARCHAR(64)  = NULL, 
    @IP VARCHAR(40)  = NULL, 
    @DATA_INCLUSAO SMALLDATETIME = NULL, 
    @DATA_VALIDADE SMALLDATETIME = NULL 
AS BEGIN 
    INSERT INTO LOGIN_ACESSO_TB ( 
        REGISTRO_LOGIN_ID, 
        TOKEN, 
        IP, 
        DATA_INCLUSAO, 
        DATA_VALIDADE 
    ) VALUES ( 
        @REGISTRO_LOGIN_ID, 
        @TOKEN, 
        @IP, 
        @DATA_INCLUSAO, 
        @DATA_VALIDADE 
    ); 

    SELECT SCOPE_IDENTITY() AS LOGIN_ACESSO_ID; 
END 
