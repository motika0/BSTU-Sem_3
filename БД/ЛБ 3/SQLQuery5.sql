use test
CREATE TABLE ОБОРУДОВАНИЕ (
    ОборудованиеID int primary key foreign key references СПИСАНИЕ(СписаниеID),
    Название nvarchar(100) not null,
    Тип nvarchar(50)not null,
    Дата_прибытия date not null,
    Количество int not null,
    ПодразделениеID int not nulL foreign key  references ПОДРАЗДЕЛЕНИЯ(ПодразделениеID)

);