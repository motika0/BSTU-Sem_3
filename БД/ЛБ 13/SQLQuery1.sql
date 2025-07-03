--1
use test;
go
CREATE FUNCTION COUNT_obo(@f int) 
RETURNS int 
AS 
BEGIN 
    -- ��������� ���������� ��� �������� ����������
    DECLARE @rc int;

    -- �������� ���������� ������������ �� ID �������������
    SELECT @rc = COUNT(������������ID)
    FROM ������������ 
    WHERE �������������ID = @f;

    -- ���������� ���������
    RETURN @rc; 
END;

DECLARE @f int = 1; -- ������ ID �������������
PRINT '���������� ������������=' + CAST(dbo.COUNT_obo(@f) AS varchar(4));

SELECT �������������ID, dbo.COUNT_obo(�������������ID) AS ���������������������� 
FROM �������������;
 --2
 CREATE FUNCTION Fobo(@tz int) 
RETURNS char(300) 
AS 
BEGIN  
    DECLARE @tv nvarchar(100);  
    DECLARE @t varchar(300) = '������������: ';  
    DECLARE ZkOBO CURSOR LOCAL FOR 
        SELECT �������� FROM ������������ 
        WHERE �������������ID = @tz;

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

-- ������ �������������
SELECT ��������, dbo.Fobo(�������������ID) AS ������������������ 
FROM �������������;
--3

CREATE FUNCTION FTovCena (@f nvarchar(100), @p int) 
RETURNS TABLE 
AS 
RETURN (
    SELECT o.��������, o.����������, o.���, o.����_��������
    FROM ������������ o left outer join ������������� p
		ON o.�������������ID = p.�������������ID
    WHERE (o.����������= ISNULL(@p, o.����������)) 
      AND (p.�������� = ISNULL(@F, p.��������))
);
-- ������� �������������
SELECT * FROM dbo.FTovCena(NULL, 10); -- ��������, �������� ������������ � ����������� 10
Drop FUNCTION dbo.FTovCena;
--4

CREATE FUNCTION FKolTov(@oID int) 
RETURNS int 
AS 
BEGIN
    DECLARE @rc int = (SELECT COUNT(*) FROM ������ WHERE ������������ID = ISNULL(@oID, ������������ID));
    RETURN @rc;
END;

-- ������ �������������
SELECT o.��������, dbo.FKolTov(o.������������ID) AS [���������� �������] 
FROM ������������ o;

SELECT dbo.FKolTov(NULL) AS [����� �������];
