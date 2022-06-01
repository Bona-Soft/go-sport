CREATE PROCEDURE [dbo].[POP_ChallengeTypes]
AS
BEGIN

	;MERGE INTO [dbo].[ChallengeTypes] AS Target
	USING (VALUES
		(1,'friendly','Amistoso'),
		(2,'tournament','Torneo'),
		(3,'cup','Copa'),
		(4,'profesional','Profesional'),
		(5,'challenge','Desafío')
	) AS Source ([ChallengeTypeID],[Value],[Description])
	ON (Target.[ChallengeTypeID] = Source.[ChallengeTypeID])
	WHEN MATCHED THEN
		UPDATE 
		SET [Value] = Source.[Value],
			[Description] = Source.[Description]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([ChallengeTypeID],
			[Value],
			[Description])
		VALUES
			(Source.[ChallengeTypeID],
			Source.[Value],
			Source.[Description]);

	UPDATE
		ChallengeTypes
	SET
		[Default] = 1
	WHERE
		[ChallengeTypeID] = 1

END

