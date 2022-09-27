SELECT t1.date,(t2.s_time - t1.s_time) as total
 FROM
 ( SELECT date, SUM(DATEDIFF(MILLISECOND, '0:00:00', Signning.time)) as s_time,in_out
  FROM Signning
WHERE  date = convert(smalldatetime,'13/08/2013',103) and in_out ='i' and username='idan' 
GROUP BY date,in_out) as t1,
(SELECT date,SUM(DATEDIFF(MILLISECOND, '0:00:00', Signning.time)) as s_time,in_out
  FROM Signning
WHERE  date = convert(smalldatetime,'13/08/2013',103) and in_out ='o' and username='idan' 
GROUP BY date,in_out) as t2