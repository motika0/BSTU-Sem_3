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
(1,'��������� �������������� ����������'),
(2,'���������-������������� ���������'),
(3,'���� ���������'),
(4,'��� ���������');

insert into KAFED(KODK,Name_KAF,FACLTY) values
(1,'������� �������������� ����������',1),
(2,'������� ���-�������',1),
(3,'������� ���������',2),
(4,'������� ������������ �������', 3);

insert into TEACHER(KOD,T_NAME,GENDER,PULPIT) values
(1,'������ �.�','M',1),
(2,'������� �.�','F',2),
(3,'���������� �.�','F',3),
(4,'���������� �.�','M',4);

select
    FACULTET.Nam_F ���������,
	KAFED.Name_KAF �������
	from FACULTET join KAFED on FACULTET.FACULTY=KAFED.FACLTY
	where KAFED.Name_KAF like '%������%' and FACULTET.Nam_F like '%������%';

--2
select
KAFED.Name_KAF �������,
isnull(TEACHER.T_NAME, '��� �������') �������������
from
KAFED left outer join TEACHER on KAFED.KODK=TEACHER.PULPIT;