-- Сценарий A: Чтение с уровнями изоляции READ UNCOMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 

BEGIN TRANSACTION; 

-- t1: 
SELECT @@SPID AS 'SessionID', 'Чтение из ОБОРУДОВАНИЯ' AS 'Результат', *
FROM ОБОРУДОВАНИЯ
WHERE Название = 'Экскаватор';
UPDATE ОБОРУДОВАНИЯ
SET Название = 'Эксковатор'
WHERE Название = 'Эксковатор';ы

-- 
SELECT @@SPID AS 'SessionID', 'Чтение из Заказы' AS 'Результат', Должность, РабочиеID
FROM ОТВЕТСТВЕНННЫЕ
WHERE Должность = 'Офицер';

COMMIT; -- Завершение транзакции
