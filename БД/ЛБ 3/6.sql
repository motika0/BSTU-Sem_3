use master
go
create database UNIVER
on primary
(name =N'UNIVER_mdf', filename =N'D:\ÁÃÒÓ\3 סולוסענ\ÁÄ\ËÁ 3\UNIVER_mdf.mdf',
size=10240Kb, maxsize=UNLIMITED, filegrowth=1024Kb) 
log on
(name =N'UNIVER_log', filename=N'D:\ÁÃÒÓ\3 סולוסענ\ÁÄ\ËÁ 3\UNIVER_log.ldf', size=10240Kb, maxsize=2048Gb, filegrowth=10%)
go
