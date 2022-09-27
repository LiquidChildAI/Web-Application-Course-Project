select username,MAX(time) as "s_time",in_out
from Signning
where in_out = 'o' and date = CONVERT(date,getdate(),108)
group by username,in_out