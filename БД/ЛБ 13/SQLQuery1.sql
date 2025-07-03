--1
use test;
go
CREATE FUNCTION COUNT_obo(@f int) 
RETURNS int 
AS 
BEGIN 
    -- Объявляем переменную для хранения результата
    DECLARE @rc int;

    -- Получаем количество оборудования по ID подразделения
    SELECT @rc = COUNT(ОборудованиеID)
    FROM ОБОРУДОВАНИЕ 
    WHERE ПодразделениеID = @f;

    -- Возвращаем результат
    RETURN @rc; 
END;

DECLARE @f int = 1; -- Пример ID подразделения
PRINT 'Количество оборудования=' + CAST(dbo.COUNT_obo(@f) AS varchar(4));

SELECT ПодразделениеID, dbo.COUNT_obo(ПодразделениеID) AS КоличествоОборудования 
FROM ПОДРАЗДЕЛЕНИЯ;
 --2
 CREATE FUNCTION Fobo(@tz int) 
RETURNS char(300) 
AS 
BEGIN  
    DECLARE @tv nvarchar(100);  
    DECLARE @t varchar(300) = 'Оборудования: ';  
    DECLARE ZkOBO CURSOR LOCAL FOR 
        SELECT Название FROM ОБОРУДОВАНИЕ 
        WHERE ПодразделениеID = @tz;

    OPEN ZkOBO;   
    FETCH NEXT FROM ZkOBO INTO @tv;     
    WHILE @@FETCH_STATUS = 0                                     
    BEGIN 
        SET @t = @t + ', ' + RTRIM(@tv);         
        FETCH NEXT FROM ZkOBO INTO @tv; 
    END;    
    CLOSE ZkOBO;
    DEALLOCATE ZkOBO;
    
    RETURN @t;
END;  

-- Пример использования
SELECT Название, dbo.Fobo(ПодразделениеID) AS СписокОборудования 
FROM ПОДРАЗДЕЛЕНИЯ;
--3

CREATE FUNCTION FTovCena (@f nvarchar(100), @p int) 
RETURNS TABLE 
AS 
RETURN (
    SELECT o.Название, o.Количество, o.Тип, o.Дата_прибытия
    FROM ОБОРУДОВАНИЕ o left outer join ПОДРАЗДЕЛЕНИЯ p
		ON o.ПодразделениеID = p.ПодразделениеID
    WHERE (o.Количество= ISNULL(@p, o.Количество)) 
      AND (p.Название = ISNULL(@F, p.Название))
);
-- Примеры использования
SELECT * FROM dbo.FTovCena(NULL, 10); -- Например, получаем оборудование с количеством 10
Drop FUNCTION dbo.FTovCena;
--4

CREATE FUNCTION FKolTov(@oID int) 
RETURNS int 
AS 
BEGIN
    DECLARE @rc int = (SELECT COUNT(*) FROM Заказы WHERE ОборудованиеID = ISNULL(@oID, ОборудованиеID));
    RETURN @rc;
END;

-- Пример использования
SELECT o.Название, dbo.FKolTov(o.ОборудованиеID) AS [Количество заказов] 
FROM ОБОРУДОВАНИЕ o;

SELECT dbo.FKolTov(NULL) AS [Всего заказов];
