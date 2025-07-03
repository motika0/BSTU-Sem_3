--1
SET NOCOUNT ON;
IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.ОБОРУДОВАНИЯ'))
    DROP TABLE ОБОРУДОВАНИЯ;

SET IMPLICIT_TRANSACTIONS ON;

CREATE TABLE ОБОРУДОВАНИЯ (
    [ОБОРУДОВАНИЯID] INT NOT NULL,
    [Название] NVARCHAR(100) NULL,
    [Тип] NVARCHAR(50) NOT NULL,
    [Дата_прибытия] DATE NOT NULL,
    [Количество] INT NOT NULL,
    [ПодразделениеID] INT NOT NULL
);

INSERT INTO ОБОРУДОВАНИЯ (ОБОРУДОВАНИЯID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
VALUES 
    (1, 'Экскаватор', 'Строительное', '2024-01-15', 5, 10),
    (2, 'Кран', 'Строительное', '2024-02-20', 3, 10),
    (3, 'Бульдозер', 'Строительное', '2024-03-10', 4, 10);

DECLARE @c INT
SET @c = (SELECT COUNT(*) FROM ОБОРУДОВАНИЯ);
PRINT 'Количество строк в таблице оборудования: ' + CAST(@c AS NVARCHAR(10));

DECLARE @flag CHAR(1) = 'c'; -- commit или rollback?

-- Проверка флага для завершения транзакции
IF @flag = 'c' 
    COMMIT; -- Завершение транзакции: фиксация
ELSE 
    ROLLBACK; -- Завершение транзакции: откат

SET IMPLICIT_TRANSACTIONS OFF;

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.ОБОРУДОВАНИЯ'))
    PRINT 'Таблица оборудования существует';  
ELSE 
    PRINT 'Таблицы оборудования нет';
--2
SET NOCOUNT ON;
IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.ОБОРУДОВАНИЯ'))
    DROP TABLE ОБОРУДОВАНИЯ;

CREATE TABLE [dbo].[ОБОРУДОВАНИЯ] (
    [ОБОРУДОВАНИЯID] INT NOT NULL PRIMARY KEY,
    [Название] NVARCHAR(100) NULL,
    [Тип] NVARCHAR(50) NOT NULL,
    [Дата_прибытия] DATE NOT NULL,
    [Количество] INT NOT NULL,
    [ПодразделениеID] INT NOT NULL
);

BEGIN TRY
    BEGIN TRANSACTION; 

    DELETE FROM ОБОРУДОВАНИЯ WHERE Название = 'Старый Экскаватор';

    INSERT INTO ОБОРУДОВАНИЯ (ОБОРУДОВАНИЯID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
    VALUES 
        (1, 'Экскаватор', 'Строительное', '2024-01-15', 5, 10),
        (2, 'Кран', 'Строительное', '2024-02-20', 3, 10),
        (3, 'Бульдозер', 'Строительное', '2024-03-10', 4, 10);

    COMMIT TRANSACTION;
    PRINT 'Транзакция успешно завершена.';

END TRY
BEGIN CATCH
    PRINT 'Ошибка: ' + CASE
        WHEN ERROR_NUMBER() = 2627 AND PATINDEX('%PK_Оборудования%', ERROR_MESSAGE()) > 0 THEN 'Дублирование ID ОБОРУДОВАНИЯ'
        ELSE 'Неизвестная ошибка: ' + CAST(ERROR_NUMBER() AS VARCHAR(5)) + ', ' + ERROR_MESSAGE() 
    END;

    IF @@TRANCOUNT > 0 
        ROLLBACK TRANSACTION;
END CATCH;

SELECT * FROM ОБОРУДОВАНИЯ; 
--3
SET NOCOUNT ON;

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.ОБОРУДОВАНИЯ'))
    DROP TABLE ОБОРУДОВАНИЯ;

CREATE TABLE [dbo].[ОБОРУДОВАНИЯ] (
    [ОБОРУДАНИЯID] INT NOT NULL PRIMARY KEY,
    [Название] NVARCHAR(100) NULL,
    [Тип] NVARCHAR(50) NOT NULL,
    [Дата_прибытия] DATE NOT NULL,
    [Количество] INT NOT NULL,
    [ПодразделениеID] INT NOT NULL
);

DECLARE @point VARCHAR(32);

BEGIN TRY
    BEGIN TRANSACTION;

    DELETE FROM ОБОРУДОВАНИЯ WHERE Название = 'Старый Экскаватор';

    -- Контрольная точка p1
    SET @point = 'p1';
    SAVE TRANSACTION @point; 

    INSERT INTO ОБОРУДОВАНИЯ (ОБОРУДАНИЯID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
    VALUES 
        (1, 'Экскаватор', 'Строительное', '2024-01-15', 5, 10);

    -- Контрольная точка p2
    SET @point = 'p2';
    SAVE TRANSACTION @point; 

    INSERT INTO ОБОРУДОВАНИЯ (ОБОРУДАНИЯID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
    VALUES 
        (2, 'Кран', 'Строительное', '2024-02-20', 3, 10);

    -- Операция, которая может вызвать ошибку
    INSERT INTO ОБОРУДОВАНИЯ (ОБОРУДАНИЯID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
    VALUES 
        (1, 'Бульдозер', 'Строительное', '2024-03-10', 4, 10);

    COMMIT TRANSACTION;
    PRINT 'Транзакция успешно завершена.';

END TRY
BEGIN CATCH
    PRINT 'Ошибка: ' + CASE
        WHEN ERROR_NUMBER() = 2627 AND PATINDEX('%PK_ОБОРУДАНИЯ%', ERROR_MESSAGE()) > 0 THEN 'Дублирование ID оборудования.'
        ELSE 'Неизвестная ошибка: ' + CAST(ERROR_NUMBER() AS VARCHAR(5)) + ', ' + ERROR_MESSAGE() 
    END;


    IF @@TRANCOUNT > 0 
    BEGIN
        PRINT 'Контрольная точка: ' + @point;
        ROLLBACK TRANSACTION @point; -- Откат к контрольной точке
    END
END CATCH;

SELECT * FROM ОБОРУДОВАНИЯ; 
--4



--5
-- Сценарий A: Чтение с уровнем изоляции READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 

BEGIN TRANSACTION;

-- t1:
SELECT COUNT(*) AS 'Количество_Заказов'
FROM Заказы
WHERE Наименование_товара = 'Стул';


COMMIT; -- Завершение транзакции

-- Сценарий B: Обновление товара в Заказы
BEGIN TRANSACTION;

-- t2:
UPDATE Заказы
SET Наименование_товара = 'Стул'
WHERE Наименование_товара = 'Стол';

COMMIT;
--6
-- Сценарий A: Чтение с уровнем изоляции REPEATABLE READ
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ; 

BEGIN TRANSACTION;

-- t1:
SELECT Заказчик 
FROM Заказы 
WHERE Наименование_товара = 'Стул';


COMMIT;
-- Сценарий B: Обновление данных с уровнем изоляции READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION; 

-- Транзакция t2:
INSERT INTO Заказы (Заказчик, Наименование_товара, Цена, Количество, Дата_заказа)
VALUES ('Луч', 'Стул', 78, 10, '2014-12-01');

COMMIT;
--7
-- Сценарий A: Чтение с уровнем изоляции SERIALIZABLE
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

BEGIN TRANSACTION;

DELETE FROM Заказы WHERE Заказчик = 'Луч';

INSERT INTO Заказы (Заказчик, Наименование_товара, Цена, Количество, Дата_заказа)
VALUES ('Луч', 'Стул', 78, 10, '2014-12-01');

UPDATE Заказы SET Заказчик = 'Луч' WHERE Наименование_товара = 'Стул';

SELECT Заказчик FROM Заказы WHERE Наименование_товара = 'Стул';


COMMIT;
-- Сценарий B: Чтение с уровнем изоляции READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

BEGIN TRANSACTION;

DELETE FROM Заказы WHERE Заказчик = 'Луч';

INSERT INTO Заказы (Заказчик, Наименование_товара, Цена, Количество, Дата_заказа)
VALUES ('Луч', 'Стул', 78, 10, '2014-12-01');

UPDATE Заказы SET Заказчик = 'Луч' WHERE Наименование_товара = 'Стул';

SELECT Заказчик FROM Заказы WHERE Наименование_товара = 'Стул';


COMMIT;
--8
SET NOCOUNT ON;

IF EXISTS (SELECT * FROM SYS.OBJECTS WHERE OBJECT_ID = OBJECT_ID(N'DBO.ОБОРУДОВАНИЯ'))
    DROP TABLE ОБОРУДОВАНИЯ;

CREATE TABLE [dbo].[ОБОРУДОВАНИЯ] (
    [ОборудованиеID] INT NOT NULL PRIMARY KEY,
    [Название] NVARCHAR(100) NULL,
    [Тип] NVARCHAR(50) NOT NULL,
    [Дата_прибытия] DATE NOT NULL,
    [Количество] INT NOT NULL,
    [ПодразделениеID] INT NOT NULL
);

BEGIN TRY
    BEGIN TRANSACTION

    BEGIN TRY
        BEGIN TRANSACTION;

        INSERT INTO ОБОРУДОВАНИЯ (ОборудованиеID, Название, Тип, Дата_прибытия, Количество, ПодразделениеID)
        VALUES (1, 'Экскаватор', 'Строительное', '2024-01-15', 5, 10);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        PRINT 'Ошибка при вставке оборудования: ' + ERROR_MESSAGE();
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION; 
    END CATCH;

    BEGIN TRY
        BEGIN TRANSACTION;

        UPDATE ОБОРУДОВАНИЯ
        SET Количество = 10
        WHERE Название = 'Экскаватор';

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        PRINT 'Ошибка при обновлении оборудования: ' + ERROR_MESSAGE();
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    END CATCH;

    COMMIT TRANSACTION;
    PRINT 'Все операции выполнены успешно.';
END TRY
BEGIN CATCH
    PRINT 'Ошибка в внешней транзакции: ' + ERROR_MESSAGE();
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
END CATCH;

SELECT * FROM ОБОРУДОВАНИЯ;