

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

--后创建表
--drop table Sys_Log_History
CREATE TABLE [Sys_Log_History](
	[Id] [int] NOT NULL,
	[Text] text NULL,
	[CreateTime] [datetime] NULL
)
on PS_CreateTime(CreateTime)

--CREATE CLUSTERED INDEX [IX_Sys_Log_History_Id] ON Sys_Log_History(Id) ON  [PRIMARY]
--CREATE NONCLUSTERED INDEX [IX_Sys_Log_History_CreateTime] ON Sys_Log_History(CreateTime) on PS_CreateTime(CreateTime)

--先建表的情况
--如果是已经存在的聚集索引(clustered)，那么需要删除然后重新建立
-- 1. 查询 表的约束 
exec sp_helpconstraint Sys_Log_History
-- 2. 删除主键约束
alter table Sys_Log_History drop constraint PK__Sys_Log_History__C9F2845741CC166E 
-- 3. 添加主键约束，但设置为非聚集索引 (non-clustered)
alter table Sys_Log_History add constraint PK__Sys_Log_History__C9F2845741CC166E primary key nonclustered (Id)
-- 4. 添加一个聚集索引，并使用分区方案指定分区的列
CREATE CLUSTERED INDEX [CIX_PS_CreateTime] ON Sys_Log_History(CreateTime) on PS_CreateTime(CreateTime)
--DROP INDEX [CIX_PS_CreateTime] ON Sys_Log_History

select $partition.PF_CreateTime(CreateTime) as number, COUNT(1) as count
 from [Sys_Log_History] group by $partition.PF_CreateTime(CreateTime)
select * from Sys_Log_History where $partition.PF_CreateTime(CreateTime)=3



--热数据
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

--历史数据
select * from Sys_Log_History
--1.外键约束
select
a.name as 约束名,
object_name(b.parent_object_id) as 外键表,
d.name as 外键列,
object_name(b.referenced_object_id) as 主健表,
c.name as 主键列
from sys.foreign_keys A
inner join sys.foreign_key_columns B on A.object_id=b.constraint_object_id
inner join sys.columns C on B.parent_object_id=C.object_id and B.parent_column_id=C.column_id 
inner join sys.columns D on B.referenced_object_id=d.object_id and B.referenced_column_id=D.column_id 
where object_name(B.referenced_object_id)='Sys_Log';
--2.删除约束
ALTER TABLE Sys_Log  DROP CONSTRAINT FK_Sys_Log_xxxx

select * into Sys_Log_2012 --select * 
	from Sys_Log  where CreateTime > N'2012' and CreateTime < N'2013'
select * from Sys_Log_2012
--drop table Sys_Log_2012
--2.1移动数据
--delete from Sys_Log_History
insert into Sys_Log_History select * from Sys_Log  where CreateTime > N'2012' and CreateTime < N'2013'
begin
insert into Sys_Log_History select * from Sys_Log  where convert(varchar(10),CreateTime,120)=convert(varchar(10),dateadd(year,-6,getdate()),120)
delete from Sys_Log where exists (select 1 from Sys_Log_History h where h.Id=Sys_Log.Id)
end
--3.删除索引
DROP INDEX [Sys_Log_Index5] ON Sys_Log
--3.1删除数据
delete Sys_Log where CreateTime > N'2012' and CreateTime < N'2013'
--4.恢复索引
CREATE NONCLUSTERED INDEX [Sys_Log_Index5] ON Sys_Log([Id])





--复制数据
--两个表必须位于同一文件组
--drop table [Sys_Log_2014]
CREATE TABLE [Sys_Log_2014](
	[Id] [int] NOT NULL,
	[Text] text NULL,
	[CreateTime] [datetime] NULL
) on Y2014
--第 3 个分区的数据复制到普通表 Sys_Log_2014 中
alter table Sys_Log switch partition 3 to Sys_Log_2014
select * from Sys_Log_2014
--将普通表 Sys_Log_2014 中的数据复制到分区表 Sys_Log 中的第 4 个分区
alter table Sys_Log_2014 switch to Sys_Log partition 4

