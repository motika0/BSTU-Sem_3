--1
SET NOCOUNT ON;
IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.������������'))
    DROP TABLE ������������;

SET IMPLICIT_TRANSACTIONS ON;

CREATE TABLE ������������ (
    [������������ID] INT NOT NULL,
    [��������] NVARCHAR(100) NULL,
    [���] NVARCHAR(50) NOT NULL,
    [����_��������] DATE NOT NULL,
    [����������] INT NOT NULL,
    [�������������ID] INT NOT NULL
);

INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
VALUES 
    (1, '����������', '������������', '2024-01-15', 5, 10),
    (2, '����', '������������', '2024-02-20', 3, 10),
    (3, '���������', '������������', '2024-03-10', 4, 10);

DECLARE @c INT
SET @c = (SELECT COUNT(*) FROM ������������);
PRINT '���������� ����� � ������� ������������: ' + CAST(@c AS NVARCHAR(10));

DECLARE @flag CHAR(1) = 'c'; -- commit ��� rollback?

-- �������� ����� ��� ���������� ����������
IF @flag = 'c' 
    COMMIT; -- ���������� ����������: ��������
ELSE 
    ROLLBACK; -- ���������� ����������: �����

SET IMPLICIT_TRANSACTIONS OFF;

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.������������'))
    PRINT '������� ������������ ����������';  
ELSE 
    PRINT '������� ������������ ���';
--2
SET NOCOUNT ON;
IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.������������'))
    DROP TABLE ������������;

CREATE TABLE [dbo].[������������] (
    [������������ID] INT NOT NULL PRIMARY KEY,
    [��������] NVARCHAR(100) NULL,
    [���] NVARCHAR(50) NOT NULL,
    [����_��������] DATE NOT NULL,
    [����������] INT NOT NULL,
    [�������������ID] INT NOT NULL
);

BEGIN TRY
    BEGIN TRANSACTION; 

    DELETE FROM ������������ WHERE �������� = '������ ����������';

    INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
    VALUES 
        (1, '����������', '������������', '2024-01-15', 5, 10),
        (2, '����', '������������', '2024-02-20', 3, 10),
        (3, '���������', '������������', '2024-03-10', 4, 10);

    COMMIT TRANSACTION;
    PRINT '���������� ������� ���������.';

END TRY
BEGIN CATCH
    PRINT '������: ' + CASE
        WHEN ERROR_NUMBER() = 2627 AND PATINDEX('%PK_������������%', ERROR_MESSAGE()) > 0 THEN '������������ ID ������������'
        ELSE '����������� ������: ' + CAST(ERROR_NUMBER() AS VARCHAR(5)) + ', ' + ERROR_MESSAGE() 
    END;

    IF @@TRANCOUNT > 0 
        ROLLBACK TRANSACTION;
END CATCH;

SELECT * FROM ������������; 
--3
SET NOCOUNT ON;

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.������������'))
    DROP TABLE ������������;

CREATE TABLE [dbo].[������������] (
    [����������ID] INT NOT NULL PRIMARY KEY,
    [��������] NVARCHAR(100) NULL,
    [���] NVARCHAR(50) NOT NULL,
    [����_��������] DATE NOT NULL,
    [����������] INT NOT NULL,
    [�������������ID] INT NOT NULL
);

DECLARE @point VARCHAR(32);

BEGIN TRY
    BEGIN TRANSACTION;

    DELETE FROM ������������ WHERE �������� = '������ ����������';

    -- ����������� ����� p1
    SET @point = 'p1';
    SAVE TRANSACTION @point; 

    INSERT INTO ������������ (����������ID, ��������, ���, ����_��������, ����������, �������������ID)
    VALUES 
        (1, '����������', '������������', '2024-01-15', 5, 10);

    -- ����������� ����� p2
    SET @point = 'p2';
    SAVE TRANSACTION @point; 

    INSERT INTO ������������ (����������ID, ��������, ���, ����_��������, ����������, �������������ID)
    VALUES 
        (2, '����', '������������', '2024-02-20', 3, 10);

    -- ��������, ������� ����� ������� ������
    INSERT INTO ������������ (����������ID, ��������, ���, ����_��������, ����������, �������������ID)
    VALUES 
        (1, '���������', '������������', '2024-03-10', 4, 10);

    COMMIT TRANSACTION;
    PRINT '���������� ������� ���������.';

END TRY
BEGIN CATCH
    PRINT '������: ' + CASE
        WHEN ERROR_NUMBER() = 2627 AND PATINDEX('%PK_����������%', ERROR_MESSAGE()) > 0 THEN '������������ ID ������������.'
        ELSE '����������� ������: ' + CAST(ERROR_NUMBER() AS VARCHAR(5)) + ', ' + ERROR_MESSAGE() 
    END;


    IF @@TRANCOUNT > 0 
    BEGIN
        PRINT '����������� �����: ' + @point;
        ROLLBACK TRANSACTION @point; -- ����� � ����������� �����
    END
END CATCH;

SELECT * FROM ������������; 
--4



--5
-- �������� A: ������ � ������� �������� READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 

BEGIN TRANSACTION;

-- t1:
SELECT COUNT(*) AS '����������_�������'
FROM ������
WHERE ������������_������ = '����';


COMMIT; -- ���������� ����������

-- �������� B: ���������� ������ � ������
BEGIN TRANSACTION;

-- t2:
UPDATE ������
SET ������������_������ = '����'
WHERE ������������_������ = '����';

COMMIT;
--6
-- �������� A: ������ � ������� �������� REPEATABLE READ
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ; 

BEGIN TRANSACTION;

-- t1:
SELECT �������� 
FROM ������ 
WHERE ������������_������ = '����';


COMMIT;
-- �������� B: ���������� ������ � ������� �������� READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION; 

-- ���������� t2:
INSERT INTO ������ (��������, ������������_������, ����, ����������, ����_������)
VALUES ('���', '����', 78, 10, '2014-12-01');

COMMIT;
--7
-- �������� A: ������ � ������� �������� SERIALIZABLE
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

DELETE FROM ������ WHERE �������� = '���';

INSERT INTO ������ (��������, ������������_������, ����, ����������, ����_������)
VALUES ('���', '����', 78, 10, '2014-12-01');

UPDATE ������ SET �������� = '���' WHERE ������������_������ = '����';

SELECT �������� FROM ������ WHERE ������������_������ = '����';


COMMIT;
-- �������� B: ������ � ������� �������� READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION;

DELETE FROM ������ WHERE �������� = '���';

INSERT INTO ������ (��������, ������������_������, ����, ����������, ����_������)
VALUES ('���', '����', 78, 10, '2014-12-01');

UPDATE ������ SET �������� = '���' WHERE ������������_������ = '����';

SELECT �������� FROM ������ WHERE ������������_������ = '����';


COMMIT;
--8
SET NOCOUNT ON;

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.������������'))
    DROP TABLE ������������;

CREATE TABLE [dbo].[������������] (
    [������������ID] INT NOT NULL PRIMARY KEY,
    [��������] NVARCHAR(100) NULL,
    [���] NVARCHAR(50) NOT NULL,
    [����_��������] DATE NOT NULL,
    [����������] INT NOT NULL,
    [�������������ID] INT NOT NULL
);

BEGIN TRY
    BEGIN TRANSACTION

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
        VALUES (1, '����������', '������������', '2024-01-15', 5, 10);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        PRINT '������ ��� ������� ������������: ' + ERROR_MESSAGE();
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
    END CATCH;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE ������������
        SET ���������� = 10
        WHERE �������� = '����������';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        PRINT '������ ��� ���������� ������������: ' + ERROR_MESSAGE();
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    END CATCH;

    COMMIT TRANSACTION;
    PRINT '��� �������� ��������� �������.';
END TRY
BEGIN CATCH
    PRINT '������ � ������� ����������: ' + ERROR_MESSAGE();
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
END CATCH;

SELECT * FROM ������������;