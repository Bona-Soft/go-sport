CREATE PROCEDURE [dbo].[Debug_DropAll]
AS
BEGIN

   EXEC Debug_DropAllFK
   EXEC sp_MSforeachtable @command1 = "DROP TABLE ?"

   DECLARE @procName VARCHAR(500)
   DECLARE cur CURSOR 

   FOR SELECT [name] FROM sys.objects WHERE TYPE = 'p'
   OPEN cur
   FETCH NEXT FROM cur INTO @procName
   WHILE @@FETCH_STATUS = 0
   BEGIN
	  EXEC('drop procedure [' + @procName + ']')
	  FETCH NEXT FROM cur INTO @procName
   END
   CLOSE cur
   DEALLOCATE cur

END