CREATE PROCEDURE LOGIN_SPI 
    @PESSOA_ID INT = NULL, 
    @USUARIO VARCHAR(50)  = NULL, 
    @SENHA VARCHAR(500)  = NULL 
AS BEGIN 
    INSERT INTO LOGIN_TB ( 
        PESSOA_ID, 
        USUARIO, 
        SENHA 
    ) VALUES ( 
        @PESSOA_ID, 
        @USUARIO, 
        @SENHA 
    ); 

    SELECT SCOPE_IDENTITY() AS REGISTRO_LOGIN_ID; 
END 