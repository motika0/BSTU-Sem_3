ALTER DATABASE test
ADD FILE 
(
    NAME = N'testtFile',
    FILENAME = N'D:\����\3 �������\��\�� 3\testFile.ndf',
    SIZE = 10240KB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1024KB
) TO FILEGROUP FG1;
GO