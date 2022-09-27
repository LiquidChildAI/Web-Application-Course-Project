 SELECT TOP 1 Signning.in_out,MAX(Signning.date) AS "max_date" , MAX(Signning.time) AS "max_time" 
 FROM Signning
 WHERE date='10/08/2013'
 GROUP BY Signning.in_out
 ORDER BY "max_time" DESC
