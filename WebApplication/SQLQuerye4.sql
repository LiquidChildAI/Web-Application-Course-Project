select t1.username,t2.username,t1.time
from
(select username,time
from Signning as s
where  in_out = 'i' s.time between convert(time,'10:00',108) and convert(time,'18:00',108)
group by username,time ) as t1 ,

(select username,time
from Signning as s
where  in_out = 'o'
group by username,s.time ) as t2 

where t1.time between convert(time,'10:00',108) and convert(time,'18:00',108)
group by t1.username,t2.username,t1.time