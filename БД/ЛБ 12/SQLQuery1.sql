--1
CREATE PROCEDURE PSUBJECT
as
BEGIN
    DECLARE @row INT =(select count(*) from ������������);
    SELECT * FROM ������������;
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
    SELECT @row = COUNT(*) FROM ������������;
    PRINT '���������: @p = ' + @p + ', @c = ' + CAST(@c AS VARCHAR(3));
    SELECT * 
    FROM ������������ 
    WHERE �������������ID = @p;
    SET @c = @@ROWCOUNT;
    RETURN @row;
END;
GO

DECLARE @row INT;
DECLARE @count INT;

EXEC @row = PSUBJECT @p = '1', @c = @count OUTPUT;

PRINT '���-�� ������������ = ' + CAST(@count AS VARCHAR(3));
PRINT '����� ���������� ������������� = ' + CAST(@row AS VARCHAR(3));
--3
CREATE TABLE #SUBJECT (
    �������������ID VARCHAR(20),
    ������������ VARCHAR(100),
    ���������� INT,
);
ALTER PROCEDURE PSUBJECT 
    @p VARCHAR(20) = NULL
AS
BEGIN
    SELECT * FROM ������������ 
    WHERE �������������ID = @p;
END;
GO
INSERT INTO #SUBJECT (�������������ID, ������������, ����������);
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
        INSERT INTO ������������ (��������, ���, ����������) 
        VALUES (@t, @cn, @kl);
        RETURN @rc; 
    END TRY
    BEGIN CATCH
        PRINT '����� ������: ' + CAST(ERROR_NUMBER() AS VARCHAR(6));
        PRINT '���������: ' + ERROR_MESSAGE();
        PRINT '�������: ' + CAST(ERROR_SEVERITY() AS VARCHAR(6));
        PRINT '�����: ' + CAST(ERROR_STATE() AS VARCHAR(8));
        PRINT '����� ������: ' + CAST(ERROR_LINE() AS VARCHAR(8));
        IF ERROR_PROCEDURE() IS NOT NULL
            PRINT '��� ���������: ' + ERROR_PROCEDURE();
        RETURN -1;
    END CATCH;
END;
GO

DECLARE @rc INT;  

EXEC @rc = TovaryInsert @t = '�������', @cn = '��� ����������', @kl = 90;  
DROP PROCEDURE TovaryInsert;

PRINT '��� ������: ' + CAST(@rc AS VARCHAR(3));
--5
CREATE PROCEDURE OBO_REPORT 
    @p CHAR(50)
AS  
BEGIN
    DECLARE @rc INT = 0;                            
    DECLARE @tv CHAR(20), @t CHAR(300) = ' ';  

    BEGIN TRY    
        DECLARE OBO_OB CURSOR FOR 
        SELECT �������� FROM ������������ WHERE �������������ID = @p;

        IF NOT EXISTS (SELECT �������� FROM ������������ WHERE �������������ID = @p)
            RAISERROR('������: ��� ������������ ��� ������� �������������ID', 11, 1);
        ELSE 
            OPEN OBO_OB;	  
        
        FETCH NEXT FROM OBO_OB INTO @tv;

        PRINT '������������';   

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
        PRINT '������ � ����������'; 

        IF ERROR_PROCEDURE() IS NOT NULL   
            PRINT '��� ���������: ' + ERROR_PROCEDURE();

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

        INSERT INTO ������������ (������������ID, ��������, ����������, ����_��������, �������������ID)
        VALUES (@a, @b, @d, @e, @f);

        
        COMMIT TRANSACTION; 
        RETURN @rc;           
    END TRY
    BEGIN CATCH 
        PRINT '����� ������  : ' + CAST(ERROR_NUMBER() AS VARCHAR(6));
        PRINT '���������     : ' + ERROR_MESSAGE();
        PRINT '�������       : ' + CAST(ERROR_SEVERITY() AS VARCHAR(6));
        PRINT '�����         : ' + CAST(ERROR_STATE() AS VARCHAR(8));
        PRINT '����� ������  : ' + CAST(ERROR_LINE() AS VARCHAR(8));
        
        IF ERROR_PROCEDURE() IS NOT NULL   
            PRINT '��� ��������� : ' + ERROR_PROCEDURE();

        IF @@TRANCOUNT > 0 
            ROLLBACK TRANSACTION; 

        RETURN -1;	  
    END CATCH;
END;
GO


DECLARE @rc INT;  
EXEC @rc = OBOinsert @a = 20, @b = '����', @d = 10, @e = '2014-12-01', @f = 2;  
PRINT '��� ������ = ' + CAST(@rc AS VARCHAR(3));

drop PROCEDURE OBOinsert;