--1
CREATE PROCEDURE PSUBJECT
as
BEGIN
    DECLARE @row INT =(select count(*) from ОБОРУДОВАНИЕ);
    SELECT * FROM ОБОРУДОВАНИЕ;
    return @row;
END; 
exec psubject;
--2
ALTER PROCEDURE PSUBJECT 
    @p VARCHAR(20) = NULL, 
    @c INT OUTPUT
AS
BEGIN
    DECLARE @row INT;
    SELECT @row = COUNT(*) FROM ОБОРУДОВАНИЕ;
    PRINT 'параметры: @p = ' + @p + ', @c = ' + CAST(@c AS VARCHAR(3));
    SELECT * 
    FROM ОБОРУДОВАНИЕ 
    WHERE ПодразделениеID = @p;
    SET @c = @@ROWCOUNT;
    RETURN @row;
END;
GO

DECLARE @row INT;
DECLARE @count INT;

EXEC @row = PSUBJECT @p = '1', @c = @count OUTPUT;

PRINT 'кол-во оборудования = ' + CAST(@count AS VARCHAR(3));
PRINT 'общее количество ответственных = ' + CAST(@row AS VARCHAR(3));
--3
CREATE TABLE #SUBJECT (
    ПодразделениеID VARCHAR(20),
    Наименование VARCHAR(100),
    Количество INT,
);
ALTER PROCEDURE PSUBJECT 
    @p VARCHAR(20) = NULL
AS
BEGIN
    SELECT * FROM ОБОРУДОВАНИЕ 
    WHERE ПодразделениеID = @p;
END;
GO
INSERT INTO #SUBJECT (ПодразделениеID, Наименование, Количество);
INSERT #SUBJECT EXEC PSUBJECT @p = '1';

SELECT * FROM #SUBJECT;

DROP TABLE #SUBJECT;
--4
USE test;
GO

CREATE PROCEDURE TovaryInsert
    @t NVARCHAR(50), 
    @cn NVARCHAR(50), 
    @kl INT = NULL
AS
BEGIN
    DECLARE @rc INT = 1;

    BEGIN TRY
        INSERT INTO ОБОРУДОВАНИЕ (Название, тип, Количество) 
        VALUES (@t, @cn, @kl);
        RETURN @rc; 
    END TRY
    BEGIN CATCH
        PRINT 'номер ошибки: ' + CAST(ERROR_NUMBER() AS VARCHAR(6));
        PRINT 'сообщение: ' + ERROR_MESSAGE();
        PRINT 'уровень: ' + CAST(ERROR_SEVERITY() AS VARCHAR(6));
        PRINT 'метка: ' + CAST(ERROR_STATE() AS VARCHAR(8));
        PRINT 'номер строки: ' + CAST(ERROR_LINE() AS VARCHAR(8));
        IF ERROR_PROCEDURE() IS NOT NULL
            PRINT 'имя процедуры: ' + ERROR_PROCEDURE();
        RETURN -1;
    END CATCH;
END;
GO

DECLARE @rc INT;  

EXEC @rc = TovaryInsert @t = 'Планшет', @cn = 'Тип устройства', @kl = 90;  
DROP PROCEDURE TovaryInsert;

PRINT 'код ошибки: ' + CAST(@rc AS VARCHAR(3));
--5
CREATE PROCEDURE OBO_REPORT 
    @p CHAR(50)
AS  
BEGIN
    DECLARE @rc INT = 0;                            
    DECLARE @tv CHAR(20), @t CHAR(300) = ' ';  

    BEGIN TRY    
        DECLARE OBO_OB CURSOR FOR 
        SELECT Название FROM ОБОРУДОВАНИЕ WHERE ПодразделениеID = @p;

        IF NOT EXISTS (SELECT Название FROM ОБОРУДОВАНИЕ WHERE ПодразделениеID = @p)
            RAISERROR('Ошибка: нет оборудования для данного ПодразделениеID', 11, 1);
        ELSE 
            OPEN OBO_OB;	  
        
        FETCH NEXT FROM OBO_OB INTO @tv;

        PRINT 'Оборудование';   

        WHILE @@FETCH_STATUS = 0                                     
        BEGIN 
            SET @t = RTRIM(@tv) + ', ' + @t;  
            SET @rc = @rc + 1;       
            FETCH NEXT FROM OBO_OB INTO @tv; 
        END;   

        PRINT @t;        
        CLOSE OBO_OB;  
        DEALLOCATE OBO_OB;  

        RETURN @rc;
    END TRY  

    BEGIN CATCH              
        PRINT 'Ошибка в параметрах'; 

        IF ERROR_PROCEDURE() IS NOT NULL   
            PRINT 'Имя процедуры: ' + ERROR_PROCEDURE();

        RETURN @rc;
    END CATCH;
END;
GO
--6
USE test;
GO

CREATE PROCEDURE OBOinsert 
    @a INT, 
    @b NVARCHAR(50), 
    @d INT = NULL, 
    @e DATE, 
    @f INT   
AS  
BEGIN
    DECLARE @rc INT = 1;                            

    BEGIN TRY 
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;          
        BEGIN TRANSACTION;

        INSERT INTO ОБОРУДОВАНИЕ (ОборудованиеID, Название, Количество, Дата_прибытия, ПодразделениеID)
        VALUES (@a, @b, @d, @e, @f);

        
        COMMIT TRANSACTION; 
        RETURN @rc;           
    END TRY
    BEGIN CATCH 
        PRINT 'номер ошибки  : ' + CAST(ERROR_NUMBER() AS VARCHAR(6));
        PRINT 'сообщение     : ' + ERROR_MESSAGE();
        PRINT 'уровень       : ' + CAST(ERROR_SEVERITY() AS VARCHAR(6));
        PRINT 'метка         : ' + CAST(ERROR_STATE() AS VARCHAR(8));
        PRINT 'номер строки  : ' + CAST(ERROR_LINE() AS VARCHAR(8));
        
        IF ERROR_PROCEDURE() IS NOT NULL   
            PRINT 'имя процедуры : ' + ERROR_PROCEDURE();

        IF @@TRANCOUNT > 0 
            ROLLBACK TRANSACTION; 

        RETURN -1;	  
    END CATCH;
END;
GO


DECLARE @rc INT;  
EXEC @rc = OBOinsert @a = 20, @b = 'Стол', @d = 10, @e = '2014-12-01', @f = 2;  
PRINT 'код ошибки = ' + CAST(@rc AS VARCHAR(3));

drop PROCEDURE OBOinsert;