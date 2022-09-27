select t1.date,(t2.s_time - t1.s_time) as total 
from(
select date,sum(DATEDIFF(MILLISECOND, '0:00:00',time)) as s_time,in_out from Signning
where  date ='2013-11-08' and in_out ='i' and username='idan'
group by date,in_out) as t1 ,(
select date,sum(DATEDIFF(MILLISECOND, '0:00:00',time)) as s_time,in_out from Signning
where  date ='2013-11-08' and in_out ='o'  and username='idan'
group by date,in_out) as t2