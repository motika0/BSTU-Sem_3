--1
declare @tr char(20), @t char(300) = '';
declare ������������������ cursor
 for select �������� from ������������;
 open  ������������������;
fetch  ������������������ into @tr;
print '������������';
while @@FETCH_STATUS = 0
begin
set @t = rtrim(@tr) + ',' + @t;
fetch  ������������������ into @tr;
end;
print @t;
close  ������������������;
--2
DECLARE Tovary1 CURSOR LOCAL                            
	             for SELECT ��������, ���������� from ������������;
DECLARE @tv char(20), @cena real;      
	OPEN Tovary1;	  
	fetch  Tovary1 into @tv, @cena; 	
      print '1. '+@tv+cast(@cena as varchar(6));   
      go
	
 DECLARE @tv char(20), @cena real;     	
	fetch  Tovary1 into @tv, @cena; 	
      print '2. '+@tv+cast(@cena as varchar(6));  
  go   


  DECLARE Tovary2 CURSOR global                           
	             for SELECT ��������, ���������� from ������������;
DECLARE @tg char(20), @sena real;      
	OPEN Tovary2;	  
	fetch  Tovary2 into @tg, @sena; 	
      print '1. '+@tg+cast(@sena as varchar(6));   
      go
	
 DECLARE @tg char(20), @sena real;     	
	fetch  Tovary2 into @tg, @sena; 	
      print '2. '+@tg+cast(@sena as varchar(6));
	  close Tovary2;
	  deallocate Tovary2;
  go   

  --3
        DECLARE @tid char(10), @tnm char(40), @tgn char(1);  
	DECLARE ������ CURSOR LOCAL STATIC                              
		 for SELECT ��������, ���, ���������� 
		       FROM dbo.������������ where �������������ID = '1';				   
	open ������;
	print   '���������� ����� : '+cast(@@CURSOR_ROWS as varchar(5)); 
    	UPDATE ������������ set ���������� = 5 where �������� = '����';
	DELETE ������������ where �������� = '����';
	INSERT ������������(������������ID, ��������, ���,    
                                ����_��������, ����������, �������������ID) 
	                 values (3, '����', '����������', '2014-08-02', 30 , 3); 
	FETCH  ������ into @tid, @tnm, @tgn;     
	while @@fetch_status = 0                                    
      begin 
          print @tid + ' '+ @tnm + ' '+ @tgn;      
          fetch ������ into @tid, @tnm, @tgn; 
       end;          
   CLOSE  ������;
   --4
            DECLARE  @tc int, @rn char(50);  
         DECLARE Primer1 cursor local dynamic SCROLL                               
               for SELECT row_number() over (order by ��������) N,
	                           �������� FROM dbo.������������ 
                                                         where �������������ID = '3' 
	OPEN Primer1;
	FETCH  Primer1 into  @tc, @rn;                 
	print '��������� ������        : ' + cast(@tc as varchar(3))+ rtrim(@rn);      
	FETCH  LAST from  Primer1 into @tc, @rn;       
	print '��������� ������          : ' +  cast(@tc as varchar(3))+ rtrim(@rn);      
      CLOSE Primer1;
	  --next, prior.....

--5
declare @tn char(20), @tk int;
declare Primer2 cursor local dynamic
     for select ��������, ���������� from ������������ for update;
	 open Primer2;
	 fetch Primer2 into @tn,@tk;
	 delete ������������ where current of Primer2;
	 fetch Primer2 into @tn,@tk;
	 UPDATE ������������ set ���������� = ���������� +1 where current of Primer2;
	 close Primer2;
--6
DELETE FROM PROGRESS
WHERE IDSTUDENT IN (
    SELECT P.IDSTUDENT
    FROM PROGRESS P
    JOIN STUDENT S ON P.IDSTUDENT = S.IDSTUDENT
    JOIN GROUPS G ON S.IDGROUP = G.IDGROUP
    WHERE P.GRADE < 4
);
UPDATE PROGRESS
SET GRADE = GRADE + 1
WHERE IDSTUDENT = :--id; 