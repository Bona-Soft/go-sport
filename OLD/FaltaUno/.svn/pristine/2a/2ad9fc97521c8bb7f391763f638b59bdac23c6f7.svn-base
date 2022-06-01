CREATE PROCEDURE [dbo].[POP_UserPrivacyDefault]
AS
BEGIN

	;MERGE INTO [dbo].[UserPrivacyDefault] AS Target
	USING (VALUES
		(1,'User','MobileNumber','N'),
		(2,'User','BirthDate','N')
	) AS Source ([UserPrivacyDefaultID],[Entity],[Property],[DefValue])
	ON (Target.[UserPrivacyDefaultID] = Source.[UserPrivacyDefaultID])
	WHEN MATCHED THEN
		UPDATE 
		SET [Entity] = Source.[Entity],
			 [Property] = Source.[Property],
			 [DefValue] = Source.[DefValue]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([UserPrivacyDefaultID],
			[Entity],
			[Property],
			[DefValue])
		VALUES
			(Source.[UserPrivacyDefaultID],
			Source.[Entity],
			Source.[Property],
			Source.[DefValue]);

END
