CREATE PROCEDURE [dbo].[POP_Sports]
AS
BEGIN

	;MERGE INTO [dbo].[Sports] AS Target
	USING (VALUES
		(1,'futbol','Futbol'),
		(2,'basket','Basquet'),
		(3,'volley','Volley'),
		(4,'tennis','Tenis'),
		(5,'squash','Squash'),
		(6,'rugby','Rugby'),
		(7,'american_futbol','Futbol Americano'),
		(8,'baseball','Baseball'),
		(9,'boxing','Boxeo'),
		(10,'golf','Golf'),
		(11,'handball','Handball'),
		(12,'hockey','Hockey'),
		(13,'table_tennis','Tenis de mesa')
	) AS Source ([SportID],[Value],[Name])
	ON (Target.[SportID] = Source.[SportID])
	WHEN MATCHED THEN
		UPDATE 
		SET [Value] = Source.[Value],
			[Name] = Source.[Name]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([SportID],
			[Value],
			[Name])
		VALUES
			(Source.[SportID],
			Source.[Value],
			Source.[Name]);
END

