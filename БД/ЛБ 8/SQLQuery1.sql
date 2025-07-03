--1
DECLARE 
    @c CHAR(10) = 'гуд',
    @v VARCHAR(50) = 'файн', 
    @d DATETIME, 
    @t TIME,
    @i INT,  
    @s SMALLINT,
    @ti TINYINT, 
    @n NUMERIC(12, 5); 

SET @d = GETDATE();  
SET @t = CAST(GETDATE() AS TIME); 
SET @i = 12345; 
SET @s = 31999;  
SET @ti = 245;  
SET @n = 99345.67890; 

PRINT 'int = ' + CAST(@i AS VARCHAR(10));
PRINT 'smallint = ' + CAST(@s AS VARCHAR(10));
PRINT 'tinyint = ' + CAST(@ti AS VARCHAR(10));
PRINT 'numeric = ' + CAST(@n AS VARCHAR(15));

SELECT 
    @c Переменная,
    @v Вторая,
    @d время,
    @t часы;
--2
CREATE TABLE Auditories (
    id INT PRIMARY KEY,
    capacity INT
);

INSERT INTO Auditories (id, capacity) VALUES
(1, 50),
(2, 30),
(3, 40),
(4, 70),
(5, 20);

DECLARE @all INT;
DECLARE @acount INT;

SELECT 
    @all = SUM(capacity),
    @acount = COUNT(*)
FROM Auditories;

IF @all > 200
BEGIN
    DECLARE @averall FLOAT = @all * 1.0 / @acount; 
    DECLARE @smalaverall INT;

    SELECT @smalaverall = COUNT(*)
    FROM Auditories
    WHERE capacity < @averall;

    SELECT 
        @acount AS ВсегоАудиторий,
        @averall AS Среднееколичаудиторий,
        @smalaverall AS Меньшесреднегоколичаудиторий,
        (@smalaverall * 100.0 / @acount) AS процент;
END
ELSE
BEGIN
    PRINT 'Общая вместимость аудиторий: ' + CAST(@all AS VARCHAR(10));
END
--3
CREATE TABLE #TimeTable (ID INT);

INSERT INTO #TimeTable (ID) VALUES (1), (2), (3);

SELECT * FROM #TimeTable;

PRINT 'Количество обработанных строк (@@ROWCOUNT): ' + CAST(@@ROWCOUNT AS VARCHAR(10));
PRINT 'Версия SQL Server (@@VERSION): ' + @@VERSION;
PRINT 'Системный идентификатор процесса (@@SPID): ' + CAST(@@SPID AS VARCHAR(10));
PRINT 'Код последней ошибки (@@ERROR): ' + CAST(@@ERROR AS VARCHAR(10));
PRINT 'Имя сервера (@@SERVERNAME): ' + @@SERVERNAME;
PRINT 'Уровень вложенности транзакции (@@TRANCOUNT): ' + CAST(@@TRANCOUNT AS VARCHAR(10));
PRINT 'Статус выборки (@@FETCH_STATUS): ' + CAST(@@FETCH_STATUS AS VARCHAR(10));
PRINT 'Уровень вложенности текущей процедуры (@@NESTLEVEL): ' + CAST(@@NESTLEVEL AS VARCHAR(10));

DROP TABLE #TimeTable;


--4
DECLARE @x int=2, @tt int=1, @z float;
IF(@tt>@x) SET @z=SIN(@tt)*SIN(@tt)
IF(@tt<@x) SET @z=4*(@tt+@x)
IF(@tt=@x) SET @z=1-POWER(EXP,@x-2)

DECLARE @next INT = MONTH(GETDATE()) + 1;
DECLARE @now INT = YEAR(GETDATE());

IF @next > 12
BEGIN
    SET @next = 1;
    SET @now = @now + 1; 
END

SELECT 
    StudentID,
    Name,
    DATEDIFF(YEAR, BirthDate, GETDATE()) - 
    CASE 
        WHEN MONTH(BirthDate) = @next AND DAY(BirthDate) > DAY(GETDATE()) THEN 1 
        ELSE 0 
    END AS Age,
    BirthDate
FROM 
    Students
WHERE 
    MONTH(BirthDate) = @next AND YEAR(BirthDate) = @now;

DECLARE @GroupID INT = 1;

SELECT 
    ExamID,
    ExamDate,
    DATENAME(WEEKDAY, ExamDate) AS DayOfWeek
FROM 
    Exams
WHERE 
    GroupID = @GroupID;



--5
DECLARE @FacultyName VARCHAR(50) = 'Факультет информационных технологий'; 

SELECT 
    StudentID,
    Score,
    CASE 
        WHEN Score >= 90 THEN 'Отлично'
        WHEN Score >= 75 THEN 'Хорошо'
        WHEN Score >= 60 THEN 'Удовлетворительно'
        WHEN Score >= 50 THEN 'Неудовлетворительно'
        ELSE 'Не сдал'
    END AS Evaluation
FROM 
    Exams
WHERE 
    Faculty = @FacultyName;


--6
CREATE TABLE #TempTable (
    ID INT,
    Name VARCHAR(50),
    Score INT
);

DECLARE @Counter INT = 1;
WHILE @Counter <= 10
BEGIN
    INSERT INTO #TempTable (ID, Name, Score)
    VALUES (@Counter, 'Student ' + CAST(@Counter AS VARCHAR), 50 + @Counter * 5);

    SET @Counter = @Counter + 1;
END

SELECT * FROM #TempTable;

DROP TABLE #TempTable;


--7
CREATE PROCEDURE dbo.DemoReturn
AS
BEGIN
    DECLARE @x INT = 1;

    PRINT @x + 1; 
    PRINT @x + 2;  

    RETURN; 
END;
GO

EXEC dbo.DemoReturn;

DROP PROCEDURE dbo.DemoReturn;

