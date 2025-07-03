--1
USE UNIVER;
SELECT 
    t.name AS TableName,
    i.name AS IndexName,
    i.type_desc AS IndexType
FROM 
    sys.indexes i
JOIN 
    sys.tables t ON i.object_id = t.object_id
WHERE 
    t.is_ms_shipped = 0;


	CREATE TABLE #TempTable (
    ID INT IDENTITY(1,1),
    Name NVARCHAR(100),
    Age INT,
    Email NVARCHAR(100)
);

DECLARE @i INT = 1;

WHILE @i <= 1000
BEGIN
    INSERT INTO #TempTable (Name, Age, Email) 
    VALUES (CONCAT('User', @i), 20 + (@i % 30), CONCAT('user', @i, '@example.com'));
    SET @i = @i + 1;
END;

SELECT 
    Name, 
    Age, 
    Email 
FROM 
    #TempTable 
WHERE 
    Age > 25;

CREATE CLUSTERED INDEX IX_TempTable_Age ON #TempTable (Age);
Drop INDEX IX_TempTable_Age  ON #TempTable;

--2

CREATE TABLE #TimeTable (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Value INT NOT NULL
);

INSERT INTO #TimeTable (Name, Value)
SELECT TOP 10000 
    'Name' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS NVARCHAR(10)),
    ABS(CHECKSUM(NEWID())) % 1000
FROM master.dbo.spt_values;


SELECT Name, Value
FROM #TimeTable
WHERE Value > 500;

CREATE NONCLUSTERED INDEX IX_Name_Value
ON #TimeTable (Name, Value);

DROP TABLE #TempTable;
 
 --3

CREATE TABLE #EX (
    TKEY INT IDENTITY(1,1),
    CC NVARCHAR(100)
);

DECLARE @i INT = 1;

WHILE @i <= 10000
BEGIN
    INSERT INTO #EX (CC) 
    VALUES (CONCAT('Value', @i));
    SET @i = @i + 1;
END;



SELECT CC 
FROM #EX 
WHERE TKEY > 15000;


CREATE NONCLUSTERED INDEX #EX_TKEY_X ON #EX(TKEY) INCLUDE (CC);


SELECT CC 
FROM #EX 
WHERE TKEY > 15000;
GO

--4

CREATE TABLE #TimeTable1 (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Value INT NOT NULL
);

INSERT INTO #TimeTable1 (Name, Value)
SELECT TOP 10000 
    'Name' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS NVARCHAR(10)),
    ABS(CHECKSUM(NEWID())) % 1000
FROM master.dbo.spt_values;


SELECT Name, Value
FROM #TimeTable1
WHERE Value > 500;

CREATE NONCLUSTERED INDEX IX_Value_Filtered
ON #TimeTable1 (Value)
WHERE Value > 500;


DROP TABLE #TimeTable1;

--5
CREATE TABLE #TempTable (
    ID INT primary key IDENTITY(1,1),
    Value NVARCHAR(100)
);

DECLARE @i INT = 1;
WHILE @i <= 10000
BEGIN
    INSERT INTO #TempTable (Value) VALUES (CONCAT('Value', @i));
    SET @i = @i + 1;
END;

CREATE NONCLUSTERED INDEX IX_Value ON #TempTable(Value);

SELECT 
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    i.index_id,
    ps.avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(DB_ID(), OBJECT_ID('tempdb..#TempTable'), NULL, NULL, 'LIMITED') AS ps
JOIN sys.indexes AS i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
WHERE i.index_id = 1; 

DELETE FROM #TempTable WHERE ID <= 5000;

declare @i int = 1;
WHILE @i <= 10000
BEGIN
    INSERT INTO #TempTable (Value) VALUES (CONCAT('NewValue', @i));
    SET @i = @i + 1;
END;

SELECT 
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    i.index_id,
    ps.avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(DB_ID(), OBJECT_ID('tempdb..#TempTable'), NULL, NULL, 'LIMITED') AS ps
JOIN sys.indexes AS i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
WHERE i.index_id = 1; 

ALTER INDEX ID ON #TempTable REORGANIZE;

SELECT 
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    i.index_id,
    ps.avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(DB_ID(), OBJECT_ID('tempdb..#TempTable'), NULL, NULL, 'LIMITED') AS ps
JOIN sys.indexes AS i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
WHERE i.index_id = 1;

ALTER INDEX IX_Value ON #TempTable REBUILD;

SELECT 
    OBJECT_NAME(i.object_id) AS TableName,
    i.name AS IndexName,
    i.index_id,
    ps.avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(DB_ID(), OBJECT_ID('tempdb..#TempTable'), NULL, NULL, 'LIMITED') AS ps
JOIN sys.indexes AS i ON ps.object_id = i.object_id AND ps.index_id = i.index_id
WHERE i.index_id = 1; 

DROP TABLE #TempTable;

--6
CREATE TABLE Employees (
    EmployeeID INT PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Department NVARCHAR(50),
    Salary DECIMAL(10, 2)
);
INSERT INTO Employees (EmployeeID, FirstName, LastName, Department, Salary)
VALUES
(1, 'John', 'Doe', 'HR', 50000),
(2, 'Jane', 'Smith', 'IT', 60000),
(3, 'Emily', 'Jones', 'Finance', 70000),
(4, 'Michael', 'Brown', 'IT', 65000),
(5, 'Sarah', 'Davis', 'HR', 55000);
CREATE NONCLUSTERED INDEX IX_LastName
ON Employees (LastName)
WITH (FILLFACTOR = 70);
EXEC sp_helpindex 'Employees';

