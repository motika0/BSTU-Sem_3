-- �������� A: ������ � �������� �������� READ UNCOMMITTED
SET TRANSACTION ISOLATION LEVEL READ COMMITTED; 

BEGIN TRANSACTION; 

-- t1: 
SELECT @@SPID AS 'SessionID', '������ �� ������������' AS '���������', *
FROM ������������
WHERE �������� = '����������';
UPDATE ������������
SET �������� = '����������'
WHERE �������� = '����������';�

-- 
SELECT @@SPID AS 'SessionID', '������ �� ������' AS '���������', ���������, �������ID
FROM ��������������
WHERE ��������� = '������';

COMMIT; -- ���������� ����������
