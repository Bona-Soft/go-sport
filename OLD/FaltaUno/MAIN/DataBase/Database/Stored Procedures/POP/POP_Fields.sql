CREATE PROCEDURE [dbo].[POP_Fields]
AS
BEGIN

	;MERGE INTO [dbo].[Fields] AS Target
	USING (VALUES
		(1,1,'futbol_grass','Cesped',0),
		(2,1,'futbol_hard','Cemento',0),
		(3,1,'futbol_wood','Parket',0),
		(4,1,'futbol_sintetic','Sintetico',0),
		(5,1,'futbol_other','Indistinto / Otros',1),
		(6,2,'basket_wood','Madera',1),
		(7,2,'basket_hard','Cemento',0),
		(8,2,'basket_other','Indistinto / Otros',0),
		(9,3,'volley_normal','Normal',1),
		(10,3,'volley_beach','Playa',0),
		(11,3,'volley_minivol','Minivol',0),
		(12,3,'volley_other','Otros',0),
		(13,4,'tennis_single','Cesped',1),
		(14,4,'tennis_clay','Arcilla',0),
		(15,4,'tennis_clay','Dura',0),
		(16,4,'tennis_other','Indistinto',0),
		(17,5,'squash_english','Inglesa',1),
		(18,5,'squash_american','Americana',0),
		(19,5,'squash_doubles','Dobles',0),
		(20,5,'squash_cristal','Cristal',0),
		(21,5,'squash_other','Indistinto',0),
		(22,6,'rugby_grass','Cesped',1),
		(23,6,'rugby_sand','Arena',0),
		(24,6,'rugby_dust','Tierra',0),
		(25,6,'rugby_snow','Nieve',0),
		(26,6,'rugby_sintetic','Cesped artificial',0),
		(27,6,'rugby_other','Otros',0),
		(28,7,'american_other','Indistinto',1)
	) AS Source ([FieldID],[SportID],[Value],[Description],[Default])
	ON (Target.[FieldID] = Source.[FieldID])
	WHEN MATCHED THEN
		UPDATE 
		SET [SportID] = Source.[SportID],
			[Value] = Source.[Value],
			[Description] = Source.[Description],
			[Default] = Source.[Default]
	WHEN NOT MATCHED BY TARGET THEN
		INSERT
			([FieldID],
			[SportID],
			[Value],
			[Description],
			[Default])
		VALUES
			(Source.[FieldID],
			Source.[SportID],
			Source.[Value],
			Source.[Description],
			Source.[Default]);

END

