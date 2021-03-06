CREATE PROCEDURE [dbo].[POP_MatchStates]
AS
BEGIN

	;MERGE INTO [dbo].[MatchStates] AS Target
	USING (VALUES
		(1,'start','Creado',0,0,2),
		(2,'onhold','Pausado',0,0,3),
		(3,'hot','Emergencia',0,1,1),
		(4,'inprogress','Jugando',0,0,4),
		(5,'finished','Terminado',1,0,5),
		(6,'cancelled','Cancelado',1,0,6)		
		
	) AS Source ([MatchStateID],[Value],[Description],[Closed],[Hot],[Order])
	ON (Target.[MatchStateID] = Source.[MatchStateID])
	WHEN MATCHED THEN
		UPDATE 
		SET [Value] = Source.[Value],
			[Description] = Source.[Description],
			[Closed] = Source.[Closed],
			[Hot] = Source.[Hot],
			[Order] = Source.[Order]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([MatchStateID],
			[Value],
			[Description],
			[Closed],
			[Hot],
			[Order])
		VALUES
			(Source.[MatchStateID],
			Source.[Value],
			Source.[Description],
			Source.[Closed],
			Source.[Hot],
			Source.[Order]);
END

