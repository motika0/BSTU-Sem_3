--1
USE test;
GO

CREATE TABLE TR_Equipment (
    ID INT IDENTITY, -- номер
    ST VARCHAR(20) CHECK (ST IN ('INS', 'DEL', 'UPD')), -- статус
    TRN VARCHAR(50), -- имя триггера
    C VARCHAR(300) -- комментарий
);

CREATE TRIGGER TRIG_Equipment_Ins
ON ОБОРУДОВАНИЕ
AFTER INSERT
AS
DECLARE @name NVARCHAR(100), @type NVARCHAR(50), @date DATE, @quantity INT, @in NVARCHAR(300);
PRINT 'Операция вставки';
SET @name = (SELECT [Название] FROM INSERTED);
SET @type = (SELECT [Тип] FROM INSERTED);
SET @date = (SELECT [Дата_прибытия] FROM INSERTED);
SET @quantity = (SELECT [Количество] FROM INSERTED);
SET @in = @name + ' ' + @type + ' ' + CAST(@date AS NVARCHAR(20)) + ' ' + CAST(@quantity AS NVARCHAR(20));
INSERT INTO TR_Equipment (ST, TRN, C) VALUES ('INS', 'TRIG_Equipment_Ins', @in);
RETURN;

INSERT INTO ОБОРУДОВАНИЕ (ОборудованиеID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
VALUES (1, 'Компьютер', 'Электроника', '2024-12-10', 10, 1);

SELECT * FROM TR_Equipment;

--2 и 3

CREATE TABLE TR_AUDIT (
    ID INT IDENTITY PRIMARY KEY, -- номер
    ST VARCHAR(20) CHECK (ST IN ('DEL', 'UPD')), -- статус
    TRN VARCHAR(50), -- имя триггера
    SS NVARCHAR(MAX) -- данные
);
CREATE TRIGGER TR_EQUIPMENT_DEL
ON ОБОРУДОВАНИЕ
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_AUDIT (ST, TRN, SS) VALUES ('DEL', 'TR_EQUIPMENT_DEL', @deletedData);
END;

CREATE TRIGGER TR_EQUIPMENT_UPD
ON ОБОРУДОВАНИЕ
AFTER UPDATE
AS
BEGIN
    DECLARE @oldData NVARCHAR(MAX), @newData NVARCHAR(MAX);
    SELECT @oldData = (SELECT * FROM DELETED FOR XML PATH(''));
    SELECT @newData = (SELECT * FROM INSERTED FOR XML PATH(''));
    INSERT INTO TR_AUDIT (ST, TRN, SS) VALUES ('UPD', 'TR_EQUIPMENT_UPD', 'Old Data: ' + @oldData + ' New Data: ' + @newData);
END;

INSERT INTO ОБОРУДОВАНИЕ (ОборудованиеID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
VALUES (1, 'Компьютер', 'Электроника', '2024-12-10', 10, 1);

DELETE FROM ОБОРУДОВАНИЕ WHERE ОборудованиеID = 1;

UPDATE ОБОРУДОВАНИЕ SET Название = 'Ноутбук' WHERE ОборудованиеID = 1;

SELECT * FROM TR_AUDIT;
--4
CREATE TABLE TR_AUDITT (
    ID INT IDENTITY PRIMARY KEY, -- номер
    ST VARCHAR(20) CHECK (ST IN ('INS', 'DEL', 'UPD')), -- статус
    TRN VARCHAR(50), -- имя триггера
    SS NVARCHAR(MAX) -- данные
);
CREATE TRIGGER TR_EQUIPMENTT
ON ОБОРУДОВАНИЕ
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    DECLARE @a1 NVARCHAR(100), @a2 NVARCHAR(50), @a3 DATE, @a4 INT, @a5 INT, @in NVARCHAR(MAX);
    DECLARE @ins INT = (SELECT COUNT(*) FROM INSERTED),
            @del INT = (SELECT COUNT(*) FROM DELETED);

    IF @ins > 0 AND @del = 0
    BEGIN
        PRINT 'Событие: INSERT';
        SET @a1 = (SELECT [Название] FROM INSERTED);
        SET @a2 = (SELECT [Тип] FROM INSERTED);
        SET @a3 = (SELECT [Дата_прибытия] FROM INSERTED);
        SET @a4 = (SELECT [Количество] FROM INSERTED);
        SET @a5 = (SELECT [ПодразделениеID] FROM INSERTED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20));
        INSERT INTO TR_AUDITT (ST, TRN, SS) VALUES ('INS', 'TR_EQUIPMENT', @in);
    END
    ELSE IF @ins = 0 AND @del > 0
    BEGIN
        PRINT 'Событие: DELETE';
        SET @a1 = (SELECT [Название] FROM DELETED);
        SET @a2 = (SELECT [Тип] FROM DELETED);
        SET @a3 = (SELECT [Дата_прибытия] FROM DELETED);
        SET @a4 = (SELECT [Количество] FROM DELETED);
        SET @a5 = (SELECT [ПодразделениеID] FROM DELETED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20));
        INSERT INTO TR_AUDITT (ST, TRN, SS) VALUES ('DEL', 'TR_EQUIPMENT', @in);
    END
    ELSE IF @ins > 0 AND @del > 0
    BEGIN
        PRINT 'Событие: UPDATE';
        SET @a1 = (SELECT [Название] FROM INSERTED);
        SET @a2 = (SELECT [Тип] FROM INSERTED);
        SET @a3 = (SELECT [Дата_прибытия] FROM INSERTED);
        SET @a4 = (SELECT [Количество] FROM INSERTED);
        SET @a5 = (SELECT [ПодразделениеID] FROM INSERTED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20));
        SET @a1 = (SELECT [Название] FROM DELETED);
        SET @a2 = (SELECT [Тип] FROM DELETED);
        SET @a3 = (SELECT [Дата_прибытия] FROM DELETED);
        SET @a4 = (SELECT [Количество] FROM DELETED);
        SET @a5 = (SELECT [ПодразделениеID] FROM DELETED);
        SET @in = @a1 + ' ' + @a2 + ' ' + CAST(@a3 AS NVARCHAR(20)) + ' ' + CAST(@a4 AS NVARCHAR(20)) + ' ' + CAST(@a5 AS NVARCHAR(20)) + ' ' + @in;
        INSERT INTO TR_AUDITT (ST, TRN, SS) VALUES ('UPD', 'TR_EQUIPMENT', @in);
    END;
END;
INSERT INTO ОБОРУДОВАНИЕ (ОборудованиеID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
VALUES (1, 'Компьютер', 'Электроника', '2024-12-10', 10, 1);

DELETE FROM ОБОРУДОВАНИЕ WHERE ОборудованиеID = 1;

UPDATE ОБОРУДОВАНИЕ SET Название = 'Ноутбук' WHERE ОборудованиеID = 1;

SELECT * FROM TR_AUDITT;
--5
CREATE TABLE TR_EQUIPM (
    ID INT IDENTITY PRIMARY KEY, -- номер
    ST VARCHAR(20) CHECK (ST IN ('DEL')), -- статус
    TRN VARCHAR(50), -- имя триггера
    SS NVARCHAR(MAX) -- данные
);
CREATE TRIGGER TR_OBO_DEL1
ON ОБОРУДОВАНИЕ
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_EQUIPM (ST, TRN, SS) VALUES ('DEL', 'TR_OBO_DEL1', @deletedData);
END;
GO

CREATE TRIGGER TR_OBO_DEL2
ON ОБОРУДОВАНИЕ
AFTER DELETE
AS
BEGIN
    DECLARE @deletedData NVARCHAR(MAX);
    SELECT @deletedData = (SELECT * FROM DELETED FOR XML PATH(''));
    INSERT INTO TR_EQUIPM (ST, TRN, SS) VALUES ('DEL', 'TR_OBO_DEL2', @deletedData);
END;
GO

CREATE TRIGGER TR_OBO_DEL3
ON ОБОРУДОВАНИЕ
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
WHERE OBJECT_NAME(t.parent_id) = 'ОБОРУДОВАНИЕ' AND e.type_desc = 'DELETE';

EXEC SP_SETTRIGGERORDER @triggername = 'TR_OBO_DEL3', 
                        @order = 'First', @stmttype = 'DELETE';

EXEC SP_SETTRIGGERORDER @triggername = 'TR_OBO_DEL2', 
                        @order = 'Last', @stmttype = 'DELETE';
INSERT INTO ОБОРУДОВАНИЕ (ОборудованиеID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
VALUES (1, 'Компьютер', 'Электроника', '2024-12-10', 10, 1);

DELETE FROM ОБОРУДОВАНИЕ WHERE ОборудованиеID = 1;

SELECT * FROM TR_EQUIPM;
--6
CREATE TABLE TR_EQUIP (
    ID INT IDENTITY PRIMARY KEY, -- номер
    ST VARCHAR(20) CHECK (ST IN ('INS', 'DEL', 'UPD')), -- статус
    TRN VARCHAR(50), -- имя триггера
    SS NVARCHAR(MAX) -- данные
);
CREATE TRIGGER TR_OBO_TRAN
ON ОБОРУДОВАНИЕ
AFTER INSERT, DELETE, UPDATE
AS
BEGIN
    DECLARE @c INT = (SELECT SUM(Количество) FROM ОБОРУДОВАНИЕ);
    IF (@c > 2000)
    BEGIN
        RAISERROR('Общее количество оборудования не может быть >2000', 10, 1);
        ROLLBACK;
    END;
    RETURN;
END;
GO

UPDATE ОБОРУДОВАНИЕ SET Количество = 2001 WHERE ОборудованиеID = 1;
--7
CREATE TRIGGER TR_OBO_INSTEAD_OF_DELETE
ON ОБОРУДОВАНИЕ INSTEAD OF DELETE
AS
raiserror('Удаление строк из таблицы ОБОРУДОВАНИЕ запрещено.',10,1);
rollback;
return;
drop TRIGGER TR_OBO_INSTEAD_OF_DELETE;

DELETE FROM ОБОРУДОВАНИЕ WHERE ОборудованиеID = 1;

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
        PRINT 'Тип события: ' + @eventType;
        PRINT 'Имя объекта: ' + @objectName;
        PRINT 'Тип объекта: ' + @objectType;
        RAISERROR(N'Операции создания новых таблиц и удаления существующих запрещены', 16, 1);
        ROLLBACK;
    END;
END;
GO

CREATE TABLE TestTable (
    ID INT PRIMARY KEY,
    Name NVARCHAR(50)
);

DROP TABLE ОБОРУДОВАНИЕ;

SELECT * FROM ОБОРУДОВАНИЕ;


