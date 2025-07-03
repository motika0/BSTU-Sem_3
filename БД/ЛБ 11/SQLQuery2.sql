
-- Сценарий B: Чтение с уровнями изоляции READ COMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 

BEGIN TRANSACTION; 

-- t2:
SELECT @@SPID AS 'SessionID', 'Чтение из ОБОРУДОВАНИЯ' AS 'Результат', *
FROM ОБОРУДОВАНИЯ
WHERE Название = 'Экскаватор'; 
UPDATE ОБОРУДОВАНИЯ
SET Название = 'Эксковатор'
WHERE Название = 'Экскаватор';

--
SELECT @@SPID AS 'SessionID', 'Чтение из Заказы' AS 'Результат', Должность, РабочиеID
FROM ОТВЕТСТВЕНННЫЕ
WHERE Должность = 'Офицер';

COMMIT; -- Завершение транзакции