ALTER DATABASE test
ADD FILE 
(
    NAME = N'testtFile',
    FILENAME = N'D:\ÁÃÒÓ\3 סולוסענ\ÁÄ\ËÁ 3\testFile.ndf',
    SIZE = 10240KB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 1024KB
) TO FILEGROUP FG1;
GO