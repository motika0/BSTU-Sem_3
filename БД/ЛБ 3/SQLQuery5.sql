use test
CREATE TABLE ������������ (
    ������������ID int primary key foreign key references ��������(��������ID),
    �������� nvarchar(100) not null,
    ��� nvarchar(50)not null,
    ����_�������� date not null,
    ���������� int not null,
    �������������ID int not nulL foreign key  references �������������(�������������ID)

);