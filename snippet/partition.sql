

alter database bimst3 add filegroup Y2012
alter database bimst3 add filegroup Y2013
alter database bimst3 add filegroup Y2014
/*
alter database bimst3 remove filegroup Y2012
alter database bimst3 remove filegroup Y2013
alter database bimst3 remove filegroup Y2014
*/

alter database bimst3 add file (Name=N'Y2012',filename='E:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Y2012.ndf',size=5mb,filegrowth=5mb)  to filegroup Y2012
alter database bimst3 add file (Name=N'Y2013',filename='E:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Y2013.ndf',size=5mb,filegrowth=5mb)  to filegroup Y2013
alter database bimst3 add file (Name=N'Y2014',filename='E:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Y2014.ndf',size=5mb,filegrowth=5mb)  to filegroup Y2014
/*
alter database bimst3 remove file Y2012
alter database bimst3 remove file Y2013
alter database bimst3 remove file Y2014
*/

create partition function PF_CreateTime (datetime) as range left for values ('2012-12-31','2013-12-31')
--drop partition function PF_CreateTime

create partition scheme PS_CreateTime as partition PF_CreateTime to (Y2012,Y2013,Y2014)
--drop PARTITION SCHEME PS_CreateTime

--�󴴽���
--drop table Sys_Log_History
CREATE TABLE [Sys_Log_History](
	[Id] [int] NOT NULL,
	[Text] text NULL,
	[CreateTime] [datetime] NULL
)
on PS_CreateTime(CreateTime)

--CREATE CLUSTERED INDEX [IX_Sys_Log_History_Id] ON Sys_Log_History(Id) ON  [PRIMARY]
--CREATE NONCLUSTERED INDEX [IX_Sys_Log_History_CreateTime] ON Sys_Log_History(CreateTime) on PS_CreateTime(CreateTime)

--�Ƚ�������
--������Ѿ����ڵľۼ�����(clustered)����ô��Ҫɾ��Ȼ�����½���
-- 1. ��ѯ ���Լ�� 
exec sp_helpconstraint Sys_Log_History
-- 2. ɾ������Լ��
alter table Sys_Log_History drop constraint PK__Sys_Log_History__C9F2845741CC166E 
-- 3. �������Լ����������Ϊ�Ǿۼ����� (non-clustered)
alter table Sys_Log_History add constraint PK__Sys_Log_History__C9F2845741CC166E primary key nonclustered (Id)
-- 4. ���һ���ۼ���������ʹ�÷�������ָ����������
CREATE CLUSTERED INDEX [CIX_PS_CreateTime] ON Sys_Log_History(CreateTime) on PS_CreateTime(CreateTime)
--DROP INDEX [CIX_PS_CreateTime] ON Sys_Log_History

select $partition.PF_CreateTime(CreateTime) as number, COUNT(1) as count
 from [Sys_Log_History] group by $partition.PF_CreateTime(CreateTime)
select * from Sys_Log_History where $partition.PF_CreateTime(CreateTime)=3



--������
CREATE TABLE [Sys_Log](
	[Id] [int] NOT NULL,
	[Text] text NULL,
	[CreateTime] [datetime] default(getdate()) NULL
)
--test data
declare @i int
set @i = 1
while @i < 1000
begin 
	insert into [Sys_Log] values(@i,'abc'+cast(@i as varchar(10)),dateadd(dd,datediff(dd,'2012-01-01','2012-12-30')*RAND(),'2012-01-01'))
	set @i = @i +1
end
select * from Sys_Log order by CreateTime
select * from Sys_Log where convert(varchar(10),CreateTime,120)=convert(varchar(10),dateadd(year,-6,getdate()),120)
select * from Sys_Log where exists (select 1 from Sys_Log_History h where h.Id=Sys_Log.Id)

--��ʷ����
select * from Sys_Log_History
--1.���Լ��
select
a.name as Լ����,
object_name(b.parent_object_id) as �����,
d.name as �����,
object_name(b.referenced_object_id) as ������,
c.name as ������
from sys.foreign_keys A
inner join sys.foreign_key_columns B on A.object_id=b.constraint_object_id
inner join sys.columns C on B.parent_object_id=C.object_id and B.parent_column_id=C.column_id 
inner join sys.columns D on B.referenced_object_id=d.object_id and B.referenced_column_id=D.column_id 
where object_name(B.referenced_object_id)='Sys_Log';
--2.ɾ��Լ��
ALTER TABLE Sys_Log  DROP CONSTRAINT FK_Sys_Log_xxxx

select * into Sys_Log_2012 --select * 
	from Sys_Log  where CreateTime > N'2012' and CreateTime < N'2013'
select * from Sys_Log_2012
--drop table Sys_Log_2012
--2.1�ƶ�����
--delete from Sys_Log_History
insert into Sys_Log_History select * from Sys_Log  where CreateTime > N'2012' and CreateTime < N'2013'
begin
insert into Sys_Log_History select * from Sys_Log  where convert(varchar(10),CreateTime,120)=convert(varchar(10),dateadd(year,-6,getdate()),120)
delete from Sys_Log where exists (select 1 from Sys_Log_History h where h.Id=Sys_Log.Id)
end
--3.ɾ������
DROP INDEX [Sys_Log_Index5] ON Sys_Log
--3.1ɾ������
delete Sys_Log where CreateTime > N'2012' and CreateTime < N'2013'
--4.�ָ�����
CREATE NONCLUSTERED INDEX [Sys_Log_Index5] ON Sys_Log([Id])





--��������
--���������λ��ͬһ�ļ���
--drop table [Sys_Log_2014]
CREATE TABLE [Sys_Log_2014](
	[Id] [int] NOT NULL,
	[Text] text NULL,
	[CreateTime] [datetime] NULL
) on Y2014
--�� 3 �����������ݸ��Ƶ���ͨ�� Sys_Log_2014 ��
alter table Sys_Log switch partition 3 to Sys_Log_2014
select * from Sys_Log_2014
--����ͨ�� Sys_Log_2014 �е����ݸ��Ƶ������� Sys_Log �еĵ� 4 ������
alter table Sys_Log_2014 switch to Sys_Log partition 4

