SELECT Signning.username, MAX(Signning.time) as s_time
FROM Signning
WHERE in_out='i' AND date = CONVERT(DATE, GETDATE(), 108) AND Signning.time BETWEEN CONVERT(TIME, '18:00', 108) AND CONVERT(TIME, '18:30', 108) 
GROUP BY username,in_out
ORDER BY "s_time" DESC


