CREATE PROCEDURE [dbo].[Debug_DeleteAll]
AS
BEGIN

	EXEC sp_MSForEachTable 'DISABLE TRIGGER ALL ON ?'
	
	EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
	
	EXEC sp_MSForEachTable 'DELETE FROM ?'
	
	EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
	
	EXEC sp_MSForEachTable 'ENABLE TRIGGER ALL ON ?'
	
	EXEC POP_Sports

	EXEC POP_Translations

	EXEC POP_Fields

	EXEC POP_ChallengeTypes

	EXEC POP_Headquarters

	EXEC POP_MatchStates

END