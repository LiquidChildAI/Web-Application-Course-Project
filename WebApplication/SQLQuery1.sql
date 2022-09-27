SELECT username,date,time,in_out FROM  Signning
WHERE username = 'idan' AND date ='2013-11-08' OR date='2013-12-08' 
GROUP BY username,date,time ,in_out
ORDER BY date ASC,time ASC 