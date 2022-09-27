select t1.username 
from (select username,MAX(time) as "s_time",in_out
from Signning
where in_out = 'i' and date = CONVERT(date,getdate(),108) 
group by username,in_out
having MAX(time) between CONVERT(time,'08:00',108) and CONVERT(time,'08:20',108) ) as t1,
(select username,MAX(time) as "s_time",in_out
from Signning
where in_out = 'o' and date = CONVERT(date,getdate(),108)
group by username,in_out) as t2
where t1.s_time>t2.s_time and t1.username = t2.username