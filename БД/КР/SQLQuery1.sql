use master;
create database Ukrainskiy_5;

--1

use Ukrainskiy_5;
create table FACULTET (
FACULTY INT PRIMARY KEY not null,
Nam_F varchar(50) not null
);
create table KAFED (
KODK INT PRIMARY KEY not null,
Name_KAF varchar(50) not null,
FACLTY int foreign key (FACLTY) references FACULTET(FACULTY)
);
create table TEACHER (
KOD int primary key not null,
T_NAME varchar(50) not null,
GENDER char(1) check (GENDER in ('M','F')),
PULPIT int foreign key (PULPIT) references KAFED(KODK)
);
insert into FACULTET(FACULTY,Nam_F) values
(1,'Факультет информационных технологий'),
(2,'Инженерно-экономический факультет'),
(3,'ХТИТ факультет'),
(4,'ТОВ факультет');

insert into KAFED(KODK,Name_KAF,FACLTY) values
(1,'Кафедра информационных технологий',1),
(2,'Кафедра веб-дизайна',1),
(3,'Кафедра инженерии',2),
(4,'Кафедра органических веществ', 3);

insert into TEACHER(KOD,T_NAME,GENDER,PULPIT) values
(1,'Петров П.П','M',1),
(2,'Сидарок М.С','F',2),
(3,'Робальская Я.С','F',3),
(4,'Украинский М.Л','M',4);

select
    FACULTET.Nam_F Факультет,
	KAFED.Name_KAF Кафедра
	from FACULTET join KAFED on FACULTET.FACULTY=KAFED.FACLTY
	where KAFED.Name_KAF like '%информ%' and FACULTET.Nam_F like '%информ%';

--2
select
KAFED.Name_KAF кафедра,
isnull(TEACHER.T_NAME, 'нет препода') преподователь
from
KAFED left outer join TEACHER on KAFED.KODK=TEACHER.PULPIT;