--1
USE test;
GO

CREATE TABLE TR_Equipment (
    ID INT IDENTITY, -- �����
    ST VARCHAR(20) CHECK (ST IN ('INS', 'DEL', 'UPD')), -- ������
    TRN VARCHAR(50), -- ��� ��������
    C VARCHAR(300) -- �����������
);

CREATE TRIGGER TRIG_Equipment_Ins
ON ������������
AFTER INSERT
AS
DECLARE @name NVARCHAR(100), @type NVARCHAR(50), @date DATE, @quantity INT, @in NVARCHAR(300);
PRINT '�������� �������';
SET @name = (SELECT [��������] FROM INSERTED);
SET @type = (SELECT [���] FROM INSERTED);
SET @date = (SELECT [����_��������] FROM INSERTED);
SET @quantity = (SELECT [����������] FROM INSERTED);
SET @in = @name + ' ' + @type + ' ' + CAST(@date AS NVARCHAR(20)) + ' ' + CAST(@quantity AS NVARCHAR(20));
INSERT INTO TR_Equipment (ST, TRN, C) VALUES ('INS', 'TRIG_Equipment_Ins', @in);
RETURN;

INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
VALUES (1, '���������', '�����������', '2024-12-10', 10, 1);

SELECT * FROM TR_Equipment;

--2 � 3

CREATE TABLE TR_AUDIT (
    ID INT IDENTITY PRIMARY KEY, -- �����
    ST VARCHAR(20) CHECK (ST IN ('DEL', 'UPD')), -- ������
    TRN VARCHAR(50), -- ��� ��������
    SS NVARCHAR(MAX) -- ������
);
CREATE TRIGGER TR_EQUIPMENT_DEL
ON ������������
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_AUDIT (ST, TRN, SS) VALUES ('DEL', 'TR_EQUIPMENT_DEL', @deletedData);
END;

CREATE TRIGGER TR_EQUIPMENT_UPD
ON ������������
AFTER UPDATE
AS
BEGIN
    DECLARE @oldData NVARCHAR(MAX), @newData NVARCHAR(MAX);
    SELECT @oldData = (SELECT * FROM DELETED FOR XML PATH(''));
    SELECT @newData = (SELECT * FROM INSERTED FOR XML PATH(''));
    INSERT INTO TR_AUDIT (ST, TRN, SS) VALUES ('UPD', 'TR_EQUIPMENT_UPD', 'Old Data: ' + @oldData + ' New Data: ' + @newData);
END;

INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
VALUES (1, '���������', '�����������', '2024-12-10', 10, 1);

DELETE FROM ������������ WHERE ������������ID = 1;

UPDATE ������������ SET �������� = '�������' WHERE ������������ID = 1;

SELECT * FROM TR_AUDIT;
--4
CREATE TABLE TR_AUDITT (
    ID INT IDENTITY PRIMARY KEY, -- �����
    ST VARCHAR(20) CHECK (ST IN ('INS', 'DEL', 'UPD')), -- ������
    TRN VARCHAR(50), -- ��� ��������
    SS NVARCHAR(MAX) -- ������
);
CREATE TRIGGER TR_EQUIPMENTT
ON ������������
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    DECLARE @a1 NVARCHAR(100), @a2 NVARCHAR(50), @a3 DATE, @a4 INT, @a5 INT, @in NVARCHAR(MAX);
    DECLARE @ins INT = (SELECT COUNT(*) FROM INSERTED),
            @del INT = (SELECT COUNT(*) FROM DELETED);

    IF @ins > 0 AND @del = 0
    BEGIN
        PRINT '�������: INSERT';
        SET @a1 = (SELECT [��������] FROM INSERTED);
        SET @a2 = (SELECT [���] FROM INSERTED);
        SET @a3 = (SELECT [����_��������] FROM INSERTED);
        SET @a4 = (SELECT [����������] FROM INSERTED);
        SET @a5 = (SELECT [�������������ID] FROM INSERTED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20));
        INSERT INTO TR_AUDITT (ST, TRN, SS) VALUES ('INS', 'TR_EQUIPMENT', @in);
    END
    ELSE IF @ins = 0 AND @del > 0
    BEGIN
        PRINT '�������: DELETE';
        SET @a1 = (SELECT [��������] FROM DELETED);
        SET @a2 = (SELECT [���] FROM DELETED);
        SET @a3 = (SELECT [����_��������] FROM DELETED);
        SET @a4 = (SELECT [����������] FROM DELETED);
        SET @a5 = (SELECT [�������������ID] FROM DELETED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20));
        INSERT INTO TR_AUDITT (ST, TRN, SS) VALUES ('DEL', 'TR_EQUIPMENT', @in);
    END
    ELSE IF @ins > 0 AND @del > 0
    BEGIN
        PRINT '�������: UPDATE';
        SET @a1 = (SELECT [��������] FROM INSERTED);
        SET @a2 = (SELECT [���] FROM INSERTED);
        SET @a3 = (SELECT [����_��������] FROM INSERTED);
        SET @a4 = (SELECT [����������] FROM INSERTED);
        SET @a5 = (SELECT [�������������ID] FROM INSERTED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20));
        SET @a1 = (SELECT [��������] FROM DELETED);
        SET @a2 = (SELECT [���] FROM DELETED);
        SET @a3 = (SELECT [����_��������] FROM DELETED);
        SET @a4 = (SELECT [����������] FROM DELETED);
        SET @a5 = (SELECT [�������������ID] FROM DELETED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20)) + ' ' + @in;
        INSERT INTO TR_AUDITT (ST, TRN, SS) VALUES ('UPD', 'TR_EQUIPMENT', @in);
    END;
END;
INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
VALUES (1, '���������', '�����������', '2024-12-10', 10, 1);

DELETE FROM ������������ WHERE ������������ID = 1;

UPDATE ������������ SET �������� = '�������' WHERE ������������ID = 1;

SELECT * FROM TR_AUDITT;
--5
CREATE TABLE TR_EQUIPM (
    ID INT IDENTITY PRIMARY KEY, -- �����
    ST VARCHAR(20) CHECK (ST IN ('DEL')), -- ������
    TRN VARCHAR(50), -- ��� ��������
    SS NVARCHAR(MAX) -- ������
);
CREATE TRIGGER TR_OBO_DEL1
ON ������������
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_EQUIPM (ST, TRN, SS) VALUES ('DEL', 'TR_OBO_DEL1', @deletedData);
END;
GO

CREATE TRIGGER TR_OBO_DEL2
ON ������������
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_EQUIPM (ST, TRN, SS) VALUES ('DEL', 'TR_OBO_DEL2', @deletedData);
END;
GO

CREATE TRIGGER TR_OBO_DEL3
ON ������������
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_EQUIPM (ST, TRN, SS) VALUES ('DEL', 'TR_OBO_DEL3', @deletedData);
END;
GO
SELECT t.name, e.type_desc 
FROM sys.triggers t 
JOIN sys.trigger_events e ON t.object_id = e.object_id  
WHERE OBJECT_NAME(t.parent_id) = '������������' AND e.type_desc = 'DELETE';

EXEC SP_SETTRIGGERORDER @triggername = 'TR_OBO_DEL3', 
                        @order = 'First', @stmttype = 'DELETE';

EXEC SP_SETTRIGGERORDER @triggername = 'TR_OBO_DEL2', 
                        @order = 'Last', @stmttype = 'DELETE';
INSERT INTO ������������ (������������ID, ��������, ���, ����_��������, ����������, �������������ID)
VALUES (1, '���������', '�����������', '2024-12-10', 10, 1);

DELETE FROM ������������ WHERE ������������ID = 1;

SELECT * FROM TR_EQUIPM;
--6
CREATE TABLE TR_EQUIP (
    ID INT IDENTITY PRIMARY KEY, -- �����
    ST VARCHAR(20) CHECK (ST IN ('INS', 'DEL', 'UPD')), -- ������
    TRN VARCHAR(50), -- ��� ��������
    SS NVARCHAR(MAX) -- ������
);
CREATE TRIGGER TR_OBO_TRAN
ON ������������
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    DECLARE @c INT = (SELECT SUM(����������) FROM ������������);
    IF (@c > 2000)
    BEGIN
        RAISERROR('����� ���������� ������������ �� ����� ���� >2000', 10, 1);
        ROLLBACK;
    END;
    RETURN;
END;
GO

UPDATE ������������ SET ���������� = 2001 WHERE ������������ID = 1;
--7
CREATE TRIGGER TR_OBO_INSTEAD_OF_DELETE
ON ������������ INSTEAD OF DELETE
AS
raiserror('�������� ����� �� ������� ������������ ���������.',10,1);
rollback;
return;
drop TRIGGER TR_OBO_INSTEAD_OF_DELETE;

DELETE FROM ������������ WHERE ������������ID = 1;

--8

CREATE TRIGGER DDL_UNIVER
ON DATABASE 
FOR DDL_DATABASE_LEVEL_EVENTS
AS
BEGIN
    DECLARE @eventType NVARCHAR(50) = EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]', 'NVARCHAR(50)');
    DECLARE @objectName NVARCHAR(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'NVARCHAR(50)');
    DECLARE @objectType NVARCHAR(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]', 'NVARCHAR(50)');

    IF @eventType IN ('CREATE_TABLE', 'DROP_TABLE')
    BEGIN
        PRINT '��� �������: ' + @eventType;
        PRINT '��� �������: ' + @objectName;
        PRINT '��� �������: ' + @objectType;
        RAISERROR(N'�������� �������� ����� ������ � �������� ������������ ���������', 16, 1);
        ROLLBACK;
    END;
END;
GO

CREATE TABLE TestTable (
    ID INT PRIMARY KEY,
    Name NVARCHAR(50)
);

DROP TABLE ������������;

SELECT * FROM ������������;


