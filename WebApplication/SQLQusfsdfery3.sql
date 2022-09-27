select t1.username
from Signning as t1
where in_out = 'i' AND t1.username NOT IN (
	select t2.username
	FROM Signning AS t2
	where in_out = 'o'
)





